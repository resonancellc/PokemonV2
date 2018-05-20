using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class PokemonGenerator
    {
        public static Pokemon GetPokemon(int id, int level)
        {     
            return new Pokemon(id, level, CalculatorHelper.CalculateStats(level, StaticTypes.GetPokeStatsByID(id)));

        }
    }
}
