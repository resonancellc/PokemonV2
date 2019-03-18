using Pokemon.AdditionalEffects;
using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Factory
{
    public static class AdditionalEffectFactory
    {
        public static IAdditionalEffect CreateAdditionalEffect(object[] data)
        {
            IAdditionalEffect additionalEffect = null;
            int id = (int)data[0];

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

            additionalEffect = FillAdditionalEffectData(additionalEffect, data);
            return additionalEffect;
        }

        public static IAdditionalEffect FillAdditionalEffectData(IAdditionalEffect additionalEffect, object[] data)
        {
            additionalEffect.ID = (int)data[0];
            additionalEffect.Name = (string)data[1];
            additionalEffect.Description = data[2] != DBNull.Value ? (string)data[2] : "";
            additionalEffect.PrimaryValue = data[3] != DBNull.Value ? (int?)data[3] : null;
            additionalEffect.SecondaryValue = data[4] != DBNull.Value ? (int?)data[4] : null;
            additionalEffect.IsOnSelf = data[5] != DBNull.Value ? (bool)data[5] : false;
            return additionalEffect;
        }


        public static IAdditionalEffect GetAdditionalEffect(int id)
        {
            return AdditionalEffects.AdditionalEffectsList.AdditionalEffects.Where(p => p.Key == id).FirstOrDefault().Value;
        }

        public static ICollection<IAdditionalEffect> GetAdditionalEffects(string additionalEffectIDs)
        {
            ICollection<IAdditionalEffect> additionalEffects = new List<IAdditionalEffect>();

            string[] effects = additionalEffectIDs.Trim().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string effectId in effects)
            {
                additionalEffects.Add(AdditionalEffects.AdditionalEffectsList.AdditionalEffects.Where(p => p.Key == Convert.ToInt32(effectId)).FirstOrDefault().Value);
            }

            return additionalEffects;
        }

    }
}