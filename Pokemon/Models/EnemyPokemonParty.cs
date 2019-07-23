using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pokemon.Models
{
    public class EnemyPokemonParty : IPokemonParty<IPokemon>
    {
        public IList<IPokemon> Pokemons { get; set; }
        public IPokemon ActivePokemon { get; set; }

        public EnemyPokemonParty()
        {
            Pokemons = new List<IPokemon>();
        }

        public IPokemon GetFirstAlivePokemon()
        {
            IPokemon pokemon = Pokemons.First(p => p.HPCurrent > 0);
            ActivePokemon = pokemon;
            return pokemon;
        }

        public IPokemon GetPokemonByIndex(int index) => Pokemons[index];

        public bool IsAnyPokemonAlive() => Pokemons.Any(p => p.HPCurrent > 0);

        public void ResetParty()
        {
            foreach (IPokemon pokemon in Pokemons)
            {
                pokemon.ResetStats();
                pokemon.HPCurrent = pokemon.HPMax;
            }
        }

        public IEnumerator<IPokemon> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
