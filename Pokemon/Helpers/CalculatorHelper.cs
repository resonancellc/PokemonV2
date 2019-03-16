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
        private static int CalculateBaseDamage(int level)
        {
            return (2 * level / 5) + 2;
        }

        private static float CalculateAttackDefenceRatio(int attackStat, int defenceStat)
        {
            return (float)attackStat / defenceStat;
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

        public static int CalculateAttackPower(bool isPlayerAttack, IAttack attack, IBattle battle)
        {
            int damage = 0;
            if (isPlayerAttack)
            {
                if (attack.Power.HasValue)
                {
                    int baseDamage = CalculateBaseDamage(battle.Pokemon.Level);

                    float attackDefenceRatio;
                    if (!attack.IsSpecial)
                    {
                        attackDefenceRatio = CalculateAttackDefenceRatio(battle.Pokemon.Stats.Attack, battle.EnemyPokemon.Stats.Defence);
                    }
                    else
                    {
                        attackDefenceRatio = CalculateAttackDefenceRatio(battle.Pokemon.Stats.SpecialAttack, battle.EnemyPokemon.Stats.SpecialDefence);
                    }
                    
                    float multipler = CalculateMultipler(battle.EnemyPokemon.PrimaryTypeID, (int)attack.ElementalType, battle.EnemyPokemon.SecondaryTypeID);

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

                    float attackDefenceRatio;
                    if (!attack.IsSpecial)
                    {
                        attackDefenceRatio = CalculateAttackDefenceRatio(battle.EnemyPokemon.Stats.Attack, battle.Pokemon.Stats.Defence);
                    }
                    else
                    {
                        attackDefenceRatio = CalculateAttackDefenceRatio(battle.EnemyPokemon.Stats.SpecialAttack, battle.Pokemon.Stats.SpecialDefence);
                    }

                    float multipler = CalculateMultipler(battle.Pokemon.PrimaryTypeID, (int)attack.ElementalType, battle.Pokemon.SecondaryTypeID);

                    if (multipler > 1) BattleLog.AppendText("It's super effective!");
                    else if (multipler < 1) BattleLog.AppendText("It's not very effective!");
                    damage = CalculateOverallDamage(baseDamage, (int)attack.Power, attackDefenceRatio, multipler);
                }

                return damage;
            }
        }     



        public static bool ChanceCalculator(int chance, int maxRange = 100)
        {
            int chanceScore = GenerateRandomNumber.GetRandomNumber(0, maxRange);
            if (chanceScore <= chance)
            {
                return true; // success
            }
            return false;
        }

        public static int CalculateWinnings()
        {
            //float sum = 0;
            //int pokemonCount = 0;
            //int winnings = 0;
            //foreach (Pokemon pokemon in PokemonParty.playerPokemons)
            //{
            //    if (pokemon == null) break;
            //    pokemonCount++;
            //    sum += (float)pokemon.HPCurrent / (float)pokemon.HPMax;
            //}
            //winnings = Convert.ToInt32(sum * 100 / pokemonCount);
            //PlayerEquipment.Money += winnings;
            //return winnings;
            return 100;
        }
    }
}
