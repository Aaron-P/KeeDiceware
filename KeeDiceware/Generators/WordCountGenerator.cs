using KeeDiceware.Extensions;
using KeePassLib.Cryptography;
using KeePassLib.Security;
using System;
using System.Collections.Generic;

namespace KeeDiceware.Generators
{
    /// <summary>
    /// A <see cref="GeneratorBase"/> subclass for passphrases limited by word count.
    /// </summary>
    public class WordCountGenerator : GeneratorBase
    {
        /// <inheritdoc/>
        public override uint DefaultCount
        {
            get
            {
                return 6;
            }
        }

        /// <inheritdoc/>
        public override string Description
        {
            get
            {
                return null;
            }
        }

        /// <inheritdoc/>
        public override string DisplayName
        {
            get
            {
                return "Word Count";
            }
        }

        /// <inheritdoc/>
        public override string Key
        {
            get
            {
                return typeof(WordCountGenerator).Name;
            }
        }

        /// <inheritdoc/>
        public override ProtectedString Generate(CryptoRandomStream random, Settings settings)
        {
            if (random == null)
                throw new ArgumentNullException("random" /*nameof(random)*/);
            if (settings == null)
                throw new ArgumentNullException("settings" /*nameof(settings)*/);

            var wordlist = GetWordlist(settings);
            var result = new List<string>();
            for (uint i = 0; i < Count; i++)
            {
                result.Add(wordlist[random.Next(0, wordlist.Length)]);
            }

            return new ProtectedString(true, string.Join(Separator, result.ToArray()));
        }
    }
}
