﻿using System;
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

        private static DataTable ExecureSQLQuery(string query)
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

        public static DataTable GetPokemonList()
        {
            return ExecureSQLQuery("SELECT ID, Name FROM Pokemon");
        }

        public static DataTable GetPokemonStatList()
        {
            return ExecureSQLQuery(@"SELECT Pokemon.ID, Pokemon.Name, BaseStats.Health, BaseStats.Attack,BaseStats.Defence,BaseStats.SpecialAttack,BaseStats.SpecialDefence,BaseStats.Speed 
                                     FROM Pokemon 
                                     INNER JOIN BaseStats ON Pokemon.ID = BaseStats.ID 
                                     ORDER BY Pokemon.ID ASC");
        }

        public static DataTable GetPokemonAttackList()
        {
            return ExecureSQLQuery(@"SELECT ID,[Name],[Power],Accuracy,BoostStats
                                     FROM Attack");
        }

        public static DataTable GetPokemonAttackPool(int pokemonID)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT 
                                                        	AttackPool.Level,
                                                        	Attack.Name
                                                        
                                                        FROM AttackPool 
                                                        INNER JOIN Attack ON Attack.ID = AttackPool.AttackID
                                                        
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
    }
}
