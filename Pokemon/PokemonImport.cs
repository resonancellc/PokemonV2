using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class PokemonImport
    {
        private readonly string _filePath;

        public PokemonImport(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<IPokemon> Import()
        {
            try
            {
                string importJson = File.ReadAllText(_filePath);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<IPokemon>>(importJson);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
