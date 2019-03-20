using NUnit.Framework;
using Pokemon.Factory;
using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.UnitTests
{
    public class StringCalculator_UnitTests
    {
        public void Prepare()
        {
            StaticSQL.SetConnectionString("Server=DESKTOP-6CLE20J\\SQLEXPRESS;Database=Pokemon;Trusted_Connection=true;");
            PokemonList.Pokemons.Clear();
            PokemonList.FillPokemonList();
        }

        [TestCase(
            50,
            13
            )]
        public void CreatePokemon_GivenID_Returns_CorrectName(int level, int id)
        {
            Prepare();

            IPokemon pokemon = PokemonFactory.CreatePokemon(level, id);

            string expectedName = "Weedle";
            string result = pokemon.Name;

            Assert.AreEqual(expectedName, result);
        }

        [Test]
        public void CreatePokemon_NoParameters_EmptyPokemon()
        {
            Prepare();
            IPokemon pokemon = PokemonFactory.CreatePokemon();

            Assert.AreEqual(pokemon.ID, 0);
        }
    }
}
