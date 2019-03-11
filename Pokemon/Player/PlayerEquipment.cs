using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class PlayerEquipment
    {
        public static int Money { get; set; }
        public static int[] playerItems = new int[StaticTypes.equipmentItemList.Count];

        public static void InitPlayerEquipment()
        {
            Money = 100;
            for (int i = 0; i < StaticTypes.equipmentItemList.Count; i++)
            {
                playerItems[i] = 0;
            }
        }
    }
}
