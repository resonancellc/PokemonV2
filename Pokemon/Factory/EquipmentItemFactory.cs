using Pokemon.Models;

namespace Pokemon.Factory
{
    public static class EquipmentItemFactory
    {
        public static IEquipmentItem CreateItem(object[] values)
        {
            return new EquipmentItem()
            {
                ID = (int)values[0],
                Name = (string)values[1],
                Description = (string)values[2],
                Cost = (int)values[3]
            };
        }
    }
}
