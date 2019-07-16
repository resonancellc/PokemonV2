using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.ObjectMappers
{
    public static class StatsMapper
    {
        public static PokemonStats ToDomainObject(this StatsDto dto)
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
