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

    public enum AdditionalEffectEnum
    {
        SameDamageLow = 1,
        SameDamageHigh = 2,
        SameDamageLevel = 3,
        DrainLife = 10,
        LeechLife = 19,
        Fast = 20,
        AlwaysHits = 21,
        HighCriticalChance = 50,
        BoostCriticalSelf = 51,
        BoostCriticalTarget = 52,
        ChargeLow = 60,
        ChargeHigh = 61,
        RechargeLow = 69,
        RechargeHigh = 68,
        TwoToFiveHits = 70,
        SwapPokemonMax = 84,
        PoisonWeak = 1001,
        PoisonMid = 1002,
        PoisonHigh = 1003,
        PoisonMax = 1004,
        BurnWeak = 1010,
        BurnMaxSelf = 1019,
        ParalysisWeak = 1020,
        ParalysisMax = 1024,
        ParalysisMaxWeak = 1028,
        ParalysisMaxSelf = 1029,
        SleepMax = 1054,
        SleepMaxSelf = 1059,
        FlinchWeak = 1060,
        ConfusionWeak = 1070,
        ConfusionMax = 1074
    }

    public class StringEnums
    {
        public const string
        SameDamage = "Same Damage",
        AlwaysHits = "Always Hits",
        FastAttack = "Fast";
    }

    public enum LevelValidatorResult
    {
        OK,
        NotInRange,
        InvalidFormat
    }
}
