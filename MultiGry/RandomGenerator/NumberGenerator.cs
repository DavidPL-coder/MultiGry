using System;

namespace MultiGry
{
    class NumberGenerator : INumberGenerator
    {
        public byte GetNumberBetween1And100()
        {
            var random = new Random();
            return (byte) random.Next(1, 101);
        }

        public int Next(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }
    }
}
