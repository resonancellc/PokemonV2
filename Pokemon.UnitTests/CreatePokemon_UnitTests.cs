using NUnit.Framework;
using NUnit;
using Pokemon.Factory;
using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.UnitTests
{
    public class CreatePokemon_UnitTests
    {
        public void Prepare()
        {
            StaticSQL.SetConnectionString("Server=DESKTOP-6CLE20J\\SQLEXPRESS;Database=Pokemon;Trusted_Connection=true;");
            PokemonList.Pokemons.Clear();
            PokemonList.FillPokemonList();
        }

        [TestCase(50,13,"Weedle")]
        [TestCase(50,14,"Kakuna")]
        [TestCase(50,6216, "Bulbasaur")]
        public void CreatePokemon_GivenID_Returns_CorrectName(int level, int id, string expectedResult)
        {
            Prepare();
            IPokemon pokemon = PokemonFactory.CreatePokemon(level, id);

            Assert.AreEqual(expectedResult, pokemon.Name);
            Assert.AreEqual(level, pokemon.Level);
        }

        [TestCase(0, 1, 5)]
        [TestCase(-20, 1, 5)]
        [TestCase(120, -1, 5)]
        [TestCase(0, 0, 5)]
        public void CreatePokemon_GivenBadLevel_Returns_CorrectLevel(int level, int id, int expectedLevel)
        {
            Prepare();
            IPokemon pokemon = PokemonFactory.CreatePokemon(level, id);

            Assert.AreEqual(expectedLevel, pokemon.Level);
        }


        [Test]
        public void CreatePokemon_NoParameters_EmptyPokemon()
        {
            IPokemon pokemon = PokemonFactory.CreatePokemon();

            Assert.AreEqual(pokemon.ID, 0);
        }
    }

}
