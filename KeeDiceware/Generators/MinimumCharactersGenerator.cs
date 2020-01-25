using KeeDiceware.Extensions;
using KeePassLib.Cryptography;
using KeePassLib.Security;
using System;
using System.Collections.Generic;

namespace KeeDiceware.Generators
{
    public class MinimumCharactersGenerator : GeneratorBase
    {
        public override uint DefaultCount
        {
            get
            {
                return 30;
            }
        }

        public override string Description
        {
            get
            {
                return null;
            }
        }

        public override string DisplayName
        {
            get
            {
                return "Minimum Character Count";
            }
        }

        public override string Key
        {
            get
            {
                return "MinimumCharactersGenerator";//nameof(MinimumCharactersGenerator);
            }
        }

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
            while (string.Join(string.Empty, result.ToArray()).Length < Count);
            return new ProtectedString(true, string.Join(Separator, result.ToArray()));
        }
    }
}
