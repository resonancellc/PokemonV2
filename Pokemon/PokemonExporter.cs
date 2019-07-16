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

        public PokemonExporter(IEnumerable<IPokemon> pokemonsToExport)
        {
            _pokemonsToExport = pokemonsToExport;
        }

        public bool Export()
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(_pokemonsToExport);
            return true;
        }
    }
}
