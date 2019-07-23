using System.Collections.Generic;

namespace Pokemon.Models
{
    public interface IEquipment
    {
        int Money { get; set; }

        Dictionary<IEquipmentItem, int> EquipmentList { get; set; }

        void ChangeMoneyQuantity(int value);

        void UseItem(IPokemon pokemon, int id);
    }
}
