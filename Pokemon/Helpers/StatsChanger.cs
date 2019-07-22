using Pokemon.Models;
using Pokemon.ObjectMappers;

namespace Pokemon.Helpers
{
    public static class StatsChanger
    {
        public static void ChangeTempStats(IAttack attack, IPokemon attackingPokemon, IPokemon targetPokemon)
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
                }
                else
                {
                    BattleLog.AppendText($"{affectedPokemon.Name} {statsBoost.StatType.ToString()} cannot go any higher");
                }
            }
        }

        public static void ChangeTempPokemonStats(IPokemon affectedPokemon, StatsBoost statsBoost)
        {
            affectedPokemon.StatModifierStages[(int)statsBoost.StatType] += statsBoost.Value;
        }
    }
}
