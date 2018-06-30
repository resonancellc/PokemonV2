using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class AdditionalEffectHelper
    {
        public static bool IsAlwaysHits(string additionalEffect)
        {
            return additionalEffect.Contains("alwaysHits") ? true : false;
        }

        public static int IsAlwaysSameDamage(string additionalEffect)
        {
            if (additionalEffect != string.Empty)
            {
                string[] effectSplit = additionalEffect.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (effectSplit[0] == "sameDamage")
                {
                    return Convert.ToInt32(effectSplit[1]);
                }
            }
            return 0;
        }

        public static bool IsFlinch(string additionalEffect, Pokemon targetPokemon)
        {
            if (additionalEffect != string.Empty)
            {
                string[] effectSplit = additionalEffect.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); //AttackBoostStatsSplitter();

                if (effectSplit[0].Contains("flinch"))
                {
                    targetPokemon.IsFlinched = CalculatorHelper.ChanceCalculator(Convert.ToInt32(effectSplit[1]), 100);
                    return true;
                }
            }
            return false;
        }
    }
}
