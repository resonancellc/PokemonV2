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
            Random rand = new Random();
            int chance = rand.Next(0, 255);

            if (attack.AdditionalEffect == "highCrit") chance -= 20;

            if (chance <= 1) return true;

            return false;
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
