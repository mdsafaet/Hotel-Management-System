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
    public partial class LoginForm : Form
    {
        public LoginForm()
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

        private void BtnSignup_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Signup().Show();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string uname = tbUname.Text;
            string password = tbPassword.Text;
            var conn = Database.ConnectDB();
            conn.Open();
            string query = string.Format("select * from Users where Username='{0}' and Password='{1}'", uname, password);
            SqlCommand cmd = new SqlCommand(query,conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                this.Hide();
                new Dashboard().Show();
            }
            else
            {
                MessageBox.Show("Invalid User!");
            }
            conn.Close();
        }
    }
}
