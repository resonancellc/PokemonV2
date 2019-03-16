using System.Collections.Generic;

namespace Pokemon.Models
{
    public interface IPokemonParty<T>
    {
        IList<IPokemon> Pokemons { get; set; }
        IPokemon GetFirstAlivePokemon();
        IPokemon GetPokemonByIndex(int index);
        bool IsAnyPokemonAlive();
    }
}