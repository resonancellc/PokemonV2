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
        public static List<IdNameItem> pokemonList = new List<IdNameItem>();
        public static List<PokemonStat> pokemonStatList = new List<PokemonStat>();
        public static List<Attack> attackList = new List<Attack>();
        public static List<EquipmentItem> equipmentItemList = new List<EquipmentItem>();

        public static void FillItemList()
        {
            foreach (DataRow row in StaticSQL.GetItemList().Rows)
            {
                var values = row.ItemArray;
                EquipmentItem item = new EquipmentItem()
                {
                    ID = (int)values[0],
                    Name = (string)values[1],
                    Cost = (int)values[2],
                    Description = (string)values[3]
                };
                equipmentItemList.Add(item);
            }
        }

        public static void FillPokemonList()
        {
            foreach (DataRow row in StaticSQL.GetPokemonList().Rows)
            {
                var values = row.ItemArray;
                IdNameItem pokemon = new IdNameItem()
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
                    PrimaryTypeID = (int)values[8],
                    SecondaryTypeID = values[9] != DBNull.Value ? (int?)values[9] : null,
                    MinimalLevel = values[10] != DBNull.Value ? (int)values[10] : 1
                };
                pokemonStat.SetStatsArray((int)values[3], (int)values[4], (int)values[5], (int)values[6], (int)values[7]);
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
                    BoostStats = values[4] != DBNull.Value ? (string)values[4].ToString().Trim(' ') : string.Empty,
                    TypeID = values[5] != DBNull.Value ? (int?)values[5] : null,
                    IsSpecial = values[6] != DBNull.Value ? (bool)values[6] : false,
                    AdditionalEffect = values[7] != DBNull.Value ? (string)values[7].ToString().Trim(' ') : string.Empty
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
