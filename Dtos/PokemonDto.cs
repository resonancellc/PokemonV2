using System.Collections.Generic;

namespace Dtos
{
    public class PokemonDto
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int HPCurrent { get; set; }

        public int HPMax { get; set; }

        public int Level { get; set; }

        public StatsDto Stats { get; set; }

        public IList<AttackDto> Attacks { get; set; }

        public int Condition { get; set; }

        public int PrimaryTypeID { get; set; }

        public int? SecondaryTypeID { get; set; }
    }
}