using System;
using System.Linq;

namespace Kontur.Extern.Api.Client.Testing.Generators
{
    public class Randomizer
    {
        private readonly Random random = new();

        public Randomizer()
        {
        }

        public Randomizer(Random random) => this.random = random;

        public int Int(int min, int max) => random.Next(min, max);

        public byte[] Bytes(int length)
        {
            var bytes = new byte[length];
            random.NextBytes(bytes);
            return bytes;
        }

        public string DigitsString(int length, params int[] excludeDigits)
        {
            var result = string.Empty;
            for (var i = 0; i < length; i++)
            {
                int digit;
                do
                {
                    digit = random.Next(0, 9);
                } while (excludeDigits.Contains(digit));

                result = string.Concat(result, digit);
            }
            return result;
        }
    }
}