using BussenisLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Code_Genarator
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        
        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";
            if (clsRegister.GetAccountFromRegistry(ref UserName, ref Password))
            {
                txtPassword.Text = Password;
                txtUserName.Text = UserName;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (cbRememberMe.Checked)
            {
                clsRegister.SaveRegistry(txtUserName.Text, txtPassword.Text);
            }
            else
            {
                clsRegister.DeleteRegistry();
            }
            clsGlobal.UserName = txtUserName.Text.Trim();
            clsGlobal.Password = txtPassword.Text.Trim();
            frmDealingWithDatabase frmDealingWithDatabase = new frmDealingWithDatabase();
            frmDealingWithDatabase.ShowDialog();
        }
    }
}
