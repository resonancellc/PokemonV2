namespace Pokemon.Models
{
    public interface IAdditionalEffect
    {
        int ID { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        int? PrimaryParameter { get; set; }

        int? SecondaryParameter { get; set; }

        bool IsOnSelf { get; set; }
    }
}