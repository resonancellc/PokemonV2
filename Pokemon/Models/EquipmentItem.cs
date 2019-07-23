namespace Pokemon.Models
{
    public class EquipmentItem : IEquipmentItem
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Cost { get; set; }
    }
}
