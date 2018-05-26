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
        public int[] Stats = new int[5];
        public int Health { get; set; }
        public int PrimaryTypeID { get; set; }
        public int? SecondaryTypeID { get; set; }

        public Stat()
        {

        }

        public Stat(Stat stat)
        {
            this.Stats[0] = stat.Stats[0];
            this.Stats[1] = stat.Stats[1];
            this.Stats[2] = stat.Stats[2];
            this.Stats[3] = stat.Stats[3];
            this.Stats[4] = stat.Stats[4];
        }
    }

    public class PokemonStat : Stat
    {
        public string Name { get; set; }
        public int ID { get; set; }
        
        public void SetStatsArray(int attack, int defence, int specialAttack, int specialDefence, int speed)
        {
            this.Stats[0] = attack;
            this.Stats[1] = defence;
            this.Stats[2] = specialAttack;
            this.Stats[3] = specialDefence;
            this.Stats[4] = speed;
        }
    }



}
