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

        public static void AddManyToParty(Pokemon[] pokemons, bool isPlayerParty)
        {
            if (isPlayerParty)
            {
                if (playerPokemons.Where(x => x == null).Count() >= pokemons.Length) // if we have enough place for newcoming pokemons
                {
                    int j = 0;
                    for (int i = 0; i < playerPokemons.Length; i++)
                    {
                        if (playerPokemons[i] == null)
                        {
                            playerPokemons[i] = pokemons[j];
                            j++;
                            if (j == pokemons.Length)
                            {
                                break;
                            }
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
                if (enemyPokemons.Where(x => x == null).Count() >= pokemons.Length) // if we have enough place for newcoming pokemons
                {
                    int j = 0;
                    for (int i = 0; i < enemyPokemons.Length; i++)
                    {
                        if (enemyPokemons[i] == null)
                        {
                            enemyPokemons[i] = pokemons[j];
                            j++;
                            if (j == pokemons.Length)
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    // partys full
                }
            }
        }

        public static Pokemon GetPokemon(int index, bool fromPlayersParty)
        {
            if (fromPlayersParty)
            {
                if (playerPokemons[index] != null)
                {
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
