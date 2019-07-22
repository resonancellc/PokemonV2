using Pokemon.Models;
using System;

namespace Pokemon.ObjectMappers
{
    public static class StatsBoostMapper
    {
        public static StatsBoost ToDomainObject(string boostString)
        {
            string[] splittedBoosts = boostString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            StatsBoost boost = new StatsBoost()
            {
                Target = splittedBoosts[0] == "self" ? Target.Self : Target.Enemy,
                StatType = (StatType)Int32.Parse(splittedBoosts[1]),
                Value = Int32.Parse(splittedBoosts[2])
            };
            return boost;
        }
    }
}
