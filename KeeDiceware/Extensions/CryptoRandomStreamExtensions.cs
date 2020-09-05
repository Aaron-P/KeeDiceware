using KeePassLib.Cryptography;
using System;

namespace KeeDiceware.Extensions
{
    /// <summary>
    /// Static class of <see cref="CryptoRandomStream"/> extension methods.
    /// </summary>
    internal static class CryptoRandomStreamExtensions
    {
        /// <summary>
        /// Generates a random integer between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="random">A <see cref="CryptoRandomStream"/> instance.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>A random value between <paramref name="min"/> and <paramref name="max"/>.</returns>
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
            }
            while (ui >= upperBound);
            return (int)(min + (ui % diff));
        }
    }
}
