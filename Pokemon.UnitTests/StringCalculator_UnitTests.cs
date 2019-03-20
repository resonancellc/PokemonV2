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
    public class StringCalculator_UnitTests
    {
        private static readonly List<List<int>> _sourceList = new List<List<int>>()
        {
            new List<int>() { 1,2,3},
            new List<int>() { 1,2,5,6},
            new List<int>() { }
        };


        public void Prepare()
        {
            StaticSQL.SetConnectionString("Server=DESKTOP-6CLE20J\\SQLEXPRESS;Database=Pokemon;Trusted_Connection=true;");
            PokemonList.Pokemons.Clear();
            PokemonList.FillPokemonList();
        }

        [TestCase(50,13,"Weedle")]
        [TestCase(50,14,"Kakuna")]
        public void CreatePokemon_GivenID_Returns_CorrectName(int level, int id, string expectedResult)
        {
            Prepare();
            IPokemon pokemon = PokemonFactory.CreatePokemon(level, id);

            Assert.AreEqual(expectedResult, pokemon.Name);
        }

        [Test]
        public void CreatePokemon_NoParameters_EmptyPokemon()
        {
            IPokemon pokemon = PokemonFactory.CreatePokemon();

            Assert.AreEqual(pokemon.ID, 0);
        }

        [Test]
        public void CreatePokemon_GivenWrongID_PokemonWithNoAttacks()
        {
            IPokemon pokemon = PokemonFactory.CreatePokemon();

            Assert.Throws<InvalidOperationException>(delegate ()
            {
                pokemon.Attacks.First();
            });
        }


        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 2, 3, 4 })]
        public void RemoveLast_ShouldRemoveOnlyOneElement(int[] input)
        {
            List<int> list = input.ToList();

            int startCount = list.Count();
            if (list.Count() != 0)
            {
                startCount = list.Count() - 1;
            }

            IRemovable removableList = new RemoveMeList(list);

            removableList.RemoveLast();

            Assert.LessOrEqual(removableList.Count(), startCount);
        }

        [Test, TestCaseSource("_sourceList")]
        public void RemoveLast_ShouldRemoveOnlyOneElement_AnotherWay(List<int> list)
        {
            int startCount = list.Count();
            if (list.Count() != 0)
            {
                startCount = list.Count() - 1;
            }

            IRemovable removableList = new RemoveMeList(list);

            removableList.RemoveLast();

            Assert.LessOrEqual(removableList.Count(), startCount);
        }


    }

    public class RemoveMeList : IRemovable
    {
        IList<int> _list;

        public RemoveMeList(IList<int> inputList)
        {
            _list = inputList;
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public void RemoveLast()
        {
            if (_list.Count() == 0) return;
            _list.RemoveAt(_list.Count() - 1);
        }
        public int Count()
        {
            return _list.Count();
        }


    }

    public interface IRemovable
    {
        int Count();
        void RemoveAt(int index);
        void RemoveLast();
    }

}
