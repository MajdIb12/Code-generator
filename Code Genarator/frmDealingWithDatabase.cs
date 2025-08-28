using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussenisLayer;

namespace Code_Genarator
{
    public partial class frmDealingWithDatabase : Form
    {
        public frmDealingWithDatabase()
        {
            InitializeComponent();
        }

        private void frmDealingWithDatabase_Load(object sender, EventArgs e)
        {
            DataTable DataBases = clsGlobal.SqlDataBases();
            foreach (DataRow Row in DataBases.Rows)
            {
                cbDataBases.Items.Add(Row[0]);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            clsGlobal.DataBaseName = cbDataBases.Text;
            if (clsGlobal.CheckIsDataBaseExist())
            {
                frmPathFiles frmPathFiles = new frmPathFiles();
                frmPathFiles.ShowDialog();
            }
            
        }
    }
}
