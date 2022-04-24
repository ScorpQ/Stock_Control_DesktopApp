using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//
using System.Data.SqlClient;
using Stock_Control_Manager;

namespace Stock_Control_DesktopApp.AllChildPanels
{
    public partial class product : Form
    {
        funcBase FB = new funcBase();

        public product()
        {
            InitializeComponent();
        }

        private void product_Load(object sender, EventArgs e)
        {
            string query = "select * from TBL_PRODUCTS";
            FB.listAll(query,dataGridView1);
        }
       
    }
}
