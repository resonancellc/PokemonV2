using Pokemon.AdditionalEffects;
using Pokemon.Calculators;
using Pokemon.Models;
using System.Collections.Generic;

namespace Pokemon
{
    public static class BattleHelper
    {
        public static bool IsPlayerPokemonFaster(ICollection<IAdditionalEffect> playerAttackEffects, ICollection<IAdditionalEffect> enemyAttackEffects, IBattle battle)
        {
            bool playerAttackIsFast = playerAttackEffects.ContainsEffectType(typeof(FastAttack));
            bool enemyAttackIsFast = enemyAttackEffects.ContainsEffectType(typeof(FastAttack));

            if (playerAttackIsFast == enemyAttackIsFast)
            {
                if (TempStatsCalculator.GetSpeed(battle.PlayerPokemon) > TempStatsCalculator.GetSpeed(battle.EnemyPokemon))
                    return true;
                else
                    return false;
            }

            else if (playerAttackIsFast) return true;
            else if (enemyAttackIsFast) return false;

            return true;
        }

        public static bool IsMiss(IAttack attack) => !ChanceCalculator.CalculateChance(attack.Accuracy.Value);

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
