using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class ItemShopPanel : UserControl
    {
        public int ID { get; set; }
        ShopForm parentForm;

        public ItemShopPanel(EquipmentItem item)
        {
            InitializeComponent();
            this.pictureBox1.Image = ImageHelper.GetItemImageById(item.ID);
            this.ID = item.ID;
            this.lblItemName.Text = item.Name;
            this.lblDescription.Text = item.Description;
            this.btnBuy.Text = item.Cost.ToString() + "$";
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Bought {this.lblItemName.Text} for {((Button)sender).Text}");
        }
    }
}
