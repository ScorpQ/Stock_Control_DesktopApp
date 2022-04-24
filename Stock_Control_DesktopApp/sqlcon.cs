using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Sql Command Library
using System.Data.SqlClient;

namespace Stock_Control_DesktopApp
{
    class sqlcon
    {
        public SqlConnection connection()
        {
            string adress = "Data Source=DESKTOP-OF07IF9;Initial Catalog=DBbusiness;Integrated Security=True";
            SqlConnection conn = new SqlConnection(adress);
            conn.Open();
            return conn;
        }
    }
}
