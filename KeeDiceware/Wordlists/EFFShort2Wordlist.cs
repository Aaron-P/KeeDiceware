using System;
using System.Diagnostics.CodeAnalysis;

namespace KeeDiceware.Wordlists
{
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "EFF")]
    public sealed class EFFShort2Wordlist : WordlistBase
    {
        public override string Description
        {
            get
            {
                return "4 dice per word; log2(6^4) ~= 10.3 bits per word.";
            }
        }

        public override string DisplayName
        {
            get
            {
                return "EFF Short Wordlist #2";
            }
        }

        public override string Key
        {
            get
            {
                return "EFFShort2Wordlist";//nameof(EFFShort2Wordlist);
            }
        }

        public override string[] Wordlist
        {
            get
            {
                return GetWordlistResource("KeeDiceware.Resources.eff_short_wordlist_2_0.txt");
            }
        }
    }
}
