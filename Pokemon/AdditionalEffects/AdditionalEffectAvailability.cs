using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.AdditionalEffects
{
    public static class AdditionalEffectAvailability
    {
        public static bool ContainsEffectType(this List<IAdditionalEffect> additionalEffects, Type type)
        {
            return additionalEffects.Any(e => e.GetType() == type);
        }
    }
}
