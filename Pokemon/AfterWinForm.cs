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
    public partial class AfterWinForm : Form
    {
        public AfterWinForm()
        {
            InitializeComponent();
        }

        public AfterWinForm(int winnings)
        {
            InitializeComponent();
            this.rbWinnings.Text = $"You've earned {winnings} coins";
            this.rbWinnings.AppendText($"{Environment.NewLine} Current money: {PlayerEquipment.Money}");
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGoToShop_Click(object sender, EventArgs e)
        {
            ShopForm shopForm = new ShopForm();

            shopForm.Location = new Point(this.Location.X + this.Size.Width, this.Location.Y);
            shopForm.BringToFront();
            shopForm.Show();
        }
    }
}
