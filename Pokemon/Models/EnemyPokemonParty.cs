using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Models
{
    class EnemyPokemonParty : IPokemonParty<IPokemon>
    {
        public List<IPokemon> Pokemons { get; set; }

        public IPokemon GetPokemon()
        {
            return Pokemons.First(p => p.HPCurrent > 0);
        }
    }
}
