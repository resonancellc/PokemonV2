using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pokemon.Models;
using Pokemon.Factory;

namespace Pokemon
{
    public class Battle : IBattle
    {
        public IPokemon Pokemon { get; set; }
        public IPokemon EnemyPokemon { get; set; }

        public Battle(IPokemon pokemon, IPokemon enemyPokemon)
        {
            this.Pokemon = pokemon;
            this.EnemyPokemon = enemyPokemon;
        }

        public void PokemonAttack(IAttack attack, IPokemon attackingPokemon, bool isPlayerAttack)
        {
            if (!attackingPokemon.IsFlinched)
            {
                if (!BattleHelper.IsConfused(attackingPokemon))
                {
                    if (BattleHelper.ApplyConditionEffect(attackingPokemon)) // change name for something like // IsImmobilizedByCondition() ?
                    {
                        // Checking if not miss
                        if (!AdditionalEffectHelper.IsAlwaysHits(attack.AdditionalEffect) && BattleHelper.IsMiss(attack)) BattleLog.AppendText($"{attackingPokemon.Name} missed!");
                        else
                        {
                            if (isPlayerAttack)
                                BattleLog.AppendText($"Your {attackingPokemon.Name} used {attack.Name}");
                            else BattleLog.AppendText($"Foe {attackingPokemon.Name} used {attack.Name}");


                            Attack(isPlayerAttack, attack);

                            if (attack.BoostStats != string.Empty)
                                BattleHelper.ChangeTempStats(isPlayerAttack, attack, this);

                        }
                    }
                }
                else
                {
                    attackingPokemon.Hurt(CalculatorHelper.CalculateAttackPower(!isPlayerAttack, AttackList.Attacks.Where(a => a.Value.Name == "ConfusionHit").First().Value, this));
                    BattleLog.AppendText($"{attackingPokemon.Name} hurts itself in its confusion");
                }
            }
            else
            {
                attackingPokemon.IsFlinched = false;
                BattleLog.AppendText($"{attackingPokemon.Name} is flinched");
            }
        }

        public void Attack(bool isPlayerAttack, IAttack attack)
        {
            //int damage = AdditionalEffectHelper.IsAlwaysSameDamage(attack.AdditionalEffect);
            //if (damage == 0 && attack.Power.HasValue)
            //{
            //    damage = CalculatorHelper.CalculateAttackPower(isPlayerAttack, attack, this);
            //    if (damage < 1) damage = 1;
            //    if (BattleHelper.IsCritical(attack, isPlayerAttack ? this.Pokemon : this.EnemyPokemon))
            //    {
            //        damage *= 2;
            //        BattleLog.AppendText("Critical hit!");
            //    }
            //}

            //if (attack.AdditionalEffect != String.Empty)
            //{
            //    BattleHelper.IsConditionChange(attack, isPlayerAttack ? this.EnemyPokemon : this.Pokemon);
            //    AdditionalEffectHelper.IsFlinch(attack.AdditionalEffect, isPlayerAttack ? this.EnemyPokemon : this.Pokemon);
            //    AdditionalEffectHelper.IsCritBoosting(attack.AdditionalEffect, isPlayerAttack ? this.Pokemon : this.EnemyPokemon);
                
            //}

            //if (damage != 0)
            //{
            //    if (isPlayerAttack) EnemyPokemon.Hurt(damage);
            //    else Pokemon.Hurt(damage);
            //}

        }
    }
}
