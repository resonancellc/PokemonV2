using Pokemon.Models;
using System;

namespace Pokemon.Helpers
{
    public static class StatsChanger
    {
        public static void ChangeTempStats(IAttack attack, IPokemon attackingPokemon, IPokemon targetPokemon)
        {
            string[] boosts = attack.BoostStats.SplitBoosts();
            foreach (string boost in boosts)
            {
                string[] attributes = boost.SplitAttributes();
                if (attributes.Length > 0)
                {

                    ChangeTempPokemonStats(attributes[0] == "enemy" ? targetPokemon : attackingPokemon, Int32.Parse(attributes[1]), Int32.Parse(attributes[2]));
                }
            }
        }

        public static void ChangeTempPokemonStats(IPokemon affectedPokemon, int statType, int stageValue)
        {
            if (affectedPokemon.StatModifierStages[statType] <= 6 - stageValue 
                && affectedPokemon.StatModifierStages[statType] + stageValue >= -6 )
            {
                affectedPokemon.StatModifierStages[statType] += stageValue;
            }
            else
            {
                BattleLog.AppendText($"{affectedPokemon.Name} {((StatType)statType).ToString()} cannot go any higher");
            }
        }
    }
}
