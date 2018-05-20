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
            Begin();
        }

        private void Begin()
        {
            Pokemon pokemon = PokemonGenerator.GetPokemon(74,50);
            Pokemon enemyPokemon = PokemonGenerator.GetPokemon(74, 50);

            playerPkmnImage.Image = IdToImage(true, pokemon.ID);
            enemyPkmnImage.Image = IdToImage(false, enemyPokemon.ID);

            battle = new Battle(pokemon, enemyPokemon);

            SetAttackButtons(pokemon);
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
            tbLog.Text = "You don't have any other pokemon!";
        }
        private void UseItem()
        {
            tbLog.Text = "You don't have any items!";
        }

        #endregion

        private Bitmap IdToImage(bool isPlayer, int id)
        {
            return ImageHelper.GetImageById(isPlayer, id);
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
            BattleLog.ClearText();
            tbLog.Text = BattleLog.Log;

            Random rand = new Random();
            string attackName = ((Button)sender).Text;
            Attack attack = StaticTypes.attackList.Where(x => x.Name == attackName).First();



            damage = battle.Attack(true, attack);
            BattleLog.AppendText($"{battle.Pokemon.Name} used {attack.Name}! (Dmg: {damage})");
            tbLog.Text = BattleLog.Log;

            UpdateBattleInterface(battle.Pokemon, battle.EnemyPokemon);



            
            if (battle.EnemyPokemon.CheckIfPokemonAlive())
            {
                attack = null;
                while (attack == null)
                {
                    attack = battle.EnemyPokemon.attackPool[rand.Next(0, battle.EnemyPokemon.attackPool.Length)];
                }

                

                damage = battle.Attack(false, attack);
                BattleLog.AppendText($"{battle.EnemyPokemon.Name} used {attack.Name}! (Dmg: {damage})");
                tbLog.Text = BattleLog.Log;
            }

            if (!battle.Pokemon.CheckIfPokemonAlive())
            {
                tbLog.Text = $"{battle.Pokemon.Name} has fainted!";
                BlockUI();
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
