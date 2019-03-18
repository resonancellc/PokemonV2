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
            float stageMultipler = StageHelper.StageToMultipler(pokemon.StatModifierStages[(int)StatType.Attack]);
            int conditionMultipler = pokemon.Condition != Condition.BRN ? 1 : 2;
            int attack = Convert.ToInt32(pokemon.Stats.Attack / conditionMultipler * stageMultipler);
            return attack;
        }

        public static int GetDefence(IPokemon pokemon)
        {
            float stageMultipler = StageHelper.StageToMultipler(pokemon.StatModifierStages[(int)StatType.Defence]);
            int defence = Convert.ToInt32(pokemon.Stats.Defence * stageMultipler);
            return defence;
        }

        public static int GetSpecialAttack(IPokemon pokemon)
        {
            float stageMultipler = StageHelper.StageToMultipler(pokemon.StatModifierStages[(int)StatType.SpecialAttack]);
            int specialAttack = Convert.ToInt32(pokemon.Stats.SpecialAttack * stageMultipler);
            return specialAttack;
        }

        public static int GetSpecialDefence(IPokemon pokemon)
        {
            float stageMultipler = StageHelper.StageToMultipler(pokemon.StatModifierStages[(int)StatType.SpecialDefence]);
            int specialDefence = Convert.ToInt32(pokemon.Stats.SpecialDefence * stageMultipler);
            return specialDefence;
        }

        public static int GetSpeed(IPokemon pokemon)
        {
            float stageMultipler = StageHelper.StageToMultipler(pokemon.StatModifierStages[(int)StatType.Speed]);
            int conditionMultipler = Convert.ToInt32(pokemon.Condition != Condition.PAR ? 1 : 1.5f);
            int speed = Convert.ToInt32(pokemon.Stats.Speed / conditionMultipler * stageMultipler);
            return speed;
        }





    }
}
