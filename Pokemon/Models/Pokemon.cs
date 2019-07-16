using Pokemon.Factory;
using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    [DataContract]
    public class Pokemon : IPokemon
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int HPCurrent { get; set; }

        [DataMember]
        public int HPMax { get; set; }

        [DataMember]
        public int Level { get; set; }

        [DataMember]
        public IPokemonStats Stats { get; set; }

        [DataMember]
        public IList<IAttack> Attacks { get; set; }

        [DataMember]
        public Condition Condition { get; set; }

        public bool IsFlinched { get; set; }

        public bool IsEnergyFocused { get; set; }

        public bool IsConfused { get; set; }

        [DataMember]
        public int PrimaryTypeID { get; set; }

        [DataMember]
        public int? SecondaryTypeID { get; set; }

        public int[] StatModifierStages { get; set; }

        public int MinimalLevel { get; set; }

        public Pokemon()
        {
            StatModifierStages = new int[5] { 0,0,0,0,0};
        }

        public void Hurt(int value)
        {
            HPCurrent -= value;
        }
        public void Heal(int value)
        {
            if (HPCurrent + value > HPMax)
                HPCurrent = HPMax;
            else
                HPCurrent += value;
        }

        public bool IsPokemonAlive()
        {
            if (HPCurrent > 0) return true;  
            return false;
        }

        public void ResetStats()
        {
            for (int i = 0; i < StatModifierStages.Length; i++)
            {
                StatModifierStages[i] = 0;
            }

            IsConfused = false;
            IsFlinched = false;
            IsEnergyFocused = false;
        }

        public object Clone()
        {
            IPokemon pokemon = PokemonFactory.CreatePokemon();

            foreach (PropertyInfo property in pokemon.GetType().GetProperties())
            {
                property.SetValue(pokemon, property.GetValue(this, null), null);
            }
            pokemon.ResetStats();
            return pokemon;
        }
    }
}
