using System.Collections.Generic;
using System.Linq;

namespace Pokemon.Models
{
    public class Equipment : IEquipment
    {
        public int Money { get; set; }

        public Dictionary<IEquipmentItem, int> EquipmentList { get; set; }

        public void ChangeMoneyQuantity(int value) => Money += value;

        public void UseItem(IPokemon pokemon, int id)
        {
            int quantity = EquipmentList.Where(i => i.Key.ID == id).FirstOrDefault().Value;
            EquipmentList[EquipmentList.Where(i => i.Key.ID == id).FirstOrDefault().Key] = quantity - 1;
            ItemHelper.UseItem(pokemon, id);
        }
    }
}
