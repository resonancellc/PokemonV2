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

        public static IPokemonStats CreateStats(int level, IPokemonStats pokemonStat)
        {
            IPokemonStats pokemonStats = new PokemonStats()
            {
                Health = ((10 + pokemonStat.Health + GenerateRandomNumber.GetRandomNumber(0, 20) + 50) * level) / 50 + 10,
                Attack = (((10 + pokemonStat.Attack + GenerateRandomNumber.GetRandomNumber(0, 20)) * 2) * level) / 100 + 5,
                Defence = (((10 + pokemonStat.Attack + GenerateRandomNumber.GetRandomNumber(0, 20)) * 2) * level) / 100 + 5,
                SpecialAttack = (((10 + pokemonStat.Attack + GenerateRandomNumber.GetRandomNumber(0, 20)) * 2) * level) / 100 + 5,
                SpecialDefence = (((10 + pokemonStat.Attack + GenerateRandomNumber.GetRandomNumber(0, 20)) * 2) * level) / 100 + 5,
                Speed = (((10 + pokemonStat.Attack + GenerateRandomNumber.GetRandomNumber(0, 20)) * 2) * level) / 100 + 5,
            };

            return pokemonStats;
        }
    }
}
