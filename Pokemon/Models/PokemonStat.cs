using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class Stat
    {
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefence { get; set; }
        public int Speed { get; set; }
        public int Health { get; set; }
        public int PrimaryTypeID { get; set; }
        public int? SecondaryTypeID { get; set; }

        public Stat()
        {

        }

        public Stat(Stat stat)
        {
            this.Attack = stat.Attack;
            this.Defence = stat.Defence;
            this.SpecialAttack = stat.SpecialAttack;
            this.SpecialDefence = stat.SpecialDefence;
            this.Speed = stat.Speed;
        }
    }

    public class PokemonStat : Stat
    {
        public string Name { get; set; }
        public int ID { get; set; }
        
    }



}
