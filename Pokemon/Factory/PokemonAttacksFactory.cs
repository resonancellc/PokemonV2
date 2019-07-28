using Pokemon.Models;
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
            return StaticSQL.GetAttackByName(attackName);
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