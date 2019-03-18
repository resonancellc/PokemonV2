using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokemon.Models;

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
            if (pokemon.Condition != 0) 
            {
                BattleLog.AppendText($"{pokemon.Name} is unaffected");
                return;
            }

            if (Name.Contains("Poison"))
            {
                if (CalculatorHelper.ChanceCalculator((int)PrimaryValue, 100))
                {
                    BattleLog.AppendText($"{pokemon.Name} is now poisoned");
                    pokemon.Condition = Condition.PSN;
                }
            }
            if (Name.Contains("Burn"))
            {
                if (CalculatorHelper.ChanceCalculator((int)PrimaryValue, 100))
                {
                    BattleLog.AppendText($"{pokemon.Name} is now burning");
                    pokemon.Condition = Condition.BRN;
                }
            }
            if (Name.Contains("Paralysis"))
            {
                if (CalculatorHelper.ChanceCalculator((int)PrimaryValue, 100))
                {
                    BattleLog.AppendText($"{pokemon.Name} is now paralysed");
                    pokemon.Condition = Condition.PAR;
                }
            }
            if (Name.Contains("Sleep"))
            {
                if (CalculatorHelper.ChanceCalculator((int)PrimaryValue, 100))
                {
                    BattleLog.AppendText($"{pokemon.Name} is now asleep");
                    pokemon.Condition = Condition.SLP;
                }
            }

            //if (attributes.Contains("burn"))
            //{
            //    if (targetPokemon.Condition != 0) BattleLog.AppendText($"{targetPokemon.Name} is unaffected");
            //    else if (IsConditionChanged(targetPokemon, (int)Condition.BRN, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100))
            //    {
            //        BattleLog.AppendText($"{targetPokemon.Name} is now burning");
            //        targetPokemon.Stats.Attack = targetPokemon.Stats.Attack / 2;
            //        return;
            //    }
            //}
            //if (attributes.Contains("freeze"))
            //{
            //    if (targetPokemon.Condition != 0) BattleLog.AppendText($"{targetPokemon.Name} is unaffected");
            //    else if (IsConditionChanged(targetPokemon, (int)Condition.FRZ, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100))
            //    {
            //        BattleLog.AppendText($"{targetPokemon.Name} is now frozen");
            //        return;
            //    }
            //}
            //if (attributes.Contains("paralysis"))
            //{
            //    if (targetPokemon.Condition != 0) BattleLog.AppendText($"{targetPokemon.Name} is unaffected");
            //    else if (IsConditionChanged(targetPokemon, (int)Condition.PAR, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100))
            //    {
            //        BattleLog.AppendText($"{targetPokemon.Name} is now paralyzed");
            //        targetPokemon.Stats.Speed = Convert.ToInt32((float)targetPokemon.Stats.Speed / 1.5f);
            //        return;
            //    }
            //}
            //if (attributes.Contains("poison"))
            //{
            //    if (targetPokemon.Condition != 0) BattleLog.AppendText($"{targetPokemon.Name} is unaffected");
            //    else if (IsConditionChanged(targetPokemon, (int)Condition.PSN, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100))
            //    {
            //        BattleLog.AppendText($"{targetPokemon.Name} is now poisoned");
            //        return;
            //    }
            //}
            //if (attributes.Contains("sleep"))
            //{
            //    if (targetPokemon.Condition != 0) BattleLog.AppendText($"{targetPokemon.Name} is unaffected");
            //    else if (IsConditionChanged(targetPokemon, (int)Condition.SLP, attributes.Length > 2 ? Convert.ToInt32(attributes[2]) : 100))
            //    {
            //        BattleLog.AppendText($"{targetPokemon.Name} is now sleeping");
            //        return;
            //    }
            //}
            //if (attributes.Contains("confusion"))
            //{
            //    if (targetPokemon.IsConfused) BattleLog.AppendText($"{targetPokemon.Name} is already confused");
            //    else if (attributes.Length > 2)
            //    {
            //        if (CalculatorHelper.ChanceCalculator(Convert.ToInt32(attributes[2]), 100))
            //        {
            //            targetPokemon.IsConfused = true;
            //            BattleLog.AppendText($"{targetPokemon.Name} is now confused");
            //            return;
            //        }
            //    }
            //}
        }


    }
}
