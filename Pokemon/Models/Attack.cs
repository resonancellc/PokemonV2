using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class Attack : IAttack
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int? Power { get; set; }
        public int? Accuracy { get; set; }
        public string BoostStats { get; set; }
        public ElementalType ElementalType { get; set; }
        public bool IsSpecial { get; set; }
        public List<IAdditionalEffect> AdditionalEffects { get; set; }
    }

    
}
