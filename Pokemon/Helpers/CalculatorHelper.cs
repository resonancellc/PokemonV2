using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class CalculatorHelper
    {
        /// <summary>
        /// Calculating stats of new generated pokemon basing on his base stats (lvl 10 pokemon with 40 base attack gives pokemon with 15-19 attack)
        /// </summary>
        /// <param name="level"></param>
        /// <param name="pokemonStat"></param>
        /// <returns></returns>
        public static Stat CalculateStats(int level, Stat pokemonStat)
        {
            Random rand = new Random();
            Stat stat = new Stat();
            for (int i = 0; i < 5; i++)
            {
                stat.Stats[i] = (((10 + pokemonStat.Stats[i] + rand.Next(0, 20)) * 2) * level) / 100 + 5;
            }
            stat.Health = ((10 + pokemonStat.Health + rand.Next(0, 20) + 50) * level) / 50 + 10;
            stat.PrimaryTypeID = pokemonStat.PrimaryTypeID;
            stat.SecondaryTypeID = pokemonStat.SecondaryTypeID;
            return stat;
        }

        public static int CalculateAttackPower(bool isPlayerAttack, Attack attack, Battle battle)
        {
            int damage = 0;
            if (isPlayerAttack)
            {
                if (attack.BoostStats != string.Empty)
                {
                    ChangeTempStats(isPlayerAttack, attack, battle);
                }
                if (attack.Power.HasValue)
                {
                    int baseDamage = (2 * battle.Pokemon.Level / 5) + 2;                   
                    float attackDefenceRatio = (float)battle.Pokemon.Stat.Stats[0] / (float)battle.EnemyPokemon.Stat.Stats[1];
                    float multipler = ((float)AttackEffectivenessHelper.GetMultipler((int)attack.TypeID, battle.EnemyPokemon.Stat.PrimaryTypeID)) 
                                    * ((float)(battle.EnemyPokemon.Stat.SecondaryTypeID.HasValue ? AttackEffectivenessHelper.GetMultipler((int)attack.TypeID, (int)battle.EnemyPokemon.Stat.SecondaryTypeID) : 1d));

                    damage = Convert.ToInt32(baseDamage * attack.Power * attackDefenceRatio * multipler / 50);
                        
                }

                BattleLog.AppendText($"Zaatakowano {battle.EnemyPokemon.Name} za {damage} - jego obrona wynosiła {(float)battle.EnemyPokemon.Stat.Stats[1]}");
                return damage;
            }
            else
            {
                if (attack.BoostStats != string.Empty && attack.BoostStats != null)
                {
                    ChangeTempStats(isPlayerAttack, attack, battle);
                }
                if (attack.Power.HasValue)
                {
                    int baseDamage = (2 * battle.EnemyPokemon.Level / 5) + 2;
                    float attackDefenceRatio = (float)battle.EnemyPokemon.Stat.Stats[0] / (float)battle.Pokemon.Stat.Stats[1];
                    float multipler = ((float)AttackEffectivenessHelper.GetMultipler((int)attack.TypeID, battle.Pokemon.Stat.PrimaryTypeID))
                                    * ((float)(battle.Pokemon.Stat.SecondaryTypeID.HasValue ? AttackEffectivenessHelper.GetMultipler((int)attack.TypeID, (int)battle.Pokemon.Stat.SecondaryTypeID) : 1d));

                    damage = Convert.ToInt32(baseDamage * attack.Power * attackDefenceRatio * multipler / 50);

                }

                return damage;
            }
        }

        private static void ChangeTempStats(bool isPlayerAttack, Attack attack, Battle battle)
        {
            string[] attributes = attack.BoostStats.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); //AttackBoostStatsSplitter();
            if (attributes.Length > 0)
            {
                if (attributes[0] == "enemy")
                {
                    if (isPlayerAttack) // gracz na przeciwnika
                        ChangeTempStats(true, Int32.Parse(attributes[1]), Int32.Parse(attributes[2]), battle);
                    else
                        ChangeTempStats(false, Int32.Parse(attributes[1]), Int32.Parse(attributes[2]), battle);
                }
                else // używane na siebie
                {
                    if (isPlayerAttack) // gracz na siebie
                        ChangeTempStats(true, Int32.Parse(attributes[1]), Int32.Parse(attributes[2]), battle);
                    else // przeciwnik na siebie
                        ChangeTempStats(false, Int32.Parse(attributes[1]), Int32.Parse(attributes[2]), battle);
                    
                }
            }
        }

        private static void ChangeTempStats(bool isEnemyTarget, int statType, int stageValue, Battle battle)
        {
            if (!isEnemyTarget)
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
