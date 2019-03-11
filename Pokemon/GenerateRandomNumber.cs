using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class GenerateRandomNumber
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        //CalculatorHelper.RandomNumber()
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }
    }
}
