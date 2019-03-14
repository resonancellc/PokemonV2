using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.AdditionalEffects
{
    public class AlwaysSameDamage : IAdditionalEffectAvailability, IAdditionalEffectValuesObtainable
    {
        public int GetPrimaryValue(IAdditionalEffect additionalEffect)
        {
            return (int)additionalEffect.PrimaryValue;
        }

        public int GetSecondaryValue(IAdditionalEffect additionalEffect)
        {
            return (int)additionalEffect.SecondaryValue;
        }

        public bool IsAvailable(List<IAdditionalEffect> additionalEffects)
        {
            return additionalEffects.Any(p => p.Name.Contains(StringEnums.SameDamage));
        }
    }
}
