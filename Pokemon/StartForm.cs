using Pokemon.Factory;
using Pokemon.Models;
using Pokemon.ObjectMappers;
using Pokemon.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class StartForm : Form
    {
        private readonly IList<PictureBox> _pictures;
        private readonly IList<IPokemon> _pokemonList;
        private readonly IList<IPokemon> _enemyPokemonList;
        private int _teamSize = 1;

        public StartForm()
        {
            InitializeComponent();
            _pokemonList = new List<IPokemon>();
            _enemyPokemonList = new List<IPokemon>();
            _pictures = new List<PictureBox>()
            {
                pictureBox1,
                pictureBox2,
                pictureBox3,
                pictureBox4,
                pictureBox5,
                pictureBox6
            };

            StartRandomizing(_teamSize);
        }

        private void StartRandomizing(int pokeNumber)
        {
            _teamSize = pokeNumber;
            
            var levelValidatorResult = LevelValidator.IsLevelValid(tbLevel.Text);
            if (levelValidatorResult == LevelValidatorResult.OK)
            {
                _pokemonList.Clear();
                _enemyPokemonList.Clear();

                int level = Convert.ToInt32(tbLevel.Text);

                for (int i = 0; i < pokeNumber; i++)
                {
                    _pokemonList.Add(PokemonFactory.CreatePokemon(level));
                    _enemyPokemonList.Add(PokemonFactory.CreatePokemon(level));
                }

                PrepareImages();
            }
            else if(levelValidatorResult == LevelValidatorResult.InvalidFormat)
            {
                MessageBox.Show("Incorrect level format (Only numbers)");
            }
            else if (levelValidatorResult == LevelValidatorResult.NotInRange)
            {
                MessageBox.Show("Level must be higher than 0 or 100 and less");
            }
        }

        private void PrepareImages()
        {
            foreach (PictureBox pictureBox in _pictures)
            {
                pictureBox.Image = null;
            }
            for (int i = 0; i < _pokemonList.Count; i++)
            {
                _pictures[i].Image = ImageHelper.GetImageById(false, _pokemonList[i].ID);
            };
        }

        private void btnRandomize_Click(object sender, EventArgs e)
        {
            if (rbOnePoke.Checked) StartRandomizing(1);
            if (rbThreePoke.Checked) StartRandomizing(3);
            if (rbSixPoke.Checked) StartRandomizing(6);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_pokemonList.Any())
            {
                var playerParty = PokemonPartyFactory.CreatePokemonParty(_pokemonList, true);
                var enemyParty = PokemonPartyFactory.CreatePokemonParty(_enemyPokemonList, true);

                BattleForm battleForm = new BattleForm(playerParty, enemyParty);
                battleForm.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Team not selected");
            }
        }

        private void tbLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (_pokemonList[0] != null)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog()
                    {
                        Filter = "Text Files (*.txt)|*.txt",
                        DefaultExt = "txt",
                        AddExtension = true,
                        FileName = $"PokemonExport_{DateTime.Now.ToShortDateString()}",
                        RestoreDirectory = true,
                        InitialDirectory = $"{Environment.CurrentDirectory}\\ExportedObjects\\"
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                    {
                        return;
                    }

                    PokemonExport pokemonExporter = new PokemonExport(_pokemonList, saveFileDialog.FileName);
                    var isExportSuccessful = pokemonExporter.Export();
                    if (isExportSuccessful)
                    {
                        MessageBox.Show(
                            "Export",
                            "Export successful",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Filter = "Text Files (*.txt)|*.txt",
                    DefaultExt = "txt",
                    AddExtension = true,
                    RestoreDirectory = true,
                    InitialDirectory = $"{Environment.CurrentDirectory}\\ExportedObjects\\"
                };

                if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }

                PokemonImport pokemonImport = new PokemonImport(openFileDialog.FileName);

                var importedPokemons = pokemonImport.Import();
                if (importedPokemons.Any())
                {
                    _pokemonList.Clear();
                    foreach (var importedPokemon in importedPokemons)
                    {
                        _pokemonList.Add(importedPokemon.ToDomainObject());
                    }

                    PrepareImages();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
