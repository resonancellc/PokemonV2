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
        public static IPokemon CreatePokemon()
        {
            IPokemon pokemon = new Pokemon();
            pokemon.Stats = PokemonStatsFactory.CreateStats();
            pokemon.Attacks = PokemonAttacksFactory.CreateAttacks();
            return pokemon;
        }

        public static IPokemon CreatePokemon(int level, int id = 0)
        {
            IPokemon pokemon;
            //PlayerID
            if (id > 0)
            {
                // obtaining the one specific pokemon

                pokemon = (IPokemon)PokemonList.Pokemons.Where(p => p.Key == id).FirstOrDefault().Value.Clone();
            }
            else
            {
                // filtering the overall list of pokemons by level, we don't want lvl 5 charizard
                var availablePokemons = PokemonList.Pokemons.Where(p => p.Value.MinimalLevel <= level);
                // selecting random entry from filtred list
                pokemon = (IPokemon)availablePokemons
                            .ElementAt(GenerateRandomNumber.GetRandomNumber(0, availablePokemons.Count()))
                            .Value.Clone();
            }

            pokemon.Level = level;
            pokemon.Stats = PokemonStatsFactory.CreateStats(level, pokemon.Stats);
            pokemon.HPCurrent = pokemon.HPMax = pokemon.Stats.Health;

            pokemon.Attacks = PokemonAttacksFactory.GetAttacks(pokemon);

            return pokemon;
        }

        public static IPokemon CreatePokemon(int level, int id, bool enemy = true)
        {
            IPokemon pokemon;
            if (id > 0)
            {
                // obtaining the one specific pokemon

                pokemon = (IPokemon)PokemonList.Pokemons.Where(p => p.Key == id).FirstOrDefault().Value.Clone();
            }
            else
            {
                // filtering the overall list of pokemons by level, we don't want lvl 5 charizard
                var availablePokemons = PokemonList.Pokemons.Where(p => p.Value.MinimalLevel <= level);
                // selecting random entry from filtred list
                pokemon = (IPokemon)availablePokemons
                            .ElementAt(GenerateRandomNumber.GetRandomNumber(0, availablePokemons.Count()))
                            .Value.Clone();
            }

            pokemon.Level = level;
            pokemon.Stats = PokemonStatsFactory.CreateStats(level, pokemon.Stats);
            pokemon.HPCurrent = pokemon.HPMax = pokemon.Stats.Health;

            pokemon.Attacks = PokemonAttacksFactory.GetAttacks(pokemon);

            return pokemon;
        }
    }
}
