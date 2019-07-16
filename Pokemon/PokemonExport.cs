using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Pokemon
{
    public class PokemonExporter
    {
        IEnumerable<IPokemon> _pokemonsToExport;
        string _filePath;

        public PokemonExporter(IEnumerable<IPokemon> pokemonsToExport, string filePath)
        {
            _pokemonsToExport = pokemonsToExport;
            _filePath = filePath;
        }

        public bool Export()
        {
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(_pokemonsToExport);
                string path = $@"{_filePath}";

                // This text is added only once to the file.
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    File.WriteAllText(path, json);
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
