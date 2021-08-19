using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Hotel_Management
{
    class Database
    {
        public static SqlConnection ConnectDB()
        {
            string connString = @"Server=DESKTOP-0AEOSGK;Database=Hotel_Management;Integrated Security=true;";
            return new SqlConnection(connString);
        }
    }
}
