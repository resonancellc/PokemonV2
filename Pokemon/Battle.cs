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
        public Pokemon EnemyPokemon { get; set; }
        public Stat StartPokeStat { get; set; }
        public Stat TempPokeStat { get; set; }
        public Stat TempEnemyPokeStat { get; set; }

        public Battle(Pokemon pokemon, Pokemon enemyPokemon)
        {
            this.Pokemon = pokemon;
            this.EnemyPokemon = enemyPokemon;

            this.StartPokeStat = pokemon.Stat;

            this.TempPokeStat = pokemon.Stat;
            this.TempEnemyPokeStat = enemyPokemon.Stat;
        }

        public int Attack(bool isPlayerAttack, Attack attack)
        {
            int damage = CalculatorHelper.CalculateAttackPower(isPlayerAttack, attack, this);

            if (isPlayerAttack) EnemyPokemon.Hurt(damage);
            else Pokemon.Hurt(damage);

            return damage;
        }        
    }
}
