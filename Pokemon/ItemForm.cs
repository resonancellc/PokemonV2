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
    public partial class ItemForm : Form
    {
        Form parent;

        public ItemForm(bool isShopForm, Form parent)
        {
            InitializeComponent();
            if (isShopForm)
                this.parent = parent as AfterWinForm;
            else
                this.parent = parent as BattleForm;

            this.Text = isShopForm ? "Shop" : "Items";
            int offset = 0;
            foreach (EquipmentItem item in StaticTypes.equipmentItemList)
            {
                if (item != null)
                {
                    ItemPanel itemPanel = null;
                    itemPanel = parent is BattleForm ? new ItemPanel(item, isShopForm, this, parent as BattleForm) : new ItemPanel(item, isShopForm, this);
                    itemPanel.Location = new Point(0,offset);

                    this.Controls.Add(itemPanel);
                    offset += itemPanel.Size.Height;
                }
            }
        }



        private void ItemForm_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (parent is AfterWinForm) ((AfterWinForm)parent).RefreshBalance();

            StaticMain.FormClosed(this);
        }


    }
}
