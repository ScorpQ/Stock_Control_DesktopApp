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
    public partial class customer : Form
    {
        // Collection of the Classes.
        sqlcon CONNECT = new sqlcon();
        SqlCommand CMD;
        SqlDataReader DR;
        SqlDataAdapter DA;
        DataTable DT;
        funcBase FB = new funcBase();
        string query = "select * from TBL_COSTUMERS";

        // Bileşenleri temizlemek için.
        void Clear()
        {
            txtNAME.Clear();
            txtSURNAME.Clear();
            mskPHONE1.Clear();
            mskPHONE2.Clear();
            mskTC.Clear();
            txtMAIL.Clear();
            richADRESS.Clear();
            txtTAX.Clear();

            txtNAME.Focus();
        }

        public customer()
        {
            InitializeComponent();
        }

        private void customer_Load(object sender, EventArgs e)
        {
            FB.listAll(query, dataGridView1);

            //ilce combobox'a sql'den data yükleme. Devamı 139. satırda.
            cmbPROVINCE.Text = "Seciniz...";
            CMD = new SqlCommand("select * from TBL_PROVINCES", CONNECT.connection());
            DR = CMD.ExecuteReader();
            while (DR.Read())
            {
                cmbPROVINCE.Items.Add(DR[1]);
            }
            CONNECT.connection().Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // datagrid'tek dataları bileşenlere taşıma.
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

            idkeeper.Text = row.Cells[0].Value.ToString();
            txtNAME.Text = row.Cells[1].Value.ToString();
            txtSURNAME.Text = row.Cells[2].Value.ToString();
            mskPHONE1.Text = row.Cells[3].Value.ToString();
            mskPHONE2.Text = row.Cells[4].Value.ToString();
            mskTC.Text = row.Cells[5].Value.ToString();
            txtMAIL.Text = row.Cells[6].Value.ToString();
            cmbPROVINCE.Text = row.Cells[7].Value.ToString();
            cmbTOWN.Text = row.Cells[8].Value.ToString();
            richADRESS.Text = row.Cells[9].Value.ToString();
            txtTAX.Text = row.Cells[10].Value.ToString();
        }

        private void addBTN_Click(object sender, EventArgs e)
        {
            // müsteri kaydetme.
            CMD = new SqlCommand("insert into TBL_COSTUMERS (ID,NAME,SURNAME,PHONE,PHONE2,[IN],MAIL,PROVINCE,TOWN,ADRESS,TAXDEPARTMENT) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", CONNECT.connection());
            CMD.Parameters.AddWithValue("@p1", FB.generateCID());
            CMD.Parameters.AddWithValue("@p2", txtNAME.Text);
            CMD.Parameters.AddWithValue("@p3", txtSURNAME.Text);
            CMD.Parameters.AddWithValue("@p4", mskPHONE1.Text);
            CMD.Parameters.AddWithValue("@p5", mskPHONE2.Text);
            CMD.Parameters.AddWithValue("@p6", mskTC.Text);
            CMD.Parameters.AddWithValue("@p7", txtMAIL.Text);
            CMD.Parameters.AddWithValue("@p8", cmbPROVINCE.Text);
            CMD.Parameters.AddWithValue("@p9", cmbTOWN.Text);
            CMD.Parameters.AddWithValue("@p10", richADRESS.Text);
            CMD.Parameters.AddWithValue("@p11", txtTAX.Text);
            CMD.ExecuteNonQuery();
            CONNECT.connection().Close();

            // Ekleme işleminden sonra bileşenleri temizler. Detay için 26. satıra git.
            Clear();
            FB.listAll(query,dataGridView1);
        }

        private void updtBTN_Click(object sender, EventArgs e)
        {
            // Müşterilerin bilgilerini güncelleme.
            CMD = new SqlCommand("update TBL_COSTUMERS set NAME=@p1,SURNAME=@p2,PHONE=@p3,PHONE2=@p4,[IN]=@p5,MAIL=@p6,PROVINCE=@p7,TOWN=@p8,ADRESS=@p9,TAXDEPARTMENT=@p10 WHERE ID=@p0", CONNECT.connection());
            CMD.Parameters.AddWithValue("@p0", idkeeper.Text);
            CMD.Parameters.AddWithValue("@p1", txtNAME.Text);
            CMD.Parameters.AddWithValue("@p2", txtSURNAME.Text);
            CMD.Parameters.AddWithValue("@p3", mskPHONE1.Text);
            CMD.Parameters.AddWithValue("@p4", mskPHONE2.Text);
            CMD.Parameters.AddWithValue("@p5", mskTC.Text);
            CMD.Parameters.AddWithValue("@p6", txtMAIL.Text);
            CMD.Parameters.AddWithValue("@p7", cmbPROVINCE.Text);
            CMD.Parameters.AddWithValue("@p8", cmbTOWN.Text);
            CMD.Parameters.AddWithValue("@p9", richADRESS.Text);
            CMD.Parameters.AddWithValue("@p10", txtTAX.Text);
            CMD.ExecuteNonQuery();

            // Güncelleme işleminden sonra bileşenleri temizler. Detay için 26. satıra git.
            Clear();
            FB.listAll(query, dataGridView1);
        }

        private void dltBTN_Click(object sender, EventArgs e)
        {
            // silmeden önce MessageBox ile emin misin diye sor.
            DialogResult dialogResult = MessageBox.Show("Musteriyi silmek istediginden emin misin?", "Onayla", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                CMD = new SqlCommand("delete from TBL_COSTUMERS where ID=@p1", CONNECT.connection());
                CMD.Parameters.AddWithValue("@p1", idkeeper.Text);
                CMD.ExecuteNonQuery();
                CONNECT.connection().Close();
            }

            // Güncelleme işleminden sonra bileşenleri temizler. Detay için 26. satıra git.
            Clear();
            FB.listAll(query, dataGridView1);
        }

        private void cmbPROVINCE_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbTOWN.Items.Clear();
            // sehirler combobox'ından secilen sehirin ilcelerini gösterme.             ********
            CMD = new SqlCommand("select * from TBL_TOWNS where PROVINCEID = @p1", CONNECT.connection());
            CMD.Parameters.AddWithValue("@p1", cmbPROVINCE.SelectedIndex + 1);
            DR = CMD.ExecuteReader();
            while (DR.Read())
            {
                cmbTOWN.Items.Add(DR[1]);
            }
            CONNECT.connection().Close();
        }
    }
    
}
