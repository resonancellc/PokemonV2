using Pokemon.Models;
using System.Collections.Generic;

namespace Pokemon.Factory
{
    public static class EquipmentFactory
    {
        public static IEquipment CreateEquipment()
        {
            return new Equipment();
        }

        public static IEquipment CreateEquipment(int money, Dictionary<IEquipmentItem, int> equipmentList)
        {
            IEquipment equipment = new Equipment();
            equipment.Money = money;
            equipment.EquipmentList = equipmentList;

            return equipment;
        }
    }
}
