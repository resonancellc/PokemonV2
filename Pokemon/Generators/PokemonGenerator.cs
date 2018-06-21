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
            List<int> IDs = new List<int>();
            foreach (Item pokemon in StaticTypes.pokemonList)
            {
                IDs.Add(pokemon.ID);
            }

            int id = IDs[CalculatorHelper.RandomNumber(1,IDs.Count)];


            return new Pokemon(id, level, CalculatorHelper.CalculateStats(level, StaticTypes.GetPokeStatsByID(id)));
        }

        public static Pokemon GetPokemon(int id, int level)
        {     
            return new Pokemon(id, level, CalculatorHelper.CalculateStats(level, StaticTypes.GetPokeStatsByID(id)));
        }
    }
}
