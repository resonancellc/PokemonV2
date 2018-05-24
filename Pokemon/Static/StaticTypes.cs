using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Globalization;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Pokemon
{
    public class StaticTypes
    {
        public static List<Item> pokemonList = new List<Item>();
        public static List<PokemonStat> pokemonStatList = new List<PokemonStat>();
        public static List<Attack> attackList = new List<Attack>();

        public static void FillPokemonList()
        {
            foreach (DataRow row in StaticSQL.GetPokemonList().Rows)
            {
                var values = row.ItemArray;
                Item pokemon = new Item()
                {
                    ID = (int)values[0],
                    Name = (string)values[1]
                };
                pokemonList.Add(pokemon);
            }
        }

        public static void FillPokemonStatsList()
        {
            foreach (DataRow row in StaticSQL.GetPokemonStatList().Rows)
            {
                var values = row.ItemArray;
                PokemonStat pokemonStat = new PokemonStat()
                {
                    ID = (int)values[0],
                    Name = (string)values[1],
                    Health = (int)values[2],
                    Attack = (int)values[3],
                    Defence = (int)values[4],
                    SpecialAttack = (int)values[5],
                    SpecialDefence = (int)values[6],
                    Speed = (int)values[7],
                    PrimaryTypeID = (int)values[8],
                    SecondaryTypeID = values[9] != DBNull.Value ? (int?)values[9] : null
                };
                pokemonStatList.Add(pokemonStat);
            }
        }

        public static void FillAttackList()
        {
            foreach (DataRow row in StaticSQL.GetPokemonAttackList().Rows)
            {
                var values = row.ItemArray;
                Attack attack = new Attack()
                {
                    ID = (int)values[0],
                    Name = (string)values[1],
                    Power = values[2] != DBNull.Value ? (int?)values[2] : null,
                    Accuracy = values[3] != DBNull.Value ? (int?)values[3] : null,
                    BoostStats = values[4] != DBNull.Value ? (string)values[4] : string.Empty,
                    TypeID = values[5] != DBNull.Value ? (int?)values[5] : null
                };
                attackList.Add(attack);
            }
        }

        public static string GetPokemonNameByID(int id)
        {
            return pokemonList.Where(p => p.ID == id).FirstOrDefault().Name;
        }

        public static Stat GetPokeStatsByID(int id)
        {
            return pokemonStatList.Where(p => p.ID == id).FirstOrDefault();
        }



    }
}
