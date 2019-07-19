using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pokemon.AdditionalEffects
{
    public static class AdditionalEffectAvailability
    {
        public static bool ContainsEffectType(this ICollection<IAdditionalEffect> additionalEffects, Type type)
        {
            return additionalEffects.Any(e => e.GetType() == type);
        }
    }
}
