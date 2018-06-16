using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class BattleHelper
    {
        public static bool IsPlayerPokemonFaster(Attack playerAttack, Attack enemyAttack, Battle battle)
        {
            if (enemyAttack.AdditionalEffect == "fast" && playerAttack.AdditionalEffect == "fast" || enemyAttack.AdditionalEffect != "fast" && playerAttack.AdditionalEffect != "fast")
            {
                if (battle.Pokemon.Stat.Stats[(int)PokemonEnum.Stat.Speed] > battle.EnemyPokemon.Stat.Stats[(int)PokemonEnum.Stat.Speed])
                    return true;
                else
                    return false;
            }
            else if (playerAttack.AdditionalEffect == "fast")
            {
                return true;
            }
            else if (enemyAttack.AdditionalEffect == "fast")
            {
                return false;
            }
            return true;
        }

        public static bool IsMiss(Attack attack)
        {
            Random rand = new Random();
            int accuracy = rand.Next(0, 100);

            if (accuracy > attack.Accuracy)
            {
                return true;
            }
            return false;
        }

        public static bool IsCritical(Attack attack)
        {
            if (attack.AdditionalEffect == "highCrit")
                return CalculatorHelper.ChanceCalculator(21, 255);
            else
                return CalculatorHelper.ChanceCalculator(1, 255);
        }

        public static bool IsConditionChange(Attack attack, Pokemon targetPokemon)
        {
            if (targetPokemon.Condition == 0)
            {
                string[] attributes = attack.AdditionalEffect.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); //AttackBoostStatsSplitter();

                if (attributes.Contains("burn"))
                {
                    targetPokemon.Stat.Stats[(int)PokemonEnum.Stat.Attack] = targetPokemon.Stat.Stats[(int)PokemonEnum.Stat.Attack] / 2;
                    return ConditionChange(targetPokemon, (int)PokemonEnum.Condition.BRN, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100);
                }
                if (attributes.Contains("freeze"))
                {
                    return ConditionChange(targetPokemon, (int)PokemonEnum.Condition.FRZ, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100);
                }
                if (attributes.Contains("paralysis"))
                {
                    targetPokemon.Stat.Stats[(int)PokemonEnum.Stat.Speed] = Convert.ToInt32((float)targetPokemon.Stat.Stats[(int)PokemonEnum.Stat.Speed] / 1.5f);
                    return ConditionChange(targetPokemon, (int)PokemonEnum.Condition.PAR, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100);
                }
                if (attributes.Contains("poison"))
                {
                    return ConditionChange(targetPokemon, (int)PokemonEnum.Condition.PSN, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100);
                }
                if (attributes.Contains("sleep"))
                {
                    return ConditionChange(targetPokemon, (int)PokemonEnum.Condition.SLP, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100);
                }
            }
            return false;
        }

        public static bool ConditionChange(Pokemon targetPokemon, int newCondition, int effectChance)
        {
            if (CalculatorHelper.ChanceCalculator(effectChance))
            {
                targetPokemon.Condition = newCondition;
                return true;
            }
            return false;            
        }

        public static bool ApplyConditionEffect(Pokemon pokemon)
        {
            switch (pokemon.Condition)
            {
                case 0:
                    return true;
                case (int)PokemonEnum.Condition.BRN:
                    pokemon.Hurt(pokemon.HPMax / 16);
                    return pokemon.CheckIfPokemonAlive();
                case (int)PokemonEnum.Condition.FRZ:
                    return false;
                case (int)PokemonEnum.Condition.PAR:
                    return CalculatorHelper.ChanceCalculator(25);
                case (int)PokemonEnum.Condition.PSN:
                    pokemon.Hurt(pokemon.HPMax / 16);
                    return pokemon.CheckIfPokemonAlive();
                case (int)PokemonEnum.Condition.SLP:
                    if (CalculatorHelper.ChanceCalculator(25))
                    {
                        pokemon.Condition = 0;
                        return true;
                    }
                    return false;                 
                default:
                    return true;
            }
        }

        public static void ChangeTempStats(bool isPlayerAttack, Attack attack, Battle battle)
        {
            string[] attributes = attack.BoostStats.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); //AttackBoostStatsSplitter();
            if (attributes.Length > 0)
            {
                bool isEnemyTarget = attributes[0] == "enemy";
                ChangeTempStats(isPlayerAttack, isEnemyTarget, Int32.Parse(attributes[1]), Int32.Parse(attributes[2]), battle);
            }
        }

        public static void ChangeTempStats(bool isPlayerAttack, bool isEnemyTarget, int statType, int stageValue, Battle battle)
        {
            if (!isPlayerAttack && isEnemyTarget) // przeciwnik na gracza
                ChangePokemonStats(true, statType, battle, stageValue);
            else if (isPlayerAttack && !isEnemyTarget) // gracz na siebie
                ChangePokemonStats(true, statType, battle, stageValue);
            else if (isPlayerAttack && isEnemyTarget) // gracz na przeciwnika
                ChangePokemonStats(false, statType, battle, stageValue);
            else if (!isPlayerAttack && !isEnemyTarget) // przeciwnik na gracza
                ChangePokemonStats(false, statType, battle, stageValue);
        }

        private static void ChangePokemonStats(bool isPlayerTarget, int statType, Battle battle, int stageValue)
        {
            if (isPlayerTarget)
            {
                if (battle.Pokemon.statModifierStages[statType] < 6 && battle.Pokemon.statModifierStages[statType] > -6)
                {
                    battle.Pokemon.statModifierStages[statType] += stageValue;
                    battle.Pokemon.Stat.Stats[statType] = Convert.ToInt32(battle.PokemonStartStats.Stats[statType] * StageHelper.StageToMultipler(battle.Pokemon.statModifierStages[statType]));
                }
                else
                {
                    BattleLog.AppendText($"{battle.Pokemon.Name} 'statName here' cannot go any higher than {(float)battle.Pokemon.Stat.Stats[statType]}");
                } 
            }
            else
            {
                if (battle.EnemyPokemon.statModifierStages[statType] < 6 && battle.EnemyPokemon.statModifierStages[statType] > -6)
                {
                    battle.EnemyPokemon.statModifierStages[statType] += stageValue;
                    battle.EnemyPokemon.Stat.Stats[statType] = Convert.ToInt32(battle.EnemyPokemonStartStats.Stats[statType] * StageHelper.StageToMultipler(battle.EnemyPokemon.statModifierStages[statType]));
                }
                else
                {
                    BattleLog.AppendText($"{battle.EnemyPokemon.Name} 'statName here' cannot go any higher than {(float)battle.EnemyPokemon.Stat.Stats[statType]}");
                }
            }
        }
    }
}
