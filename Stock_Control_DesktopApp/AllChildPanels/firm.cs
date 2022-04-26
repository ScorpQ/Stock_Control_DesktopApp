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
    public partial class firm : Form
    {
        // Collection of 
        sqlcon CONNECT = new sqlcon();
        SqlCommand CMD;
        SqlDataReader DR;
        SqlDataAdapter DA;
        DataTable DT;
        funcBase FB = new funcBase();
        string query = "select * from TBL_FIRMS";


        // Bileşenleri temizlemek için.
        void Clear()
        {
            txtNAME.Clear();
            txtSECTOR.Clear();
            mskTC.Clear();
            txtATHFULLNAME.Clear();
            txtATHmajor.Clear();
            mskPHN1.Clear();
            mskPHN2.Clear();
            txtMAIL.Clear();
            mskFAX.Clear();
            txtTAXDep.Clear();
            richDTY.Clear();
            code1.Clear();
            code2.Clear();
            code3.Clear();

            txtNAME.Focus();
        }

        public firm()
        {
            InitializeComponent();
        }

        private void firm_Load(object sender, EventArgs e)
        {
            //ilce combobox'a sql'den data yükleme.
            cmbPROVINCE.Text = "Seciniz...";
            CMD = new SqlCommand("select * from TBL_PROVINCES", CONNECT.connection());
            DR = CMD.ExecuteReader();
            while (DR.Read())
            {
                cmbPROVINCE.Items.Add(DR[1]);
            }
            CONNECT.connection().Close();

            // table 
            FB.listAll(query, dataGridView1);

            //default olarak radio1 true gelsin. -detay için Line 185-.
            radio1.Checked = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // datagrid'tek dataları bileşenlere taşıma.
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            idkeeper.Text = row.Cells[0].Value.ToString();
            txtNAME.Text = row.Cells[1].Value.ToString();
            txtSECTOR.Text = row.Cells[2].Value.ToString();
            mskTC.Text = row.Cells[3].Value.ToString();
            txtATHFULLNAME.Text = row.Cells[4].Value.ToString();
            txtATHmajor.Text = row.Cells[5].Value.ToString();
            mskPHN1.Text = row.Cells[6].Value.ToString();
            mskPHN2.Text = row.Cells[7].Value.ToString();
            txtMAIL.Text = row.Cells[8].Value.ToString();
            mskFAX.Text = row.Cells[9].Value.ToString();
            cmbPROVINCE.Text = row.Cells[10].Value.ToString();
            cmbTOWN.Text = row.Cells[11].Value.ToString();
            txtTAXDep.Text = row.Cells[12].Value.ToString();
            richDTY.Text = row.Cells[13].Value.ToString();
            code1.Text = row.Cells[14].Value.ToString();
            code2.Text = row.Cells[15].Value.ToString();
            code3.Text = row.Cells[16].Value.ToString();
        }

        private void addBTN_Click(object sender, EventArgs e)
        {
            // firma kaydetme.
            CMD = new SqlCommand("insert into TBL_FIRMS (ID, NAME, SECTOR, AUTHORITY_ID, AUTHORITY_FULLNAME, AUTHORITY_POSITION, AUTHORITY_PHONE, AUTHORITY_PHONE2, MAIL, FAX, PROVINCE, TOWN, TAXDEPARTMANT, ADRESS, PRVTCODE1, PRVTCODE2, PRVTCODE3) values (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16, @p17)", CONNECT.connection());
            CMD.Parameters.AddWithValue("@p1", FB.generateCID());
            CMD.Parameters.AddWithValue("@p2", txtNAME.Text);
            CMD.Parameters.AddWithValue("@p3", txtSECTOR.Text);
            CMD.Parameters.AddWithValue("@p4", mskTC.Text);
            CMD.Parameters.AddWithValue("@p5", txtATHFULLNAME.Text);
            CMD.Parameters.AddWithValue("@p6", txtATHmajor.Text);
            CMD.Parameters.AddWithValue("@p7", mskPHN1.Text);
            CMD.Parameters.AddWithValue("@p8", mskPHN2.Text);
            CMD.Parameters.AddWithValue("@p9", txtMAIL.Text);
            CMD.Parameters.AddWithValue("@p10", mskFAX.Text);
            CMD.Parameters.AddWithValue("@p11", cmbPROVINCE.Text);
            CMD.Parameters.AddWithValue("@p12", cmbTOWN.Text);
            CMD.Parameters.AddWithValue("@p13", txtTAXDep.Text);
            CMD.Parameters.AddWithValue("@p14", richDTY.Text);
            CMD.Parameters.AddWithValue("@p15", code1.Text);
            CMD.Parameters.AddWithValue("@p16", code2.Text);
            CMD.Parameters.AddWithValue("@p17", code3.Text);
            CMD.ExecuteNonQuery();
            CONNECT.connection().Close();

            // Componentleri temizlemek için.
            Clear();

            //reflesh table
            FB.listAll(query, dataGridView1);
        }

        private void updtBTN_Click(object sender, EventArgs e)
        {
            //Firma bilgisi güncellemek.
            CMD = new SqlCommand("update TBL_FIRMS set  NAME=@p1, SECTOR=@p2, AUTHORITY_FULLNAME=@p3, AUTHORITY_POSITION=@p4, AUTHORITY_ID=@p5, AUTHORITY_PHONE=@p6, AUTHORITY_PHONE2=@p7, FAX=@p8, MAIL=@p9, PROVINCE=@p10, TOWN=@p11, TAXDEPARTMANT=@p12, ADRESS=@p13, PRVTCODE1=@p14,PRVTCODE2=@p15, PRVTCODE3=@p16 WHERE ID=@p0", CONNECT.connection());
            CMD.Parameters.AddWithValue("@p0", idkeeper.Text);
            CMD.Parameters.AddWithValue("@p1", txtNAME.Text);
            CMD.Parameters.AddWithValue("@p2", txtSECTOR.Text);
            CMD.Parameters.AddWithValue("@p3", txtATHFULLNAME.Text);
            CMD.Parameters.AddWithValue("@p4", txtATHmajor.Text);
            CMD.Parameters.AddWithValue("@p5", mskTC.Text);
            CMD.Parameters.AddWithValue("@p6", mskPHN1.Text);
            CMD.Parameters.AddWithValue("@p7", mskPHN2.Text);
            CMD.Parameters.AddWithValue("@p8", mskFAX.Text);
            CMD.Parameters.AddWithValue("@p9", txtMAIL.Text);
            CMD.Parameters.AddWithValue("@p10", cmbPROVINCE.Text);
            CMD.Parameters.AddWithValue("@p11", cmbTOWN.Text);
            CMD.Parameters.AddWithValue("@p12", txtTAXDep.Text);
            CMD.Parameters.AddWithValue("@p13", richDTY.Text);
            CMD.Parameters.AddWithValue("@p14", code1.Text);
            CMD.Parameters.AddWithValue("@p15", code2.Text);
            CMD.Parameters.AddWithValue("@p16", code3.Text);
            CMD.ExecuteNonQuery();
            CONNECT.connection().Close();

            //reflesh table
            FB.listAll(query, dataGridView1);
        }

        private void dltBTN_Click(object sender, EventArgs e)
        {
            // Firma silmek.
            CMD = new SqlCommand("delete from TBL_FIRMS where ID=@p1", CONNECT.connection());
            CMD.Parameters.AddWithValue("@p1", idkeeper.Text);
            CMD.ExecuteNonQuery();
            CONNECT.connection().Close();

            // Componentleri temizlemek için.
            Clear();

            //reflesh table
            FB.listAll(query, dataGridView1);
        }

        private void cmbPROVINCE_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbTOWN.Items.Clear();

            // sehirler combobox'ından secilen sehirin ilcelerini gösterme.   
            CMD = new SqlCommand("select * from TBL_TOWNS where PROVINCEID=@p1", CONNECT.connection());
            CMD.Parameters.AddWithValue("@p1", cmbPROVINCE.SelectedIndex + 1);
            DR = CMD.ExecuteReader();
            while (DR.Read())
            {
                cmbTOWN.Items.Add(DR[1]);
            }
        }

        private void radio1_CheckedChanged(object sender, EventArgs e)
        {
            //Firma kodlarının açıklamaları.
            CMD = new SqlCommand("select * from TBL_CODES",CONNECT.connection());
            DR = CMD.ExecuteReader();
            if (DR.Read())
                richTextBox2.Text = DR[0].ToString();
        }

        private void radio2_CheckedChanged(object sender, EventArgs e)
        {
            //Firma kodlarının açıklamaları.
            CMD = new SqlCommand("select * from TBL_CODES",CONNECT.connection());
            DR = CMD.ExecuteReader();
            if (DR.Read())
                richTextBox2.Text = DR[1].ToString();
        }

        private void radio3_CheckedChanged(object sender, EventArgs e)
        {
            //Firma kodlarının açıklamaları.
            CMD = new SqlCommand("select * from TBL_CODES", CONNECT.connection());
            DR = CMD.ExecuteReader();
            if (DR.Read())
                richTextBox2.Text = DR[2].ToString();
        }
    }
}
