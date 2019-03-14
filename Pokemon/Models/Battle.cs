using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pokemon.Models;
using Pokemon.Factory;
using Pokemon.AdditionalEffects;

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

        public void PreparePokemonAttack(IAttack attack, IPokemon attackingPokemon, bool isPlayerAttack)
        {
            // pokemon is scared to attack
            if (attackingPokemon.IsFlinched)
            {
                attackingPokemon.IsFlinched = false;
                BattleLog.AppendText($"{attackingPokemon.Name} is flinched");
                return;
            }

            // pokemon is confused and failed confusion test - he damaged himself and is not able to perform given attack
            if (BattleHelper.IsConfused(attackingPokemon))
            {
                int damage = CalculatorHelper.CalculateAttackPower(!isPlayerAttack, AttackList.Attacks.Where(a => a.Value.Name == "ConfusionHit").First().Value, this);
                attackingPokemon.Hurt(damage);
                BattleLog.AppendText($"{attackingPokemon.Name} hurts itself in its confusion");
                return;
            }

            // pokemon is not able to attack after applying condition effects - sleeping or dead for example
            if (!BattleHelper.IsAbleToAttackAfterConditionEffect(attackingPokemon)) return;

            // pokemon dind't use attack that always hits and missed
            //if (!AdditionalEffectHelper.IsAlwaysHits(attack.AdditionalEffect) && BattleHelper.IsMiss(attack))
            //{
            //    BattleLog.AppendText($"{attackingPokemon.Name} missed!");
            //    return;
            //}

            // If you reached that part that means you succesfully attacked opposite pokemon! :)

            // move those lines to PerformPokemonAttack method
            if (isPlayerAttack)
                BattleLog.AppendText($"Your {attackingPokemon.Name} used {attack.Name}");
            else BattleLog.AppendText($"Foe {attackingPokemon.Name} used {attack.Name}");

            PerformPokemonAttack(attack, isPlayerAttack);

            if (attack.BoostStats != string.Empty)
                BattleHelper.ChangeTempStats(isPlayerAttack, attack, this);
        }

        public void PerformPokemonAttack(IAttack attack, bool isPlayerAttack)
        {
            int damage = 0;

            if (AdditionalEffects.AlwaysSameDamage.IsAlwaysSameDamage(attack.AdditionalEffects))
            {
                damage = AdditionalEffects.AlwaysSameDamage.GetAlwaysSameDamage(attack.AdditionalEffects.Where(p => p.Name.Contains(StringEnums.SameDamage)).FirstOrDefault());
            }

            if (damage == 0 && attack.Power.HasValue)
            {
                damage = CalculatorHelper.CalculateAttackPower(isPlayerAttack, attack, this);
                if (damage < 1) damage = 1;
                if (BattleHelper.IsCritical(attack, isPlayerAttack ? Pokemon : EnemyPokemon))
                {
                    damage *= 2;
                    BattleLog.AppendText("Critical hit!");
                }
            }

            //if (attack.AdditionalEffect != String.Empty)
            //{
            //    // those are all boolean methods, why?
            //    BattleHelper.ChangeCondition(attack, isPlayerAttack ? EnemyPokemon : Pokemon);
            //    AdditionalEffectHelper.SetFlinch(attack.AdditionalEffect, isPlayerAttack ? EnemyPokemon : Pokemon);
            //    AdditionalEffectHelper.IsCritBoosting(attack.AdditionalEffect, isPlayerAttack ? Pokemon : EnemyPokemon);
            //}

            if (damage != 0)
            {
                if (isPlayerAttack) EnemyPokemon.Hurt(damage);
                else Pokemon.Hurt(damage);
            }

        }
    }
}
