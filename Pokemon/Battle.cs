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

        public Attack GeneratePokemonAttack(bool isPlayerAttack, object sender = null)
        {
            Attack attack = null;
            if (!isPlayerAttack)
            {
                while (attack == null)
                {
                    attack = EnemyPokemon.attackPool[CalculatorHelper.RandomNumber(0, EnemyPokemon.attackPool.Length)];
                }
            }
            else return attack = StaticTypes.attackList.Where(x => x.Name == ((Button)sender).Text).First();

            return attack;
        }

        public void PokemonAttack(Attack attack, Pokemon attackingPokemon, bool isPlayerAttack)
        {
            if (!attackingPokemon.IsFlinched)
            {
                if (BattleHelper.ApplyConditionEffect(attackingPokemon)) // change name for something like // IsImmobilizedByCondition() ?
                {
                    // Checking if not miss
                    if (!AdditionalEffectHelper.IsAlwaysHits(attack.AdditionalEffect) && BattleHelper.IsMiss(attack)) BattleLog.AppendText($"{attackingPokemon.Name} missed!");
                    else
                    {
                        BattleLog.AppendText($"{attackingPokemon.Name} used {attack.Name}");
                        Attack(isPlayerAttack, attack);

                        if (attack.BoostStats != string.Empty)
                            BattleHelper.ChangeTempStats(isPlayerAttack, attack, this);

                    }
                }
            }
            else
            {
                attackingPokemon.IsFlinched = false;
                BattleLog.AppendText($"{attackingPokemon.Name} is flinched");
            }

        }

        public void Attack(bool isPlayerAttack, Attack attack)
        {
            int damage = AdditionalEffectHelper.IsAlwaysSameDamage(attack.AdditionalEffect);
            if (damage == 0)
            {
                damage = CalculatorHelper.CalculateAttackPower(isPlayerAttack, attack, this);
                if (attack.Power.HasValue && damage < 1) damage = 1;
                if (attack.Power.HasValue && BattleHelper.IsCritical(attack))
                {
                    damage *= 2;
                    BattleLog.AppendText("Critical hit!");
                }
            }

            if (attack.AdditionalEffect != String.Empty)
            {
                BattleHelper.IsConditionChange(attack, isPlayerAttack ? this.EnemyPokemon : this.Pokemon);
                AdditionalEffectHelper.IsFlinch(attack.AdditionalEffect, isPlayerAttack ? this.EnemyPokemon : this.Pokemon);
            }

            if (isPlayerAttack) EnemyPokemon.Hurt(damage);
            else Pokemon.Hurt(damage);
        }


    }
}
