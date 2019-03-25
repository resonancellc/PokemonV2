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
        public Dictionary<IEquipmentItem, int> EquipmentList { get; set; }

        public void ChangeMoneyQuantity(int value)
        {
            Money += value;
        }

        public void UseItem(int id)
        {
            int quantity = EquipmentList.Where(i => i.Key.ID == id).FirstOrDefault().Value;
            EquipmentList[EquipmentList.Where(i => i.Key.ID == id).FirstOrDefault().Key] = quantity - 1;
        }
    }
}
