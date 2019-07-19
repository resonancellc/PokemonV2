using Dtos;
using Pokemon.Models;

namespace Pokemon.ObjectMappers
{
    public static class AdditionalEffectMapper
    {
        public static IAdditionalEffect ToDomainObject(this AdditionalEffectDto dto)
        {
            return new AdditionalEffect
            {
                ID = dto.ID,
                Name = dto.Name,
                Description = dto.Description,
                PrimaryValue = dto.PrimaryValue,
                SecondaryValue = dto.SecondaryValue,
                IsOnSelf = dto.IsOnSelf
            };
        }
    }
}