using System.Collections.Generic;

namespace Pokemon.Models
{
    public interface IPokemonParty<T>
    {
        List<IPokemon> Pokemons { get; set; }
        IPokemon GetPokemon();
    }
}