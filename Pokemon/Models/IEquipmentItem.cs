namespace Pokemon
{
    public interface IEquipmentItem
    {
        int Cost { get; set; }

        string Description { get; set; }

        int ID { get; set; }

        string Name { get; set; }
    }
}