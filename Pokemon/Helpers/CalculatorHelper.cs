using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class CalculatorHelper
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        //CalculatorHelper.RandomNumber()
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }




        /// <summary>
        /// Calculating stats of new generated pokemon basing on his base stats (lvl 10 pokemon with 40 base attack gives pokemon with 15-19 attack)
        /// </summary>
        /// <param name="level"></param>
        /// <param name="pokemonStat"></param>
        /// <returns></returns>
        public static Stat CalculateStats(int level, Stat pokemonStat)
        {
            Stat stat = new Stat();
            for (int i = 0; i < 5; i++)
            {
                stat.Stats[i] = (((10 + pokemonStat.Stats[i] + CalculatorHelper.RandomNumber(0,20)) * 2) * level) / 100 + 5;
            }
            stat.Health = ((10 + pokemonStat.Health + CalculatorHelper.RandomNumber(0, 20) + 50) * level) / 50 + 10;
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
                if (attack.Power.HasValue)
                {
                    int baseDamage = CalculateBaseDamage(battle.Pokemon.Level);
                    float attackDefenceRatio = CalculateAttackDefenceRatio(battle.Pokemon.Stat.Stats, battle.EnemyPokemon.Stat.Stats, attack.IsSpecial);
                    float multipler = CalculateMultipler(battle.EnemyPokemon.PrimaryTypeID, attack.TypeID, battle.EnemyPokemon.SecondaryTypeID);
                    if (multipler > 1) BattleLog.AppendText("It's super effective!");
                    else if (multipler < 1) BattleLog.AppendText("It's not very effective!");
                    damage = CalculateOverallDamage(baseDamage, (int)attack.Power, attackDefenceRatio, multipler);                      
                }

                return damage;
            }
            else
            {            
                if (attack.Power.HasValue)
                {
                    int baseDamage = CalculateBaseDamage(battle.EnemyPokemon.Level);
                    float attackDefenceRatio = CalculateAttackDefenceRatio(battle.EnemyPokemon.Stat.Stats, battle.Pokemon.Stat.Stats, attack.IsSpecial);
                    float multipler = CalculateMultipler(battle.Pokemon.PrimaryTypeID, attack.TypeID, battle.Pokemon.SecondaryTypeID);
                    if (multipler > 1) BattleLog.AppendText("It's super effective!");
                    else if (multipler < 1) BattleLog.AppendText("It's not very effective!");
                    damage = CalculateOverallDamage(baseDamage, (int)attack.Power, attackDefenceRatio, multipler);
                }

                return damage;
            }
        }     



        public static bool ChanceCalculator(int chance, int maxRange = 100)
        {
            int chanceScore = CalculatorHelper.RandomNumber(0, maxRange);
            if (chanceScore <= chance)
            {
                return true; // success
            }
            return false;
        }
    }
}
