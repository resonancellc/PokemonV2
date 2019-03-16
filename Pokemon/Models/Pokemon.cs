using Pokemon.Factory;
using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
        public IList<IAttack> Attacks { get; set; }

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
            StatModifierStages = new int[5] { 0,0,0,0,0};
        }

        public bool Hurt(int value)
        {
            HPCurrent -= value;
            return IsPokemonAlive();
        }

        public bool IsPokemonAlive()
        {
            if (HPCurrent > 0) return true;  
            return false;
        }

        public void ResetStats()
        {
            //for (int i = 0; i < Stat.Stats.Length; i++)
            //{
            //    this.statModifierStages[i] = 0;
            //    this.Stat.Stats[i] = this.StartStats.Stats[i];
            //}
            //IsConfused = false;
            //IsFlinched = false;
            //IsEnergyFocused = false;
        }

        public object Clone()
        {
            IPokemon pokemon = PokemonFactory.CreatePokemon();

            foreach (PropertyInfo property in pokemon.GetType().GetProperties())
            {
                property.SetValue(pokemon, property.GetValue(this, null), null);
            }

            return pokemon;
        }
    }
}
