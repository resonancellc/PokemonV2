using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Pokemon.Factory
{
    public static class PokemonAttacksFactory
    {
        public static IAttack CreateAttack()
        {
            return new Attack();
        }

        public static IAttack CreateAttack(string attackName)
        {
            var attackData = StaticSQL.GetAttackByName(attackName).Rows[0];

            return new Attack() 
            {
                ID = (int)attackData[0],
                Name = (string)attackData[1],
                Power = attackData[2] != DBNull.Value ? (int?)attackData[2] : null,
                Accuracy = (int)attackData[3],
                BoostStats = attackData[4] != DBNull.Value ? (string)attackData[4] : "",
                TypeID = attackData[5] != DBNull.Value ? (ElementalType)attackData[5] : 0,
                IsSpecial = attackData[6] != DBNull.Value ? (bool)attackData[6] : false
            };
        }

        public static IList<IAttack> CreateAttacks()
        {
            return new List<IAttack>();
        }

        public static IList<IAttack> GetAttacks(IPokemon pokemon)
        {
            return pokemon.Attacks.Where(a => a.Level <= pokemon.Level).OrderByDescending(a => a.Level).Take(4).ToList(); ;
        }        
    }
}