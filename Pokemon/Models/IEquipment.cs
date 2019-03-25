using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Models
{
    public interface IEquipment
    {
        int Money { get; set; }
        Dictionary<IEquipmentItem, int> EquipmentList { get; set; }

        void ChangeMoneyQuantity(int value);
        void UseItem(int id);
    }
}
