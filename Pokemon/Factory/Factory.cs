using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class Factory
    {
        public static IPokemon CreatePokemon()
        {
            return new Pokemon();
        }

        public static IAttack CreateAttack()
        {
            return new Attack();
        }

        public static IEquipmentItem CreateItem()
        {
            return new EquipmentItem();
        }
    }
}
