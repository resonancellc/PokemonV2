using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
