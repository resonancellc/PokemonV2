﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class EquipmentItem : IdNameItem
    { 
        public string Description { get; set; }
        public int Cost { get; set; }
    }
}