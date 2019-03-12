using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Models
{
    public interface IPokemon : ICloneable
    {
        int ID { get; set; }
        string Name { get; set; }

        int HPCurrent { get; set; }
        int HPMax { get; set; }
        int Level { get; set; }

        IPokemonStats Stats { get; set; }
        List<IAttack> Attacks { get; set; }

        Condition Condition { get; set; }
        bool IsFlinched { get; set; }
        bool IsEnergyFocused { get; set; }
        bool IsConfused { get; set; }

        int PrimaryTypeID { get; set; }
        int? SecondaryTypeID { get; set; }
        int MinimalLevel { get; set; }
        int[] StatModifierStages { get; set; }

        bool IsPokemonAlive();
        bool Hurt(int value);
    }
}
