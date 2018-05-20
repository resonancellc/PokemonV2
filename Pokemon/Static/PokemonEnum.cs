using System;
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
            Grass = 3
        }
        
        public enum Stat
        {
            Attack = 0,
            Defence = 1,
            SpecialAttack,
            SpecialDefence,
            Speed
        }
    }
}
