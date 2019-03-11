using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public enum ElementalType
    {
        Normal = 0,
        Fire = 1,
        Water = 2,
        Electric = 3,
        Grass = 4,
        Ice = 5,
        Fighting = 6,
        Poison = 7,
        Ground = 8,
        Flying = 9,
        Psychic = 10,
        Bug = 11,
        Rock = 12,
        Ghost = 13,
        Dragon = 14,
    }

    public enum StatType
    {
        Attack = 0,
        Defence = 1,
        SpecialAttack = 2,
        SpecialDefence = 3,
        Speed = 4
    }

    public enum Condition
    {
        None = 0,
        BRN = 1,
        FRZ = 2,
        PAR = 3,
        PSN = 4,
        SLP = 5
    }
}
