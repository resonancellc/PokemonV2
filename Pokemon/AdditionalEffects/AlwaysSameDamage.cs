using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.AdditionalEffects
{
    public class AlwaysSameDamage : IAdditionalEffectAvailability, IAdditionalEffect
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? PrimaryValue { get; set; }
        public int? SecondaryValue { get; set; }
        public bool IsOnSelf { get; set; }

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

        public string Test()
        {
            return "I'm not an abstraction anymore, i have hands and body! I'm something you can touch!";
        }
    }
}
