using Pokemon.Models;

namespace Pokemon.AdditionalEffects
{
    public class AlwaysSameDamage : IAdditionalEffect
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? PrimaryValue { get; set; }

        public int? SecondaryValue { get; set; }

        public bool IsOnSelf { get; set; }

        public bool IsBasedOnLevel() => PrimaryValue == 0;
    }
}
