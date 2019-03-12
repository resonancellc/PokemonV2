using Pokemon.Models;

namespace Pokemon
{
    public interface IBattle
    {
        IPokemon Pokemon { get; set; }
        IPokemon EnemyPokemon { get; set; }
        void PreparePokemonAttack(IAttack attack, IPokemon attackingPokemon, bool isPlayerAttack);
        void PerformPokemonAttack(IAttack attack, bool isPlayerAttack);
    }
}