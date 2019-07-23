using Pokemon.Models;
using System.Linq;

namespace Pokemon.Factory
{
    public static class PokemonFactory
    {
        public static IPokemon CreatePokemon()
        {
            IPokemon pokemon = new Models.Pokemon
            {
                Stats = PokemonStatsFactory.CreateStats(),
                Attacks = PokemonAttacksFactory.CreateAttacks()
            };
            return pokemon;
        }

        public static IPokemon CreatePokemon(int level, int id = 0)
        {
            IPokemon pokemon;
            level = level < 1 || level > 100 ? 5 : level;

            if (id != 0)
            {
                id = PokemonList.Pokemons.Any(p => p.Key == id) ? id : 1;

                 pokemon = (IPokemon)PokemonList.Pokemons
                    .Where(p => p.Key == id)
                    .First()
                    .Value
                    .Clone();
            }
            else
            {
                var availablePokemons = PokemonList.Pokemons.Where(p => p.Value.MinimalLevel <= level);
                pokemon = (IPokemon)availablePokemons
                            .ElementAt(GenerateRandomNumber.GetRandomNumber(0, availablePokemons.Count()))
                            .Value
                            .Clone();
            }

            pokemon.Level = level;
            pokemon.Stats = PokemonStatsFactory.CreateStats(pokemon.Stats, level);
            pokemon.HPCurrent = pokemon.HPMax = pokemon.Stats.Health;
            pokemon.Attacks = PokemonAttacksFactory.GetAttacks(pokemon);

            return pokemon;
        }
    }
}
