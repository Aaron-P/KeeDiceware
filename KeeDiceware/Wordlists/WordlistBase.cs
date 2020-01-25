using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace KeeDiceware.Wordlists
{
    public abstract class WordlistBase
    {
        public static IList<WordlistBase> Wordlists
        {
            get
            {
                return Types.Select(t => (WordlistBase)Activator.CreateInstance(t)).ToList();
            }
        }

        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public static Type[] Types
        {
            get
            {
                return typeof(WordlistBase)
                    .Assembly.GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(WordlistBase)) && !t.IsAbstract)
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
