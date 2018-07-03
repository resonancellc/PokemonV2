using System;
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
        public Pokemon Pokemon { get; set; }
        PokemonPartyForm parentForm;

        public PokemonPanel()
        {
            InitializeComponent();
        }

        public PokemonPanel(Pokemon pokemon)
        {
            InitializeComponent();

            this.Pokemon = pokemon;
            parentForm = (PokemonPartyForm)this.Parent;

            this.BackColor = Color.FromArgb(150, 200, 200);

            this.lblName.Text = pokemon.Name;
            this.lblLevel.Text = pokemon.Level.ToString() + "lvl";
            this.lblHealth.Text = $"{pokemon.HPCurrent}/{pokemon.HPMax}";
            this.barPkmnHealth.Maximum = pokemon.HPMax;
            this.barPkmnHealth.Value =  pokemon.HPCurrent > 0 ? pokemon.HPCurrent : 0;

            this.Selected = false;
        }

        private void PokemonPanel_Load(object sender, EventArgs e)
        {

        }

        private void PokemonPanel_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ShowDetails(((PokemonPanel)sender).Pokemon);
            }
            parentForm = (PokemonPartyForm)this.Parent;
            parentForm.UnselectAll();
            MakePanelSelected(true);  
        }

        private void PokemonPanel_DoubleClick(object sender, EventArgs e)
        {
            Selected = true;
            parentForm = (PokemonPartyForm)this.Parent;
            this.BackColor = Color.FromArgb(255, 200, 200);
            PokemonParty.ActivePokemonIndex = ((PokemonPanel)sender).Index;
            parentForm.PokemonPicked(Pokemon);

            PokemonDetailsForm pokemonDetailsForm = new PokemonDetailsForm();
            if (StaticMain.openedForms.Where(x => x.Name == pokemonDetailsForm.Name).Any())
            {
                pokemonDetailsForm = StaticMain.openedForms.Where(x => x.Name == pokemonDetailsForm.Name).First() as PokemonDetailsForm;
                pokemonDetailsForm.Close();
            }
        }

        private void MakePanelSelected(bool isSelected)
        {
            Selected = isSelected;
            if (isSelected) this.BackColor = Color.FromArgb(255, 200, 200);
            else this.BackColor = Color.FromArgb(150, 200, 200);
        }

        private void ShowDetails(Pokemon pokemon)
        {
            parentForm = (PokemonPartyForm)this.Parent;
            PokemonDetailsForm pokemonDetailsForm = new PokemonDetailsForm(pokemon);
            if (!StaticMain.openedForms.Where(x=>x.Name == pokemonDetailsForm.Name).Any())
            {
                pokemonDetailsForm.Show();
                pokemonDetailsForm.Location = new Point(parentForm.Location.X + parentForm.Width, parentForm.Location.Y);
                StaticMain.FormOpened(pokemonDetailsForm);
            }
            else
            {
                pokemonDetailsForm = StaticMain.openedForms.Where(x => x.Name == pokemonDetailsForm.Name).First() as PokemonDetailsForm;
                pokemonDetailsForm.UpdateData(pokemon);
            }
            
        }
    }
}
