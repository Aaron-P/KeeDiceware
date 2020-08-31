﻿using KeeDiceware.Extensions;
using KeePassLib.Cryptography;
using KeePassLib.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeeDiceware.Generators
{
    public class MaximumCharactersGenerator : GeneratorBase
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
                return "Maximum Character Count";
            }
        }

        public override string Key
        {
            get
            {
                return typeof(MaximumCharactersGenerator).Name;
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
            while (string.Join(Separator, result.ToArray()).Length < Count);
            result = result.Take(result.Count - 1).ToList();
            return new ProtectedString(true, string.Join(Separator, result.ToArray()));
        }
    }
}
