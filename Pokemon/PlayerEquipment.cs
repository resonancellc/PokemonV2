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

        static PlayerEquipment()
        {
            Money = 0;
        }
    }
}
