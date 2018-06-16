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
            PokemonParty.AddToParty(PokemonGenerator.GetPokemon(4, 15), true);
            PokemonParty.AddToParty(PokemonGenerator.GetPokemon(12, 20), false);

            CreateBattle();
        }

        private void CreateBattle()
        {
            Pokemon pokemon = PokemonParty.GetPokemon(0, true);
            Pokemon enemyPokemon = PokemonParty.GetPokemon(0, false);

            battle = new Battle(pokemon, enemyPokemon);

            SetAttackButtons(pokemon);

            RedrawUI();

            tbLog.Text = $"Wild {enemyPokemon.Name} appears!";
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
            //SetPkmnCondition(pokemon, enemyPokemon);
        }
        //string shadeName = ((Shade) 1).ToString();  
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
        #endregion

        private void PrintBattleInfo(string battleInfo)
        {
            BattleLog.AppendText(battleInfo);
            tbLog.Text += Environment.NewLine + battleInfo;
        }

        private void PrintBattleInfoDetailed(string battleInfo)
        {
            BattleLog.AppendText(battleInfo);
        }

        private void attackButton_Click(object sender, EventArgs e)
        {
            BeginAttackPhase(sender);
        }

        private void BeginAttackPhase(object sender)
        {
            BattleLog.ClearText();
            

            Attack playerAttack = GeneratePlayerPokemonAttack(sender);
            Attack enemyAttack = GenerateEnemyPokemonAttack();

            if (BattleHelper.IsPlayerPokemonFaster(playerAttack, enemyAttack, battle))
            {
                PlayerPokemonAttack(playerAttack);
                UpdateBattleInterface(battle.Pokemon, battle.EnemyPokemon);
                if (battle.EnemyPokemon.CheckIfPokemonAlive()) EnemyPokemonAttack(enemyAttack);
            }
            else
            {
                EnemyPokemonAttack(enemyAttack);
                UpdateBattleInterface(battle.Pokemon, battle.EnemyPokemon);
                if (battle.Pokemon.CheckIfPokemonAlive()) PlayerPokemonAttack(playerAttack);
            }

            UpdateBattleInterface(battle.Pokemon, battle.EnemyPokemon);
            tbLog.Text = BattleLog.Log;
        }
        private Attack GeneratePlayerPokemonAttack(object sender)
        {
            Attack attack = null;
            return attack = StaticTypes.attackList.Where(x => x.Name == ((Button)sender).Text).First();
        }
        private Attack GenerateEnemyPokemonAttack()
        {
            Attack attack = null;
            Random rand = new Random();
            while (attack == null)
            {
                attack = battle.EnemyPokemon.attackPool[rand.Next(0, battle.EnemyPokemon.attackPool.Length)];
            }
            return attack;
        }

        private void PlayerPokemonAttack(Attack attack)
        {

            if (BattleHelper.ApplyConditionEffect(battle.Pokemon))
            {
                if (!BattleHelper.IsMiss(attack))
                {
                    BattleLog.AppendText($"{battle.Pokemon.Name} used {attack.Name}");
                    int damage = battle.Attack(true, attack);



                    if (attack.BoostStats != string.Empty)
                        BattleHelper.ChangeTempStats(true, attack, battle);

                }
                else BattleLog.AppendText($"{battle.Pokemon.Name} missed!");
            }
        }

        private void EnemyPokemonAttack(Attack attack)
        {
            if (BattleHelper.ApplyConditionEffect(battle.EnemyPokemon))
            {
                // Checking if not miss
                if (!BattleHelper.IsMiss(attack))
                {
                    BattleLog.AppendText($"{battle.EnemyPokemon.Name} used {attack.Name}");
                    int damage = battle.Attack(false, attack);

                    if (attack.BoostStats != string.Empty)
                        BattleHelper.ChangeTempStats(false, attack, battle);

                }
                else BattleLog.AppendText($"{battle.EnemyPokemon.Name} missed!");
            }
        }

        private void tbLog_TextChanged(object sender, EventArgs e)
        {
            tbLog.SelectionStart = tbLog.Text.Length;
            tbLog.ScrollToCaret();
        }
    }
}
