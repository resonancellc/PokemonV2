using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class Pokemon
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int HPMax { get; set; }
        public int HPCurrent { get; set; }
        public Stat Stat { get; set; }

        public int PrimaryTypeID { get; set; }
        public int? SecondaryTypeID { get; set; }


        public Attack[] attackPool = new Attack[4];
        public int[] statModifierStages = { 0,0,0,0,0 }; 
 
        public Pokemon(int id, int level, Stat pokemonStat)
        {
            this.ID = id;
            this.Name = StaticTypes.GetPokemonNameByID(id);
            this.Level = level;
            this.Stat = pokemonStat;

            this.HPMax = pokemonStat.Health;
            this.HPCurrent = this.HPMax;

            this.PrimaryTypeID = pokemonStat.PrimaryTypeID;
            this.SecondaryTypeID = pokemonStat.SecondaryTypeID;

            SetPokemonAttackPool();
        }

        public void SetPokemonAttackPool()
        {
            List<Attack> attackList = new List<Attack>();
            foreach (DataRow row in StaticSQL.GetPokemonAttackPool(this.ID).Rows)
            {
                var values = row.ItemArray;
                Attack attack = new Attack()
                {
                    Level = (int)values[0],
                    Name = (string)values[1],
                    Power = StaticTypes.attackList.Where(x=>x.Name == (string)values[1]).FirstOrDefault().Power,
                    Accuracy = StaticTypes.attackList.Where(x => x.Name == (string)values[1]).FirstOrDefault().Accuracy,
                    BoostStats = StaticTypes.attackList.Where(x => x.Name == (string)values[1]).FirstOrDefault().BoostStats
                };

                if (attack.Level > this.Level) continue;
                attackList.Add(attack);
            }

            for (int i = 0; i < attackList.Count; i++)
            {
                attackPool[i] = attackList[i];
            }
        }

        public bool Hurt(int value)
        {
            HPCurrent -= value;
            return CheckIfPokemonAlive();
        }

        /// <summary>
        /// Returns True if pokemon is Alive
        /// </summary>
        /// <returns></returns>
        public bool CheckIfPokemonAlive()
        {
            return HPCurrent <= 0 ? false : true;
        }
    }
}
