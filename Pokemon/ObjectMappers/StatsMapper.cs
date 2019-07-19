using Dtos;
using Pokemon.Models;

namespace Pokemon.ObjectMappers
{
    public static class StatsMapper
    {
        public static IPokemonStats ToDomainObject(this StatsDto dto)
        {
            return new PokemonStats
            {
                Attack = dto.Attack,
                Defence = dto.Defence,
                Health = dto.Health,
                SpecialAttack = dto.SpecialAttack,
                SpecialDefence = dto.SpecialDefence,
                Speed = dto.Speed
            };
        }
    }
}
