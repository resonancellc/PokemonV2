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
        //public static List<EquipmentItem> playerItems = new List<EquipmentItem>();
        //public static Dictionary<string, int> playerItems = new Dictionary<string, int>();
        public static int[] playerItems = new int[StaticTypes.equipmentItemList.Count];


        static PlayerEquipment()
        {
            Money = 0;
            
        }

        public static void InitPlayerEquipment()
        {
            for (int i = 0; i < StaticTypes.equipmentItemList.Count; i++)
            {
                playerItems[i] = i*2;
            }
        }


    }
}
