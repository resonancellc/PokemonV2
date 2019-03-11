using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Models
{
    public class PlayerPokemonParty : IPokemonParty<IPokemon>
    {
        public List<IPokemon> Pokemons { get; set; }

        public IPokemon GetFirstAlivePokemon()
        {
            return Pokemons.First(p => p.HPCurrent > 0);
        }

        public IPokemon GetPokemon()
        {
            throw new NotImplementedException();
        }
    }
}
