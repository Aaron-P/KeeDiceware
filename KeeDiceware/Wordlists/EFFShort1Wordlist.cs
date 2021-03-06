﻿using System;

namespace KeeDiceware.Wordlists
{
    /// <summary>
    /// A <see cref="WordlistBase"/> subclass for the "EFF Short Wordlist #1".
    /// </summary>
    public sealed class EFFShort1Wordlist : WordlistBase
    {
        /// <inheritdoc/>
        public override string Description
        {
            get
            {
                return "4 dice per word; log2(6^4) ~= 10.3 bits per word.";
            }
        }

        /// <inheritdoc/>
        public override string DisplayName
        {
            get
            {
                return "EFF Short Wordlist #1";
            }
        }

        /// <inheritdoc/>
        public override string Key
        {
            get
            {
                return typeof(EFFShort1Wordlist).Name;
            }
        }

        /// <inheritdoc/>
        public override string[] Wordlist
        {
            get
            {
                return GetWordlistResource("KeeDiceware.Resources.eff_short_wordlist_1.txt");
            }
        }
    }
}
