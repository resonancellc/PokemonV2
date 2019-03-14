using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    static class StaticSQL
    {
        public static string ConnectionString { get; set; }

        public static void SetConnectionString(string conn)
        {
            ConnectionString = conn;
        }

        private static DataTable ExecuteSQLQuery(string query)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds);
                            DataTable data = ds.Tables[0];
                            return data;
                        }
                    }
                }
            }
        }

        public static DataTable GetPokemons()
        {
            return ExecuteSQLQuery(@"SELECT 
                                        Pokemon.ID, 
                                        Pokemon.Name, 
                                        BaseStats.Health, 
                                        BaseStats.Attack,
                                        BaseStats.Defence,
                                        BaseStats.SpecialAttack,
                                        BaseStats.SpecialDefence,
                                        BaseStats.Speed,
                                        BaseStats.PrimaryTypeID,
                                        BaseStats.SecondaryTypeID,
                                        BaseStats.MinimalLevel
                                     FROM Pokemon 
                                     INNER JOIN BaseStats ON Pokemon.ID = BaseStats.ID 
                                     ORDER BY Pokemon.ID ASC");
        }

        public static DataTable GetAttacks()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT 
                                                            Attack.ID,
                                                            Attack.[Name],
                                                            Attack.[Power],
                                                            Attack.Accuracy,
                                                            Attack.BoostStats,
                                                            Attack.TypeID,
                                                            Attack.IsSpecial,
                                                            Attack.AdditionalEffect
                                                         FROM Attack", con))
                {

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds);
                            DataTable data = ds.Tables[0];
                            return data;
                        }
                    }
                }
            }
        }

        public static DataTable GetAdditionalEffects()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT 
                                                               [ID]
                                                              ,[Name]
                                                              ,[Description]
                                                              ,[PrimaryParameter]
                                                              ,[SecondaryParameter]
                                                              ,[IsOnSelf]
                                                          FROM [Pokemon].[dbo].[AdditionalEffects]", con))
                {

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds);
                            DataTable data = ds.Tables[0];
                            return data;
                        }
                    }
                }
            }
        }

        public static DataTable GetPokemonAttacks(int pokemonID)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT 
                                                            Attack.ID,
                                                            Attack.[Name],
                                                            Attack.[Power],
                                                            Attack.Accuracy,
                                                            Attack.BoostStats,
                                                            Attack.TypeID,
                                                            Attack.IsSpecial,
                                                            Attack.AdditionalEffectIDs,
                                                            AttackPool.[Level]
                                                         FROM AttackPool
                                                         INNER JOIN Attack ON AttackPool.AttackID = Attack.ID 
                                                         WHERE PokemonID = @ID", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", pokemonID);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds);
                            DataTable data = ds.Tables[0];
                            return data;
                        }
                    }
                }
            }
        }

        public static DataTable GetItemList()
        {
            return ExecuteSQLQuery("SELECT ID, Name, Description, Cost FROM Items");
        }
    }
}
