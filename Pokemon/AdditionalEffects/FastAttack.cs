using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokemon.Models;

namespace Pokemon.AdditionalEffects
{
    public class FastAttack : IAdditionalEffect
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? PrimaryValue { get; set; }
        public int? SecondaryValue { get; set; }
        public bool IsOnSelf { get; set; }

        public bool IsAvailable(List<IAdditionalEffect> additionalEffects)
        {
            return additionalEffects.Any(p => p.Name.Contains(StringEnums.FastAttack));
        }
    }
}
