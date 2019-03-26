using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pokemon.Models;

namespace Pokemon
{
    public partial class PokemonPanel : UserControl
    {
        public int Index { get; set; }
        public bool Selected { get; set; }
        IPokemon _pokemon;
        PokemonPartyForm _parentForm;

        public PokemonPanel()
        {
            InitializeComponent();
        }

        public PokemonPanel(IPokemon pokemon)
        {
            InitializeComponent();

            _pokemon = pokemon;
            _parentForm = (PokemonPartyForm)this.Parent;

            this.BackColor = Color.FromArgb(150, 200, 200);

            this.lblName.Text = pokemon.Name;
            this.lblLevel.Text = pokemon.Condition == 0 ? pokemon.Level.ToString() + "lvl" : pokemon.Condition.ToString();
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
                ShowDetails(((PokemonPanel)sender)._pokemon);
            }
            _parentForm = (PokemonPartyForm)this.Parent;
            _parentForm.UnselectAll();
            MakePanelSelected(true);
        }

        private void PokemonPanel_DoubleClick(object sender, EventArgs e)
        {
            Selected = true;
            _parentForm = (PokemonPartyForm)this.Parent;
            this.BackColor = Color.FromArgb(255, 200, 200);
            //PokemonParty.ActivePokemonIndex = ((PokemonPanel)sender).Index;
            _parentForm.PokemonPicked(_pokemon);

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

        private void ShowDetails(IPokemon pokemon)
        {
            _parentForm = (PokemonPartyForm)this.Parent;
            PokemonDetailsForm pokemonDetailsForm = new PokemonDetailsForm(pokemon);
            if (!StaticMain.openedForms.Where(x=>x.Name == pokemonDetailsForm.Name).Any())
            {
                pokemonDetailsForm.Show();
                pokemonDetailsForm.Location = new Point(_parentForm.Location.X + _parentForm.Width, _parentForm.Location.Y);
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
