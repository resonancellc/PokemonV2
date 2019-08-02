using Pokemon.AdditionalEffects;
using Pokemon.Calculators;
using Pokemon.Models;
using System.Collections.Generic;

namespace Pokemon
{
    public static class BattleHelper
    {
        

        public static bool HasFailedConfusion(IPokemon attackingPokemon) => ChanceCalculator.CalculateChance(50, 100);

        public static bool IsCritical(ICollection<IAdditionalEffect> attackAdditionalEffects, bool isAttackingPokemonFocused)
        {
            int boostCrit = isAttackingPokemonFocused ? 20 : 0;
            if (AdditionalEffectAvailability.ContainsEffectType(attackAdditionalEffects, typeof(HighCriticalRatio)))
            {
                return ChanceCalculator.CalculateChance(21 + boostCrit, 255);
            }
            else
            {
                return ChanceCalculator.CalculateChance(1 + boostCrit, 255);
            }
        }


    }
}
