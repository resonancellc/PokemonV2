using Dtos;
using Pokemon.Models;
using System.Linq;

namespace Pokemon.ObjectMappers
{
    public static class AttackMapper
    {
        public static IAttack ToDomainObject(this AttackDto dto)
        {
            return new Attack
            {
                Accuracy = dto.Accuracy,
                AdditionalEffects = dto.AdditionalEffects.Select(x => x.ToDomainObject()).ToList(),
                BoostStats = dto.BoostStats,
                ElementalType = (ElementalType)dto.ElementalType,
                ID = dto.ID,
                IsSpecial = dto.IsSpecial,
                Level = dto.Level,
                Name = dto.Name,
                Power = dto.Power
            };
        }
    }
}
