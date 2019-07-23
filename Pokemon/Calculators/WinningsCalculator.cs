using Pokemon.Models;
using System;
using System.Linq;

namespace Pokemon.Calculators
{
    public static class WinningsCalculator
    {
        public static int CalculateWinnings(IPokemonParty<IPokemon> pokemonParty, IEquipment equipment)
        {
            float sum = 0;
            int winnings = 0;

            foreach (var pokemon in pokemonParty)
            {
                if (pokemon == null) break;
                sum += pokemon.HPCurrent / pokemon.HPMax;
            }

            winnings = Convert.ToInt32(sum * 100 / pokemonParty.Count());
            equipment.ChangeMoneyQuantity(winnings);
            return winnings;
        }
    }
}
