using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class ItemHelper
    {
        public static bool UseItem(Pokemon pokemon, int itemId)
        {
            switch (itemId)
            {
                case 0: //potion
                    if (pokemon.HPCurrent <= 0) return false;
                    if (pokemon.HPCurrent == pokemon.HPMax) return false;

                    if (pokemon.HPCurrent + 20 > pokemon.HPMax)
                        pokemon.HPCurrent = pokemon.HPMax;
                    else
                        pokemon.HPCurrent += 20;
                    PlayerEquipment.playerItems[0]--;
                    return true;

                case 1: // FullHeal
                    if (pokemon.Condition == 0) return false;
                    pokemon.Condition = 0;
                    PlayerEquipment.playerItems[1]--; return true;

                case 2:
                    if (pokemon.HPCurrent == pokemon.HPMax) return false;

                    if (pokemon.HPCurrent + 50 > pokemon.HPMax)
                        pokemon.HPCurrent = pokemon.HPMax;
                    else
                        pokemon.HPCurrent += 50;
                    PlayerEquipment.playerItems[2]--;
                    return true;
                case 3: // Attack X
                    return BattleHelper.ChangeTempPokemonStats(pokemon, 0, 2) ? true : false;
                default:
                    return false;
            }
        }

        public static string GetItemNameByID(int ID)
        {
           return StaticTypes.equipmentItemList.Where(i => i.ID == ID).First().Name;
        }


    }
}
