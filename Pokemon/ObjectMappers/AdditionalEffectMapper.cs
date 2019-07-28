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
                PrimaryParameter = dto.PrimaryValue,
                SecondaryParameter = dto.SecondaryValue,
                IsOnSelf = dto.IsOnSelf
            };
        }

        public static IAdditionalEffect ToDomainObject(this AdditionalEffect additionalEffect)
        {
            return new AdditionalEffect
            {
                ID = additionalEffect.ID,
                Name = additionalEffect.Name,
                Description = additionalEffect.Description,
                PrimaryParameter = additionalEffect.PrimaryParameter,
                SecondaryParameter = additionalEffect.SecondaryParameter,
                IsOnSelf = additionalEffect.IsOnSelf
            };
        }

        public static void SetAdditionalEffectProperties(this IAdditionalEffect additionalEffect, IAdditionalEffect objectToBeMapped)
        {
            additionalEffect.ID = objectToBeMapped.ID;
            additionalEffect.Name = objectToBeMapped.Name;
            additionalEffect.Description = objectToBeMapped.Description;
            additionalEffect.PrimaryParameter = objectToBeMapped.PrimaryParameter;
            additionalEffect.SecondaryParameter = objectToBeMapped.SecondaryParameter;
            additionalEffect.IsOnSelf = objectToBeMapped.IsOnSelf;
        }
    }
}