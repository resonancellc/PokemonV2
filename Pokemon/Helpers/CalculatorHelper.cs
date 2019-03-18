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

        public static int CalculateAttackDamage(IAttack attack, IPokemon attackingPokemon, IPokemon targetPokemon)
        {
            int damage = 0;
            if (attack.Power.HasValue)
            {
                int baseDamage = CalculateBaseDamage(attackingPokemon.Level);

                float attackDefenceRatio = attack.IsSpecial != true ? CalculateAttackDefenceRatio(attackingPokemon.Stats.Attack, targetPokemon.Stats.Defence)
                                                                    : CalculateAttackDefenceRatio(attackingPokemon.Stats.SpecialAttack, targetPokemon.Stats.SpecialDefence);

                float multipler = CalculateMultipler(targetPokemon.PrimaryTypeID, (int)attack.ElementalType, targetPokemon.SecondaryTypeID);

                if (multipler > 1) BattleLog.AppendText("It's super effective!");
                else if (multipler < 1) BattleLog.AppendText("It's not very effective!");

                damage = Convert.ToInt32(baseDamage * (int)attack.Power * attackDefenceRatio * multipler / 50);
            }

            return damage;
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
