using Pokemon.Models;
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
            if (additionalEffect == string.Empty) return false;
            return additionalEffect.Contains("alwaysHits") ? true : false;
        }

        public static bool IsAlwaysSameDamage(string additionalEffect)
        {
            if (additionalEffect == string.Empty) return false;

            string[] effectSplit = additionalEffect.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (effectSplit[0] == "sameDamage")
            {
                return true;
            }
            else return false;
        }

        public static int GetAlwaysSameDamage(string additionalEffect)
        {
            if (additionalEffect == string.Empty) return 0;
            string[] effectSplit = additionalEffect.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (effectSplit[0] == "sameDamage")
            {
                return Convert.ToInt32(effectSplit[1]);
            }
            else return 0;

        }

        public static bool IsFlinch(string additionalEffect, IPokemon targetPokemon)
        {
            if (additionalEffect == string.Empty) return false;

            string[] effectSplit = additionalEffect.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (effectSplit[0] == "flinch")
            {
                targetPokemon.IsFlinched = CalculatorHelper.ChanceCalculator(Convert.ToInt32(effectSplit[1]), 100);
                return true;
            }
            else return false;
        }

        public static bool IsCritBoosting(string additionalEffect, IPokemon targetPokemon)
        {
            if (additionalEffect == string.Empty) return false;

            string[] effectSplit = additionalEffect.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (effectSplit[0] == "boostCrit")
            {
                targetPokemon.IsEnergyFocused = true;
            }

            return false;
        }
    }
}
