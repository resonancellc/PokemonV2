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
        public static Dictionary<int, IEquipmentItem> Items { get; set; }

        public static void FillItemsList()
        {
            DataRowCollection itemDataRows = StaticSQL.GetItemList().Rows;
            foreach (DataRow itemRow in itemDataRows)
            {
                var values = itemRow.ItemArray;
                IEquipmentItem item = Factory.CreateItem();

                item.ID = (int)values[0];
                item.Name = (string)values[1];

                item.Description = (string)values[2];
                item.Cost = (int)values[3];

                Items.Add(item.ID, item);
            }
        }
    }
}
