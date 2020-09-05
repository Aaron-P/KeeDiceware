using System;

namespace KeeDiceware.Wordlists
{
    // https://www.eff.org/deeplinks/2016/07/new-wordlists-random-passphrases License? https://www.eff.org/copyright CC attribute https://creativecommons.org/licenses/by/3.0/us/

    /// <summary>
    /// A <see cref="WordlistBase"/> subclass for the "EFF Long Wordlist".
    /// </summary>
    public sealed class EFFLargeWordlist : WordlistBase
    {
        /// <inheritdoc/>
        public override string Description
        {
            get
            {
                return "5 dice per word; log2(6^5) ~= 12.9 bits per word.";
            }
        }

        /// <inheritdoc/>
        public override string DisplayName
        {
            get
            {
                return "EFF Long Wordlist";
            }
        }

        /// <inheritdoc/>
        public override string Key
        {
            get
            {
                return typeof(EFFLargeWordlist).Name;
            }
        }

        /// <inheritdoc/>
        public override string[] Wordlist
        {
            get
            {
                return GetWordlistResource("KeeDiceware.Resources.eff_large_wordlist.txt");
            }
        }
    }
}
