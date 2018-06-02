using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class BattleForm : Form
    {
        Button[] attackButtons = new Button[4];
        Battle battle;

        public BattleForm()
        {
            InitializeComponent();
            attackButtons[0] = btnAttack1;
            attackButtons[1] = btnAttack2;
            attackButtons[2] = btnAttack3;
            attackButtons[3] = btnAttack4;

            StaticSQL.SetConnectionString("Server=DESKTOP-6CLE20J\\SQLEXPRESS;Database=Pokemon;Trusted_Connection=true;");

            StaticTypes.FillPokemonList();
            StaticTypes.FillPokemonStatsList();
            StaticTypes.FillAttackList();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Pokemon[] pokemons = { PokemonGenerator.GetPokemon(4, 14), PokemonGenerator.GetPokemon(74, 12) };
            PokemonParty.AddManyToParty(pokemons, true);
            PokemonParty.AddToParty(PokemonGenerator.GetPokemon(7, 5), false);

            Begin();
        }

        private void Begin()
        {
            Pokemon pokemon = PokemonParty.GetPokemon(0, true);
            Pokemon enemyPokemon = PokemonParty.GetPokemon(0, false);

            battle = new Battle(pokemon, enemyPokemon);
            
            SetAttackButtons(pokemon);
            SetPokemonImages(pokemon.ID, enemyPokemon.ID);
            SetPkmnHealthBars(pokemon, enemyPokemon);
            SetPkmnLabels(pokemon, enemyPokemon);

            tbLog.Text = $"Wild {enemyPokemon.Name} appears!";
        }

        private void SetAttackButtons(Pokemon pokemon)
        {
            for (int i = 0; i < pokemon.attackPool.Length; i++)
            {
                if (pokemon.attackPool[i] != null)
                {
                    attackButtons[i].Text = pokemon.attackPool[i].Name;
                    if (pokemon.attackPool[i].Name.Length > 9)
                    {
                        attackButtons[i].Font = new System.Drawing.Font("Unispace", 8, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    }
                    attackButtons[i].Enabled = true;
                }
            }
            
        }

        #region events

        private void btnRun_Click(object sender, EventArgs e)
        {
            RunFromBattle();
        }
        private void btnSwitchPkmn_Click(object sender, EventArgs e)
        {
            SwitchPokemon();
        }
        private void btnItem_Click(object sender, EventArgs e)
        {
            UseItem();
        }
  
        #endregion

        #region BattleActionButtons

        private void RunFromBattle()
        {
            tbLog.Text = "You can't run from this battle!";
        }
        private void SwitchPokemon()
        {
            PokemonPartyForm pokemonPartyForm = new PokemonPartyForm();
            pokemonPartyForm.Show();
            pokemonPartyForm.Location = new Point(this.Location.X + this.Size.Width, this.Location.Y);



            //int index = 1;
            //Pokemon pokemon = playerParty.GetPokemon(index);
            //SetAttackButtons(pokemon);
            //battle.SetPokemon(pokemon);
            //RedrawUI();
        }
        private void UseItem()
        {
            tbLog.Text = "You don't have any items!";
        }

        #endregion

        private void RedrawUI()
        {
            SetPokemonImages(battle.Pokemon.ID, battle.EnemyPokemon.ID);
            UpdateBattleInterface(battle.Pokemon, battle.EnemyPokemon);
            SetPkmnHealthBars(battle.Pokemon, battle.EnemyPokemon);
            SetPkmnLabels(battle.Pokemon, battle.EnemyPokemon);
        }

        private void SetPokemonImages(int playerPokemonID, int enemyPokemonID)
        {
            playerPkmnImage.Image = ImageHelper.GetImageById(true, playerPokemonID);
            enemyPkmnImage.Image = ImageHelper.GetImageById(false, enemyPokemonID);
        }

        private void UpdateBattleInterface(Pokemon pokemon, Pokemon enemyPokemon)
        {
            SetPkmnHealthBars(pokemon, enemyPokemon);
            SetPkmnLabels(pokemon, enemyPokemon);
        }

        private void SetPkmnHealthBars(Pokemon pokemon, Pokemon enemyPokemon)
        {
            if (pokemon.CheckIfPokemonAlive())
            {
                barPlayerPkmnHealth.Maximum = pokemon.HPMax;
                barPlayerPkmnHealth.Value = pokemon.HPCurrent;
            }
            else
            {
                barPlayerPkmnHealth.Value = 0;
            }
            if (enemyPokemon.CheckIfPokemonAlive())
            {
                barEnemyPkmnHealth.Maximum = enemyPokemon.HPMax;
                barEnemyPkmnHealth.Value = enemyPokemon.HPCurrent;
            }
            else
            {
                barEnemyPkmnHealth.Value = 0;
            }
            
        }

        private void SetPkmnLabels(Pokemon pokemon, Pokemon enemyPokemon)
        {
            lblPlayerPkmnHealth.Text = $"{pokemon.HPCurrent}/{pokemon.HPMax}";
            lblPlayerPkmnName.Text = $"{pokemon.Name} L{pokemon.Level.ToString()}";
            if (!pokemon.CheckIfPokemonAlive())
            {
                lblPlayerPkmnHealth.Text = $"0/{pokemon.HPMax}";
            }
            lblEnemyPkmnHealth.Text = $"{enemyPokemon.HPCurrent}/{enemyPokemon.HPMax}";
            lblEnemyPkmnName.Text = $"{enemyPokemon.Name} L{enemyPokemon.Level.ToString()}";
            if (!enemyPokemon.CheckIfPokemonAlive())
            {
                lblEnemyPkmnHealth.Text = $"0/{enemyPokemon.HPMax}";
            }
        }

        private void attackButton_Click(object sender, EventArgs e)
        {
            int damage;
            Random rand = new Random();
            Attack attack = null;
            BattleLog.ClearText();
            tbLog.Text = BattleLog.Log;

            if (battle.EnemyPokemon.Stat.Stats[4] > battle.Pokemon.Stat.Stats[4]) // enemy pokemon is faster
            {
                attack = null;

                while (attack == null)
                {
                    attack = battle.EnemyPokemon.attackPool[rand.Next(0, battle.EnemyPokemon.attackPool.Length)];
                }

                if (rand.Next(0,100) < attack.Accuracy)
                {
                    damage = battle.Attack(false, attack);
                    BattleLog.AppendText($"Enemy {battle.EnemyPokemon.Name} used {attack.Name}! (Dmg: {damage})");
                    tbLog.Text = BattleLog.Log;          
                }
                else
                {
                    BattleLog.AppendText($"{battle.EnemyPokemon.Name} missed!");
                    tbLog.Text = BattleLog.Log;
                }

                if (battle.Pokemon.CheckIfPokemonAlive())
                {
                    attack = StaticTypes.attackList.Where(x => x.Name == ((Button)sender).Text).First();
                    if (rand.Next(0, 100) < attack.Accuracy)
                    {
                        damage = battle.Attack(true, attack);
                        BattleLog.AppendText($"Your {battle.Pokemon.Name} used {attack.Name}! (Dmg: {damage})");
                        tbLog.Text = BattleLog.Log;
                    }
                    else
                    {
                        BattleLog.AppendText($"{battle.Pokemon.Name} missed!");
                        tbLog.Text = BattleLog.Log;
                    }
                }
                else
                {
                    tbLog.Text = $"{battle.Pokemon.Name} has fainted!";
                    BlockUI();
                }

            }
            else
            {
                attack = StaticTypes.attackList.Where(x => x.Name == ((Button)sender).Text).First();
                if (rand.Next(0, 100) < attack.Accuracy)
                {
                    damage = battle.Attack(true, attack);
                    BattleLog.AppendText($"Your {battle.Pokemon.Name} used {attack.Name}! (Dmg: {damage})");
                    tbLog.Text = BattleLog.Log;
                }
                else
                {
                    BattleLog.AppendText($"{battle.Pokemon.Name} missed!");
                    tbLog.Text = BattleLog.Log;
                }

                if (battle.EnemyPokemon.CheckIfPokemonAlive())
                {
                    attack = null;

                    while (attack == null)
                    {
                        attack = battle.EnemyPokemon.attackPool[rand.Next(0, battle.EnemyPokemon.attackPool.Length)];
                    }
                    if (rand.Next(0, 100) < attack.Accuracy)
                    {
                        damage = battle.Attack(false, attack);
                        BattleLog.AppendText($"Enemy {battle.EnemyPokemon.Name} used {attack.Name}! (Dmg: {damage})");
                        tbLog.Text = BattleLog.Log;
                    }
                    else
                    {
                        BattleLog.AppendText($"{battle.EnemyPokemon.Name} missed!");
                        tbLog.Text = BattleLog.Log;
                    }
                    
                }
                else
                {
                    tbLog.Text = $"{battle.EnemyPokemon.Name} has fainted!";
                }
            }

            
            UpdateBattleInterface(battle.Pokemon, battle.EnemyPokemon);
        }

        private void BlockUI()
        {
            foreach (var item in attackButtons)
            {
                item.Enabled = false;
            }
        }
    }
}
