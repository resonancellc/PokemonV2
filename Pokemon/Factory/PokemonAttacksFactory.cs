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
        public static List<IAttack> CreateAttacks()
        {
            IAttack attack = new Attack();

            return attack;
        }

        
    }
}
