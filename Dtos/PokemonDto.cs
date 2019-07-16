using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
