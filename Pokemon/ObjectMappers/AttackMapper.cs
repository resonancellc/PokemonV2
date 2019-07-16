using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.ObjectMappers
{
    public static class AttackMapper
    {
        public static Attack ToDomainObject(this AttackDto dto)
        {
            return new Attack
            {
                Accuracy = dto.Accuracy,
                //AdditionalEffects = dto.AdditionalEffects,
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
