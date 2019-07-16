using Dtos;

namespace Pokemon.ObjectMappers
{
    public static class PokemonMapper
    {
        public static Pokemon ToDomainObject(this PokemonDto dto)
        {
            return new Pokemon
            {
                ID = dto.ID,
                Name = dto.Name,
                HPCurrent = dto.HPCurrent,
                HPMax = dto.HPMax,
                Level = dto.Level,
                Stats = dto.Stats.ToDomainObject(),
                Attacks = null, //dto.Attacks.Select(x => x.ToDomainObject()),
                Condition = (Condition)dto.Condition,
                PrimaryTypeID = dto.PrimaryTypeID,
                SecondaryTypeID = dto.SecondaryTypeID
            };
        }
    }
}
