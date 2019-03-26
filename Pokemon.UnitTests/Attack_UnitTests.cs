using NUnit.Framework;
using NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokemon.Models;
using Pokemon.Factory;

namespace Pokemon.UnitTests
{
    public class Attack_UnitTests
    {
        public void Prepare()
        {
            StaticSQL.SetConnectionString("Server=DESKTOP-6CLE20J\\SQLEXPRESS;Database=Pokemon;Trusted_Connection=true;");
            PokemonList.Pokemons.Clear();
            PokemonList.FillPokemonList();
        }

        [Test]
        public void CreatePokemon_()
        {
            IPokemon pokemon = PokemonFactory.CreatePokemon();

            Assert.AreEqual(pokemon.Attacks.Count(), 0);
        }
    }
}
