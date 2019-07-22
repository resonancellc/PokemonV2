namespace Pokemon.Models
{
    public class StatsChangeValidator
    {
        private readonly IPokemon _affectedPokemon;
        private readonly StatType _statType;
        private readonly int _stageValue;

        public StatsChangeValidator(IPokemon affectedPokemon, StatsBoost statsBoost)
        {
            _affectedPokemon = affectedPokemon;
            _statType = statsBoost.StatType;
            _stageValue = statsBoost.Value;
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
