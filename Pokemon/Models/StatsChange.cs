using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Models
{
    public class StatsChange
    {
        public IPokemon AffectedPokemon { get; set; }
        public StatType StatType { get; set; }
        public int StageValue { get; set; }
    }
}
