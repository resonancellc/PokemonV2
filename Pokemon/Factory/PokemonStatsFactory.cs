using Pokemon.Calculators;
using Pokemon.Models;

namespace Pokemon.Factory
{
    public static class PokemonStatsFactory
    {
        public static IPokemonStats CreateStats()
        {
            return new PokemonStats();
        }

        public static IPokemonStats CreateStats(IPokemonStats pokemonBaseStats, int level)
        {
            return StatsCalculator.GetCalculatedStats(pokemonBaseStats, level);
        }
    }
}
