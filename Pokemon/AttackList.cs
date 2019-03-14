using Pokemon.Models;
using Pokemon.Factory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class AttackList
    {
        public static Dictionary<int, IAttack> Attacks = new Dictionary<int, IAttack>();

        public static void FillAttackList()
        {
            DataRowCollection attackDataRows = StaticSQL.GetAttacks().Rows;
            foreach (DataRow attackRow in attackDataRows)
            {
                var values = attackRow.ItemArray;
                IAttack attack = PokemonAttacksFactory.CreateAttack();

                attack.ID = (int)values[0];
                attack.Name = (string)values[1];
                attack.Power = values[2] != DBNull.Value ? (int?)values[2] : null;
                attack.Accuracy = (int)values[3];
                attack.BoostStats = values[4] != DBNull.Value ? (string)values[4] : "";
                attack.ElementalType = values[5] != DBNull.Value ? (ElementalType)values[5] : 0;
                attack.IsSpecial = (bool)values[6];
                attack.AdditionalEffects = AdditionalEffectFactory.GetAdditionalEffects(values[7] != DBNull.Value ? (string)values[7] : "");

                Attacks.Add(attack.ID, attack);
            }
        }

    }
}
