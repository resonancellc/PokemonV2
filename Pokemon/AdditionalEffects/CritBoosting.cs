using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokemon.Models;

namespace Pokemon.AdditionalEffects
{
    public class CritBoosting : IAdditionalEffect
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? PrimaryValue { get; set; }
        public int? SecondaryValue { get; set; }
        public bool IsOnSelf { get; set; }

        public void SetPokemonFocus(IPokemon pokemon)
        {
            if (pokemon.IsEnergyFocused) BattleLog.AppendText($"{pokemon.Name} is already focused");
            else pokemon.IsEnergyFocused = true;
        }
    }
}
