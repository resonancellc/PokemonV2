using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Factory
{
    public static class PokemonAttacksFactory
    {
        public static IAttack CreateAttack()
        {
            return new Attack();
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
