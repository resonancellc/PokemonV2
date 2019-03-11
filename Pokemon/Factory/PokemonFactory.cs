using Pokemon.Factory;
using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Factory
{
    public static class PokemonFactory
    {
        public static IPokemon CreatePokemon(int level)
        {
            IPokemon pokemon = new Pokemon();

            // filtering the overall list of pokemons by level, we don't want lvl 5 charizard
            var availablePokemons = PokemonList.Pokemons.Where(p => p.Value.MinimalLevel <= level);
            // selecting random entry from filtred list
            pokemon = availablePokemons
                        .ElementAt(GenerateRandomNumber.GetRandomNumber(0, availablePokemons.Count()))
                        .Value;

            pokemon.Stats = PokemonStatsFactory.CreateStats(level, pokemon.Stats);
            pokemon.Attacks = PokemonAttacksFactory.CreateAttacks(level);
            return pokemon;
        } 
    }
}
