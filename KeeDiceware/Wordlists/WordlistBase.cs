using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;

namespace KeeDiceware.Wordlists
{
    /// <summary>
    /// Base class for passphrase wordlists.
    /// </summary>
    public abstract class WordlistBase
    {
        /// <summary>
        /// Gets a list of instantiated <see cref="WordlistBase"/> subclasses.
        /// </summary>
        public static readonly List<WordlistBase> Wordlists = new List<WordlistBase>();

        /// <summary>
        /// The file extension for 'plain' wordlists.
        /// </summary>
        private const string WordlistExtension = ".wordlist";

        /// <summary>
        /// The file extension for 'diceware' wordlists.
        /// </summary>
        private const string DicewareExtension = ".diceware";

        /// <summary>
        /// A simple cache used when loading embedded resource wordlists.
        /// </summary>
        private readonly IDictionary<string, string[]> _cache = new Dictionary<string, string[]>();

        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "We want to catch all exceptions so we can continue.")]
        static WordlistBase()
        {
            Wordlists.AddRange(Types.Select(t => (WordlistBase)Activator.CreateInstance(t)).ToList());

            // TODO: I would consider it more correct for custom wordlists to be where the plugin is, but I don't know a good way to get that for .plgx files.
            //var assemblyPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(WordlistBase)).Location);
            var assemblyPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(KeePass.Plugins.Plugin)).Location);
            var wordlistFiles = Directory.GetFiles(assemblyPath, "*" + WordlistExtension, SearchOption.TopDirectoryOnly);
            var dicewareFiles = Directory.GetFiles(assemblyPath, "*" + DicewareExtension, SearchOption.TopDirectoryOnly);

            var files = wordlistFiles.Union(dicewareFiles).OrderBy(_ => _); // Natural file order.

            // TODO: Show a warning message or something if loading a file fails? Right now we just fail silently.
            foreach (var file in files)
            {
                var fileExtension = Path.GetExtension(file);
                var fileType = fileExtension.Equals(WordlistExtension, StringComparison.OrdinalIgnoreCase)
                    ? CustomWordlist.FileType.Wordlist
                    : CustomWordlist.FileType.Diceware;

                try
                {
                    Wordlists.Add(new CustomWordlist(file, fileType));
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Gets an array of types that are a subclass of <see cref="WordlistBase"/> and have a default constructor.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public static Type[] Types
        {
            get
            {
                return typeof(WordlistBase)
                    .Assembly.GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(WordlistBase)) && !t.IsAbstract && t.GetConstructor(Type.EmptyTypes) != null) // Parameterless constuctors only.
                    .ToArray();
            }
        }

        /// <summary>
        /// Gets the description of the wordlist.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Gets the display name of the wordlist.
        /// </summary>
        public abstract string DisplayName { get; }

        /// <summary>
        /// Gets the internal setting key of the wordlist.
        /// </summary>
        public abstract string Key { get; }

        /// <summary>
        /// Gets the set of words in the wordlist.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public abstract string[] Wordlist { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return DisplayName;
        }

        /// <summary>
        /// Gets a set of words from a wordlist embedded as a resource.
        /// </summary>
        /// <param name="path">The embedded resource path.</param>
        /// <returns>The set of words from the wordlist.</returns>
        protected string[] GetWordlistResource(string path)
        {
            string[] cache;
            if (_cache.TryGetValue(path, out cache))
                return cache;

            var assembly = typeof(WordlistBase).Assembly;
            using (var reader = new StreamReader(assembly.GetManifestResourceStream(path)))
            {
                var content = reader.ReadToEnd();
                // 'null' seperator splits on whitespace. See: https://docs.microsoft.com/en-us/dotnet/api/system.string.split
                // TODO: Take wordlists as-is and remove dice numbers?
                var value = content.Split(default(char[]), StringSplitOptions.RemoveEmptyEntries);
                _cache.Add(path, value);
                return value;
            }
        }
    }
}
