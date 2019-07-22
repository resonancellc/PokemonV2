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
                    StatsChange statsChange = new StatsChange()
                    {
                        AffectedPokemon = attributes[0] == "enemy" ? targetPokemon : attackingPokemon,
                        StatType = (StatType)Int32.Parse(attributes[1]),
                        StageValue = Int32.Parse(attributes[2])
                    };

                    StatsChangeValidator statsChangeValidator = new StatsChangeValidator(statsChange);

                    if (statsChangeValidator.StatChangePossible())
                    {
                        ChangeTempPokemonStats(statsChange);
                    }
                    else
                    {
                        BattleLog.AppendText($"{statsChange.AffectedPokemon.Name} {statsChange.StatType.ToString()} cannot go any higher");
                    }
                }
            }
        }

        public static void ChangeTempPokemonStats(StatsChange statsChange)
        {
            statsChange.AffectedPokemon.StatModifierStages[(int)statsChange.StatType] += statsChange.StageValue;
        }
    }
}
