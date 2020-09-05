using KeeDiceware.Extensions;
using KeePassLib.Cryptography;
using KeePassLib.Security;
using System;
using System.Collections.Generic;

namespace KeeDiceware.Generators
{
    /// <summary>
    /// A <see cref="GeneratorBase"/> subclass for passphrases limited by minimum character count.
    /// </summary>
    public class MinimumCharactersGenerator : GeneratorBase
    {
        /// <inheritdoc/>
        public override uint DefaultCount
        {
            get
            {
                return 30;
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
                return "Minimum Character Count";
            }
        }

        /// <inheritdoc/>
        public override string Key
        {
            get
            {
                return typeof(MinimumCharactersGenerator).Name;
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
            do
            {
                result.Add(wordlist[random.Next(0, wordlist.Length)]);
            }
            while (string.Join(Separator, result.ToArray()).Length < Count);
            return new ProtectedString(true, string.Join(Separator, result.ToArray()));
        }
    }
}
