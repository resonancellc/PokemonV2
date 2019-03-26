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
using Pokemon.Calculators;

namespace Pokemon
{
    public partial class BattleForm : Form
    {
        AttackButton[] attackButtons = new AttackButton[4];
        IBattle battle;

        IPokemonParty<IPokemon> _playerParty;
        IPokemonParty<IPokemon> _enemyParty;
        IEquipment _equipment;

        public BattleForm(IList<IPokemon> pokemonList, int teamSize)
        {
            InitializeComponent();

            attackButtons[0] = btnAttack1;
            attackButtons[1] = btnAttack2;
            attackButtons[2] = btnAttack3;
            attackButtons[3] = btnAttack4;

            _playerParty = PokemonPartyFactory.CreatePokemonParty(true, pokemonList);
            _enemyParty = PokemonPartyFactory.CreateEnemyPokemonParty(teamSize, pokemonList.First().Level);
            _equipment = EquipmentFactory.CreateEquipment();
        }

        private void BattleForm_Load(object sender, EventArgs e)
        {
            battle = BattleFactory.CreateBattle(_playerParty.GetFirstAlivePokemon(), _enemyParty.GetFirstAlivePokemon());
            SetAttackButtons(battle.PlayerPokemon);

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

        #region events
        private void btnAttack_Click(object sender, EventArgs e)
        {
            AttackButton attackButton = sender as AttackButton;
            tbLog.Text = "";
            BeginAttackPhase(attackButton.Attack);
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
            PokemonPartyForm pokemonPartyForm = new PokemonPartyForm(this, _playerParty);
            pokemonPartyForm.BringToFront();

            if (pokemonPartyForm.ShowDialog() == DialogResult.OK)
            {
                IPokemon pokemon = pokemonPartyForm.PickedPokemon;
                if (pokemon != battle.PlayerPokemon)
                {
                    BattleLog.ClearText();
                    tbLog.Text = string.Empty;
                    pokemon.ResetStats();
                    this.battle.PlayerPokemon = pokemon;
                    _playerParty.ActivePokemon = pokemon;
                    SetAttackButtons(pokemon);
                    BattleLog.AppendText($"Go {pokemon.Name}!");

                    IAttack enemyAttack = battle.EnemyPokemon.Attacks[GenerateRandomNumber.GetRandomNumber(0, battle.EnemyPokemon.Attacks.Count)];
                    battle.PreparePokemonAttack(enemyAttack, battle.EnemyPokemon, battle.PlayerPokemon);

                    RedrawUI();
                    tbLog.Text = BattleLog.Log;
                }
            }
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
            this.BringToFront();
            if (hasItemBeenUsed)
            {
                IAttack enemyAttack = battle.EnemyPokemon.Attacks[GenerateRandomNumber.GetRandomNumber(0, battle.EnemyPokemon.Attacks.Count)];
                battle.PreparePokemonAttack(enemyAttack, battle.EnemyPokemon, battle.PlayerPokemon);
            }

            RedrawUI();
            tbLog.Text = BattleLog.Log;
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
            SetPokemonImages(battle.PlayerPokemon.ID, battle.EnemyPokemon.ID);
            SetPkmnLabels(battle.PlayerPokemon, battle.EnemyPokemon);
            SetPkmnHealthBars(battle.PlayerPokemon, battle.EnemyPokemon);
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
            if (pokemon.IsPokemonAlive())
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
            if (enemyPokemon.IsPokemonAlive())
            {
                barEnemyPkmnHealth.Maximum = enemyPokemon.HPMax;
                barEnemyPkmnHealth.Value = enemyPokemon.HPCurrent;
            }
            else
            {
                barEnemyPkmnHealth.Value = 0;
                BattleLog.AppendText($"{enemyPokemon.Name} has fainted!");
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
        private void SetPkmnLabels(IPokemon pokemon, IPokemon enemyPokemon)
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
        private void PrintBattleInfo(string battleInfo)
        {
            BattleLog.AppendText(battleInfo);
            tbLog.Text += Environment.NewLine + battleInfo;
        }

        #endregion

        private void BeginAttackPhase(IAttack attack)
        {
            bool battleEnded = false;
            BattleLog.ClearText();

            IAttack playerAttack = attack;
            IAttack enemyAttack = battle.EnemyPokemon.Attacks[GenerateRandomNumber.GetRandomNumber(0,battle.EnemyPokemon.Attacks.Count)];

            if (BattleHelper.IsPlayerPokemonFaster(attack.AdditionalEffects, enemyAttack.AdditionalEffects, battle))
            {
                battle.PreparePokemonAttack(attack, battle.PlayerPokemon, battle.EnemyPokemon);

                if (battle.EnemyPokemon.IsPokemonAlive())
                    battle.PreparePokemonAttack(enemyAttack, battle.EnemyPokemon, battle.PlayerPokemon);
                else
                    battleEnded = true;
            }
            else
            {
                battle.PreparePokemonAttack(enemyAttack, battle.EnemyPokemon, battle.PlayerPokemon);

                if (battle.PlayerPokemon.IsPokemonAlive())
                    battle.PreparePokemonAttack(attack, battle.PlayerPokemon, battle.EnemyPokemon);
                else
                    battleEnded = true;
            }
            RedrawUI();
            if (!battleEnded) tbLog.Text = BattleLog.Log;
        }  
    }
}
