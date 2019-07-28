using Pokemon.Models;
using System;

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

                float attackDefenceRatio = attack.IsSpecial
                    ? CalculateAttackDefenceRatio(
                        TempStatsCalculator.GetSpecialAttack(attackingPokemon),
                        TempStatsCalculator.GetSpecialDefence(targetPokemon))
                    : CalculateAttackDefenceRatio(
                        TempStatsCalculator.GetAttack(attackingPokemon),
                        TempStatsCalculator.GetDefence(targetPokemon)); 

                float multipler = CalculateMultipler(
                    targetPokemon.PrimaryTypeID,
                    (int)attack.TypeID,
                    targetPokemon.SecondaryTypeID);

                if (multipler != 1)
                {
                    BattleLog.AppendText(multipler > 1
                        ? "It's super effective!"
                        : "It's not very effective!");
                }

                damage = Convert.ToInt32(baseDamage * (int)attack.Power * attackDefenceRatio * multipler / 50);
            }

            return damage;
        }

        private static int CalculateBaseDamage(int level) => (2 * level / 5) + 2;

        private static float CalculateAttackDefenceRatio(int attackStat, int defenceStat) => attackStat / defenceStat;

        private static float CalculateMultipler(int targetPrimaryType, int attackType, int? targetSecondaryType = null)
        {
            float multipler = AttackEffectivenessHelper.GetMultipler(attackType, targetPrimaryType);

            if (targetSecondaryType.HasValue)
            {
                multipler *= AttackEffectivenessHelper.GetMultipler(attackType, (int)targetSecondaryType);
            }
            
            return multipler;
        }
    }
}
