using Pokemon.AdditionalEffects;
using Pokemon.Calculators;
using Pokemon.Factory;
using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static bool IsMiss(IAttack attack)
        {
            return !ChanceCalculator.CalculateChance(attack.Accuracy.Value);
        }

        public static bool HasFailedConfusion(IPokemon attackingPokemon)
        {
            if (!attackingPokemon.IsConfused) return false;
            return ChanceCalculator.CalculateChance(50, 100);
        }

        public static bool IsCritical(IAttack attack, IPokemon attackingPokemon)
        {
            int boostCrit = attackingPokemon.IsEnergyFocused ? 20 : 0;
            if (AdditionalEffectAvailability.ContainsEffectType(attack.AdditionalEffects, typeof(HighCriticalRatio)))
                return ChanceCalculator.CalculateChance(21 + boostCrit, 255);
            else
                return ChanceCalculator.CalculateChance(1 + boostCrit, 255);
        }

        public static bool IsAbleToAttackAfterConditionEffect(IPokemon pokemon)
        {
            int damage;
            switch (pokemon.Condition)
            {
                case 0:
                    return true;

                case Condition.BRN:
                    damage = pokemon.HPMax / 16;
                    pokemon.Hurt(damage);
                    BattleLog.AppendText($"{pokemon.Name} is burning (Damage: {damage})");
                    return pokemon.IsPokemonAlive();

                case Condition.FRZ:
                    BattleLog.AppendText($"{pokemon.Name} is frozen");
                    return false;

                case Condition.PAR:
                    if (ChanceCalculator.CalculateChance(50)) return true;
                    BattleLog.AppendText($"{pokemon.Name} is unable to move");
                    return false;

                case Condition.PSN:
                    damage = pokemon.HPMax / 16;
                    pokemon.Hurt(damage);
                    BattleLog.AppendText($"{pokemon.Name} is hurt by poison (Damage: {damage})");
                    return pokemon.IsPokemonAlive();

                case Condition.SLP:
                    if (ChanceCalculator.CalculateChance(50))
                    {
                        BattleLog.AppendText($"{pokemon.Name} woke up");
                        pokemon.Condition = 0;
                        return true;
                    }
                    BattleLog.AppendText($"{pokemon.Name} is still sleeping");
                    return false;
                default:
                    return true;
            }
        }
        
        public static void ChangeTempStats(IAttack attack, IPokemon attackingPokemon, IPokemon targetPokemon)
        {
            string[] boosts = attack.BoostStats.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string boost in boosts)
            {
                string[] attributes = boost.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); //AttackBoostStatsSplitter();
                if (attributes.Length > 0)
                {

                    ChangeTempPokemonStats(attributes[0] == "enemy" ? targetPokemon : attackingPokemon, Int32.Parse(attributes[1]), Int32.Parse(attributes[2]));
                }
            }

        }

        public static void ChangeTempPokemonStats(IPokemon affectedPokemon, int statType, int stageValue)
        {
            if (affectedPokemon.StatModifierStages[statType] <= 6 - stageValue && affectedPokemon.StatModifierStages[statType] >= -6 + stageValue)
            {
                affectedPokemon.StatModifierStages[statType] += stageValue;
            }
            else
            {
                BattleLog.AppendText($"{affectedPokemon.Name} {((StatType)statType).ToString()} cannot go any higher");
            }
        }
    }
}
