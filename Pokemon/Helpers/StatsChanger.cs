using Pokemon.Models;
using Pokemon.ObjectMappers;

namespace Pokemon.Helpers
{
    public static class StatsChanger
    {
        public static string ChangeTempStats(IAttack attack, IPokemon attackingPokemon, IPokemon targetPokemon)
        {
            string[] boosts = attack.BoostStats.SplitBoosts();
            string output = string.Empty;

            foreach (string boostString in boosts)
            {
                StatsBoost statsBoost = StatsBoostMapper.ToDomainObject(boostString);
                IPokemon affectedPokemon = statsBoost.Target == Target.Self ? attackingPokemon : targetPokemon;
                StatsChangeValidator statsChangeValidator = new StatsChangeValidator(
                    affectedPokemon,
                    statsBoost);

                if (statsChangeValidator.StatChangePossible())
                {
                    ChangeTempPokemonStats(affectedPokemon, statsBoost);
                    string changeType = statsBoost.Value > 0 ? "increased" : "decreased";
                    output += $"{affectedPokemon.Name} {statsBoost.StatType.ToString()} {changeType}\n"; 
                }
                else
                {
                    return $"{affectedPokemon.Name} {statsBoost.StatType.ToString()} cannot go any higher";
                }
            }
            return output;
        }

        public static void ChangeTempPokemonStats(IPokemon affectedPokemon, StatsBoost statsBoost)
        {
            affectedPokemon.StatModifierStages[(int)statsBoost.StatType] += statsBoost.Value;
        }
    }
}
