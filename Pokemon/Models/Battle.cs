using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pokemon.Models;
using Pokemon.Factory;
using Pokemon.AdditionalEffects;
using Pokemon.Calculators;
using Pokemon.Helpers;

namespace Pokemon
{
    public class Battle : IBattle
    {
        public IPokemon PlayerPokemon { get; set; }
        public IPokemon EnemyPokemon { get; set; }

        public Battle(IPokemon pokemon, IPokemon enemyPokemon)
        {
            this.PlayerPokemon = pokemon;
            this.EnemyPokemon = enemyPokemon;
        }

        public void PreparePokemonAttack(IAttack attack, IPokemon attackingPokemon, IPokemon targetPokemon)
        {
            // pokemon is scared to attack
            if (attackingPokemon.IsFlinched)
            {
                attackingPokemon.IsFlinched = false;
                BattleLog.AppendText($"{attackingPokemon.Name} is flinched");
                return;
            }

            // pokemon is confused and failed confusion test - he damaged himself and is not able to perform given attack
            if (BattleHelper.HasFailedConfusion(attackingPokemon))
            {
                IAttack confusionHit = PokemonAttacksFactory.CreateAttack("ConfusionHit");
                int damage = DamageCalculator.CalculateAttackDamage(confusionHit, attackingPokemon, attackingPokemon);
                attackingPokemon.Hurt(damage);
                BattleLog.AppendText($"{attackingPokemon.Name} hurts itself in its confusion");
                return;
            }

            // pokemon is not able to attack after applying condition effects - sleeping or dead for example
            if (!BattleHelper.IsAbleToAttackAfterConditionEffect(attackingPokemon)) return;

            // pokemon dind't use attack that always hits and missed
            if(!attack.AdditionalEffects.ContainsEffectType(typeof(AlwaysHits)) && BattleHelper.IsMiss(attack))
            {
                BattleLog.AppendText($"{attackingPokemon.Name} missed!");
                return;
            }

            // If you reached that part that means you succesfully attacked opposite pokemon! :)

            // move those lines to PerformPokemonAttack method
            BattleLog.AppendText($"{attackingPokemon.Name} used {attack.Name}");

            PerformPokemonAttack(attack, attackingPokemon, targetPokemon);

            if (attack.BoostStats != string.Empty)
                StatsChanger.ChangeTempStats(attack, attackingPokemon, targetPokemon);
        }

        private void PerformPokemonAttack(IAttack attack, IPokemon attackingPokemon, IPokemon targetPokemon)
        {
            int damage = 0;

            if (attack.AdditionalEffects.ContainsEffectType(typeof(AlwaysSameDamage)))
            {
                AlwaysSameDamage alwaysSameDamage = attack.AdditionalEffects.First(e => e is AlwaysSameDamage) as AlwaysSameDamage;
                damage = alwaysSameDamage.IsBasedOnLevel() ? attackingPokemon.Level : (int)alwaysSameDamage.PrimaryValue;
            }

            if (damage == 0 && attack.Power.HasValue)
            {
                damage = DamageCalculator.CalculateAttackDamage(attack, attackingPokemon, targetPokemon);
                if (damage < 1) damage = 1;
                if (BattleHelper.IsCritical(attack, attackingPokemon))
                {
                    damage *= 2;
                    BattleLog.AppendText("Critical hit!");
                }
            }

            // post attack effects
            if (AdditionalEffectAvailability.ContainsEffectType(attack.AdditionalEffects, typeof(CritBoosting)))
            {
                if (attack.AdditionalEffects.Any(e => e.ID == (int)AdditionalEffectEnum.BoostCriticalSelf))
                {
                    CritBoosting critBoosting = attack.AdditionalEffects.First(e => e.ID == (int)AdditionalEffectEnum.BoostCriticalSelf) as CritBoosting;
                    critBoosting.SetPokemonFocus(attackingPokemon);
                }
                else if (attack.AdditionalEffects.Any(e => e.ID == (int)AdditionalEffectEnum.BoostCriticalTarget))
                {
                    CritBoosting critBoosting = attack.AdditionalEffects.First(e => e.ID == (int)AdditionalEffectEnum.BoostCriticalTarget) as CritBoosting;
                    critBoosting.SetPokemonFocus(targetPokemon);
                }
            }

            if (AdditionalEffectAvailability.ContainsEffectType(attack.AdditionalEffects, typeof(StatusChanger)))
            {
                if (attack.AdditionalEffects.Any(e => e.ID == (int)AdditionalEffectEnum.PoisonWeak))
                {
                    StatusChanger statusChanger = attack.AdditionalEffects.First(e => e.ID == (int)AdditionalEffectEnum.PoisonWeak) as StatusChanger;
                    statusChanger.ChangeStatus(targetPokemon);
                }
                if (attack.AdditionalEffects.Any(e => e.ID == (int)AdditionalEffectEnum.PoisonMid))
                {
                    StatusChanger statusChanger = attack.AdditionalEffects.First(e => e.ID == (int)AdditionalEffectEnum.PoisonMid) as StatusChanger;
                    statusChanger.ChangeStatus(targetPokemon);
                }
                if (attack.AdditionalEffects.Any(e => e.ID == (int)AdditionalEffectEnum.PoisonHigh))
                {
                    StatusChanger statusChanger = attack.AdditionalEffects.First(e => e.ID == (int)AdditionalEffectEnum.PoisonHigh) as StatusChanger;
                    statusChanger.ChangeStatus(targetPokemon);
                }
                if (attack.AdditionalEffects.Any(e => e.ID == (int)AdditionalEffectEnum.PoisonMax))
                {
                    StatusChanger statusChanger = attack.AdditionalEffects.First(e => e.ID == (int)AdditionalEffectEnum.PoisonMax) as StatusChanger;
                    statusChanger.ChangeStatus(targetPokemon);
                }
                if (attack.AdditionalEffects.Any(e => e.ID == (int)AdditionalEffectEnum.BurnWeak))
                {
                    StatusChanger statusChanger = attack.AdditionalEffects.First(e => e.ID == (int)AdditionalEffectEnum.BurnWeak) as StatusChanger;
                    statusChanger.ChangeStatus(targetPokemon);
                }
                if (attack.AdditionalEffects.Any(e => e.ID == (int)AdditionalEffectEnum.ConfusionWeak))
                {
                    StatusChanger statusChanger = attack.AdditionalEffects.First(e => e.ID == (int)AdditionalEffectEnum.ConfusionWeak) as StatusChanger;
                    statusChanger.ChangeStatus(targetPokemon);
                }
                if (attack.AdditionalEffects.Any(e => e.ID == (int)AdditionalEffectEnum.ConfusionMax))
                {
                    StatusChanger statusChanger = attack.AdditionalEffects.First(e => e.ID == (int)AdditionalEffectEnum.ConfusionMax) as StatusChanger;
                    statusChanger.ChangeStatus(targetPokemon);
                }
            }

            //if (attack.AdditionalEffect != String.Empty)
            //{
            //    // those are all boolean methods, why?
            //    BattleHelper.ChangeCondition(attack, isPlayerAttack ? EnemyPokemon : Pokemon);
            //    AdditionalEffectHelper.SetFlinch(attack.AdditionalEffect, isPlayerAttack ? EnemyPokemon : Pokemon);
            //    AdditionalEffectHelper.IsCritBoosting(attack.AdditionalEffect, isPlayerAttack ? Pokemon : EnemyPokemon);
            //}

            if (damage != 0)
            {
                targetPokemon.Hurt(damage);
            }

        }

        public void PerformPokemonAttack(IAttack attack, bool isPlayerAttack)
        {
            throw new NotImplementedException();
        }
    }
}
