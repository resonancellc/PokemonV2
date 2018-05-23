using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class PokemonGenerator
    {
        public static Pokemon GetPokemon(int level)
        {
            Random rand = new Random();
            var IDs = StaticSQL.GetAvailablePokemonIDs().Rows;
            int id = Convert.ToInt32(IDs[rand.Next(IDs.Count)].ItemArray[0]);


            return new Pokemon(id, level, CalculatorHelper.CalculateStats(level, StaticTypes.GetPokeStatsByID(id)));
        }

        public static Pokemon GetPokemon(int id, int level)
        {     
            return new Pokemon(id, level, CalculatorHelper.CalculateStats(level, StaticTypes.GetPokeStatsByID(id)));
        }
    }
}
