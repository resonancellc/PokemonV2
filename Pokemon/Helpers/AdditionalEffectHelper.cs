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
            return additionalEffect.Contains(StringEnums.AlwaysHits) ? true : false;
        }

        public static bool IsAlwaysSameDamage(string additionalEffect)
        {
            if (additionalEffect == string.Empty || !additionalEffect.Contains(StringEnums.SameDamage)) return false;
            return true;
        }

        public static int GetAlwaysSameDamage(string additionalEffect)
        {
            if (additionalEffect == string.Empty || !additionalEffect.Contains(StringEnums.SameDamage)) return 0;

            string[] effects = SplitAdditionalEffects(additionalEffect);

            string effect = (from anyEffect in effects where anyEffect.Contains(StringEnums.SameDamage) select anyEffect).First();

            if (effects.Any(e => e.Contains(StringEnums.SameDamage)))
            {
                return Convert.ToInt32(effects.First(e => e.Contains(StringEnums.SameDamage)));
            }
            else return 0;

        }

        public static void SetFlinch(string additionalEffect, IPokemon targetPokemon)
        {
            //if (additionalEffect == string.Empty) return;
            //string[] effectSplit = SplitAdditionalEffects(additionalEffect);

            //if (effectSplit[0] == "flinch")
            //{
            //    targetPokemon.IsFlinched = CalculatorHelper.ChanceCalculator(Convert.ToInt32(effectSplit[1]), 100);
            //    return true;
            //}
            //else return false;
        }

        public static bool IsCritBoosting(string additionalEffect, IPokemon targetPokemon)
        {
            if (additionalEffect == string.Empty) return false;
            string[] effectSplit = SplitAdditionalEffects(additionalEffect);

            if (effectSplit[0] == "boostCrit")
            {
                targetPokemon.IsEnergyFocused = true;
            }

            return false;
        }

        private static string[] SplitAdditionalEffects(string additionalEffect)
        {
            return additionalEffect.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static int GetChanceOfAdditionalEffect(string additionalEffect)
        {

            throw new NotImplementedException();
        }
    }
}
