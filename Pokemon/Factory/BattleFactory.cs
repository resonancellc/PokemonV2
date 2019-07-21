using Pokemon.Models;

namespace Pokemon.Factory
{
    public static class BattleFactory
    {
        public static IBattle CreateBattle(IPokemon playerPokemon, IPokemon enemyPokemon)
        {
            return new Battle(playerPokemon, enemyPokemon);
        }
    }
}
