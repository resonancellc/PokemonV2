using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Factory
{
    public static class EquipmentItemFactory
    {
        public static IEquipmentItem CreateItem()
        {
            return new EquipmentItem();
        }
    }
}
