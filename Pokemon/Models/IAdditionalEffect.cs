using System.Collections.Generic;

namespace Pokemon.Models
{
    public interface IAdditionalEffect
    {
        int ID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int? PrimaryValue { get; set; }
        int? SecondaryValue { get; set; }
        bool IsOnSelf { get; set; }
    }
}