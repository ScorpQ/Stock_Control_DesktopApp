using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


// Sql Commands Library and Others.
using System.Data.SqlClient;
using Stock_Control_Manager;

namespace Stock_Control_DesktopApp.AllChildPanels
{
    public partial class product : Form
    {
        // Collection of the Classes.
        funcBase FB = new funcBase();
        SqlCommand CMD;
        SqlDataAdapter DA;
        DataTable DT;
        sqlcon CONNECT = new sqlcon();
        string query = "select * from TBL_PRODUCTS";

        public product()
        {
            InitializeComponent();
        }

        private void product_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dBbusinessDataSet.TBL_PRODUCTS' table. You can move, or remove it, as needed.
            this.tBL_PRODUCTSTableAdapter.Fill(this.dBbusinessDataSet.TBL_PRODUCTS);
            
            //list
            FB.listAll(query,dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // datagrid'e tıkladıktan sonra o satırdaki tüm dataları bileşenlere gönderme.
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

            idkeeper.Text = row.Cells[0].Value.ToString();
            txtNAME.Text = row.Cells[1].Value.ToString();
            txtBRAND.Text = row.Cells[2].Value.ToString();
            txtMODEL.Text = row.Cells[3].Value.ToString();
            msktxtYEAR.Text = row.Cells[4].Value.ToString();
            numQUANTITY.Value = Convert.ToDecimal(row.Cells[5].Value);
            txtBUY.Text = row.Cells[6].Value.ToString();
            txtSELL.Text = row.Cells[7].Value.ToString();
            richDSCRB.Text = row.Cells[8].Value.ToString();
        }

        private void addBTN_Click(object sender, EventArgs e)
        {
            //database'e ve datagrid'e yeni ürün ekleme.
            string query = "insert into TBL_PRODUCTS (ID,PRODUCTNAME,PRODUCTBRAND,MODEL,YEAR,STOCK,BUYPRİCE,SELLPRİCE,DETAIL) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)";

            CMD = new SqlCommand(query, CONNECT.connection());
            CMD.Parameters.AddWithValue("@p1", FB.generatePID());
            CMD.Parameters.AddWithValue("@p2", txtNAME.Text);
            CMD.Parameters.AddWithValue("@p3", txtBRAND.Text);
            CMD.Parameters.AddWithValue("@p4", txtMODEL.Text);
            CMD.Parameters.AddWithValue("@p5", msktxtYEAR.Text);
            CMD.Parameters.AddWithValue("@p6", int.Parse((numQUANTITY.Value).ToString()));
            CMD.Parameters.AddWithValue("@p7", decimal.Parse(txtBUY.Text));
            CMD.Parameters.AddWithValue("@p8", decimal.Parse(txtSELL.Text));
            CMD.Parameters.AddWithValue("@p9", richDSCRB.Text);
            CMD.ExecuteNonQuery();
            CONNECT.connection().Close();
            MessageBox.Show("EKLENDİ");

            //reflesh table
            FB.listAll(query, dataGridView1);
        }

        private void updtBTN_Click(object sender, EventArgs e)
        {
            //datagrid'e tıkladıktan sonra textboxtaki bilgiler üzerinden güncelleme.
            string query = "update TBL_PRODUCTS set PRODUCTNAME =@p1, PRODUCTBRAND=@p2, MODEL=@p3, YEAR=@p4, STOCK=@p5, BUYPRİCE=@p6, SELLPRİCE=@p7, DETAIL=@p8 where ID=@p0";

            CMD = new SqlCommand(query, CONNECT.connection());
            CMD.Parameters.AddWithValue("@p1", txtNAME.Text);
            CMD.Parameters.AddWithValue("@p2", txtBRAND.Text);
            CMD.Parameters.AddWithValue("@p3", txtMODEL.Text);
            CMD.Parameters.AddWithValue("@p4", msktxtYEAR.Text);
            CMD.Parameters.AddWithValue("@p5", numQUANTITY.Value);
            CMD.Parameters.AddWithValue("@p6", txtBUY.Text);
            CMD.Parameters.AddWithValue("@p7", txtSELL.Text);
            CMD.Parameters.AddWithValue("@p8", richDSCRB.Text);
            
            // condition (WHERE)
            CMD.Parameters.AddWithValue("@p0", idkeeper.Text);
            CMD.ExecuteNonQuery();
            CONNECT.connection().Close();
            MessageBox.Show("Güncellendi....");

            // reflesh table
            FB.listAll(query, dataGridView1);
        }

        private void dltBTN_Click(object sender, EventArgs e)
        {
            // textbox'tan gelen id'ye göre ürün silme.
            string query = "delete from TBL_PRODUCTS where ID=@p0";
            CMD = new SqlCommand(query, CONNECT.connection());
            CMD.Parameters.AddWithValue("@p0", idkeeper.Text);
            CMD.ExecuteNonQuery();
            CONNECT.connection().Close();

            // reflesh table
            FB.listAll(query, dataGridView1);
        }
    }
}
