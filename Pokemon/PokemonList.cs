using Pokemon.AdditionalEffects;
using Pokemon.Factory;
using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Data;

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
                var attacks = StaticSQL.GetPokemonAttacks(pokemon.ID);
                foreach (var attack in attacks)
                {
                    attack.AdditionalEffects = AdditionalEffectsList.GetAdditionalEffects(attack.ID);
                    pokemon.Attacks.Add(attack);
                }
            }
        }
    }
}
