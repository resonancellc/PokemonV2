using Pokemon.Factory;
using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class PokemonList
    {
        public static Dictionary<int, IPokemon> Pokemons = new Dictionary<int, IPokemon>();

        public static void FillPokemonList()
        {
            DataRowCollection pokemonDataRows = StaticSQL.GetPokemons().Rows;
            foreach (DataRow pokemonRow in pokemonDataRows)
            {
                var values = pokemonRow.ItemArray;
                IPokemon pokemon = PokemonFactory.CreatePokemon();

                pokemon.ID = (int)values[0];
                pokemon.Name = (string)values[1];

                pokemon.Stats.Health = (int)values[2];
                pokemon.Stats.Attack = (int)values[3];
                pokemon.Stats.Defence = (int)values[4];
                pokemon.Stats.SpecialAttack = (int)values[5];
                pokemon.Stats.SpecialDefence = (int)values[6];
                pokemon.Stats.Speed = (int)values[7];

                pokemon.PrimaryTypeID = (int)values[8];
                pokemon.SecondaryTypeID = values[9] != DBNull.Value ? (int?)values[9] : null;
                pokemon.MinimalLevel = (int)values[10];

                Pokemons.Add(pokemon.ID, pokemon);
            }
            AddAttacksToPokemons();
        }

        private static void AddAttacksToPokemons()
        {
            foreach (IPokemon pokemon in Pokemons.Values)
            {
                DataRowCollection attackDataRows = StaticSQL.GetPokemonAttacks(pokemon.ID).Rows;
                foreach (DataRow attackDataRow in attackDataRows)
                {
                    var values = attackDataRow.ItemArray;
                    IAttack attack = PokemonAttacksFactory.CreateAttack();

                    attack.ID = (int)values[0];
                    attack.Name = (string)values[1];
                    attack.Power = values[2] != DBNull.Value ? (int?)values[2] : null;
                    attack.Accuracy = (int)values[3];
                    attack.BoostStats = values[4] != DBNull.Value ? (string)values[4] : "";
                    attack.ElementalType = values[5] != DBNull.Value ? (ElementalType)values[5] : 0;
                    attack.IsSpecial = (bool)values[6];
                    attack.AdditionalEffects = AdditionalEffectFactory.GetAdditionalEffects(values[7] != DBNull.Value ? (string)values[7] : "");
                    attack.Level = (int)values[8];

                    pokemon.Attacks.Add(attack);
                }
            }
        }
    }
}
