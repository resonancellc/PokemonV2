using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Factory
{
    public static class EquipmentItemFactory
    {
        public static IEquipmentItem CreateItem(object[] values)
        {
            IEquipmentItem item = new EquipmentItem();

            item.ID = (int)values[0];
            item.Name = (string)values[1];
            item.Description = (string)values[2];
            item.Cost = (int)values[3];

            return item;
        }
    }
}
