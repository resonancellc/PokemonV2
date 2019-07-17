using Dtos;
using Pokemon.Factory;
using Pokemon.Models;
using Pokemon.ObjectMappers;
using Pokemon.Validators;
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
    public partial class StartForm : Form
    {
        IList<PictureBox> pictures = new List<PictureBox>();
        IList<IPokemon> pokemonList = new List<IPokemon>();
        int teamSize = 1;

        public StartForm()
        {
            InitializeComponent();
            LoadPicturesToList();
            StartRandomizing(teamSize);
        }

        private void StartRandomizing(int pokeNumber)
        {
            teamSize = pokeNumber;
            var levelValidatorResult = LevelValidator.IsLevelValid(tbLevel.Text);
            if (levelValidatorResult == LevelValidatorResult.OK)
            {
                pokemonList.Clear();

                int level = Convert.ToInt32(tbLevel.Text);

                for (int i = 0; i < pokeNumber; i++)
                {
                    pokemonList.Add(PokemonFactory.CreatePokemon(level));
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
            foreach (PictureBox pictureBox in pictures)
            {
                pictureBox.Image = null;
            }
            for (int i = 0; i < pokemonList.Count; i++)
            {
                pictures[i].Image = ImageHelper.GetImageById(false, pokemonList[i].ID);
            };
        }

        private void LoadPicturesToList()
        {
            pictures.Add(pictureBox1);
            pictures.Add(pictureBox2);
            pictures.Add(pictureBox3);
            pictures.Add(pictureBox4);
            pictures.Add(pictureBox5);
            pictures.Add(pictureBox6);
        }

        private void btnRandomize_Click(object sender, EventArgs e)
        {
            if (rbOnePoke.Checked) StartRandomizing(1);
            if (rbThreePoke.Checked) StartRandomizing(3);
            if (rbSixPoke.Checked) StartRandomizing(6);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (pokemonList.Any())
            {
                BattleForm battleForm = new BattleForm(pokemonList, teamSize);
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
                if (pokemonList[0] != null)
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

                    PokemonExport pokemonExporter = new PokemonExport(pokemonList, saveFileDialog.FileName);
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
                    pokemonList.Clear();
                    foreach (var importedPokemon in importedPokemons)
                    {
                        pokemonList.Add(importedPokemon.ToDomainObject());
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
