using Pokemon.Models;
using Pokemon.Factory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Pokemon.Views;

namespace Pokemon
{
    public partial class BattleForm : Form, IBattleView
    {
        AttackButton[] attackButtons = new AttackButton[4];
        IBattle _battleController;
        BattleLogController _battleLogController;

        IPokemonParty<IPokemon> _playerParty;
        IPokemonParty<IPokemon> _enemyParty;
        IEquipment _equipment;

        public BattleForm(IPokemonParty<IPokemon> pokemonParty, IPokemonParty<IPokemon> enemyPokemonParty)
        {
            InitializeComponent();

            attackButtons[0] = btnAttack1;
            attackButtons[1] = btnAttack2;
            attackButtons[2] = btnAttack3;
            attackButtons[3] = btnAttack4;

            _battleLogController = new BattleLogController(this);
            _playerParty = pokemonParty;
            _enemyParty = enemyPokemonParty;
            _equipment = EquipmentFactory.CreateEquipment();

            this.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
        }

        private void BattleForm_Load(object sender, EventArgs e)
        {
            _battleController = BattleFactory.CreateBattle(
                _playerParty.GetFirstAlivePokemon(),
                _enemyParty.GetFirstAlivePokemon(),
                this,
                _battleLogController);

            SetAttackButtons(_battleController.PlayerPokemon);

            this.Show();
            RedrawUI();
            _battleLogController.SetText($"Wild {_battleController.EnemyPokemon.Name} appears!");
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
            //AfterWinForm afterWinForm = new AfterWinForm(WinningsCalculator.CalculateWinnings(_playerParty, _equipment));
            ////afterWinForm.Show();
            //this.Hide();

            //if (afterWinForm.ShowDialog() == DialogResult.OK)
            //{
            //    _playerParty.ResetParty();
            //    _enemyParty.ClearEnemyParty();

            //    for (int i = 0; i < _playerParty.Count(); i++)
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

        private void btnAttack_Click(object sender, EventArgs e)
        {
            AttackButton attackButton = sender as AttackButton;
            if (attackButton.Name == "---") return;
            tbLog.Text = "";
            BeginAttackPhase(attackButton.Attack);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
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

        private void RunFromBattle()
        {
            _battleLogController.SetText("You can't run from this battle!");
        }

        private void SwitchPokemon()
        {
            //PokemonPartyForm pokemonPartyForm = new PokemonPartyForm(this, _playerParty);
            //pokemonPartyForm.BringToFront();

            //if (pokemonPartyForm.ShowDialog() == DialogResult.OK)
            //{
            //    IPokemon pokemon = pokemonPartyForm.PickedPokemon;
            //    if (pokemon != _battleController.PlayerPokemon)
            //    {
            //        _battleLogController.ClearText();
            //        tbLog.Text = string.Empty;
            //        pokemon.ResetStats();
            //        _battleController.PlayerPokemon = pokemon;
            //        _playerParty.ActivePokemon = pokemon;
            //        SetAttackButtons(pokemon);
            //        _battleLogController.SetText($"Go {pokemon.Name}!");

            //        IAttack enemyAttack = _battleController.EnemyPokemon.Attacks[GenerateRandomNumber.GetRandomNumber(0, _battleController.EnemyPokemon.Attacks.Count)];
            //        _battleController.PreparePokemonAttack(enemyAttack, _battleController.EnemyPokemon, _battleController.PlayerPokemon);

            //        RedrawUI();
            //        RefreshBattleLog();
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
            ItemForm playerEquipmentForm = new ItemForm(_equipment, _playerParty, this);
            if (!StaticMain.openedForms.Where(x => x.Name == playerEquipmentForm.Name).Any())
            {
                StaticMain.FormOpened(playerEquipmentForm);
                playerEquipmentForm.Location = new Point(this.Location.X + this.Size.Width, this.Location.Y);
                playerEquipmentForm.BringToFront();
                playerEquipmentForm.Show();
            }
        }

        public void AfterItemPickAction(bool hasItemBeenUsed)
        {
            //this.BringToFront();
            //if (hasItemBeenUsed)
            //{
            //    IAttack enemyAttack = _battleController.EnemyPokemon.Attacks[GenerateRandomNumber.GetRandomNumber(0, _battleController.EnemyPokemon.Attacks.Count)];
            //    _battleController.PreparePokemonAttack(enemyAttack, _battleController.EnemyPokemon, _battleController.PlayerPokemon);
            //}

            //RedrawUI();
        }
        
        private void BlockUI()
        {
            foreach (var item in attackButtons)
            {
                item.Enabled = false;
            }
        }

        private void RedrawUI()
        {
            SetPokemonImages(_battleController.PlayerPokemon.ID, _battleController.EnemyPokemon.ID);
            SetPokemonLabels(_battleController.PlayerPokemon, _battleController.EnemyPokemon);
            SetPokemonHealthBars(_battleController.PlayerPokemon, _battleController.EnemyPokemon);
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

        private void SetPokemonHealthBars(IPokemon pokemon, IPokemon enemyPokemon)
        {
            if (pokemon.IsPokemonAlive())
            {
                barPlayerPkmnHealth.Maximum = pokemon.HPMax;
                barPlayerPkmnHealth.Value = pokemon.HPCurrent;
            }
            else
            {
                barPlayerPkmnHealth.Value = 0;
                _battleLogController.SetText($"{pokemon.Name} has fainted!");
                BlockUI();
                SwitchPokemon();
            }
            if (enemyPokemon.IsPokemonAlive())
            {
                barEnemyPkmnHealth.Maximum = enemyPokemon.HPMax;
                barEnemyPkmnHealth.Value = enemyPokemon.HPCurrent;
            }
            else
            {
                barEnemyPkmnHealth.Value = 0;
                _battleLogController.SetText($"{enemyPokemon.Name} has fainted!");
                BlockUI();


                if (!_enemyParty.IsAnyPokemonAlive())
                {
                    BattleResult(true);
                }
                else
                {

                    tbLog.AppendText($"Next pokemon: {_enemyParty.GetFirstAlivePokemon().Name}");
                    AfterBattlePokemonSwitch();
                }
            }
        }

        private void SetPokemonLabels(IPokemon pokemon, IPokemon enemyPokemon)
        {
            lblPlayerPkmnLevel.Text = pokemon.Condition == 0 ? "L" + pokemon.Level.ToString() : (pokemon.Condition).ToString();
            lblPlayerPkmnHealth.Text = $"{pokemon.HPCurrent}/{pokemon.HPMax}";
            lblPlayerPkmnName.Text = pokemon.Name;
            if (!pokemon.IsPokemonAlive())
            {
                lblPlayerPkmnHealth.Text = $"0/{pokemon.HPMax}";
            }
            lblEnemyPkmnLevel.Text = enemyPokemon.Condition == 0 ? "L" + enemyPokemon.Level.ToString() : (enemyPokemon.Condition).ToString();
            lblEnemyPkmnHealth.Text = $"{enemyPokemon.HPCurrent}/{enemyPokemon.HPMax}";
            lblEnemyPkmnName.Text = enemyPokemon.Name;
            if (!enemyPokemon.IsPokemonAlive())
            {
                lblEnemyPkmnHealth.Text = $"0/{enemyPokemon.HPMax}";
            }
        }
        
        private void BeginAttackPhase(IAttack playerAttack)
        {
            //bool battleEnded = false;

            _battleController.PerformAttack(playerAttack);
            BlockUI();
            RedrawUI();
            //if (!battleEnded)
            //{
            //    _battleLogController.SetText($"What will {_battleController.PlayerPokemon.Name} do?");
            //}
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                _battleController.ExecuteNextAction();
            }
            RedrawUI();
        }

        public void AttacksExecutionOver()
        {
            this.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            foreach (var item in attackButtons)
            {
                item.Enabled = true;
            }
            _battleLogController.SetText($"What will {_battleController.PlayerPokemon.Name} do?");
        }

        public void RefreshBattleLog()
        {
            tbLog.Text = _battleLogController.StringBuilder.ToString();
            RedrawUI();
            Refresh();
        }
    }
}
