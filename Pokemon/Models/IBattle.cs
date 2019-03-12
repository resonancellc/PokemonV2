using Pokemon.Models;

namespace Pokemon
{
    public interface IBattle
    {
        IPokemon Pokemon { get; set; }
        IPokemon EnemyPokemon { get; set; }
        void PokemonAttack(IAttack attack, IPokemon attackingPokemon, bool isPlayerAttack);
    }
}