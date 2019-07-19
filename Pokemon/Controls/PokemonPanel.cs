using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Pokemon.Models;

namespace Pokemon
{
    public partial class PokemonPanel : UserControl
    {
        public int Index { get; set; }

        public bool Selected { get; set; }

        private readonly IPokemon _pokemon;
        PokemonPartyForm _parentForm;

        public PokemonPanel()
        {
            InitializeComponent();
        }

        public PokemonPanel(IPokemon pokemon)
        {
            InitializeComponent();

            _pokemon = pokemon;
            _parentForm = (PokemonPartyForm)Parent;

            BackColor = Color.FromArgb(150, 200, 200);

            lblName.Text = pokemon.Name;
            lblLevel.Text = pokemon.Condition == 0 ? pokemon.Level.ToString() + "lvl" : pokemon.Condition.ToString();
            lblHealth.Text = $"{pokemon.HPCurrent}/{pokemon.HPMax}";
            barPkmnHealth.Maximum = pokemon.HPMax;
            barPkmnHealth.Value = pokemon.HPCurrent > 0 ? pokemon.HPCurrent : 0;

            Selected = false;
        }

        private void PokemonPanel_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Right)
            {
                ShowDetails(((PokemonPanel)sender)._pokemon);
            }
            _parentForm = (PokemonPartyForm)Parent;
            _parentForm.UnselectAll();
            MakePanelSelected(true);
        }

        private void PokemonPanel_DoubleClick(object sender, EventArgs e)
        {
            Selected = true;
            _parentForm = (PokemonPartyForm)Parent;
            BackColor = Color.FromArgb(255, 200, 200);
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
            BackColor = isSelected
                ? Color.FromArgb(255, 200, 200)
                : Color.FromArgb(150, 200, 200);
        }

        private void ShowDetails(IPokemon pokemon)
        {
            _parentForm = (PokemonPartyForm)Parent;
            PokemonDetailsForm pokemonDetailsForm = new PokemonDetailsForm(pokemon);
            if (!StaticMain.openedForms.Where(x => x.Name == pokemonDetailsForm.Name).Any())
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
