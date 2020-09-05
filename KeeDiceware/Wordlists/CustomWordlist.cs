using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace KeeDiceware.Wordlists
{
    /// <summary>
    /// A <see cref="WordlistBase"/> subclass for an arbitrary wordlist file.
    /// </summary>
    public class CustomWordlist : WordlistBase
    {
        private const string DicewareWordCaptureGroupName = "Word";

        private static readonly Regex DicewareLine = new Regex(@"^\d{5,}\s+(?<Word>.*)$", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        private readonly string description;

        private readonly string displayName;

        private readonly string key;

        private readonly string[] wordlist;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomWordlist"/> class.
        /// </summary>
        /// <param name="path">The path to a wordlist file.</param>
        /// <param name="fileType">The type of wordlist file.</param>
        public CustomWordlist(string path, FileType fileType)
        {
            if (path == null)
                throw new ArgumentNullException("path" /*nameof(path)*/);
            if (path.Length == 0)
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "File path cannot be empty."), "path" /*nameof(path)*/);
            if (!File.Exists(path))
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "A file does not exist at the given path."), "path" /*nameof(path)*/);

            key = Path.GetFileName(path);
            // Currently we append the type of list in parens to the name of the file for the display name, just in case there's a .wordlist and .diceware file with the same name.
            // There may be a better we to handle names/descriptions for custom files.
            displayName = description = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", Path.GetFileNameWithoutExtension(path), Enum.GetName(typeof(FileType), fileType));

            var lines = File.ReadAllLines(path, Encoding.UTF8).Select(line =>
            {
                if (fileType == FileType.Wordlist)
                    return line;

                var match = DicewareLine.Match(line);
                if (!match.Success)
                    return null;

                return match.Groups[DicewareWordCaptureGroupName].Value;
            }).Where(_ => !string.IsNullOrEmpty(_)).ToArray();

            if (lines.Length < 1)
                throw new IndexOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "File contains no usable words."));

            wordlist = lines;
        }

        /// <summary>
        /// An enum of supported wordlist file types.
        /// </summary>
        public enum FileType
        {
            /// <summary>
            /// A .wordlist file; a plain file of words.
            /// </summary>
            Wordlist = 0,

            /// <summary>
            /// A .diceware file; a file of dice combinations and words.
            /// </summary>
            Diceware = 1,
        }

        /// <inheritdoc/>
        public override string Description
        {
            get
            {
                return description;
            }
        }

        /// <inheritdoc/>
        public override string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        /// <inheritdoc/>
        public override string Key
        {
            get
            {
                return key;
            }
        }

        /// <inheritdoc/>
        public override string[] Wordlist
        {
            get
            {
                return wordlist;
            }
        }
    }
}
