using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Factory
{
    public static class PokemonPartyFactory
    {
        public static IPokemonParty<IPokemon> CreatePokemonParty(bool isPlayerParty)
        {
            if (isPlayerParty) return new PlayerPokemonParty();
            else return new EnemyPokemonParty();
        }

        public static IPokemonParty<IPokemon> CreatePokemonParty(bool isPlayerParty, List<IPokemon> pokemons)
        {
            if (isPlayerParty)
            {
                PlayerPokemonParty party = new PlayerPokemonParty();
                party.Pokemons = pokemons;
                return party;
            }
            else
            {
                EnemyPokemonParty party = new EnemyPokemonParty();
                party.Pokemons = pokemons;
                return party;
            }
        }

        public static IPokemonParty<IPokemon> CreateEnemyPokemonParty(int teamSize, int level)
        {
            IPokemonParty<IPokemon> party = new EnemyPokemonParty();
            for (int i = 0; i < teamSize; i++)
            {
                IPokemon pokemon = PokemonFactory.CreatePokemon(level);
                party.Pokemons.Add(pokemon);
            }
            return party;
        }
    }
}
