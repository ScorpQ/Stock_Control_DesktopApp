using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


// Neccecerys
using Stock_Control_DesktopApp.AllChildPanels;
using System.Data.SqlClient;


namespace Stock_Control_DesktopApp
{
    public partial class mainmenu : Form
    {
        product PRDCT;
        customer CSTRM;
        firm FRM;

        public mainmenu()
        {
            InitializeComponent();
        }
    
        private void prdctBTN_Click(object sender, EventArgs e)
        {
            monitorPanel.Controls.Clear();
            PRDCT = new product();
            PRDCT.TopLevel = false;
            monitorPanel.Controls.Add(PRDCT);
            PRDCT.Show();
        }

        private void cstrmBTN_Click(object sender, EventArgs e)
        {
            monitorPanel.Controls.Clear();
            CSTRM = new customer();
            CSTRM.TopLevel = false;
            monitorPanel.Controls.Add(CSTRM);
            CSTRM.Show();
        } 
       
        private void frmBTN_Click(object sender, EventArgs e)
        {
            monitorPanel.Controls.Clear();
            FRM = new firm();
            FRM.TopLevel = false;
            monitorPanel.Controls.Add(FRM);
            FRM.Show();
        }

        private void exitBTN_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    
    }
}







/*
  protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }
        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;
 
 */