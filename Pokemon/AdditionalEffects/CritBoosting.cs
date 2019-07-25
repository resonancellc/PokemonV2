using Pokemon.Models;

namespace Pokemon.AdditionalEffects
{
    public class CritBoosting : IAdditionalEffect
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? PrimaryParameter { get; set; }

        public int? SecondaryParameter { get; set; }

        public bool IsOnSelf { get; set; }

        public void SetPokemonFocus(IPokemon pokemon)
        {
            if (pokemon.IsEnergyFocused)
            {
                BattleLog.AppendText($"{pokemon.Name} is already focused");
            }
            else
            {
                pokemon.IsEnergyFocused = true;
            }
        }
    }
}
