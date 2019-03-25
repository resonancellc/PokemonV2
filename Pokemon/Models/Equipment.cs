using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Models
{
    public class Equipment : IEquipment
    {
        public int Money { get; set; }
        public List<IEquipmentItem> EquipmentList { get; set; }

        public void ChangeMoneyQuantity(int value)
        {
            Money += value;
        }
    }
}
