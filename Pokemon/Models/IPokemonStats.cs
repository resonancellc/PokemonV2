namespace Pokemon.Models
{
    public interface IPokemonStats
    {
        int Attack { get; set; }

        int Defence { get; set; }

        int Health { get; set; }

        int SpecialAttack { get; set; }

        int SpecialDefence { get; set; }

        int Speed { get; set; }
    }
}
