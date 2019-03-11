using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class CalculatorHelper
    {
        ///// <summary>
        ///// Calculating stats of new generated pokemon basing on his base stats (lvl 10 pokemon with 40 base attack gives pokemon with 15-19 attack)
        ///// </summary>
        ///// <param name="level"></param>
        ///// <param name="pokemonStat"></param>
        ///// <returns></returns>
        //public static IPokemonStats CalculateStats(int level, IPokemonStats pokemonStat)
        //{
        //    IPokemonStats pokemonStats = Factory.CreatePokemonStats();

        //    pokemonStats.Health = ((10 + pokemonStat.Health + GenerateRandomNumber.GetRandomNumber(0, 20) + 50) * level) / 50 + 10;
        //    pokemonStats.Attack = (((10 + pokemonStat.Attack + GenerateRandomNumber.GetRandomNumber(0, 20)) * 2) * level) / 100 + 5;
        //    pokemonStats.Defence = (((10 + pokemonStat.Attack + GenerateRandomNumber.GetRandomNumber(0, 20)) * 2) * level) / 100 + 5;
        //    pokemonStats.SpecialAttack = (((10 + pokemonStat.Attack + GenerateRandomNumber.GetRandomNumber(0, 20)) * 2) * level) / 100 + 5;
        //    pokemonStats.SpecialDefence = (((10 + pokemonStat.Attack + GenerateRandomNumber.GetRandomNumber(0, 20)) * 2) * level) / 100 + 5;
        //    pokemonStats.Speed = (((10 + pokemonStat.Attack + GenerateRandomNumber.GetRandomNumber(0, 20)) * 2) * level) / 100 + 5;

        //    return pokemonStats;
        //}

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

        public static int CalculateWinnings()
        {
            float sum = 0;
            int pokemonCount = 0;
            int winnings = 0;
            foreach (Pokemon pokemon in PokemonParty.playerPokemons)
            {
                if (pokemon == null) break;
                pokemonCount++;
                sum += (float)pokemon.HPCurrent / (float)pokemon.HPMax;
            }
            winnings = Convert.ToInt32(sum * 100 / pokemonCount);
            PlayerEquipment.Money += winnings;
            return winnings;
        }
    }
}
