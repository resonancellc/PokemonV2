using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class AttackEffectivenessHelper
    {
        private static double[,] multiplier = {
                                {1,     1,     1,     1,     1,     1,     1,     1,     1,     1,     1,     1,     0.5,   0,     1},
                                {1,     0.5,   0.5,   1,     2,     2,     1,     1,     1,     1,     1,     2,     0.5,   1,     0.5},
                                {1,     2,     0.5,   1,     0.5,   1,     1,     1,     2,     1,     1,     1,     2,     1,     0.5},
                                {1,     1,     2,     0.5,   0.5,   1,     1,     1,     0,     2,     1,     1,     1,     1,     0.5},
                                {1,     0.5,   2,     1,     0.5,   1,     1,     0.5,   2,     0.5,   1,     0.5,   2,     1,     0.5},
                                {1,     0.5,   0.5,   1,     2,     0.5,   1,     1,     2,     2,     1,     1,     1,     1,     2},
                                {2,     1,     1,     1,     1,     2,     1,     0.5,   1,     0.5,   0.5,   0.5,   2,     0,     1},
                                {1,     1,     1,     1,     2,     1,     1,     0.5,   0.5,   1,     1,     1,     0.5,   0.5,   1},
                                {1,     2,     1,     2,     0.5,   1,     1,     2,     1,     0,     1,     0.5,   2,     1,     1},
                                {1,     1,     1,     0.5,   2,     1,     2,     1,     1,     1,     1,     2,     0.5,   1,     1},
                                {1,     1,     1,     1,     1,     1,     2,     2,     1,     1,     0.5,   1,     1,     1,     1},
                                {1,     0.5,   1,     1,     2,     1,     0.5,   0.5,   1,     0.5,   2,     1,     1,     0.5,   1},
                                {1,     2,     1,     1,     1,     2,     0.5,   1,     0.5,   2,     1,     2,     1,     1,     1},
                                {0,     1,     1,     1,     1,     1,     1,     1,     1,     1,     2,     1,     1,     2,     1},
                                {1,     1,     1,     1,     1,     1,     1,     1,     1,     1,     1,     1,     1,     1,     2} };

        public static double GetMultipler(int attackType, int targetType)
        {
            return multiplier[attackType, targetType];
        }


    }


}
