using System.Collections.Generic;

namespace Pokemon.Models
{
    public interface IPokemonParty<T> : IEnumerable<T>
    {
        IPokemon ActivePokemon { get; set; }
        IList<IPokemon> Pokemons { get; set; }
        IPokemon GetFirstAlivePokemon();
        IPokemon GetPokemonByIndex(int index);
        bool IsAnyPokemonAlive();
        void ResetParty();
    }
}