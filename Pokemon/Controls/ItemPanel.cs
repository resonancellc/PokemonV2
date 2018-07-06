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
    public partial class ItemPanel : UserControl
    {
        public int ID { get; set; }
        ShopForm parentForm;

        public ItemPanel(EquipmentItem item, bool isShopPanel)
        {
            InitializeComponent();
            this.pictureBox1.Image = ImageHelper.GetItemImageById(item.ID);
            this.ID = item.ID;
            this.lblItemName.Text = item.Name;
            this.lblDescription.Text = item.Description;
            if (isShopPanel)
            {
                this.btnAction.Text = item.Cost.ToString() + "$";
            }
            else
            {
                this.btnAction.Text = $"{PlayerEquipment.playerItems[this.ID - 1].ToString()}x";
            }
            
        }


        private void btnBuy_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Bought {this.lblItemName.Text} for {((Button)sender).Text}");
        }
    }
}
