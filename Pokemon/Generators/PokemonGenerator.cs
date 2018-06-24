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
#warning Przeniesc listę IDków pokemonów do stałej - załaduj na starcie i nigdy więcej
            int id = 0;
            List<int> IDs = new List<int>();
            foreach (Item pokemon in StaticTypes.pokemonList)
            {
                IDs.Add(pokemon.ID);
            }
            id = IDs[CalculatorHelper.RandomNumber(1, IDs.Count)];

            while (level < StaticTypes.GetPokeStatsByID(id).MinimalLevel)
            {
                id = IDs[CalculatorHelper.RandomNumber(1, IDs.Count)];
            }



            return new Pokemon(id, level, CalculatorHelper.CalculateStats(level, StaticTypes.GetPokeStatsByID(id)));
        }

        public static Pokemon GetPokemon(int id, int level)
        {     
            return new Pokemon(id, level, CalculatorHelper.CalculateStats(level, StaticTypes.GetPokeStatsByID(id)));
        }
    }
}
