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
using Stock_Control_DesktopApp;

namespace Stock_Control_Manager
{
    class funcBase
    {

        public string generatePID()  // Ürünler için.
        {
            Random rndm = new Random();
            int randomNum = rndm.Next(999999, 9999999);

            string collection = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int index = rndm.Next(26);

            return collection[index] + randomNum.ToString();
        }

        public int generateCID() // Müşteriler,Personeller, Firmalar için.
        {
            Random rndm = new Random();
            return rndm.Next(100000, 1000000);
        }
        public void listAll(string query, DataGridView grid) // spesifik bir datagrid'e veri yüklemek için.
        {
            sqlcon CONNECT = new sqlcon();
            DataTable DT = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter("select * from TBL_PRODUCTS", CONNECT.connection());
            DA.Fill(DT);
            grid.DataSource = DT;
        }


    }
}
