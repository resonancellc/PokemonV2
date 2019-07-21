using Pokemon.Models;

namespace Pokemon.Calculators
{
    public static class StatsCalculator
    {
        public static IPokemonStats GetCalculatedStats(IPokemonStats pokemonBaseStats, int level)
        {
            return new PokemonStats
            {
                Health = ((10 + pokemonBaseStats.Health + GenerateRandomNumber.GetRandomNumber(0, 20) + 50) * level) / 50 + 10,
                Attack = (((10 + pokemonBaseStats.Attack + GenerateRandomNumber.GetRandomNumber(0, 20)) * 2) * level) / 100 + 5,
                Defence = (((10 + pokemonBaseStats.Defence + GenerateRandomNumber.GetRandomNumber(0, 20)) * 2) * level) / 100 + 5,
                SpecialAttack = (((10 + pokemonBaseStats.SpecialAttack + GenerateRandomNumber.GetRandomNumber(0, 20)) * 2) * level) / 100 + 5,
                SpecialDefence = (((10 + pokemonBaseStats.SpecialDefence + GenerateRandomNumber.GetRandomNumber(0, 20)) * 2) * level) / 100 + 5,
                Speed = (((10 + pokemonBaseStats.Speed + GenerateRandomNumber.GetRandomNumber(0, 20)) * 2) * level) / 100 + 5
            };
        }
    }
}
