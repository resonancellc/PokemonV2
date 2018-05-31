using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public abstract class PokemonParty : IPokemonParty
    {
        public Pokemon[] playerPokemons = new Pokemon[6];

        public virtual void AddToParty(Pokemon pokemon)
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

        public virtual void AddManyToParty(Pokemon[] pokemons)
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
                        if (pokemons[j + 1] == null)
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
}
