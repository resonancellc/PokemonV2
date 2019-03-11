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
        public static int[] playerItems = new int[ItemsList.Items.Count];

        public static void InitPlayerEquipment()
        {
            Money = 100;
            for (int i = 0; i < ItemsList.Items.Count; i++)
            {
                playerItems[i] = 0;
            }
        }
    }
}
