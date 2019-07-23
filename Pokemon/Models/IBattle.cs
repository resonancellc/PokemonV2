namespace Pokemon.Models
{
    public interface IBattle
    {
        IPokemon PlayerPokemon{ get; set; }

        IPokemon EnemyPokemon{ get; set; }

        void PreparePokemonAttack(IAttack attack, IPokemon attackingPokemon, IPokemon targetPokemon);
    }
}