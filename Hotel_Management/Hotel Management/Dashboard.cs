using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hotel_Management
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason != CloseReason.WindowsShutDown)
            {
                Application.Exit();
            }
        }

        private void BtnRoomAvailability_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Room().Show();
        }

        private void BtnNewCustomer_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Customers().Show();
        }

        private void BtnViewUsers_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Users().Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            new LoginForm().Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
