using System;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class ItemPanel : UserControl
    {
        public int ID { get; set; }

        public int ItemValue { get; set; }

        ItemForm _parentForm;

        public ItemPanel(IEquipmentItem item, int quantity, ItemForm parentForm)
        {
            InitializeComponent();
            pictureBox1.Image = ImageHelper.GetItemImageById(item.ID);
            lblItemName.Text = item.Name;
            lblDescription.Text = item.Description;
            btnAction.Text = $"{quantity.ToString()}x";
            ID = item.ID;
            _parentForm = parentForm;
        }

        private void btnAction_TextChanged(object sender, EventArgs e)
        {
            btnAction.Enabled = this.btnAction.Text == "0x" ? false : true;
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
