using Pokemon.Factory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class ItemsList
    {
        public static Dictionary<int, IEquipmentItem> Items = new Dictionary<int, IEquipmentItem>();

        public static void FillItemsList()
        {
            DataRowCollection itemDataRows = StaticSQL.GetItemList().Rows;
            foreach (DataRow itemRow in itemDataRows)
            {
                var values = itemRow.ItemArray;
                IEquipmentItem item = EquipmentItemFactory.CreateItem(values);
                Items.Add(item.ID, item);
            }
        }
    }
}
