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
        public static IAdditionalEffect CreateAdditionalEffect()
        {
            return new AdditionalEffect();
        }

        public static IAdditionalEffect GetAdditionalEffect(int id)
        {
            return AdditionalEffectsList.AdditionalEffects.Where(p => p.Key == id).FirstOrDefault().Value;
        }

        public static List<IAdditionalEffect> GetAdditionalEffects(string additionalEffectIDs)
        {
            List<IAdditionalEffect> additionalEffects = new List<IAdditionalEffect>();

            string[] effects = additionalEffectIDs.Trim().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string effectId in effects)
            {
                additionalEffects.Add(AdditionalEffectsList.AdditionalEffects.Where(p => p.Key == Convert.ToInt32(effectId)).FirstOrDefault().Value);
            }

            return additionalEffects;
        }
    }
}
