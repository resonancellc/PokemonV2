using Pokemon.Calculators;
using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class PokemonDetailsForm : Form
    {
        public PokemonDetailsForm()
        {
            InitializeComponent();
        }

        public PokemonDetailsForm(IPokemon pokemon)
        {
            InitializeComponent();

            SetUI(pokemon);
        }

        public void UpdateData(IPokemon pokemon)
        {
            SetUI(pokemon);
        }

        private void SetUI(IPokemon pokemon)
        {
            lblAtkValue.Text = pokemon.Stats.Attack.ToString();
            lblDefValue.Text = pokemon.Stats.Defence.ToString();
            lblSpAtkValue.Text = pokemon.Stats.SpecialAttack.ToString();
            lblSpDefValue.Text = pokemon.Stats.SpecialDefence.ToString();
            lblSpeedValue.Text = pokemon.Stats.Speed.ToString();

            var actualAttack = TempStatsCalculator.GetAttack(pokemon);
            var actualDefence = TempStatsCalculator.GetDefence(pokemon);
            var actualSpAttack = TempStatsCalculator.GetSpecialAttack(pokemon);
            var actualSpDefence = TempStatsCalculator.GetSpecialDefence(pokemon);
            var actualSpeed = TempStatsCalculator.GetSpeed(pokemon);

            lblCurrentAttackValue.Text = $"({actualAttack.ToString()})";
            lblCurrentDefenceValue.Text = $"({actualDefence.ToString()})";
            lblCurrentSpAttackValue.Text = $"({actualSpAttack.ToString()})";
            lblCurrentSpDefenceValue.Text = $"({actualSpDefence.ToString()})";
            lblCurrentSpeedValue.Text = $"({actualSpeed.ToString()})";

            lblCurrentAttackValue.ForeColor = GetColorBasedOnValueDiffrence(actualAttack, pokemon.Stats.Attack);
            lblCurrentDefenceValue.ForeColor = GetColorBasedOnValueDiffrence(actualDefence, pokemon.Stats.Defence);
            lblCurrentSpAttackValue.ForeColor = GetColorBasedOnValueDiffrence(actualSpAttack, pokemon.Stats.SpecialAttack);
            lblCurrentSpDefenceValue.ForeColor = GetColorBasedOnValueDiffrence(actualSpDefence, pokemon.Stats.SpecialDefence);
            lblCurrentSpeedValue.ForeColor = GetColorBasedOnValueDiffrence(actualSpeed, pokemon.Stats.Speed);

            lblName.Text = pokemon.Name;
            lblLevel.Text = pokemon.Condition == 0 ? "L" + pokemon.Level.ToString() : (pokemon.Condition).ToString();
            lblHealth.Text = $"{pokemon.HPCurrent}/{pokemon.HPMax}";
            progressBar1.Maximum = pokemon.HPMax;
            progressBar1.Value = pokemon.HPCurrent;
            pictureBox1.Image = ImageHelper.GetImageById(false, pokemon.ID);
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StaticMain.FormClosed(this);
        }

        private Color GetColorBasedOnValueDiffrence(int currentValue, int defaultValue)
        {
            if (currentValue == defaultValue) return Color.Black;
            return currentValue > defaultValue
                ? Color.Green
                : Color.Red; 
        }
    }
}
