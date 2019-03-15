using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokemon.Models;
using Pokemon.Factory;
using System.Data;

namespace Pokemon.AdditionalEffects
{
    public static class AdditionalEffectsList
    {
        public static Dictionary<int, IAdditionalEffect> AdditionalEffects = new Dictionary<int, IAdditionalEffect>();

        public static void FillAdditionalEffectsList()
        {
            DataRowCollection additionalEffectDataRows = StaticSQL.GetAdditionalEffects().Rows;
            foreach (DataRow additionalEffectRow in additionalEffectDataRows)
            {
                IAdditionalEffect additionalEffect = AdditionalEffectFactory.CreateAdditionalEffect(additionalEffectRow.ItemArray);
                AdditionalEffects.Add(additionalEffect.ID, additionalEffect);
            }
        }


    }
}
