using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pokemon.Models;

namespace Pokemon
{
    public partial class ItemPanel : UserControl
    {
        public int ID { get; set; }
        public int ItemValue { get; set; }
        //BattleForm parent;
        ItemForm _parentForm;

        public ItemPanel(IEquipmentItem item, int quantity, ItemForm parentForm)
        {
            InitializeComponent();
            this.pictureBox1.Image = ImageHelper.GetItemImageById(item.ID);
            this.lblItemName.Text = item.Name;
            this.lblDescription.Text = item.Description;
            this.btnAction.Text = $"{quantity.ToString()}x";
            this.ID = item.ID;
            _parentForm = parentForm;
        }


        public ItemPanel(EquipmentItem item, bool isShopPanel, ItemForm formParent, BattleForm parent = null)
        {
            //InitializeComponent();
            //this.formParent = formParent;
            //this.pictureBox1.Image = ImageHelper.GetItemImageById(item.ID);
            //this.ItemValue = item.Cost;
            //this.ID = item.ID;
            //this.lblItemName.Text = item.Name;
            //this.lblDescription.Text = item.Description;
            //this.btnAction.Text = isShopPanel == true ? item.Cost.ToString() + "$" : $"{PlayerEquipment.playerItems[this.ID - 1].ToString()}x";
            //if (parent != null) this.parent = parent;
        }


        private void btnAction_TextChanged(object sender, EventArgs e)
        {
            this.btnAction.Enabled = this.btnAction.Text == "0x" ? false : true;
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            UseItem();
        }

        public void UseItem()
        {
            _parentForm.ItemPicked(this.ID);
        }


    }
}
