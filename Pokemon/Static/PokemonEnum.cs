﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class PokemonEnum
    {
        public enum Type
        {
            Normal = 0,
            Fire = 1,
            Water = 2,
            Electric = 3,
            Grass = 4,
            Ice = 5,
            Fighting = 6,
            Poison = 7,
            Ground = 8,
            Flying = 9,
            Psychic = 10,
            Bug = 11,
            Rock = 12,
            Ghost = 13,
            Dragon = 14,
        }
        
        public enum Stat
        {
            Attack = 0,
            Defence = 1,
            SpecialAttack = 2,
            SpecialDefence = 3,
            Speed = 4
        }

        public enum Condition
        {
            None = 0,
            Burn = 1,
            Freeze = 2,
            Paralysis = 3,
            Poison = 4,
            Sleep = 5 
        }
        
    }
}
