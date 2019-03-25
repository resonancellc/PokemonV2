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
            //InitializeComponent();
            //this.rbWinnings.Text = $"You've earned {winnings} coins";
            //this.rbWinnings.AppendText($"{Environment.NewLine} Current money: {PlayerEquipment.Money}");
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGoToShop_Click(object sender, EventArgs e)
        {
            //ItemForm shopForm = new ItemForm(true, this);
            //if (!StaticMain.openedForms.Where(x => x.Name == shopForm.Name).Any())
            //{
            //    StaticMain.FormOpened(shopForm);
            //    shopForm.Location = new Point(this.Location.X + this.Size.Width, this.Location.Y);
            //    shopForm.BringToFront();
            //    shopForm.Show();
            //}
            //else
            //{

            //}
        }

        public void RefreshBalance()
        {
            //this.rbWinnings.Text = $"Current money: {PlayerEquipment.Money}";
        }
    }
}
