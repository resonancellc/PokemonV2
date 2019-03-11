using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class Pokemon : IPokemon
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int HPCurrent { get; set; }
        public int HPMax { get; set; }
        public int Level { get; set; }

        public IPokemonStats Stats { get; set; }
        public List<IAttack> Attacks { get; set; }

        public Condition Condition { get; set; }
        public bool IsFlinched { get; set; }
        public bool IsEnergyFocused { get; set; }
        public bool IsConfused { get; set; }

        public int PrimaryTypeID { get; set; }
        public int? SecondaryTypeID { get; set; }

        public int[] StatModifierStages { get; set; }
        public int MinimalLevel { get; set; }

        public Pokemon()
        {

        }

        public Pokemon(int id, int level, Stat pokemonStat)
        {
            this.ID = id;
            this.Name = StaticTypes.GetPokemonNameByID(id);
            this.Level = level;

            this.Stat = pokemonStat;
            Stat stat = new Stat(pokemonStat);
            this.StartStats = stat;

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
                    BoostStats = StaticTypes.attackList.Where(x => x.Name == (string)values[1]).FirstOrDefault().BoostStats,
                    TypeID = StaticTypes.attackList.Where(x => x.Name == (string)values[1]).FirstOrDefault().TypeID,
                    IsSpecial = StaticTypes.attackList.Where(x => x.Name == (string)values[1]).FirstOrDefault().IsSpecial,
                    AdditionalEffect = StaticTypes.attackList.Where(x => x.Name == (string)values[1]).FirstOrDefault().AdditionalEffect
                };

                if (attack.Level > this.Level) continue;
                attackList.Add(attack);
            }

            for (int i = 0; i < attackList.Count; i++)
            {
                int offset = 0;

                if (attackList.Count > 4) offset = attackList.Count - 4; // przesunięcie atakow

#warning moze przeniesc to do parametrow for'a?
                if (i >= 4) break; 

                attackPool[i] = attackList[i+offset];
            }
        }

        public bool Hurt(int value)
        {
            HPCurrent -= value;
            return CheckIfPokemonAlive();
        }

        public bool CheckIfPokemonAlive()
        {
            if (HPCurrent > 0) return true;  
            return false;
        }

        public void ResetStats()
        {
            for (int i = 0; i < Stat.Stats.Length; i++)
            {
                this.statModifierStages[i] = 0;
                this.Stat.Stats[i] = this.StartStats.Stats[i];
            }
            IsConfused = false;
            IsFlinched = false;
            IsEnergyFocused = false;
        }
    }
}
