using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class Attack : IdNameItem
    {
        public int Level { get; set; }
        public int? Power { get; set; }
        public int? Accuracy { get; set; }

        public string BoostStats { get; set; }

        public int? TypeID { get; set; }

        public bool IsSpecial { get; set; }
        public string AdditionalEffect { get; set; }
    }

    
}
