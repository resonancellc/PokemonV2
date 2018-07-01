using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class ShopForm : Form
    {
        public ShopForm()
        {
            InitializeComponent();
            int offset = 0;
            foreach (EquipmentItem item in StaticTypes.equipmentItemList)
            {
                if (item != null)
                {
                    ItemShopPanel itemShopPanel = new ItemShopPanel(item);
                    itemShopPanel.Location = new Point(0,offset);

                    this.Controls.Add(itemShopPanel);
                    offset += itemShopPanel.Size.Height;
                }
            }
        }
    }
}
