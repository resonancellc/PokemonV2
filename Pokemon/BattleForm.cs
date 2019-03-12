using Pokemon.Models;
using Pokemon.Factory;
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
        AttackButton[] attackButtons = new AttackButton[4];
        IBattle battle;
        PokemonPartyForm pokemonPartyForm = null;

        IPokemonParty<IPokemon> _playerParty;
        IPokemonParty<IPokemon> _enemyParty;

        public BattleForm(List<IPokemon> pokemonList, int teamSize)
        {
            InitializeComponent();
            PlayerEquipment.InitPlayerEquipment();


            attackButtons[0] = btnAttack1;
            attackButtons[1] = btnAttack2;
            attackButtons[2] = btnAttack3;
            attackButtons[3] = btnAttack4;

            _playerParty = PokemonPartyFactory.CreatePokemonParty(true, pokemonList);
            _enemyParty = PokemonPartyFactory.CreateEnemyPokemonParty(teamSize, pokemonList.First().Level);
        }

        private void BattleForm_Load(object sender, EventArgs e)
        {
            battle = BattleFactory.CreateBattle(_playerParty.GetFirstAlivePokemon(), _enemyParty.GetFirstAlivePokemon());
            SetAttackButtons(battle.Pokemon);

            this.Show();
            RedrawUI();
            tbLog.Text = $"Wild {battle.EnemyPokemon.Name} appears!";
        }

        private void BattleResult(bool isWin)
        {
            if (isWin)
            {
                BattleWon();
            }
        }
        private void BattleWon()
        {
            
            //AfterWinForm afterWinForm = new AfterWinForm(CalculatorHelper.CalculateWinnings());
            ////afterWinForm.Show();
            //this.Hide();

            //if (afterWinForm.ShowDialog() == DialogResult.OK)
            //{
            //    PokemonParty.ResetParty();
            //    PokemonParty.ClearEnemyParty();

            //    for (int i = 0; i < PokemonParty.playerPokemons.Length; i++)
            //    {
            //        if (PokemonParty.playerPokemons[i] == null) break;
            //        PokemonParty.AddToParty(PokemonGenerator.GetPokemon(PokemonParty.GetPokemon(0, true).Level), false);
            //    }
                
            //    afterWinForm.Dispose();
            //    //AfterBattlePokemonSwitch();
            //    CreateBattle(PokemonParty.GetPokemon(PokemonParty.ActivePokemonIndex, true), PokemonParty.GetFirstPokemonAlive(false));
            //}
        }

        private void SetAttackButtons(IPokemon pokemon)
        {
            foreach (AttackButton attackButton in attackButtons)
            {
                attackButton.Text = "---";
                attackButton.Enabled = false;
            }
            if (pokemon.Attacks != null)
            {
                for (int i = 0; i < pokemon.Attacks.Count; i++)
                {
                    attackButtons[i].Attack = pokemon.Attacks[i];
                    attackButtons[i].Text = attackButtons[i].Attack.Name;

                    if (attackButtons[i].Attack.Name.Length > 9)
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
            tbLog.Text = "";
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
            ShowItemForm();
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
            //pokemonPartyForm = new PokemonPartyForm(this);
            //pokemonPartyForm.BringToFront();

            //if (pokemonPartyForm.ShowDialog() == DialogResult.OK)
            //{
            //    Pokemon pokemon = pokemonPartyForm.PickedPokemon;
            //    if (pokemon != battle.Pokemon)
            //    {
            //        BattleLog.ClearText();
            //        tbLog.Text = string.Empty;
            //        pokemon.ResetStats();
            //        this.battle.Pokemon = pokemon;
            //        SetAttackButtons(pokemon);
            //        BattleLog.AppendText($"Go {pokemon.Name}!");
            //        Attack enemyAttack = battle.GeneratePokemonAttack(false);
            //        battle.PokemonAttack(enemyAttack, battle.EnemyPokemon, false);
            //        RedrawUI();
            //        tbLog.Text = BattleLog.Log;
            //    }
            //}
        }
        private void AfterBattlePokemonSwitch()
        {
            //if (PokemonParty.playerPokemons[1] != null)
            //{
            //    if (MessageBox.Show("Switch pokemon?", "Pokemon", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        SwitchPokemon();
            //    }
            //}
            //battle = BattleFactory.CreateBattle(_playerParty.GetFirstAlivePokemon(), _enemyParty.GetFirstAlivePokemon());
            //CreateBattle(PokemonParty.GetPokemon(PokemonParty.ActivePokemonIndex, true), PokemonParty.GetFirstPokemonAlive(false));
        }

        private void ShowItemForm()
        {
            //ItemForm playerEquipmentForm = new ItemForm(false, this);
            //if (!StaticMain.openedForms.Where(x => x.Name == playerEquipmentForm.Name).Any())
            //{
            //    StaticMain.FormOpened(playerEquipmentForm);
            //    playerEquipmentForm.Location = new Point(this.Location.X + this.Size.Width, this.Location.Y);
            //    playerEquipmentForm.BringToFront();
            //    playerEquipmentForm.Show();
            //}
        }

        public void UseItem(int itemID)
        {
            //BattleLog.ClearText();
            //if (ItemHelper.UseItem(battle.Pokemon, itemID))
            //{
            //    BattleLog.AppendText($"You used {ItemHelper.GetItemNameByID(itemID+1)} on {battle.Pokemon.Name}");
            //    Attack enemyAttack = battle.GeneratePokemonAttack(false);
            //    battle.PokemonAttack(enemyAttack, battle.EnemyPokemon, false);
            //    RedrawUI();
            //    tbLog.Text = BattleLog.Log;
            //}
            //else tbLog.Text = "It's not the time to use this item";

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
            SetPkmnLabels(battle.Pokemon, battle.EnemyPokemon);
            SetPkmnHealthBars(battle.Pokemon, battle.EnemyPokemon);
            tbLog.Refresh();
        }

        private void SetPokemonImages(int playerPokemonID, int enemyPokemonID)
        {
            playerPkmnImage.Image = ImageHelper.GetImageById(true, playerPokemonID);
            enemyPkmnImage.Image = ImageHelper.GetImageById(false, enemyPokemonID);
        }

        private void SetPkmnHealthBars(IPokemon pokemon)
        {
            barPlayerPkmnHealth.Maximum = pokemon.HPMax;
            barPlayerPkmnHealth.Value = pokemon.HPCurrent;
        }

        private void SetPkmnHealthBars(IPokemon pokemon, IPokemon enemyPokemon)
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
                SwitchPokemon();
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


                if (!PokemonParty.CheckIfAnyPokemonAlive(false))
                {
                    BattleResult(true);
                }
                else
                {

                    tbLog.AppendText($"Next pokemon: {PokemonParty.GetFirstPokemonAlive(false).Name}");
                    AfterBattlePokemonSwitch();
                }
                
            }

        }
        private void SetPkmnLabels(IPokemon pokemon, IPokemon enemyPokemon)
        {
            lblPlayerPkmnLevel.Text = pokemon.Condition == 0 ? "L" + pokemon.Level.ToString() : (pokemon.Condition).ToString();
            lblPlayerPkmnHealth.Text = $"{pokemon.HPCurrent}/{pokemon.HPMax}";
            lblPlayerPkmnName.Text = pokemon.Name;
            if (!pokemon.CheckIfPokemonAlive())
            {
                lblPlayerPkmnHealth.Text = $"0/{pokemon.HPMax}";
            }
            lblEnemyPkmnLevel.Text = enemyPokemon.Condition == 0 ? "L" + enemyPokemon.Level.ToString() : (enemyPokemon.Condition).ToString();
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
            bool battleEnded = false;
            BattleLog.ClearText();

            IAttack playerAttack = battle.GeneratePokemonAttack(true, sender);
            IAttack enemyAttack = battle.EnemyPokemon.Attacks[GenerateRandomNumber.GetRandomNumber(0,4)];

            if (BattleHelper.IsPlayerPokemonFaster(playerAttack, enemyAttack, battle))
            {
                battle.PokemonAttack(playerAttack, battle.Pokemon, true);
                if (battle.EnemyPokemon.CheckIfPokemonAlive())
                    battle.PokemonAttack(enemyAttack, battle.EnemyPokemon, false);
                else
                    battleEnded = true;
            }
            else
            {
                battle.PokemonAttack(enemyAttack, battle.EnemyPokemon, false);
                if (battle.Pokemon.CheckIfPokemonAlive())
                    battle.PokemonAttack(playerAttack, battle.Pokemon, true);
                else
                    battleEnded = true;
            }
            RedrawUI();
            if (!battleEnded) tbLog.Text = BattleLog.Log;

        }



        

        
    }
}
