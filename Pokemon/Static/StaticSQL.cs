using Dapper;
using Pokemon.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Pokemon
{
    public static class StaticSQL
    {
        public static string ConnectionString { get; set; }

        public static void SetConnectionString(string conn)
        {
            ConnectionString = conn;
        }

        private static DataTable ExecuteSQLQuery(string query, Dictionary<string, object> parameters = null)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.CommandType = CommandType.Text;
                if (parameters != null && parameters.Any())
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                }

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                using (DataSet ds = new DataSet())
                {
                    sda.Fill(ds);
                    DataTable data = ds.Tables[0];
                    return data;
                }
            }
        }

        public static DataTable GetPokemons()
        {
            string sql = 
                @"SELECT Pokemons.ID, Pokemons.Name, BaseStats.Health, BaseStats.Attack,
                    BaseStats.Defence, BaseStats.SpecialAttack, BaseStats.SpecialDefence,
                    BaseStats.Speed, BaseStats.PrimaryTypeID, BaseStats.SecondaryTypeID, BaseStats.MinimalLevel 
                FROM Pokemons 
                INNER JOIN BaseStats ON Pokemons.ID = BaseStats.ID 
                ORDER BY Pokemons.ID ASC";

            return ExecuteSQLQuery(sql);
        }

        public static DataTable GetAttackByName(string attackName)
        {
            string sql =
                @"SELECT Attacks.ID, Attacks.[Name], Attacks.[Power],
                Attacks.Accuracy, Attacks.BoostStats, Attacks.TypeID, Attacks.IsSpecial 
                FROM Attacks
                WHERE Attacks.Name = @attackName";

            var parameters = new Dictionary<string, object>
            {
                { "@attackName", attackName }
            };

            return ExecuteSQLQuery(sql, parameters);
        }

        public static DataTable GetAttacks()
        {
            string sql =
                @"SELECT Attacks.ID, Attacks.[Name], Attacks.[Power],
                Attacks.Accuracy, Attacks.BoostStats, Attacks.TypeID, Attacks.IsSpecial
                FROM Attacks";

            return ExecuteSQLQuery(sql);
        }

        public static List<AdditionalEffect> GetAdditionalEffects()
        {
            string sql = 
                @"SELECT * FROM AdditionalEffects";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<AdditionalEffect>(sql).ToList();
            }
        }

        public static List<Attack> GetPokemonAttacks(int ID)
        {
            string sql =
                @"SELECT Attacks.ID, Attacks.[Name], Attacks.[Power], Attacks.Accuracy,
                Attacks.BoostStats, Attacks.TypeID, Attacks.IsSpecial, AttackPools.[Level] 
                FROM AttackPools
                INNER JOIN Attacks ON AttackPools.AttackID = Attacks.ID
                WHERE PokemonID = @ID";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Attack>(sql, new { ID }).ToList();
            }
        }

        public static List<int> GetAttackAdditionalEffectIDs(int ID)
        {
            string sql =
                @"SELECT AdditionalEffectID
                FROM Attacks_AdditionalEffects
                WHERE AttackId = @ID";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<int>(sql, new { ID }).ToList();
            }
        }

        public static DataTable GetItemList()
        {
            string sql =
                @"SELECT ID, Name, Description, Cost FROM Items";

            return ExecuteSQLQuery(sql);
        }
    }
}
