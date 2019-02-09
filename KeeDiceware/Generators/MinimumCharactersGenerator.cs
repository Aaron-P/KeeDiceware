﻿using KeeDiceware.Extensions;
using KeePassLib.Cryptography;
using KeePassLib.Security;
using System;
using System.Collections.Generic;

namespace KeeDiceware.Generators
{
    public class MinimumCharactersGenerator : GeneratorBase
    {
        public override uint DefaultCount => 30;

        public override string Description => throw new NotImplementedException();

        public override string DisplayName => "Minimum Character Count";

        public override string Key => nameof(MinimumCharactersGenerator);

        public override ProtectedString Generate(CryptoRandomStream random, Settings settings)
        {
            if (random == null)
                throw new ArgumentNullException(nameof(random));
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

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