using KeeDiceware.Wordlists;
using KeePassLib.Cryptography;
using KeePassLib.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace KeeDiceware.Generators
{
    /// <summary>
    /// Base class for passphrase generators.
    /// </summary>
    [Serializable]
    public abstract class GeneratorBase
    {
        /// <summary>
        /// The passphrase word separator.
        /// </summary>
        public const string Separator = " ";

        /// <summary>
        /// Gets a list of instantiated <see cref="GeneratorBase"/> subclasses.
        /// </summary>
        public static readonly List<GeneratorBase> Generators = Types.Select(t => (GeneratorBase)Activator.CreateInstance(t)).ToList();

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorBase"/> class.
        /// </summary>
        protected GeneratorBase()
        {
            Count = DefaultCount;
        }

        /// <summary>
        /// Gets an array of types that are a subclass of <see cref="GeneratorBase"/>.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the 'limit' of the passphrase generator.
        /// </summary>
        public uint Count { get; set; }

        /// <summary>
        /// Gets the default 'limit' of the passphrase generator.
        /// </summary>
        public abstract uint DefaultCount { get; }

        /// <summary>
        /// Gets the description of the passphrase generator.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Gets the display name of the passphrase generator.
        /// </summary>
        public abstract string DisplayName { get; }

        /// <summary>
        /// Gets the internal setting key of the passphrase generator.
        /// </summary>
        public abstract string Key { get; }

        /// <summary>
        /// Generates a diceware style passphrase with given settings.
        /// </summary>
        /// <param name="random">A <see cref="CryptoRandomStream"/> instance for generating random numbers.</param>
        /// <param name="settings">A set of <see cref="Settings"/>.</param>
        /// <returns>A <see cref="ProtectedString"/> containing a generated passphrase.</returns>
        public abstract ProtectedString Generate(CryptoRandomStream random, Settings settings);

        /// <inheritdoc/>
        public override string ToString()
        {
            return DisplayName;
        }

        /// <summary>
        /// Gets the list of words from the wordlist saved in a given set of <see cref="Settings"/>.
        /// </summary>
        /// <param name="settings">A set of <see cref="Settings"/>.</param>
        /// <returns>A list of passphrase words.</returns>
        protected static string[] GetWordlist(Settings settings)
        {
            return WordlistBase.Wordlists.Single(_ => _.Key == settings.Wordlist).Wordlist;
        }
    }
}
