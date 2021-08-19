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
    public partial class Room : Form
    {
        public Room()
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
            var list = GetRooms();
            dtRooms.DataSource = list;
        }
        private List<Room1>GetRooms()
        {
            var conn = Database.ConnectDB();
            conn.Open();
            string query = "select * from Rooms";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            //ArrayList list = new ArrayList();
            List<Room1> list = new List<Room1>();
            while (reader.Read())
            {
                Room1 r = new Room1();
                r.RoomNo = reader.GetInt32(reader.GetOrdinal("RoomNo"));
                r.CustomerName = reader.GetString(reader.GetOrdinal("CustomerName"));
                r.RoomType = reader.GetString(reader.GetOrdinal("RoomType"));
                r.Floor = reader.GetString(reader.GetOrdinal("Floor"));
                

                list.Add(r);
            }
            return list;
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

        private void dtRooms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dtRooms.Rows[e.RowIndex];
                tbRoomNo.Text = row.Cells["RoomNo"].Value.ToString();
                tbCName.Text = row.Cells["CustomerName"].Value.ToString();
                tbRoomType.Text = row.Cells["RoomType"].Value.ToString();
                tbFloorNo.Text = row.Cells["Floor"].Value.ToString();
            }
        }

        public void Clear()
        {
            tbRoomNo.Text = "";
            tbCName.Text = "";
            tbRoomType.Text = "";
            tbFloorNo.Text = "";
        }

        private void BtnAddRoom_Click(object sender, EventArgs e)
        {
            string r_cname = tbCName.Text;
            string r_roomtype = tbRoomType.Text;
            string r_floor = tbFloorNo.Text;
            var conn = Database.ConnectDB();
            conn.Open();
            string query = string.Format("insert into Rooms values ('{0}','{1}','{2}')", r_cname, r_roomtype, r_floor);
            SqlCommand cmd = new SqlCommand(query, conn);
            int r = cmd.ExecuteNonQuery();
            if (r > 0)
            {
                MessageBox.Show("Room Information Added!");
            }
            else
            {
                MessageBox.Show("Cannot Add Room!");
            }
            conn.Close();
            var list = GetRooms();
            dtRooms.DataSource = list;
            Clear();
        }

        private void BtnUpdateRoom_Click(object sender, EventArgs e)
        {
            int ro_roomno = Int32.Parse(tbRoomNo.Text);
            string ro_cname = tbCName.Text;
            string ro_roomtype = tbRoomType.Text;
            string ro_floorno = tbFloorNo.Text;
            var conn = Database.ConnectDB();
            conn.Open();
            string query = string.Format("update Rooms set CustomerName='{0}', RoomType='{1}', Floor='{2}' where RoomNo={3}", ro_cname, ro_roomtype, ro_floorno, ro_roomno);
            SqlCommand cmd = new SqlCommand(query, conn);
            int r = cmd.ExecuteNonQuery();
            MessageBox.Show("Room Information Updated!");
            conn.Close();
            var list = GetRooms();
            dtRooms.DataSource = list;
            Clear();
        }

        private void BtnDeleteRoom_Click(object sender, EventArgs e)
        {
            var conn = Database.ConnectDB();
            conn.Open();
            string query = string.Format("delete from Rooms where CustomerName='" + tbCName.Text + "'", conn);
            SqlCommand cmd = new SqlCommand(query, conn);
            int r = cmd.ExecuteNonQuery();
            if (r > 0)
            {
                MessageBox.Show("Room Deleted!");
            }
            else
            {
                MessageBox.Show("Room cannot be Deleted!");
            }
            conn.Close();

            var list = GetRooms();
            dtRooms.DataSource = list;
            Clear();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            int ro_roomno = Int32.Parse(tbRoomNo.Text);
            var conn = Database.ConnectDB();
            conn.Open();
            string query = string.Format("select * from Rooms where RoomNo={0}", ro_roomno);
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            Room1 ro = new Room1();
            while (reader.Read())
            {
                ro.CustomerName = reader.GetString(reader.GetOrdinal("CustomerName"));
                ro.RoomType = reader.GetString(reader.GetOrdinal("RoomType"));
                ro.Floor = reader.GetString(reader.GetOrdinal("Floor"));
                ro.RoomNo = reader.GetInt32(reader.GetOrdinal("RoomNo"));
            }
            tbCName.Text = ro.CustomerName;
            tbRoomType.Text = ro.RoomType;
            tbFloorNo.Text = ro.Floor;
        }
    }
}
