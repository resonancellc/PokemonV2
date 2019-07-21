using Pokemon.Models;
using System.Collections.Generic;

namespace Pokemon.Factory
{
    public static class PokemonPartyFactory
    {
        public static IPokemonParty<IPokemon> CreatePokemonParty(IList<IPokemon> pokemons, bool isPlayerParty)
        {
            if (isPlayerParty)
            {
                return new PlayerPokemonParty() { Pokemons = pokemons };
            }
            else
            {
                return new EnemyPokemonParty() { Pokemons = pokemons };
            }
        }
    }
}
