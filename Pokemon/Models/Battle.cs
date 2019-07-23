using System.Linq;
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
            PlayerPokemon = pokemon;
            EnemyPokemon = enemyPokemon;
        }

        public void PreparePokemonAttack(IAttack attack, IPokemon attackingPokemon, IPokemon targetPokemon)
        {
            if (attackingPokemon.IsFlinched)
            {
                attackingPokemon.IsFlinched = false;
                BattleLog.AppendText($"{attackingPokemon.Name} is flinched");
                return;
            }
            
            if (attackingPokemon.IsConfused && BattleHelper.HasFailedConfusion(attackingPokemon))
            {
                var confusionHit = PokemonAttacksFactory.CreateAttack("ConfusionHit");
                int damage = DamageCalculator.CalculateAttackDamage(confusionHit, attackingPokemon, attackingPokemon);
                attackingPokemon.Hurt(damage);
                BattleLog.AppendText($"{attackingPokemon.Name} hurts itself in its confusion");
                return;
            }

            if (!BattleHelper.IsAbleToAttackAfterConditionEffect(attackingPokemon))
            {
                return;
            }

            if(!attack.AdditionalEffects.ContainsEffectType(typeof(AlwaysHits)) && BattleHelper.IsMiss(attack))
            {
                BattleLog.AppendText($"{attackingPokemon.Name} missed!");
                return;
            }
            
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
                if (damage < 1)
                {
                    damage = 1;

                }
                if (BattleHelper.IsCritical(attack.AdditionalEffects, attackingPokemon.IsEnergyFocused))
                {
                    damage *= 2;
                    BattleLog.AppendText("Critical hit!");
                }
            }
            
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

            if (damage != 0)
            {
                targetPokemon.Hurt(damage);
            }
        }
    }
}
