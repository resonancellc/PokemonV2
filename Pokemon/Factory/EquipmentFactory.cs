using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokemon.Models;
namespace Pokemon.Factory
{
    public static class EquipmentFactory
    {
        public static IEquipment CreateEquipment()
        {
            IEquipment equipment = new Equipment();
            equipment.Money = 100;
            equipment.EquipmentList = new Dictionary<IEquipmentItem, int>();

            foreach (IEquipmentItem item in ItemsList.Items.Values)
            {
                equipment.EquipmentList.Add(item,4);
            }

            return equipment;
        }
    }
}
