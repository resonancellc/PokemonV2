using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokemon.Models;
using Pokemon.Calculators;

namespace Pokemon.AdditionalEffects
{
    public class StatusChanger : IAdditionalEffect
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? PrimaryValue { get; set; }
        public int? SecondaryValue { get; set; }
        public bool IsOnSelf { get; set; }

        public void ChangeStatus(IPokemon pokemon)
        {
            if (Name.Contains("Poison"))
            {
                if (ChanceCalculator.CalculateChance((int)PrimaryValue, 100))
                {
                    BattleLog.AppendText($"{pokemon.Name} is now poisoned");
                    pokemon.Condition = Condition.PSN;
                }
            }
            if (Name.Contains("Burn"))
            {
                if (ChanceCalculator.CalculateChance((int)PrimaryValue, 100))
                {
                    BattleLog.AppendText($"{pokemon.Name} is now burning");
                    pokemon.Condition = Condition.BRN;
                }
            }
            if (Name.Contains("Paralysis"))
            {
                if(pokemon.Condition != 0)
                {
                    BattleLog.AppendText($"{pokemon.Name} is unaffected");
                    return;
                }
                if (ChanceCalculator.CalculateChance((int)PrimaryValue, 100))
                {
                    BattleLog.AppendText($"{pokemon.Name} is now paralysed");
                    pokemon.Condition = Condition.PAR;
                }
            }
            if (Name.Contains("Sleep"))
            {
                if (pokemon.Condition != 0)
                {
                    BattleLog.AppendText($"{pokemon.Name} is unaffected");
                    return;
                }
                if (ChanceCalculator.CalculateChance((int)PrimaryValue, 100))
                {
                    BattleLog.AppendText($"{pokemon.Name} is now asleep");
                    pokemon.Condition = Condition.SLP;
                }
            }
            if (Name.Contains("Confusion"))
            {
                if (pokemon.Condition == Condition.SLP || pokemon.IsConfused)
                {
                    BattleLog.AppendText($"{pokemon.Name} is unaffected");
                    return;
                }
                if (ChanceCalculator.CalculateChance((int)PrimaryValue, 100))
                {
                    BattleLog.AppendText($"{pokemon.Name} is now confused");
                    pokemon.IsConfused = true;
                }
            }
            if (Name.Contains("Flinch"))
            {
                if (ChanceCalculator.CalculateChance((int)PrimaryValue, 100))
                {
                    pokemon.IsFlinched = true;
                }
            }
        }
    }
}
