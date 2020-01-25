using KeeDiceware.Wordlists;
using KeePassLib.Cryptography;
using KeePassLib.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace KeeDiceware.Generators
{
    [Serializable()]
    public abstract class GeneratorBase
    {
        public const string Separator = " ";

        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected GeneratorBase()
        {
            Count = DefaultCount;
        }

        public static IList<GeneratorBase> Generators
        {
            get
            {
                return Types.Select(t => (GeneratorBase)Activator.CreateInstance(t)).ToList();
            }
        }

        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public static Type[] Types
        {
            get
            {
                return typeof(GeneratorBase)
                    .Assembly.GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(GeneratorBase)) && !t.IsAbstract)
                    .ToArray();
            }
        }

        public uint Count { get; set; }

        public abstract uint DefaultCount { get; }

        public abstract string Description { get; }

        public abstract string DisplayName { get; }

        public abstract string Key { get; }

        public abstract ProtectedString Generate(CryptoRandomStream random, Settings settings);

        public override string ToString()
        {
            return DisplayName;
        }

        protected static string[] GetWordlist(Settings settings)
        {
            return WordlistBase.Wordlists.Single(_ => _.Key == settings.Wordlist).Wordlist;
        }
    }
}
