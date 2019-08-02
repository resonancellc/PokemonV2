using Pokemon.Models;
using Pokemon.ObjectMappers;

namespace Pokemon.Helpers
{
    public static class StatsChanger
    {
        public static bool ChangeTempStats(IAttack attack, IPokemon attackingPokemon, IPokemon targetPokemon)
        {
            string[] boosts = attack.BoostStats.SplitBoosts();
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
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public static void ChangeTempPokemonStats(IPokemon affectedPokemon, StatsBoost statsBoost)
        {
            affectedPokemon.StatModifierStages[(int)statsBoost.StatType] += statsBoost.Value;
        }
    }
}
