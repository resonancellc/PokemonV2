using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Calculators
{
    public static class TempStatsCalculator
    {
        public static int GetAttack(IPokemon pokemon)
        {
            int attack = Convert.ToInt32(pokemon.Stats.Attack / (pokemon.Condition != Condition.BRN ? 1 : 2));
            return attack;
        }

        public static int GetDefence(IPokemon pokemon)
        {
            return pokemon.Stats.Defence;
        }

        public static int GetSpecialAttack(IPokemon pokemon)
        {
            return pokemon.Stats.SpecialAttack;
        }

        public static int GetSpecialDefence(IPokemon pokemon)
        {
            return pokemon.Stats.SpecialDefence;
        }

        public static int GetSpeed(IPokemon pokemon)
        {
            int speed = Convert.ToInt32(pokemon.Stats.Speed / (pokemon.Condition != Condition.PAR ? 1 : 1.5f));
            return speed;
        }





    }
}
