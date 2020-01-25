using KeePassLib.Cryptography;
using System;

namespace KeeDiceware.Extensions
{
    internal static class CryptoRandomStreamExtensions
    {
        public static int Next(this CryptoRandomStream random, int min, int max)
        {
            if (min >= max)
                throw new ArgumentOutOfRangeException("min" /*nameof(min)*/);

            var diff = (long)max - min;
            var upperBound = uint.MaxValue / diff * diff;

            uint ui;
            do
            {
                ui = (uint)random.GetRandomUInt64();
            } while (ui >= upperBound);
            return (int)(min + (ui % diff));
        }
    }
}
