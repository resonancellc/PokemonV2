using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.AdditionalEffects
{
    public interface IAdditionalEffectAvailability
    {
        bool IsAvailable(List<IAdditionalEffect> additionalEffects);
    }
}
