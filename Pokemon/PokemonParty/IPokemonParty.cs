using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public interface IPokemonParty
    {
        void AddToParty(Pokemon pokemon);
        void AddManyToParty(Pokemon[] pokemons);
    }
}
