using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Models
{
    public class StatsChangeValidator
    {
        private readonly IPokemon _affectedPokemon;
        private readonly StatType _statType;
        private readonly int _stageValue;

        public StatsChangeValidator(StatsChange statsChange)
        {
            _affectedPokemon = statsChange.AffectedPokemon;
            _statType = statsChange.StatType;
            _stageValue = statsChange.StageValue;
        }

        public bool StatChangePossible()
        {
            bool result = true;

            if (IsNewStatValueTooHigh() || IsNewStatValueTooLow())
            {
                result = false;
            }
            return result;
        }

        private bool IsNewStatValueTooHigh() => _affectedPokemon.StatModifierStages[(int)_statType] + _stageValue > 6;

        private bool IsNewStatValueTooLow() => _affectedPokemon.StatModifierStages[(int)_statType] - _stageValue < -6;
    }
}
