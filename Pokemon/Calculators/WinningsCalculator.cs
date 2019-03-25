using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Calculators
{
    public static class WinningsCalculator
    {
        public static int CalculateWinnings(IPokemonParty<IPokemon> pokemonParty, IEquipment equipment)
        {
            float sum = 0;
            int winnings = 0;

            foreach (Pokemon pokemon in pokemonParty)
            {
                if (pokemon == null) break;
                sum += (float)pokemon.HPCurrent / (float)pokemon.HPMax;
            }

            winnings = Convert.ToInt32(sum * 100 / pokemonParty.Count());
            equipment.ChangeMoneyQuantity(winnings);
            return winnings;
        }
    }
}
