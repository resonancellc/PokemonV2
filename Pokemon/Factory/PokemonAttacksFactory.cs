using Pokemon.Models;
using System;
using System.Collections.Generic;
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
            return new Attack() 
            {
                Power = 40,
                Accuracy = 100,
                ElementalType = 0,
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
