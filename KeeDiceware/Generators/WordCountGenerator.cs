using KeeDiceware.Extensions;
using KeePassLib.Cryptography;
using KeePassLib.Security;
using System;
using System.Collections.Generic;

namespace KeeDiceware.Generators
{
    public class WordCountGenerator : GeneratorBase
    {
        public override uint DefaultCount
        {
            get
            {
                return 6;
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
                return "Word Count";
            }
        }

        public override string Key
        {
            get
            {
                return "WordCountGenerator";//nameof(WordCountGenerator);
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
            for (uint i = 0; i < Count; i++)
            {
                result.Add(wordlist[random.Next(0, wordlist.Length)]);
            }
            return new ProtectedString(true, string.Join(Separator, result.ToArray()));
        }
    }
}
