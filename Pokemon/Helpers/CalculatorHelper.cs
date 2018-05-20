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
            if (isPlayerAttack)
            {
                if (attack.BoostStats != string.Empty && attack.BoostStats != null)
                {
                    ChangeTempStats(attack, battle);
                }
                return Convert.ToInt32((((2 * battle.Pokemon.Level / 5) + 2) * attack.Power * ((float)battle.TempPokeStat.Attack / (float)battle.TempEnemyPokeStat.Defence)) / 50);
            }
            else
            {
                if (attack.BoostStats != string.Empty && attack.BoostStats != null)
                {
                    ChangeTempStats(attack, battle);
                }
                return Convert.ToInt32((((2 * battle.EnemyPokemon.Level / 5) + 2) * attack.Power * ((float)battle.TempEnemyPokeStat.Attack / (float)battle.TempPokeStat.Defence)) / 50);
            }
        }

        private static void ChangeTempStats(Attack attack, Battle battle)
        {
            string[] attributes = attack.BoostStats.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); //AttackBoostStatsSplitter();
            if (attributes.Length > 0)
            {
                if (attributes[0] == "enemy")
                {
                    switch (attributes[1])
                    {
                        case "attack":
                            if (battle.TempEnemyPokeStat.Attack > 10 && battle.TempEnemyPokeStat.Attack > battle.EnemyPokemon.Stat.Attack - 25)
                            {
                                battle.TempEnemyPokeStat.Attack += Int32.Parse(attributes[2]) * 5;
                            }
                            break;
                        case "defence":
                            battle.TempEnemyPokeStat.Defence += Int32.Parse(attributes[2]) * 5;
                            break;
                        case "specialAttack":
                            battle.TempEnemyPokeStat.SpecialAttack += Int32.Parse(attributes[2]) * 5;
                            break;
                        case "specialDefence":
                            battle.TempEnemyPokeStat.SpecialDefence += Int32.Parse(attributes[2]) * 5;
                            break;
                        case "speed":
                            battle.TempEnemyPokeStat.Speed += Int32.Parse(attributes[2]) * 5;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (attributes[1])
                    {
                        case "attack":
                            battle.TempPokeStat.Attack += Int32.Parse(attributes[2]) * 5;
                            break;
                        case "defence":
                            battle.TempPokeStat.Defence += Int32.Parse(attributes[2]) * 5;
                            break;
                        case "specialAttack":
                            battle.TempPokeStat.SpecialAttack += Int32.Parse(attributes[2]) * 5;
                            break;
                        case "specialDefence":
                            battle.TempPokeStat.SpecialDefence += Int32.Parse(attributes[2]) * 5;
                            break;
                        case "speed":
                            battle.TempPokeStat.Speed += Int32.Parse(attributes[2]) * 5;
                            break;
                        default:
                            break;
                    }
                }
            }
            
        }
    }
}
