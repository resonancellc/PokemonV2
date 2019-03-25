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
    public partial class PokemonPartyForm : Form
    {
        public IPokemon PickedPokemon { get; set; }
        public int ActivePokemonIndex { get; set; }
        Form parentForm;

        public PokemonPartyForm(IPokemonParty<IPokemon> playerPokemonParty)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            this.PickedPokemon = playerPokemonParty.GetFirstAlivePokemon();
            int offset = 0;
            int index = 0;
            foreach (IPokemon pokemon in playerPokemonParty.Pokemons)
            {
                if (pokemon != null)
                {
                    PokemonPanel pokemonPanel = new PokemonPanel(pokemon);
                    //pokemonPanel.Dock = DockStyle.Top;
                    pokemonPanel.Location = new Point(0, offset);
                    pokemonPanel.Index = index;
                    this.Controls.Add(pokemonPanel);
                    offset += pokemonPanel.Size.Height;
                    index++;
                }
            }
        }

        public PokemonPartyForm(Form parent, IPokemonParty<IPokemon> playerPokemonParty)
        {
            parentForm = parent;
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new Point(parent.Location.X + parent.Size.Width, parent.Location.Y);

            this.PickedPokemon = playerPokemonParty.GetFirstAlivePokemon();
            int offset = 0;
            int index = 0;
            foreach (IPokemon pokemon in playerPokemonParty.Pokemons)
            {
                if (pokemon != null)
                {
                    PokemonPanel pokemonPanel = new PokemonPanel(pokemon);
                    //pokemonPanel.Dock = DockStyle.Top;
                    pokemonPanel.Location = new Point(0,offset);
                    pokemonPanel.Index = index;
                    this.Controls.Add(pokemonPanel);
                    offset += pokemonPanel.Size.Height;
                    index++;
                }
            }
        }

        public void UnselectAll()
        {
            foreach (PokemonPanel panel in this.Controls)
            {
                panel.Selected = false;
                panel.BackColor = Color.FromArgb(150, 200, 200);
            }
        }

        public void PokemonPicked(IPokemon pokemon)
        {
            this.PickedPokemon = pokemon;
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void PokemonPartyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            Hide();
        }
    }
}
