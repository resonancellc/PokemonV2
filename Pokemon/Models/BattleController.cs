using System.Linq;
using Pokemon.Factory;
using Pokemon.AdditionalEffects;
using Pokemon.Calculators;
using Pokemon.Helpers;
using Pokemon.Views;
using System.Collections.Generic;
using System;

namespace Pokemon.Models
{
    public class BattleController : IBattle
    {
        public IPokemon PlayerPokemon { get; set; }
        public IPokemon EnemyPokemon { get; set; }
        IPokemon fasterPokemon;
        IPokemon slowerPokemon;
        IAttack fasterPokemonAttack;
        IAttack slowerPokemonAttack;



        private readonly IBattleView _battleView;
        private readonly IBattleLogController _battleLogController;

        private List<Action> attackActionQueue = new List<Action>();
        private int _actionIndex = 0;


        public BattleController(IPokemon pokemon, IPokemon enemyPokemon, IBattleView battleView, IBattleLogController battleLogController)
        {
            PlayerPokemon = pokemon;
            EnemyPokemon = enemyPokemon;
            _battleView = battleView;
            _battleLogController = battleLogController;

            attackActionQueue.Add(() => ExecuteAttack(fasterPokemon, fasterPokemonAttack, slowerPokemon));
            attackActionQueue.Add(() => ExecuteAfterAttackAdditionalEffects(fasterPokemon, fasterPokemonAttack, slowerPokemon));
            attackActionQueue.Add(() => ExecuteChangeStatsAfterAttack(fasterPokemon, fasterPokemonAttack, slowerPokemon));

            attackActionQueue.Add(() => ExecuteAttack(slowerPokemon, slowerPokemonAttack, fasterPokemon));
            attackActionQueue.Add(() => ExecuteAfterAttackAdditionalEffects(slowerPokemon, slowerPokemonAttack, fasterPokemon));
            attackActionQueue.Add(() => ExecuteChangeStatsAfterAttack(slowerPokemon, slowerPokemonAttack, fasterPokemon));

            attackActionQueue.Add(null);
        }

        public void PerformAttack(IAttack playerAttack)
        {
            var enemyAttack = GetEnemyAttack();

            if (PlayerPokemon == GetFasterPokemon(playerAttack.AdditionalEffects, enemyAttack.AdditionalEffects))
            {
                fasterPokemon = PlayerPokemon;
                fasterPokemonAttack = playerAttack;
                slowerPokemon = EnemyPokemon;
                slowerPokemonAttack = enemyAttack;
            }
            else
            {
                fasterPokemon = EnemyPokemon;
                fasterPokemonAttack = enemyAttack;
                slowerPokemon = PlayerPokemon;
                slowerPokemonAttack = playerAttack;
            }

            PerformAttack(fasterPokemon, fasterPokemonAttack, slowerPokemon);
            _actionIndex++;
        }

        private bool ExecutePreAttackConditions(IPokemon pokemon)
        {
            if (pokemon.IsAbleToAttackAfterConditionEffect())
            {
                if (pokemon.IsAbleToAttack())
                {
                    return true;
                }
            }
            return false;
        }

        public void ExecuteNextAction()
        {
            if (attackActionQueue.ElementAt(_actionIndex) != null)
            {
                attackActionQueue.ElementAt(_actionIndex).Invoke();
            }

            _actionIndex++;

            if (_actionIndex == attackActionQueue.Count)
            {
                _actionIndex = 0;
                _battleView.AttacksExecutionOver();
            }
        }

        private void PerformAttack(IPokemon pokemon, IAttack attack, IPokemon target)
        {
            if (ExecutePreAttackConditions(pokemon))
            {
                attackActionQueue.ElementAt(_actionIndex).Invoke();
            }
        }

        private void ExecuteAttack(IPokemon pokemon, IAttack attack, IPokemon target)
        {
            _battleLogController.SetText($"{pokemon.Name} used {attack.Name}");
            int damage = attack.CalculateDamage(pokemon, target);
            if (damage != 0)
            {
                target.Hurt(damage);
            }
        }

        private void ExecuteChangeStatsAfterAttack(IPokemon pokemon, IAttack attack, IPokemon target)
        {
            if (!string.IsNullOrWhiteSpace(attack.BoostStats))
            {
                var statsChangeOutput = StatsChanger.ChangeTempStats(attack, pokemon, target);
                _battleLogController.SetText(statsChangeOutput);
            }
        }

        private void ExecuteAfterAttackAdditionalEffects(IPokemon pokemon, IAttack attack, IPokemon target)
        {
            if (AdditionalEffectAvailability.ContainsEffectType(attack.AdditionalEffects, typeof(CritBoosting)))
            {
                if (attack.AdditionalEffects.Any(e => e.ID == (int)AdditionalEffectEnum.BoostCriticalSelf))
                {
                    CritBoosting critBoosting = attack.AdditionalEffects.First(e => e.ID == (int)AdditionalEffectEnum.BoostCriticalSelf) as CritBoosting;
                    if (pokemon.IsEnergyFocused == false)
                    {
                        pokemon.IsEnergyFocused = true;
                    }
                    else
                    {
                        _battleLogController.SetText($"{pokemon.Name} is already focused");
                    }
                }
                else if (attack.AdditionalEffects.Any(e => e.ID == (int)AdditionalEffectEnum.BoostCriticalTarget))
                {
                    CritBoosting critBoosting = attack.AdditionalEffects.First(e => e.ID == (int)AdditionalEffectEnum.BoostCriticalTarget) as CritBoosting;
                    if (target.IsEnergyFocused == false)
                    {
                        target.IsEnergyFocused = true;
                    }
                    else
                    {
                        _battleLogController.SetText($"{target.Name} is already focused");
                    }
                }
            }

            if (AdditionalEffectAvailability.ContainsEffectType(attack.AdditionalEffects, typeof(StatusChanger)))
            {
                var statusChanger = attack.AdditionalEffects.First();


                if (attack.AdditionalEffects.Any(e => e.ID == (int)AdditionalEffectEnum.PoisonWeak ||
                                                      e.ID == (int)AdditionalEffectEnum.PoisonMid ||
                                                      e.ID == (int)AdditionalEffectEnum.PoisonHigh ||
                                                      e.ID == (int)AdditionalEffectEnum.PoisonMax))
                {


                    if (ChanceCalculator.CalculateChance((int)statusChanger.PrimaryParameter, 100))
                    {
                        _battleLogController.SetText($"{target.Name} is now poisoned");
                        target.Condition = Condition.PSN;
                    }
                }

                if (attack.AdditionalEffects.Any(e => e.ID == (int)AdditionalEffectEnum.BurnWeak ||
                                                      e.ID == (int)AdditionalEffectEnum.BurnMaxSelf))
                {
                    if (ChanceCalculator.CalculateChance((int)statusChanger.PrimaryParameter, 100))
                    {
                        if (statusChanger.IsOnSelf)
                        {
                            pokemon.Condition = Condition.BRN;
                            _battleLogController.SetText($"{pokemon.Name} is now burning");
                        }
                        else
                        {
                            pokemon.Condition = Condition.BRN;
                            _battleLogController.SetText($"{pokemon.Name} is now burning");
                        }
                    }
                }

                if (attack.AdditionalEffects.Any(e => e.ID == (int)AdditionalEffectEnum.ConfusionWeak ||
                                                      e.ID == (int)AdditionalEffectEnum.ConfusionMax))
                {
                    if (ChanceCalculator.CalculateChance((int)statusChanger.PrimaryParameter, 100))
                    {
                        if (statusChanger.IsOnSelf)
                        {
                            pokemon.IsConfused = true;
                            _battleLogController.SetText($"{pokemon} is now confused");
                        }
                        else
                        {
                            target.IsConfused = true;
                            _battleLogController.SetText($"{target} is now confused");
                        }
                    }
                }
            }
        }

        public IAttack GetEnemyAttack() => EnemyPokemon.Attacks.ElementAt(GenerateRandomNumber.GetRandomNumber(0, EnemyPokemon.Attacks.Count));

        public IPokemon GetFasterPokemon(ICollection<IAdditionalEffect> playerAttackAdditionalEffects, ICollection<IAdditionalEffect> enemyAttackAdditionalEffects)
        {
            var playerAttackIsFast = playerAttackAdditionalEffects.ContainsEffectType(typeof(FastAttack));
            var enemyAttackIsFast = enemyAttackAdditionalEffects.ContainsEffectType(typeof(FastAttack));

            if (playerAttackIsFast == enemyAttackIsFast)
            {
                if (TempStatsCalculator.GetSpeed(PlayerPokemon) > TempStatsCalculator.GetSpeed(EnemyPokemon))
                    return PlayerPokemon;
                else
                    return EnemyPokemon;
            }

            else if (playerAttackIsFast) return PlayerPokemon;
            else if (enemyAttackIsFast) return EnemyPokemon;

            return PlayerPokemon;
        }
    }
}
