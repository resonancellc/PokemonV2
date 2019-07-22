using Pokemon.Models;
using System.Linq;

namespace Pokemon
{
    public static class ItemHelper
    {
        public static bool CanUseItem(IPokemon pokemon, int itemId)
        {
            switch (itemId)
            {
                case 1: // Potion
                    if (pokemon.HPCurrent <= 0) return false;
                    if (pokemon.HPCurrent == pokemon.HPMax) return false;
                    return true;
                case 2: // FullHeal
                    if (pokemon.Condition == 0) return false;
                    return true;
                case 3: // Super Potion
                    if (pokemon.HPCurrent <= 0) return false;
                    if (pokemon.HPCurrent == pokemon.HPMax) return false;
                    return true;
                case 4: // AttackX
                    if (pokemon.StatModifierStages[0] > 4) return false;
                    return true;
                default:
                    return false;
            }
        }

        public static void UseItem(IPokemon pokemon, int itemId)
        {
            switch (itemId)
            {
                case 1: //potion
                    pokemon.Heal(20);
                    break;
                case 2: // FullHeal
                    pokemon.Condition = 0;
                    break;
                case 3: // super potion
                    pokemon.Heal(50);
                    break;
                case 4: // Attack X
                    pokemon.StatModifierStages[0] += 2;      
                    break;
            }
        }

        public static string GetItemNameByID(int ID) => ItemsList.Items.Where(i => i.Key == ID).First().Value.Name;
    }
}
