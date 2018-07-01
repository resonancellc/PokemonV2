using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class PokemonParty
    {
        public static Pokemon[] playerPokemons = new Pokemon[6];
        public static Pokemon[] enemyPokemons = new Pokemon[6];
        public static int ActivePokemonIndex { get; set; }


        public static void AddToParty(Pokemon pokemon, bool isPlayerParty)
        {
            if (isPlayerParty)
            {
                if (playerPokemons.Where(x => x == null).Any())
                {
                    for (int i = 0; i < playerPokemons.Length; i++)
                    {
                        if (playerPokemons[i] == null)
                        {
                            playerPokemons[i] = pokemon;
                            break;
                        }
                    }
                }
                else
                {
                    // partys full
                } 
            }
            else
            {
                if (enemyPokemons.Where(x => x == null).Any())
                {
                    for (int i = 0; i < enemyPokemons.Length; i++)
                    {
                        if (enemyPokemons[i] == null)
                        {
                            enemyPokemons[i] = pokemon;
                            break;
                        }
                    }
                }
                else
                {
                    // partys full
                }
            }
        }

        public static void InsertIntoParty(Pokemon[] pokemonParty, Pokemon[] newPokemons)
        {
            if (pokemonParty.Where(x => x == null).Count() >= newPokemons.Where(x => x == null).Count()) // if we have enough place for newcoming pokemons
            {
                int j = 0;
                for (int i = 0; i < pokemonParty.Length; i++)
                {
                    if (pokemonParty[i] == null)
                    {
                        pokemonParty[i] = newPokemons[j];
                        j++;

                        if (j == newPokemons.Length) break;
                    }
                }
            }
            else
            {
                // partys full
            }
        }

        public static void AddManyToParty(Pokemon[] pokemons, bool isPlayerParty)
        {
            if (isPlayerParty)
                InsertIntoParty(playerPokemons, pokemons);
            else InsertIntoParty(enemyPokemons, pokemons);
        }

        public static Pokemon GetPokemon(int index, bool fromPlayersParty)
        {
            if (fromPlayersParty)
            {
                if (playerPokemons[index] != null)
                {
                    ActivePokemonIndex = index;
                    return playerPokemons[index];
                } 
            }
            else
            {
                if (enemyPokemons[index] != null)
                {
                    return enemyPokemons[index];
                }
            }

            return null;
        }

        public static void ClearEnemyParty()
        {
            for (int i = 0; i < enemyPokemons.Length; i++)
            {
                enemyPokemons[i] = null;
            }
        }

        public static bool CheckIfAnyPokemonAlive(bool isPlayerParty)
        {
            if (isPlayerParty) return playerPokemons.Where(p => p != null).Any(p => p.HPCurrent > 0);
            else return enemyPokemons.Where(p => p != null).Any(p => p.HPCurrent > 0);
        }

        public static Pokemon GetFirstPokemonAlive(bool isPlayerParty)
        {
            if (isPlayerParty) return playerPokemons.Where(p => p.HPCurrent > 0).First();
            else return enemyPokemons.Where(p => p.HPCurrent > 0).First();

        }

        public static void HealAll()
        {
            foreach (Pokemon pokemon in playerPokemons)
            {
                if (pokemon == null) break;
                pokemon.HPCurrent = pokemon.HPMax;
                pokemon.Condition = (int)PokemonEnum.Condition.None;
            }
        }
    }
}
