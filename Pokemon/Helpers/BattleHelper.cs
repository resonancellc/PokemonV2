﻿using System;
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
            return !CalculatorHelper.ChanceCalculator(attack.Accuracy.Value);
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
                    if(ConditionChange(targetPokemon, (int)PokemonEnum.Condition.BRN, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100))
                    {
                        BattleLog.AppendText($"{targetPokemon.Name} is now burning");
                        targetPokemon.Stat.Stats[(int)PokemonEnum.Stat.Attack] = targetPokemon.Stat.Stats[(int)PokemonEnum.Stat.Attack] / 2;
                        return true;
                    }
                }
                if (attributes.Contains("freeze"))
                {
                    if (ConditionChange(targetPokemon, (int)PokemonEnum.Condition.FRZ, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100))
                    {
                        BattleLog.AppendText($"{targetPokemon.Name} is now frozen");
                        return true;
                    }
                }
                if (attributes.Contains("paralysis"))
                {
                    if (ConditionChange(targetPokemon, (int)PokemonEnum.Condition.PAR, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100))
                    {
                        BattleLog.AppendText($"{targetPokemon.Name} is now paralyzed");
                        targetPokemon.Stat.Stats[(int)PokemonEnum.Stat.Speed] = Convert.ToInt32((float)targetPokemon.Stat.Stats[(int)PokemonEnum.Stat.Speed] / 1.5f);
                        return true;
                    }
                }
                if (attributes.Contains("poison"))
                {
                    if (ConditionChange(targetPokemon, (int)PokemonEnum.Condition.PSN, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100))
                    {
                        BattleLog.AppendText($"{targetPokemon.Name} is now poisoned");
                        return true;
                    }
                }
                if (attributes.Contains("sleep"))
                {
                    if (ConditionChange(targetPokemon, (int)PokemonEnum.Condition.SLP, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100))
                    {
                        BattleLog.AppendText($"{targetPokemon.Name} is now sleeping");
                        return true;
                    }
                }
            }
            else
            {
                BattleLog.AppendText($"{targetPokemon.Name} is unaffected");
                return false;
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
            int damage;
            switch (pokemon.Condition)
            {
                case 0:
                    return true;

                case (int)PokemonEnum.Condition.BRN:
                    damage = pokemon.HPMax / 16;
                    pokemon.Hurt(damage);
                    BattleLog.AppendText($"{pokemon.Name} is burning (Damage: {damage})");
                    return pokemon.CheckIfPokemonAlive();

                case (int)PokemonEnum.Condition.FRZ:
                    BattleLog.AppendText($"{pokemon.Name} is frozen");
                    return false;

                case (int)PokemonEnum.Condition.PAR:
                    if (CalculatorHelper.ChanceCalculator(50)) return true;
                    BattleLog.AppendText($"{pokemon.Name} is unable to move");
                    return false;

                case (int)PokemonEnum.Condition.PSN:
                    damage = pokemon.HPMax / 16;
                    pokemon.Hurt(damage);
                    BattleLog.AppendText($"{pokemon.Name} is hurt by poison (Damage: {damage})");
                    return pokemon.CheckIfPokemonAlive();

                case (int)PokemonEnum.Condition.SLP:
                    if (CalculatorHelper.ChanceCalculator(50))
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
            int previousValue = 0;
            if (isPlayerTarget)
            {
                if (battle.Pokemon.statModifierStages[statType] < 6 && battle.Pokemon.statModifierStages[statType] > -6)
                {
                    battle.Pokemon.statModifierStages[statType] += stageValue;
                    previousValue = battle.Pokemon.Stat.Stats[statType];
                    battle.Pokemon.Stat.Stats[statType] = Convert.ToInt32(battle.PokemonStartStats.Stats[statType] * StageHelper.StageToMultipler(battle.Pokemon.statModifierStages[statType]));
                    BattleLog.AppendText($"{battle.Pokemon.Name} {((PokemonEnum.Stat)statType).ToString()} changed from {previousValue} to {battle.Pokemon.Stat.Stats[statType]}");
                }
                else
                {
                    BattleLog.AppendText($"{battle.Pokemon.Name} {((PokemonEnum.Stat)statType).ToString()} cannot go any higher than {(float)battle.Pokemon.Stat.Stats[statType]}");
                } 
            }
            else
            {
                if (battle.EnemyPokemon.statModifierStages[statType] < 6 && battle.EnemyPokemon.statModifierStages[statType] > -6)
                {
                    battle.EnemyPokemon.statModifierStages[statType] += stageValue;
                    previousValue = battle.Pokemon.Stat.Stats[statType];
                    battle.EnemyPokemon.Stat.Stats[statType] = Convert.ToInt32(battle.EnemyPokemonStartStats.Stats[statType] * StageHelper.StageToMultipler(battle.EnemyPokemon.statModifierStages[statType]));
                    BattleLog.AppendText($"{battle.EnemyPokemon.Name} {((PokemonEnum.Stat)statType).ToString()} changed from {previousValue} to {battle.EnemyPokemon.Stat.Stats[statType]}");
                }
                else
                {
                    BattleLog.AppendText($"{battle.EnemyPokemon.Name} {((PokemonEnum.Stat)statType).ToString()} cannot go any higher than {(float)battle.EnemyPokemon.Stat.Stats[statType]}");
                }
            }
        }
    }
}