using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class ItemHelper
    {
        public static bool CanUseItem(IPokemon pokemon, int itemId)
        {
            switch (itemId)
            {
                case 0: // Potion
                    if (pokemon.HPCurrent <= 0) return false;
                    if (pokemon.HPCurrent == pokemon.HPMax) return false;

                    UseItem(pokemon, itemId);
                    return true;
                case 1: // FullHeal
                    if (pokemon.Condition == 0) return false;
                    UseItem(pokemon, itemId);
                    return true;
                case 2: // Super Potion
                    if (pokemon.HPCurrent <= 0) return false;
                    if (pokemon.HPCurrent == pokemon.HPMax) return false;

                    UseItem(pokemon, itemId);
                    return true;
                case 3: // AttackX
                    if (pokemon.StatModifierStages[0] > 4) return false;

                    UseItem(pokemon, itemId);
                    return true;
                default:
                    return false;
            }
        }

        public static void UseItem(IPokemon pokemon, int itemId)
        {
            //Equipment.playerItems[itemId]--;
            switch (itemId)
            {
                case 0: //potion
                    pokemon.Heal(20);
                    break;
                case 1: // FullHeal
                    pokemon.Condition = 0;
                    break;
                case 2: // super potion
                    pokemon.Heal(50);
                    break;
                case 3: // Attack X
                    pokemon.StatModifierStages[0] += 2;      
                    break;
            }
        }

        public static string GetItemNameByID(int ID)
        {
           return ItemsList.Items.Where(i => i.Key == ID).First().Value.Name;
        }


    }
}
