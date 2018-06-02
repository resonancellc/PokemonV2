﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class PokemonPanel : UserControl
    {
        public int Index { get; set; }
        public bool Selected { get; set; }

        public PokemonPanel()
        {
            InitializeComponent();
        }

        public PokemonPanel(Pokemon pokemon)
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(150, 200, 200);

            this.lblName.Text = pokemon.Name;
            this.lblLevel.Text = pokemon.Level.ToString() + "lvl";
            this.lblHealth.Text = $"{pokemon.HPCurrent}/{pokemon.HPMax}";
            this.barPkmnHealth.Maximum = pokemon.HPMax;
            this.barPkmnHealth.Value = pokemon.HPCurrent;

            this.Selected = false;
        }

        private void PokemonPanel_Load(object sender, EventArgs e)
        {

        }

        private void PokemonPanel_Click(object sender, EventArgs e)
        {
            Selected = true;
            this.BackColor = Color.FromArgb(255, 200, 200);
        }

        private void PokemonPanel_DoubleClick(object sender, EventArgs e)
        {
            Selected = true;
            this.BackColor = Color.FromArgb(255, 200, 200);
            MessageBox.Show($"{Index} pokemon has been picked");
        }
    }
}
