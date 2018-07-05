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
        public static List<EquipmentItem> playerItems = new List<EquipmentItem>();

        static PlayerEquipment()
        {
            Money = 0;
        }

        public static void AddItem(EquipmentItem item)
        {
            playerItems.Add(item);
        }
    }
}
