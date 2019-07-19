using Pokemon.Calculators;
using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Factory
{
    public static class PokemonStatsFactory
    {
        public static IPokemonStats CreateStats()
        {
            return new PokemonStats();
        }

        public static IPokemonStats CreateStats(int level, IPokemonStats pokemonBaseStats)
        {
            IPokemonStats pokemonStats = new PokemonStats();
            pokemonStats = StatsCalculator.GetCalculatedStats(pokemonStats, level, pokemonBaseStats);
            return pokemonStats;
        }
    }
}
