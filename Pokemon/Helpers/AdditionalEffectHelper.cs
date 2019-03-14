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

        public static bool IsAlwaysSameDamage(List<IAdditionalEffect> additionalEffects)
        {
            return additionalEffects.Any(p => p.Name.Contains(StringEnums.SameDamage));
        }

        public static int GetAlwaysSameDamage(IAdditionalEffect additionalEffect)
        {
            return (int)additionalEffect.PrimaryValue;
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
