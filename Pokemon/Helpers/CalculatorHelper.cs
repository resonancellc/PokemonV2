using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class CalculatorHelper
    {

        public static Stat CalculateStats(int level, Stat pokemonStat)
        {
            Random rand = new Random();
            Stat stat = new Stat();
            stat.Attack = (((10 + pokemonStat.Attack + rand.Next(0,20)) * 2) * level) / 100 + 5;
            stat.Defence = (((10 + pokemonStat.Defence + rand.Next(0, 20)) * 2) * level) / 100 + 5;
            stat.SpecialAttack = (((10 + pokemonStat.SpecialAttack + rand.Next(0, 20)) * 2) * level) / 100 + 5;
            stat.SpecialDefence = (((10 + pokemonStat.SpecialDefence + rand.Next(0, 20)) * 2) * level) / 100 + 5;
            stat.Speed = (((10 + pokemonStat.Speed + rand.Next(0, 20)) * 2) * level) / 100 + 5;
            stat.Health = ((10 + pokemonStat.Health + rand.Next(0, 20) + 50) * level) / 50 + 10;
            return stat;
        }

        public static int CalculateAttackPower(bool isPlayerAttack, Attack attack, Battle battle)
        {
            int damage = 0;
            if (isPlayerAttack)
            {
                if (attack.BoostStats != string.Empty && attack.BoostStats != null)
                {
                    ChangeTempStats(isPlayerAttack, attack, battle);
                }
                damage = Convert.ToInt32((((2 * battle.Pokemon.Level / 5) + 2) * attack.Power * ((float)battle.Pokemon.Stat.Attack / (float)battle.EnemyPokemon.Stat.Defence)) / 50);
                BattleLog.AppendText($"Zaatakowano {battle.EnemyPokemon.Name} za {damage} - jego obrona wynosiła {(float)battle.EnemyPokemon.Stat.Defence}");
                return damage;
            }
            else
            {
                if (attack.BoostStats != string.Empty && attack.BoostStats != null)
                {
                    ChangeTempStats(isPlayerAttack, attack, battle);
                }
                damage = Convert.ToInt32((((2 * battle.EnemyPokemon.Level / 5) + 2) * attack.Power * ((float)battle.EnemyPokemon.Stat.Attack / (float)battle.Pokemon.Stat.Defence)) / 50);
                return damage;
            }
        }

        private static void ChangeTempStats(bool isPlayerAttack, Attack attack, Battle battle)
        {
            string[] attributes = attack.BoostStats.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); //AttackBoostStatsSplitter();
            if (attributes.Length > 0)
            {
                if (attributes[0] == "enemy")
                {
                    if (isPlayerAttack) // gracz na przeciwnika
                    {
                        ChangeTempStats(true, Int32.Parse(attributes[1]), Int32.Parse(attributes[2]), battle);
                    }
                    else // przeciwnik na gracza
                    {
                        ChangeTempStats(false, Int32.Parse(attributes[1]), Int32.Parse(attributes[2]), battle);
                    }

                }
                else // używane na siebie
                {
                    if (isPlayerAttack) // gracz na siebie
                    {
                        ChangeTempStats(false, Int32.Parse(attributes[1]), Int32.Parse(attributes[2]), battle);
                    }
                    else // przeciwnik na siebie
                    {
                        ChangeTempStats(true, Int32.Parse(attributes[1]), Int32.Parse(attributes[2]), battle);
                    }
                }
            }
        }

        private static void ChangeTempStats(bool isEnemyTarget, int statType, int stageValue, Battle battle)
        {
            if (isEnemyTarget)
            {
                switch (statType)
                {
                    case (int)PokemonEnum.Stat.Attack:
                        if (battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.Attack] < 6 || battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.Attack] > -6)
                        {
                            battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.Attack] += stageValue;
                            battle.EnemyPokemon.Stat.Attack = Convert.ToInt32(battle.EnemyPokemonStartStats.Attack * StageHelper.StageToMultipler(battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.Attack]));
                        }
                        break;
                    case (int)PokemonEnum.Stat.Defence:
                        if (battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.Defence] < 6 || battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.Defence] > -6)
                        {
                            battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.Defence] += stageValue;
                            battle.EnemyPokemon.Stat.Defence = Convert.ToInt32(battle.EnemyPokemonStartStats.Defence * StageHelper.StageToMultipler(battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.Defence]));
                        }
                        break;
                    case (int)PokemonEnum.Stat.SpecialAttack:
                        if (battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.SpecialAttack] < 6 || battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.SpecialAttack] > -6)
                        {
                            battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.SpecialAttack] += stageValue;
                            battle.EnemyPokemon.Stat.SpecialAttack = Convert.ToInt32(battle.EnemyPokemonStartStats.SpecialAttack * StageHelper.StageToMultipler(battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.SpecialAttack]));
                        }
                        break;
                    case (int)PokemonEnum.Stat.SpecialDefence:
                        if (battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.SpecialDefence] < 6 || battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.SpecialDefence] > -6)
                        {
                            battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.SpecialDefence] += stageValue;
                            battle.EnemyPokemon.Stat.SpecialDefence = Convert.ToInt32(battle.EnemyPokemonStartStats.SpecialDefence * StageHelper.StageToMultipler(battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.SpecialDefence]));
                        }
                        break;
                    case (int)PokemonEnum.Stat.Speed:
                        if (battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.Speed] < 6 || battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.Speed] > -6)
                        {
                            battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.Speed] += stageValue;
                            battle.EnemyPokemon.Stat.Speed = Convert.ToInt32(battle.EnemyPokemonStartStats.Speed * StageHelper.StageToMultipler(battle.EnemyPokemon.statModifierStages[(int)PokemonEnum.Stat.Speed]));
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (statType)
                {
                    case (int)PokemonEnum.Stat.Attack:
                        if (battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.Attack] < 6 || battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.Attack] > -6)
                        {
                            battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.Attack] += stageValue;
                            battle.Pokemon.Stat.Attack = Convert.ToInt32(battle.PokemonStartStats.Attack * StageHelper.StageToMultipler(battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.Attack]));
                        }
                        break;
                    case (int)PokemonEnum.Stat.Defence:
                        if (battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.Defence] < 6 || battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.Defence] > -6)
                        {
                            battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.Defence] += stageValue;
                            battle.Pokemon.Stat.Defence = Convert.ToInt32(battle.PokemonStartStats.Defence * StageHelper.StageToMultipler(battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.Defence]));
                        }
                        break;
                    case (int)PokemonEnum.Stat.SpecialAttack:
                        if (battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.SpecialAttack] < 6 || battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.SpecialAttack] > -6)
                        {
                            battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.SpecialAttack] += stageValue;
                            battle.Pokemon.Stat.SpecialAttack = Convert.ToInt32(battle.PokemonStartStats.SpecialAttack * StageHelper.StageToMultipler(battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.SpecialAttack]));
                        }
                        break;
                    case (int)PokemonEnum.Stat.SpecialDefence:
                        if (battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.SpecialDefence] < 6 || battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.SpecialDefence] > -6)
                        {
                            battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.SpecialDefence] += stageValue;
                            battle.Pokemon.Stat.SpecialDefence = Convert.ToInt32(battle.PokemonStartStats.SpecialDefence * StageHelper.StageToMultipler(battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.SpecialDefence]));
                        }
                        break;
                    case (int)PokemonEnum.Stat.Speed:
                        if (battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.Speed] < 6 || battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.Speed] > -6)
                        {
                            battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.Speed] += stageValue;
                            battle.Pokemon.Stat.Speed = Convert.ToInt32(battle.PokemonStartStats.Speed * StageHelper.StageToMultipler(battle.Pokemon.statModifierStages[(int)PokemonEnum.Stat.Speed]));
                        }
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
