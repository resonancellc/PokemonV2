﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class AttackDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int? Power { get; set; }
        public int? Accuracy { get; set; }
        public string BoostStats { get; set; }
        public int ElementalType { get; set; }
        public bool IsSpecial { get; set; }
        public ICollection<IAdditionalEffect> AdditionalEffects { get; set; }
    }
}