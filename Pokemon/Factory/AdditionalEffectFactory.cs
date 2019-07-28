using Pokemon.AdditionalEffects;
using Pokemon.Models;
using Pokemon.ObjectMappers;
namespace Pokemon.Factory
{
    public static class AdditionalEffectFactory
    {
        public static IAdditionalEffect CreateAdditionalEffect(AdditionalEffect additionalEffectRow)
        {
            IAdditionalEffect additionalEffect = null;
            int id = additionalEffectRow.ID;

            switch (id)
            {
                case (int)AdditionalEffectEnum.SameDamageLow:
                    additionalEffect = new AlwaysSameDamage();
                    break;
                case (int)AdditionalEffectEnum.SameDamageHigh:
                    additionalEffect = new AlwaysSameDamage();
                    break;
                case (int)AdditionalEffectEnum.SameDamageLevel:
                    additionalEffect = new AlwaysSameDamage();
                    break;
                case (int)AdditionalEffectEnum.DrainLife:
                    additionalEffect = new AdditionalEffect();
                    break;
                case (int)AdditionalEffectEnum.LeechLife:
                    additionalEffect = new AdditionalEffect();
                    break;
                case (int)AdditionalEffectEnum.Fast:
                    additionalEffect = new FastAttack();
                    break;
                case (int)AdditionalEffectEnum.AlwaysHits:
                    additionalEffect = new AlwaysHits();
                    break;
                case (int)AdditionalEffectEnum.HighCriticalChance:
                    additionalEffect = new HighCriticalRatio();
                    break;
                case (int)AdditionalEffectEnum.BoostCriticalSelf:
                    additionalEffect = new CritBoosting();
                    break;
                case (int)AdditionalEffectEnum.BoostCriticalTarget:
                    additionalEffect = new CritBoosting();
                    break;
                case (int)AdditionalEffectEnum.ChargeLow:
                    additionalEffect = new AdditionalEffect();
                    break;
                case (int)AdditionalEffectEnum.ChargeHigh:
                    additionalEffect = new AdditionalEffect();
                    break;
                case (int)AdditionalEffectEnum.RechargeLow:
                    additionalEffect = new AdditionalEffect();
                    break;
                case (int)AdditionalEffectEnum.RechargeHigh:
                    additionalEffect = new AdditionalEffect();
                    break;
                case (int)AdditionalEffectEnum.TwoToFiveHits:
                    additionalEffect = new AdditionalEffect();
                    break;
                case (int)AdditionalEffectEnum.SwapPokemonMax:
                    additionalEffect = new AdditionalEffect();
                    break;
                case (int)AdditionalEffectEnum.PoisonWeak:
                    additionalEffect = new StatusChanger();
                    break;
                case (int)AdditionalEffectEnum.PoisonMid:
                    additionalEffect = new StatusChanger();
                    break;
                case (int)AdditionalEffectEnum.PoisonHigh:
                    additionalEffect = new StatusChanger();
                    break;
                case (int)AdditionalEffectEnum.PoisonMax:
                    additionalEffect = new StatusChanger();
                    break;
                case (int)AdditionalEffectEnum.BurnWeak:
                    additionalEffect = new StatusChanger();
                    break;
                case (int)AdditionalEffectEnum.BurnMaxSelf:
                    additionalEffect = new StatusChanger();
                    break;
                case (int)AdditionalEffectEnum.ParalysisWeak:
                    additionalEffect = new StatusChanger();
                    break;
                case (int)AdditionalEffectEnum.ParalysisMax:
                    additionalEffect = new StatusChanger();
                    break;
                case (int)AdditionalEffectEnum.ParalysisMaxWeak:
                    additionalEffect = new StatusChanger();
                    break;
                case (int)AdditionalEffectEnum.ParalysisMaxSelf:
                    additionalEffect = new StatusChanger();
                    break;
                case (int)AdditionalEffectEnum.SleepMax:
                    additionalEffect = new StatusChanger();
                    break;
                case (int)AdditionalEffectEnum.SleepMaxSelf:
                    additionalEffect = new StatusChanger();
                    break;
                case (int)AdditionalEffectEnum.FlinchWeak:
                    additionalEffect = new StatusChanger();
                    break;
                case (int)AdditionalEffectEnum.ConfusionWeak:
                    additionalEffect = new StatusChanger();
                    break;
                case (int)AdditionalEffectEnum.ConfusionMax:
                    additionalEffect = new StatusChanger();
                    break;
                default:
                    additionalEffect = new AdditionalEffect();
                    break;
            }

            additionalEffect.SetAdditionalEffectProperties(additionalEffectRow);

            return additionalEffect;
        }
    }
}