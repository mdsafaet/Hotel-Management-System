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
    public partial class Signup : Form
    {
        public Signup()
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
        private void BtnRegistration_Click(object sender, EventArgs e)
        {
            string c_name = tbName.Text;
            string c_username = tbUname.Text;
            string c_password = tbPassword.Text;
            string c_contact = tbContact.Text;
            string c_address = tbAddress.Text;
            var conn = Database.ConnectDB();
            conn.Open();
            string query = string.Format("insert into Users values ('{0}','{1}','{2}','{3}','{4}')", c_name, c_username, c_password, c_contact, c_address);
            SqlCommand cmd = new SqlCommand(query, conn);
            int r = cmd.ExecuteNonQuery();
            if (r > 0)
            {
                MessageBox.Show("Registration Complete!");
            }
            else
            {
                MessageBox.Show("Cannot Complete Registration!");
            }
            conn.Close();
        }

        public void Clear()
        {
            tbName.Text = "";
            tbUname.Text = "";
            tbPassword.Text = "";
            tbContact.Text = "";
            tbAddress.Text = "";
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            new LoginForm().Show();
        }
    }
}
