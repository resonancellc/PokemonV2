using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class EquipmentItem : IEquipmentItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }

        public void Use()
        {
            throw new NotImplementedException();
        }

        public string Test()
        {
            return "tested";
        }
    }
}
