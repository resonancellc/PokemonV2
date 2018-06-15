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

        private static int CalculateBaseDamage(int level)
        {
            return (2 * level / 5) + 2;
        }
        private static float CalculateAttackDefenceRatio(int[] attackerStats, int[] targetStats, bool isSpecial)
        {
            if (!isSpecial)
                return (float)attackerStats[(int)PokemonEnum.Stat.Attack] / (float)targetStats[(int)PokemonEnum.Stat.Defence];
            else
                return (float)attackerStats[(int)PokemonEnum.Stat.SpecialAttack] / (float)targetStats[(int)PokemonEnum.Stat.SpecialDefence];
        }
        private static float CalculateMultipler(int targetPrimaryType, int? attackType, int? targetSecondaryType = null)
        {
            return ((float)AttackEffectivenessHelper.GetMultipler((int)attackType, targetPrimaryType))
                                                * ((float)(targetSecondaryType.HasValue ? AttackEffectivenessHelper.GetMultipler((int)attackType, (int)targetSecondaryType) : 1d));
        }
        private static int CalculateOverallDamage(int baseDamage, int attackPower, float attackDefenceRatio, float multipler)
        {
            return Convert.ToInt32(baseDamage * attackPower * attackDefenceRatio * multipler / 50);
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
                    int baseDamage = CalculateBaseDamage(battle.Pokemon.Level);
                    float attackDefenceRatio = CalculateAttackDefenceRatio(battle.Pokemon.Stat.Stats, battle.EnemyPokemon.Stat.Stats, attack.IsSpecial);
                    float multipler = CalculateMultipler(battle.EnemyPokemon.PrimaryTypeID, attack.TypeID, battle.EnemyPokemon.SecondaryTypeID);

                    damage = CalculateOverallDamage(baseDamage, (int)attack.Power, attackDefenceRatio, multipler);                      
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
                    int baseDamage = CalculateBaseDamage(battle.EnemyPokemon.Level);
                    float attackDefenceRatio = CalculateAttackDefenceRatio(battle.EnemyPokemon.Stat.Stats, battle.Pokemon.Stat.Stats, attack.IsSpecial);
                    float multipler = CalculateMultipler(battle.Pokemon.PrimaryTypeID, attack.TypeID, battle.Pokemon.SecondaryTypeID);

                    damage = CalculateOverallDamage(baseDamage, (int)attack.Power, attackDefenceRatio, multipler);
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
