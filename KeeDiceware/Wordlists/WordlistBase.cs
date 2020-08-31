using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;

namespace KeeDiceware.Wordlists
{
    public abstract class WordlistBase
    {
        private const string WordlistExtension = ".wordlist";

        private const string DicewareExtension = ".diceware";

        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
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
                catch { }
            }
        }

        public static readonly List<WordlistBase> Wordlists = new List<WordlistBase>();

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

        public abstract string Description { get; }

        public abstract string DisplayName { get; }

        public abstract string Key { get; }

        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public abstract string[] Wordlist { get; }

        public override string ToString()
        {
            return DisplayName;
        }

        private readonly IDictionary<string, string[]> Cache = new Dictionary<string, string[]>();

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        protected string[] GetWordlistResource(string path)
        {
            string[] cache;
            if (Cache.TryGetValue(path, out cache))
                return cache;

            var assembly = typeof(WordlistBase).Assembly;
            using (var reader = new StreamReader(assembly.GetManifestResourceStream(path)))
            {
                var content = reader.ReadToEnd();
                //'null' seperator splits on whitespace. See: https://docs.microsoft.com/en-us/dotnet/api/system.string.split
                //TODO: Take wordlists as-is and remove dice numbers?
                var value = content.Split(default(char[]), StringSplitOptions.RemoveEmptyEntries);
                Cache.Add(path, value);
                return value;
            }
        }
    }
}
