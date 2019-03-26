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

        public static IPokemonParty<IPokemon> CreatePokemonParty(bool isPlayerParty, IList<IPokemon> pokemons)
        {
            // no need for switch case here because it is not going to be extend, there will never be more than 2 parties at given time
            IPokemonParty<IPokemon> party;

            if (isPlayerParty) party = new PlayerPokemonParty();
            else party = new EnemyPokemonParty();

            party.Pokemons = pokemons;
            return party;
        }

        public static IPokemonParty<IPokemon> CreateEnemyPokemonParty(int teamSize, int level)
        {
            IPokemonParty<IPokemon> party = new EnemyPokemonParty();
            for (int i = 0; i < teamSize; i++)
            {
                // EnemyID
                IPokemon pokemon = PokemonFactory.CreatePokemon(level, 35, true);
                party.Pokemons.Add(pokemon);
            }
            return party;
        }
    }
}
