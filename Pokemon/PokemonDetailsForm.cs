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

        public PokemonDetailsForm(Pokemon pokemon)
        {
            InitializeComponent();
            statsLabel[0] = lblAtkValue;
            statsLabel[1] = lblDefValue;
            statsLabel[2] = lblSpAtkValue;
            statsLabel[3] = lblSpDefValue;
            statsLabel[4] = lblSpeedValue;

            SetUI(pokemon);
        }

        public void UpdateData(Pokemon pokemon)
        {
            SetUI(pokemon);
        }

        private void SetUI(Pokemon pokemon)
        {
            //for (int i = 0; i < statsLabel.Length; i++)
            //{
            //    statsLabel[i].Text = pokemon.StartStats.Stats[i].ToString();
            //}

            //lblName.Text = pokemon.Name;
            //lblLevel.Text = pokemon.Condition == 0 ? "L" + pokemon.Level.ToString() : ((PokemonEnum.Condition)pokemon.Condition).ToString();
            //lblHealth.Text = $"{pokemon.HPCurrent}/{pokemon.HPMax}";
            //progressBar1.Maximum = pokemon.HPMax;
            //progressBar1.Value = pokemon.HPCurrent;
            //pictureBox1.Image = ImageHelper.GetImageById(false, pokemon.ID);
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StaticMain.FormClosed(this);
        }
    }


}
