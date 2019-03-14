using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokemon.Models;

namespace Pokemon.AdditionalEffects
{
    public class FastAttack : IAdditionalEffectAvailability
    {
        public bool IsAvailable(List<IAdditionalEffect> additionalEffects)
        {
            return additionalEffects.Any(p => p.Name.Contains(StringEnums.FastAttack));
        }
    }
}
