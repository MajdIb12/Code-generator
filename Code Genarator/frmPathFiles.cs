using BussenisLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Code_Genarator
{
    public partial class frmPathFiles : Form
    {
        public frmPathFiles()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDialog1.SelectedPath;
                if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                {
                    clsGlobal.PathFiles = path;
                    txtBrowse.Text = path;
                }
                else
                {
                    MessageBox.Show("Invalid Path");
                }
            }
        }

        private void btnGenrate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBrowse.Text))
            {
                MessageBox.Show("Fillled");
                return;
            }
            if (rdSync.Checked)
            {
                GenrateSPs();
                GenrateDataAcces();
                GenrateBuissenis();
            }
            if (rdMulti.Checked)
            {
                MultiThreadingGenrate();
            }
            if (rdAsync.Checked)
            {
                AsyncGenrate();
            }
            
        }

        private void MultiThreadingGenrate()
        {
            Thread Method1 = new Thread(GenrateSPs);
            Thread Method2 = new Thread(GenrateDataAcces);
            Thread Method3 = new Thread(GenrateBuissenis);

            Method1.Start();
            Method2.Start();
            Method3.Start();
        }

        private void AsyncGenrate()
        {
            Task task1 = GenrateSPsAsync();
            Task task2 = GenrateDataAccesAsync();
            Task task3 = GenrateBuissenisAsync();

            //await Task.WhenAll(task1, task2, task3);
        }

        private void GenrateSPs()
        {
            clsCreateSp NewSPs = new clsCreateSp();
            if (cbSp.Checked)
            {
                if (NewSPs.CreateSPsFile())
                {
                    MessageBox.Show("Stored procedure created Successfuly");
                }
                else
                {
                    MessageBox.Show("Falid to create stored Procedure");
                }
            }
        }

        private void GenrateDataAcces()
        {
            clsCreateDataAccesLayer accesLayer = new clsCreateDataAccesLayer();
            if (cbDataAccesLayer.Checked)
            {
                if (accesLayer.CreateDataAccesLayer())
                {
                    MessageBox.Show("Data acces layer created Successfuly");
                }
                else
                {
                    MessageBox.Show("Falid to create Data acces layer");
                }
            }
        }

        private void GenrateBuissenis()
        {
            clsCreateBussenisLayer bus = new clsCreateBussenisLayer();
            if (cbBussenisLayer.Checked)
            {
                if (bus.CreateBussenisLayer())
                {
                    MessageBox.Show("Busseines layer created Successfuly");
                }
                else
                {
                    MessageBox.Show("Falid to create Busseines layer");
                }
            }
        }


        private async Task GenrateSPsAsync()
        {
            clsCreateSp NewSPs = new clsCreateSp();
            if (cbSp.Checked)
            {
                
                if (NewSPs.CreateSPsFile())
                {
                    MessageBox.Show("Stored procedure created Successfuly");
                }
                else
                {
                    MessageBox.Show("Falid to create stored Procedure");
                }
            }
        }

        private async Task GenrateDataAccesAsync()
        {
            clsCreateDataAccesLayer accesLayer = new clsCreateDataAccesLayer();
            if (cbDataAccesLayer.Checked)
            {
                if (accesLayer.CreateDataAccesLayer())
                {
                    MessageBox.Show("Data acces layer created Successfuly");
                }
                else
                {
                    MessageBox.Show("Falid to create Data acces layer");
                }
            }
        }

        private async Task GenrateBuissenisAsync()
        {
            clsCreateBussenisLayer bus = new clsCreateBussenisLayer();
            if (cbBussenisLayer.Checked)
            {
                if (bus.CreateBussenisLayer())
                {
                    MessageBox.Show("Busseines layer created Successfuly");
                }
                else
                {
                    MessageBox.Show("Falid to create Busseines layer");
                }
            }
        }
    }
}
