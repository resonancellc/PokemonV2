using Pokemon.Models;

namespace Pokemon
{
    public interface IBattle
    {
        IPokemon PlayerPokemon{ get; set; }
        IPokemon EnemyPokemon{ get; set; }
        void PreparePokemonAttack(IAttack attack, IPokemon attackingPokemon, IPokemon targetPokemon);
        void PerformPokemonAttack(IAttack attack, bool isPlayerAttack);
    }
}