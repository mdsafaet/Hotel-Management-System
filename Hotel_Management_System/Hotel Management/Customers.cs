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
    public partial class Customers : Form
    {
        public Customers()
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

        private void AddCustomer_Click(object sender, EventArgs e)
        {

            string c_name = tbCName.Text;
            string c_contact = tbCContact.Text;
            string c_room = tbCRoom.Text;
            string c_payment = tbCPayment.Text;
            var conn = Database.ConnectDB();
            conn.Open();
            string query = string.Format("insert into Customers values ('{0}','{1}','{2}','{3}')", c_name, c_contact, c_room, c_payment);
            SqlCommand cmd = new SqlCommand(query, conn);
            int r = cmd.ExecuteNonQuery();
            if (r > 0)
            {
                MessageBox.Show("Customer Added!");
            }
            else
            {
                MessageBox.Show("Cannot Add Customer!");
            }
            conn.Close();
            var list = GetCustomers();
            dtCustomers.DataSource = list;
            Clear();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Dashboard().Show();
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            new LoginForm().Show();
        }

        private void LoadCustomerData(object sender, EventArgs e)
            {
                var list = GetCustomers();
                dtCustomers.DataSource = list;
            }
            private List<Customers1> GetCustomers()
            {
                var conn = Database.ConnectDB();
                conn.Open();
                string query = "select * from Customers";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                //ArrayList list = new ArrayList();
                List<Customers1> list = new List<Customers1>();
                while (reader.Read())
                {
                    Customers1 c = new Customers1();
                    c.CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId"));
                    c.CustomerName = reader.GetString(reader.GetOrdinal("CustomerName"));
                    c.CustomerContact = reader.GetString(reader.GetOrdinal("CustomerContact"));
                    c.CustomerRoomNo = reader.GetString(reader.GetOrdinal("CustomerRoomNo"));
                    c.Payment = reader.GetString(reader.GetOrdinal("Payment"));


                list.Add(c);
                }
                return list;
            }
        public void Clear()
        {
            tbCId.Text = "";
            tbCName.Text = "";
            tbCContact.Text = "";
            tbCRoom.Text = "";
            tbCPayment.Text = "";
        }

        private void dtCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dtCustomers.Rows[e.RowIndex];
                tbCId.Text = row.Cells["CustomerId"].Value.ToString();
                tbCName.Text = row.Cells["CustomerName"].Value.ToString();
                tbCContact.Text = row.Cells["CustomerContact"].Value.ToString();
                tbCRoom.Text = row.Cells["CustomerRoomNo"].Value.ToString();
                tbCPayment.Text = row.Cells["Payment"].Value.ToString();
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            int c_id = Int32.Parse(tbCId.Text);
            string c_Name = tbCName.Text;
            string c_Contact = tbCContact.Text;
            string c_Room = tbCRoom.Text;
            string c_Payment = tbCPayment.Text;
            var conn = Database.ConnectDB();
            conn.Open();
            string query = string.Format("update Customers set CustomerName='{0}', CustomerContact='{1}', CustomerRoomNo='{2}', Payment='{3}' where CustomerId={4}", c_Name, c_Contact, c_Room, c_Payment, c_id);
            SqlCommand cmd = new SqlCommand(query, conn);
            int r = cmd.ExecuteNonQuery();
            MessageBox.Show("Customer Information Updated!");
            conn.Close();
            var list = GetCustomers();
            dtCustomers.DataSource = list;
            Clear();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var conn = Database.ConnectDB();
            conn.Open();
            string query = string.Format("delete from Customers where CustomerName='" + tbCName.Text + "'", conn);
            SqlCommand cmd = new SqlCommand(query, conn);
            int r = cmd.ExecuteNonQuery();
            if (r > 0)
            {
                MessageBox.Show("Customer Deleted!");
            }
            else
            {
                MessageBox.Show("Customer cannot be Deleted!");
            }
            conn.Close();

            var list = GetCustomers();
            dtCustomers.DataSource = list;
            Clear();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            int c_id = Int32.Parse(tbCId.Text);
            var conn = Database.ConnectDB();
            conn.Open();
            string query = string.Format("select * from Customers where CustomerId={0}", c_id);
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            Customers1 c = new Customers1();
            while (reader.Read())
            {
                c.CustomerName = reader.GetString(reader.GetOrdinal("CustomerName"));
                c.CustomerContact = reader.GetString(reader.GetOrdinal("CustomerContact"));
                c.CustomerRoomNo = reader.GetString(reader.GetOrdinal("CustomerRoomNo"));
                c.Payment = reader.GetString(reader.GetOrdinal("Payment"));
                c.CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId"));
            }
            tbCName.Text = c.CustomerName;
            tbCContact.Text = c.CustomerContact;
            tbCRoom.Text = c.CustomerRoomNo;
            tbCPayment.Text = c.Payment;
        }
    }
}
