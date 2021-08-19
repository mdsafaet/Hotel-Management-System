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
    public partial class Users : Form
    {
        public Users()
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

        private void LoadData(object sender, EventArgs e)
        {
            var list = GetUsers();
            dtUsers.DataSource = list;
        }
        private List<Users1> GetUsers()
        {
            var conn = Database.ConnectDB();
            conn.Open();
            string query = "select * from Users";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            //ArrayList list = new ArrayList();
            List<Users1> list = new List<Users1>();
            while (reader.Read())
            {
                Users1 u = new Users1();
                u.UserId = reader.GetInt32(reader.GetOrdinal("UserId"));
                u.Name = reader.GetString(reader.GetOrdinal("Name"));
                u.Username = reader.GetString(reader.GetOrdinal("Username"));
                u.Password = reader.GetString(reader.GetOrdinal("Password"));
                u.ContactNo = reader.GetString(reader.GetOrdinal("ContactNo"));
                u.Address = reader.GetString(reader.GetOrdinal("Address"));


                list.Add(u);
            }
            return list;
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            new LoginForm().Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Dashboard().Show();
        }

        private void dtUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dtUsers.Rows[e.RowIndex];
                tbUserid.Text = row.Cells["UserId"].Value.ToString();
                tbName.Text = row.Cells["Name"].Value.ToString();
                tbUsername.Text = row.Cells["Username"].Value.ToString();
                tbPassword.Text = row.Cells["Password"].Value.ToString();
                tbContactno.Text = row.Cells["ContactNo"].Value.ToString();
                tbAddress.Text = row.Cells["Address"].Value.ToString();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string u_name = tbName.Text;
            string u_username = tbUsername.Text;
            string u_password = tbPassword.Text;
            string u_contact = tbContactno.Text;
            string u_address = tbAddress.Text;
            var conn = Database.ConnectDB();
            conn.Open();
            string query = string.Format("insert into Users values ('{0}','{1}','{2}','{3}','{4}')", u_name, u_username, u_password, u_contact, u_address);
            SqlCommand cmd = new SqlCommand(query, conn);
            int r = cmd.ExecuteNonQuery();
            if (r > 0)
            {
                MessageBox.Show("User Added!");
            }
            else
            {
                MessageBox.Show("Cannot Add User!");
            }
            conn.Close();

            var list = GetUsers();
            dtUsers.DataSource = list;
            Clear();
        }
        public void Clear()
        {
            tbUserid.Text = "";
            tbName.Text = "";
            tbUsername.Text = "";
            tbPassword.Text = "";
            tbContactno.Text = "";
            tbAddress.Text = "";
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            int u_id = Int32.Parse(tbUserid.Text);
            string u_Name = tbName.Text;
            string u_Username = tbUsername.Text;
            string u_Password = tbPassword.Text;
            string u_ContactNo = tbContactno.Text;
            string u_Address = tbAddress.Text;
            var conn = Database.ConnectDB();
            conn.Open();
            string query = string.Format("update Users set Name='{0}', Username='{1}', Password='{2}', ContactNo='{3}', Address='{4}' where UserId={5}", u_Name, u_Username, u_Password, u_ContactNo, u_Address, u_id);
            SqlCommand cmd = new SqlCommand(query, conn);
            int r = cmd.ExecuteNonQuery();
            MessageBox.Show("User Updated!");
            conn.Close();
            var list = GetUsers();
            dtUsers.DataSource = list;
            Clear();
        }

        private void BtnDeleteUser_Click(object sender, EventArgs e)
        {

            var conn = Database.ConnectDB();
            conn.Open();
            string query = string.Format("delete from Users where Name='" + tbName.Text + "'", conn);
            SqlCommand cmd = new SqlCommand(query, conn);
            int r = cmd.ExecuteNonQuery();
            if (r > 0)
            {
                MessageBox.Show("User Deleted!");
            }
            else
            {
                MessageBox.Show("User cannot be Deleted!");
            }
            conn.Close();

            var list = GetUsers();
            dtUsers.DataSource = list;
            Clear();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            int u_id = Int32.Parse(tbUserid.Text);
            var conn = Database.ConnectDB();
            conn.Open();
            string query = string.Format("select * from Users where UserId={0}", u_id);
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            Users1 u = new Users1();
            while (reader.Read())
            {
                u.Name = reader.GetString(reader.GetOrdinal("Name"));
                u.Username = reader.GetString(reader.GetOrdinal("Username"));
                u.Password = reader.GetString(reader.GetOrdinal("Password"));
                u.ContactNo = reader.GetString(reader.GetOrdinal("ContactNo"));
                u.Address = reader.GetString(reader.GetOrdinal("Address"));
                u.UserId = reader.GetInt32(reader.GetOrdinal("UserId"));
            }
            tbName.Text = u.Name;
            tbUsername.Text = u.Username;
            tbPassword.Text = u.Password;
            tbContactno.Text = u.ContactNo;
            tbAddress.Text = u.Address;
        }
    }
}
