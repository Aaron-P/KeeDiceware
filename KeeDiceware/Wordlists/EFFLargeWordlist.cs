using System;
using System.Diagnostics.CodeAnalysis;

namespace KeeDiceware.Wordlists
{
    //https://www.eff.org/deeplinks/2016/07/new-wordlists-random-passphrases License? https://www.eff.org/copyright CC attribute https://creativecommons.org/licenses/by/3.0/us/
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "EFF")]
    public sealed class EFFLargeWordlist : WordlistBase
    {
        public override string Description => "5 dice per word; log2(6^5) ~= 12.9 bits per word.";

        public override string DisplayName => "EFF Long Wordlist";

        public override string Key => nameof(EFFLargeWordlist);

        public override string[] Wordlist => GetWordlistResource("KeeDiceware.Resources.eff_large_wordlist.txt");
    }
}
