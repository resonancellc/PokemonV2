using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Calculators
{
    public static class DamageCalculator
    {
        public static int CalculateAttackDamage(IAttack attack, IPokemon attackingPokemon, IPokemon targetPokemon)
        {
            int damage = 0;
            if (attack.Power.HasValue)
            {
                int baseDamage = CalculateBaseDamage(attackingPokemon.Level);

                float attackDefenceRatio = attack.IsSpecial != true ? CalculateAttackDefenceRatio(TempStatsCalculator.GetAttack(attackingPokemon), TempStatsCalculator.GetDefence(targetPokemon))
                                                                    : CalculateAttackDefenceRatio(TempStatsCalculator.GetSpecialAttack(attackingPokemon), TempStatsCalculator.GetSpecialDefence(targetPokemon));

                float multipler = CalculateMultipler(targetPokemon.PrimaryTypeID, (int)attack.ElementalType, targetPokemon.SecondaryTypeID);

                if (multipler > 1) BattleLog.AppendText("It's super effective!");
                else if (multipler < 1) BattleLog.AppendText("It's not very effective!");

                damage = Convert.ToInt32(baseDamage * (int)attack.Power * attackDefenceRatio * multipler / 50);
            }

            return damage;
        }

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
    }
}
