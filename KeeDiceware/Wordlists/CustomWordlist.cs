using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace KeeDiceware.Wordlists
{
    public class CustomWordlist : WordlistBase
    {
        private const string DicewareWordCaptureGroupName = "Word";

        private static readonly Regex DicewareLine = new Regex(@"^\d{5,}\s+(?<Word>.*)$", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        public enum FileType
        {
            Wordlist = 0,

            Diceware = 1,
        }

        public CustomWordlist(string path, FileType fileType)
        {
            if (path == null)
                throw new ArgumentNullException("path" /*nameof(path)*/);
            if (path.Length == 0)
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "File path cannot be empty."), "path" /*nameof(path)*/);
            if (!File.Exists(path))
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "A file does not exist at the given path."), "path" /*nameof(path)*/);

            _Key = Path.GetFileName(path);
            // Currently we append the type of list in parens to the name of the file for the display name, just in case there's a .wordlist and .diceware file with the same name.
            // There may be a better we to handle names/descriptions for custom files.
            _DisplayName = _Description = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", Path.GetFileNameWithoutExtension(path), Enum.GetName(typeof(FileType), fileType));

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

            _Wordlist = lines;
        }

        private string _Description { get; set; }

        public override string Description
        {
            get
            {
                return _Description;
            }
        }

        private string _DisplayName { get; set; }

        public override string DisplayName
        {
            get
            {
                return _DisplayName;
            }
        }

        private string _Key { get; set; }

        public override string Key
        {
            get
            {
                return _Key;
            }
        }

        private string[] _Wordlist { get; set; }

        public override string[] Wordlist
        {
            get
            {
                return _Wordlist;
            }
        }
    }
}
