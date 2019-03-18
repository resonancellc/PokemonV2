using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Calculators
{
    public static class ChanceCalculator
    {
        public static bool CalculateChance(int chance, int maxRange = 100)
        {
            int chanceScore = GenerateRandomNumber.GetRandomNumber(0, maxRange);
            if (chanceScore <= chance)
            {
                return true; // success
            }
            return false;
        }
    }
}
