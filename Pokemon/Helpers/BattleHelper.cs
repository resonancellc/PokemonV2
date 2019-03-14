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
        public static bool IsPlayerPokemonFaster(IAttack playerAttack, IAttack enemyAttack, IBattle battle)
        {
            return true;


            //if (enemyAttack.AdditionalEffect == "fast" && playerAttack.AdditionalEffect == "fast" || enemyAttack.AdditionalEffect != "fast" && playerAttack.AdditionalEffect != "fast")
            //{
            //    if (battle.Pokemon.Stats.Speed > battle.EnemyPokemon.Stats.Speed)
            //        return true;
            //    else
            //        return false;
            //}
            //else if (playerAttack.AdditionalEffect == "fast")
            //{
            //    return true;
            //}
            //else if (enemyAttack.AdditionalEffect == "fast")
            //{
            //    return false;
            //}
            //return true;
        }

        public static bool IsMiss(IAttack attack)
        {
            return !CalculatorHelper.ChanceCalculator(attack.Accuracy.Value);
        }

        public static bool IsConfused(IPokemon attackingPokemon)
        {
            if (!attackingPokemon.IsConfused) return false;
            return CalculatorHelper.ChanceCalculator(50, 100);
        }

        public static bool IsCritical(IAttack attack, IPokemon attackingPokemon)
        {
            return true;
            //int boostCrit = attackingPokemon.IsEnergyFocused ? 20 : 0;
            //if (attack.AdditionalEffect == "highCrit")
            //    return CalculatorHelper.ChanceCalculator(21 + boostCrit, 255);
            //else
            //    return CalculatorHelper.ChanceCalculator(1 + boostCrit, 255);
        }

        public static void ChangeCondition(IAttack attack, IPokemon targetPokemon)
        {
            //string[] attributes = attack.AdditionalEffect.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); //AttackBoostStatsSplitter();

            //if (attributes.Contains("burn"))
            //{
            //    if (targetPokemon.Condition != 0) BattleLog.AppendText($"{targetPokemon.Name} is unaffected");
            //    else if (IsConditionChanged(targetPokemon, (int)Condition.BRN, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100))
            //    {
            //        BattleLog.AppendText($"{targetPokemon.Name} is now burning");
            //        targetPokemon.Stats.Attack = targetPokemon.Stats.Attack / 2;
            //        return;
            //    }
            //}
            //if (attributes.Contains("freeze"))
            //{
            //    if (targetPokemon.Condition != 0) BattleLog.AppendText($"{targetPokemon.Name} is unaffected");
            //    else if (IsConditionChanged(targetPokemon, (int)Condition.FRZ, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100))
            //    {
            //        BattleLog.AppendText($"{targetPokemon.Name} is now frozen");
            //        return;
            //    }
            //}
            //if (attributes.Contains("paralysis"))
            //{
            //    if (targetPokemon.Condition != 0) BattleLog.AppendText($"{targetPokemon.Name} is unaffected");
            //    else if (IsConditionChanged(targetPokemon, (int)Condition.PAR, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100))
            //    {
            //        BattleLog.AppendText($"{targetPokemon.Name} is now paralyzed");
            //        targetPokemon.Stats.Speed = Convert.ToInt32((float)targetPokemon.Stats.Speed / 1.5f);
            //        return;
            //    }
            //}
            //if (attributes.Contains("poison"))
            //{
            //    if (targetPokemon.Condition != 0) BattleLog.AppendText($"{targetPokemon.Name} is unaffected");
            //    else if (IsConditionChanged(targetPokemon, (int)Condition.PSN, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100))
            //    {
            //        BattleLog.AppendText($"{targetPokemon.Name} is now poisoned");
            //        return;
            //    }
            //}
            //if (attributes.Contains("sleep"))
            //{
            //    if (targetPokemon.Condition != 0) BattleLog.AppendText($"{targetPokemon.Name} is unaffected");
            //    else if (IsConditionChanged(targetPokemon, (int)Condition.SLP, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100))
            //    {
            //        BattleLog.AppendText($"{targetPokemon.Name} is now sleeping");
            //        return;
            //    }
            //}
            //if (attributes.Contains("confusion"))
            //{
            //    if (targetPokemon.IsConfused) BattleLog.AppendText($"{targetPokemon.Name} is already confused");
            //    else if (attributes.Length > 2)
            //    {
            //        if (CalculatorHelper.ChanceCalculator(Convert.ToInt32(attributes[2]), 100))
            //        {
            //            targetPokemon.IsConfused = true;
            //            BattleLog.AppendText($"{targetPokemon.Name} is now confused");
            //            return;
            //        }
            //    }
            //}
        }

        public static bool IsConditionChanged(IPokemon targetPokemon, int newCondition, int effectChance)
        {
            if (CalculatorHelper.ChanceCalculator(effectChance))
            {
                targetPokemon.Condition = (Condition)newCondition;
                return true;
            }
            return false;            
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
                    if (CalculatorHelper.ChanceCalculator(50)) return true;
                    BattleLog.AppendText($"{pokemon.Name} is unable to move");
                    return false;

                case Condition.PSN:
                    damage = pokemon.HPMax / 16;
                    pokemon.Hurt(damage);
                    BattleLog.AppendText($"{pokemon.Name} is hurt by poison (Damage: {damage})");
                    return pokemon.IsPokemonAlive();

                case Condition.SLP:
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

        public static void ChangeTempStats(bool isPlayerAttack, IAttack attack, IBattle battle)
        {
            string[] boosts = attack.BoostStats.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string boost in boosts)
            {
                string[] attributes = boost.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); //AttackBoostStatsSplitter();
                if (attributes.Length > 0)
                {
                    bool isEnemyTarget = attributes[0] == "enemy";
                    ChangeTempStats(isPlayerAttack, isEnemyTarget, Int32.Parse(attributes[1]), Int32.Parse(attributes[2]), battle);
                }
            }

        }

        public static void ChangeTempStats(bool isPlayerAttack, bool isEnemyTarget, int statType, int stageValue, IBattle battle)
        {
            if (!isPlayerAttack && isEnemyTarget || isPlayerAttack && !isEnemyTarget) // 
                ChangePokemonStats(true, statType, battle, stageValue);

            else if (isPlayerAttack && isEnemyTarget || !isPlayerAttack && !isEnemyTarget) // 
                ChangePokemonStats(false, statType, battle, stageValue);
        }

        public static bool ChangeTempPokemonStats(Pokemon pokemon, int statType, int stageValue)
        {
            //if (pokemon.statModifierStages[statType] <= 6 - stageValue)
            //{
            //    pokemon.statModifierStages[statType] += stageValue;
            //    int previousValue = pokemon.Stat.Stats[statType];
            //    pokemon.Stat.Stats[statType] = Convert.ToInt32(pokemon.StartStats.Stats[statType] * StageHelper.StageToMultipler(pokemon.statModifierStages[statType]));
            //    //BattleLog.AppendText($"{pokemon.Name} {((PokemonEnum.Stat)statType).ToString()} changed from {previousValue} to {pokemon.Stat.Stats[statType]}");
            //    return true;
            //}
            return false;

        }

        private static void ChangePokemonStats(bool isPlayerTarget, int statType, IBattle battle, int stageValue)
        {
            //int previousValue = 0;
            //if (isPlayerTarget)
            //{
            //    if (battle.Pokemon.statModifierStages[statType] < 6 && battle.Pokemon.statModifierStages[statType] > -6)
            //    {
            //        battle.Pokemon.statModifierStages[statType] += stageValue;
            //        previousValue = battle.Pokemon.Stat.Stats[statType];
            //        battle.Pokemon.Stat.Stats[statType] = Convert.ToInt32(battle.Pokemon.StartStats.Stats[statType] * StageHelper.StageToMultipler(battle.Pokemon.statModifierStages[statType]));
            //        BattleLog.AppendText($"{battle.Pokemon.Name} {((PokemonEnum.Stat)statType).ToString()} changed from {previousValue} to {battle.Pokemon.Stat.Stats[statType]}");
            //    }
            //    else
            //    {
            //        BattleLog.AppendText($"{battle.Pokemon.Name} {((PokemonEnum.Stat)statType).ToString()} cannot go any higher than {(float)battle.Pokemon.Stat.Stats[statType]}");
            //    } 
            //}
            //else
            //{
            //    if (battle.EnemyPokemon.statModifierStages[statType] < 6 && battle.EnemyPokemon.statModifierStages[statType] > -6)
            //    {
            //        battle.EnemyPokemon.statModifierStages[statType] += stageValue;
            //        previousValue = battle.EnemyPokemon.Stat.Stats[statType];
            //        battle.EnemyPokemon.Stat.Stats[statType] = Convert.ToInt32(battle.EnemyPokemon.StartStats.Stats[statType] * StageHelper.StageToMultipler(battle.EnemyPokemon.statModifierStages[statType]));
            //        BattleLog.AppendText($"{battle.EnemyPokemon.Name} {((PokemonEnum.Stat)statType).ToString()} changed from {previousValue} to {battle.EnemyPokemon.Stat.Stats[statType]}");
            //    }
            //    else
            //    {
            //        BattleLog.AppendText($"{battle.EnemyPokemon.Name} {((PokemonEnum.Stat)statType).ToString()} cannot go any higher than {(float)battle.EnemyPokemon.Stat.Stats[statType]}");
            //    }
            //}
        }
    }
}
