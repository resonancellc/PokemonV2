using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class BattleHelper
    {
        public static bool IsPlayerPokemonFaster(Attack playerAttack, Attack enemyAttack, Battle battle)
        {
            if (enemyAttack.AdditionalEffect == "fast" && playerAttack.AdditionalEffect == "fast" || enemyAttack.AdditionalEffect != "fast" && playerAttack.AdditionalEffect != "fast")
            {
                if (battle.Pokemon.Stat.Stats[(int)PokemonEnum.Stat.Speed] > battle.EnemyPokemon.Stat.Stats[(int)PokemonEnum.Stat.Speed])
                    return true;
                else
                    return false;
            }
            else if (playerAttack.AdditionalEffect == "fast")
            {
                return true;
            }
            else if (enemyAttack.AdditionalEffect == "fast")
            {
                return false;
            }
            return true;
        }

        public static bool IsMiss(Attack attack)
        {
            Random rand = new Random();
            int accuracy = rand.Next(0, 100);

            if (accuracy > attack.Accuracy)
            {
                return true;
            }
            return false;
        }



    }
}
