using NUnit.Framework;
using NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokemon.Models;
using Pokemon.Factory;
using Pokemon.Helpers;

namespace Pokemon.UnitTests
{
    public class Stats_UnitTests
    {
        private void Prepare()
        {
            StaticSQL.SetConnectionString("Server=DESKTOP-6CLE20J\\SQLEXPRESS;Database=Pokemon;Trusted_Connection=true;");
            PokemonList.Pokemons.Clear();
            PokemonList.FillPokemonList();
        }


        [TestCase(-1,-1)]
        [TestCase(0,-1)]
        [TestCase(-1,0)]
        [TestCase(1,1)]
        [TestCase(150,1)]
        [TestCase(1,150)]
        public void CreatePokemon_GivenIdAndLevel_ReturnsDefaultStatModifierStages(int level, int id)
        {
            IPokemon pokemon = PokemonFactory.CreatePokemon(level, id);

            for (int i = 0; i < pokemon.StatModifierStages.Length; i++)
            {
                Assert.AreEqual(pokemon.StatModifierStages[i], 0);
            }
        }

        [TestCase(-1, -1, 1, 1)]
        [TestCase(0, -1, 2, 2)]
        [TestCase(-1, 0, 3, 3)]
        [TestCase(1, 1, 4, 4)]
        [TestCase(150, 1, 5, 5)]
        [TestCase(1, 150, 6, 6)]
        [TestCase(1, 150, 7, 0)]
        [TestCase(1, 150, 8, 0)]
        public void PokemonStatModifierStages_CannotGoAboveSix(int level, int id, int changeValueBy, int expectedResult)
        {
            IPokemon pokemon = PokemonFactory.CreatePokemon(level, id);

            for (int i = 0; i < pokemon.StatModifierStages.Length; i++)
            {
                //StatsChanger.ChangeTempPokemonStats(pokemon, i, changeValueBy);
            }

            for (int i = 0; i < pokemon.StatModifierStages.Length; i++)
            {
                Assert.AreEqual(expectedResult, pokemon.StatModifierStages[i]);
            }
        }

        [TestCase(-1, -1, -1, -1)]
        [TestCase(0, -1, -2, -2)]
        [TestCase(-1, 0, -3, -3)]
        [TestCase(1, 1, -4, -4)]
        [TestCase(150, 1, -5, -5)]
        [TestCase(1, 150, -6, -6)]
        [TestCase(1, 150, -7, 0)]
        [TestCase(1, 150, -8, 0)]
        public void PokemonStatModifierStages_CannotGoBelowMinusSix(int level, int id, int changeValueBy, int expectedResult)
        {
            IPokemon pokemon = PokemonFactory.CreatePokemon(level, id);

            for (int i = 0; i < pokemon.StatModifierStages.Length; i++)
            {
                //StatsChanger.ChangeTempPokemonStats(pokemon, i, changeValueBy);
            }

            for (int i = 0; i < pokemon.StatModifierStages.Length; i++)
            {
                Assert.AreEqual(expectedResult, pokemon.StatModifierStages[i]);
            }
        }
    }
}
