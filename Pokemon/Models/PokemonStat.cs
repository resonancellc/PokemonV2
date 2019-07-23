namespace Pokemon.Models
{
    public class PokemonStats : IPokemonStats
    {
        public int Health { get; set; }

        public int Attack { get; set; }

        public int Defence { get; set; }

        public int SpecialAttack { get; set; }

        public int SpecialDefence { get; set; }

        public int Speed { get; set; }
    }
}
