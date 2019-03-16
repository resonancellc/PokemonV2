using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Models
{
    public class PlayerPokemonParty : IPokemonParty<IPokemon>
    {
        public IList<IPokemon> Pokemons { get; set; }

        public PlayerPokemonParty()
        {
            Pokemons = new List<IPokemon>();
        }

        public IPokemon GetFirstAlivePokemon()
        {
            return Pokemons.First(p => p.HPCurrent > 0);
        }

        public IPokemon GetPokemonByIndex(int index)
        {
            return Pokemons[index];
        }

        public bool IsAnyPokemonAlive()
        {
            return Pokemons.Any(p => p.HPCurrent > 0)
        }
    }
}
