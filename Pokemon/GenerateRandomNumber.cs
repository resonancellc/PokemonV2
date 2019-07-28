using System;

namespace Pokemon
{
    public static class GenerateRandomNumber
    {
        private static readonly Random _random = new Random();
        private static readonly object _syncLock = new object();

        public static int GetRandomNumber(int min, int max)
        {
            lock (_syncLock)
            { // synchronize
                return _random.Next(min, max);
            }
        }
    }
}
