using System.Collections.Generic;
using Pokemon.Models;
using Pokemon.Factory;
using System.Data;
using System.Linq;

namespace Pokemon.AdditionalEffects
{
    public static class AdditionalEffectsList
    {
        public static Dictionary<int, IAdditionalEffect> AdditionalEffects = new Dictionary<int, IAdditionalEffect>();

        public static void FillAdditionalEffectsList()
        {
            AdditionalEffects = StaticSQL.GetAdditionalEffects()
                .Select(AdditionalEffectFactory.CreateAdditionalEffect)
                .ToDictionary(e => e.ID, e => e);
        }

        public static ICollection<IAdditionalEffect> GetAdditionalEffects(int attackID)
        {
            var additionalEffectIds = StaticSQL.GetAttackAdditionalEffectIDs(attackID);
            if (!additionalEffectIds.Any())
            {
                return null;
            }

            return AdditionalEffects
                .Where(t => additionalEffectIds.IndexOf(t.Key) != -1)
                .Select(x => x.Value)
                .ToList();
        }
    }
}
