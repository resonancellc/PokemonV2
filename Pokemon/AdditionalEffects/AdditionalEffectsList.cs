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
                var values = additionalEffectRow.ItemArray;
                IAdditionalEffect additionalEffect = AdditionalEffectFactory.CreateAdditionalEffect();

                additionalEffect.ID = (int)values[0];
                additionalEffect.Name = (string)values[1];
                additionalEffect.Description = values[2] != DBNull.Value ? (string)values[2] : "";

                additionalEffect.PrimaryValue = values[3] != DBNull.Value ? (int?)values[3] : null;
                additionalEffect.SecondaryValue = values[4] != DBNull.Value ? (int?)values[4] : null;

                additionalEffect.IsOnSelf = values[5] != DBNull.Value ? (bool)values[5] : false;

                AdditionalEffects.Add(additionalEffect.ID, additionalEffect);
            }
        }


    }
}
