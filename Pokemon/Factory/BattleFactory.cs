using Pokemon.Models;
using Pokemon.Views;

namespace Pokemon.Factory
{
    public static class BattleFactory
    {
        public static IBattle CreateBattle(IPokemon playerPokemon, IPokemon enemyPokemon, IBattleView battleView, IBattleLogController battleLogController)
        {
            return new BattleController(playerPokemon, enemyPokemon, battleView, battleLogController);
        }
    }
}
