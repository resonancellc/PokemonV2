using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon
{
    public class Battle
    {
        public Pokemon Pokemon { get; set; }
        public Stat PokemonStartStats { get; set; }

        public Pokemon EnemyPokemon { get; set; }
        public Stat EnemyPokemonStartStats { get; set; }


        public Battle(Pokemon pokemon, Pokemon enemyPokemon)
        {
            this.Pokemon = pokemon;
            this.PokemonStartStats = new Stat(pokemon.Stat);

            this.EnemyPokemon = enemyPokemon;
            this.EnemyPokemonStartStats = new Stat(enemyPokemon.Stat);
        }

        public void SetPokemon(Pokemon pokemon)
        {
            this.Pokemon = pokemon;
        }


        public int Attack(bool isPlayerAttack, Attack attack)
        {
            int damage = CalculatorHelper.CalculateAttackPower(isPlayerAttack, attack, this);
            if (attack.Power.HasValue  && damage < 1) damage = 1; // 1 is minimum damage can be dealt

            if (isPlayerAttack) EnemyPokemon.Hurt(damage);
            else Pokemon.Hurt(damage);

            return damage;
        }        
    }
}
