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
        PokemonPartyForm pokemonPartyForm = null;

        public BattleForm(Pokemon[] pokemonList)
        {
            InitializeComponent();
            attackButtons[0] = btnAttack1;
            attackButtons[1] = btnAttack2;
            attackButtons[2] = btnAttack3;
            attackButtons[3] = btnAttack4;
            PokemonParty.AddManyToParty(pokemonList, true);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            PokemonParty.AddToParty(PokemonGenerator.GetPokemon(PokemonParty.GetPokemon(0, true).Level), false);
            CreateBattle();
        }

        private void CreateBattle()
        {
            Pokemon pokemon = PokemonParty.GetPokemon(0, true);
            Pokemon enemyPokemon = PokemonParty.GetPokemon(0, false);

            battle = new Battle(pokemon, enemyPokemon);

            SetAttackButtons(pokemon);

            this.Show();
            RedrawUI();

            tbLog.Text = $"Wild {enemyPokemon.Name} appears!";
        }
        private void GenerateNewPokemon()
        {
            AfterWinForm afterWinForm = new AfterWinForm();
            //afterWinForm.Show();
            this.Hide();

            if (afterWinForm.ShowDialog() == DialogResult.OK)
            {
                PokemonParty.HealAll();
                PokemonParty.enemyPokemons[0] = null;
                PokemonParty.AddToParty(PokemonGenerator.GetPokemon(PokemonParty.GetPokemon(0, true).Level), false);
                afterWinForm.Dispose();
                CreateBattle();
            }
        }
        private void SetAttackButtons(Pokemon pokemon)
        {
            foreach (Button attackButton in attackButtons)
            {
                attackButton.Text = "---";
                attackButton.Enabled = false;
            }
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
        private void btnAttack_Click(object sender, EventArgs e)
        {
            BeginAttackPhase(sender);
        }
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
        private void tbLog_TextChanged(object sender, EventArgs e)
        {
            tbLog.SelectionStart = tbLog.Text.Length;
            tbLog.ScrollToCaret();
        }
        #endregion

        #region BattleActionButtons

        private void RunFromBattle()
        {
            tbLog.Text = "You can't run from this battle!";
        }
        private void SwitchPokemon()
        {
            pokemonPartyForm = new PokemonPartyForm();

            pokemonPartyForm.Location = new Point(this.Location.X + this.Size.Width, this.Location.Y);
            pokemonPartyForm.BringToFront();

            if (pokemonPartyForm.ShowDialog() == DialogResult.OK)
            {
                Pokemon pokemon = pokemonPartyForm.Pokemon;
                this.battle.Pokemon = pokemon;
                SetAttackButtons(pokemon);
                //EnemyPokemonAttack();
                RedrawUI();
            }
        }
        private void UseItem()
        {
            tbLog.Text = "You don't have any items!";
        }

        #endregion

        #region UI etc
        private void BlockUI()
        {
            foreach (var item in attackButtons)
            {
                item.Enabled = false;
            }
        }
        private void RedrawUI()
        {
            SetPokemonImages(battle.Pokemon.ID, battle.EnemyPokemon.ID);
            SetPkmnHealthBars(battle.Pokemon, battle.EnemyPokemon);
            SetPkmnLabels(battle.Pokemon, battle.EnemyPokemon);
        }

        private void SetPokemonImages(int playerPokemonID, int enemyPokemonID)
        {
            playerPkmnImage.Image = ImageHelper.GetImageById(true, playerPokemonID);
            enemyPkmnImage.Image = ImageHelper.GetImageById(false, enemyPokemonID);
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
                BattleLog.AppendText($"{pokemon.Name} has fainted!");
                BlockUI();
                
            }
            if (enemyPokemon.CheckIfPokemonAlive())
            {
                barEnemyPkmnHealth.Maximum = enemyPokemon.HPMax;
                barEnemyPkmnHealth.Value = enemyPokemon.HPCurrent;
            }
            else
            {
                barEnemyPkmnHealth.Value = 0;
                BattleLog.AppendText($"{enemyPokemon.Name} has fainted!");
                BlockUI();
                GenerateNewPokemon();
            }

        }
        private void SetPkmnLabels(Pokemon pokemon, Pokemon enemyPokemon)
        {
            lblPlayerPkmnLevel.Text = pokemon.Condition == 0 ? "L" + pokemon.Level.ToString() : ((PokemonEnum.Condition)pokemon.Condition).ToString();
            lblPlayerPkmnHealth.Text = $"{pokemon.HPCurrent}/{pokemon.HPMax}";
            lblPlayerPkmnName.Text = pokemon.Name;
            if (!pokemon.CheckIfPokemonAlive())
            {
                lblPlayerPkmnHealth.Text = $"0/{pokemon.HPMax}";
            }
            lblEnemyPkmnLevel.Text = enemyPokemon.Condition == 0 ? "L" + enemyPokemon.Level.ToString() : ((PokemonEnum.Condition)enemyPokemon.Condition).ToString();
            lblEnemyPkmnHealth.Text = $"{enemyPokemon.HPCurrent}/{enemyPokemon.HPMax}";
            lblEnemyPkmnName.Text = enemyPokemon.Name;
            if (!enemyPokemon.CheckIfPokemonAlive())
            {
                lblEnemyPkmnHealth.Text = $"0/{enemyPokemon.HPMax}";
            }
        }
        private void PrintBattleInfo(string battleInfo)
        {
            BattleLog.AppendText(battleInfo);
            tbLog.Text += Environment.NewLine + battleInfo;
        }

        #endregion

        private void BeginAttackPhase(object sender)
        {
            BattleLog.ClearText();
            
            Attack playerAttack = battle.GeneratePokemonAttack(true, sender);
            Attack enemyAttack = battle.GeneratePokemonAttack(false);

            if (BattleHelper.IsPlayerPokemonFaster(playerAttack, enemyAttack, battle))
            {
                battle.PokemonAttack(playerAttack, battle.Pokemon, true);
#warning na tą chwilę wygenerowaliśmy już nowego pokemona - bug
                if (battle.EnemyPokemon.CheckIfPokemonAlive()) battle.PokemonAttack(enemyAttack, battle.EnemyPokemon, false);
            }
            else
            {
                battle.PokemonAttack(enemyAttack, battle.EnemyPokemon, false);
                if (battle.Pokemon.CheckIfPokemonAlive()) battle.PokemonAttack(playerAttack, battle.Pokemon, true);
            }

            RedrawUI();
            tbLog.Text = BattleLog.Log;
        }



        

        
    }
}
