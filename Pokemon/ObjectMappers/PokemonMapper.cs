using Dtos;
using Pokemon.Models;
using System.Linq;

namespace Pokemon.ObjectMappers
{
    public static class PokemonMapper
    {
        public static IPokemon ToDomainObject(this PokemonDto dto)
        {
            return new Models.Pokemon
            {
                ID = dto.ID,
                Name = dto.Name,
                HPCurrent = dto.HPCurrent,
                HPMax = dto.HPMax,
                Level = dto.Level,
                Stats = dto.Stats.ToDomainObject(),
                Attacks = dto.Attacks.Select(x => x.ToDomainObject()).ToList(),
                Condition = (Condition)dto.Condition,
                PrimaryTypeID = dto.PrimaryTypeID,
                SecondaryTypeID = dto.SecondaryTypeID
            };
        }
    }
}
