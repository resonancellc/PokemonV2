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
        Label[] statsLabel = new Label[5];

        public PokemonDetailsForm()
        {
            InitializeComponent();
        }

        public PokemonDetailsForm(IPokemon pokemon)
        {
            InitializeComponent();
            statsLabel[0] = lblAtkValue;
            statsLabel[1] = lblDefValue;
            statsLabel[2] = lblSpAtkValue;
            statsLabel[3] = lblSpDefValue;
            statsLabel[4] = lblSpeedValue;

            SetUI(pokemon);
        }

        public void UpdateData(IPokemon pokemon)
        {
            SetUI(pokemon);
        }

        private void SetUI(IPokemon pokemon)
        {
            statsLabel[0].Text = pokemon.Stats.Attack.ToString();
            statsLabel[1].Text = pokemon.Stats.Defence.ToString();
            statsLabel[2].Text = pokemon.Stats.SpecialAttack.ToString();
            statsLabel[3].Text = pokemon.Stats.SpecialDefence.ToString();
            statsLabel[4].Text = pokemon.Stats.Speed.ToString();

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
    }


}
