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
        public PokemonPartyForm()
        {
            InitializeComponent();
            int offset = 0;
            int index = 0;
            foreach (Pokemon pokemon in PokemonParty.playerPokemons)
            {
                if (pokemon != null)
                {
                    PokemonPanel pokemonPanel = new PokemonPanel(pokemon);
                    //pokemonPanel.Dock = DockStyle.Top;
                    pokemonPanel.Location = new Point(0,offset);
                    pokemonPanel.Index = index;
                    this.Controls.Add(pokemonPanel);
                    offset += 56;
                    index++;
                }
            }
        }


    }
}
