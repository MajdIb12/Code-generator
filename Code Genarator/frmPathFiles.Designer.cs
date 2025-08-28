namespace Code_Genarator
{
    partial class frmPathFiles
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cbSp = new System.Windows.Forms.CheckBox();
            this.txtBrowse = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.cbDataAccesLayer = new System.Windows.Forms.CheckBox();
            this.cbBussenisLayer = new System.Windows.Forms.CheckBox();
            this.btnGenrate = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.rdSync = new System.Windows.Forms.RadioButton();
            this.rdAsync = new System.Windows.Forms.RadioButton();
            this.rdMulti = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(152, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path Direction";
            // 
            // cbSp
            // 
            this.cbSp.AutoSize = true;
            this.cbSp.Location = new System.Drawing.Point(30, 188);
            this.cbSp.Name = "cbSp";
            this.cbSp.Size = new System.Drawing.Size(90, 20);
            this.cbSp.TabIndex = 1;
            this.cbSp.Text = "Create SP";
            this.cbSp.UseVisualStyleBackColor = true;
            // 
            // txtBrowse
            // 
            this.txtBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBrowse.Location = new System.Drawing.Point(30, 91);
            this.txtBrowse.Name = "txtBrowse";
            this.txtBrowse.ReadOnly = true;
            this.txtBrowse.Size = new System.Drawing.Size(432, 22);
            this.txtBrowse.TabIndex = 2;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(522, 91);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(105, 30);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // cbDataAccesLayer
            // 
            this.cbDataAccesLayer.AutoSize = true;
            this.cbDataAccesLayer.Location = new System.Drawing.Point(148, 188);
            this.cbDataAccesLayer.Name = "cbDataAccesLayer";
            this.cbDataAccesLayer.Size = new System.Drawing.Size(173, 20);
            this.cbDataAccesLayer.TabIndex = 4;
            this.cbDataAccesLayer.Text = "Create DataAccesLayer";
            this.cbDataAccesLayer.UseVisualStyleBackColor = true;
            // 
            // cbBussenisLayer
            // 
            this.cbBussenisLayer.AutoSize = true;
            this.cbBussenisLayer.Location = new System.Drawing.Point(349, 188);
            this.cbBussenisLayer.Name = "cbBussenisLayer";
            this.cbBussenisLayer.Size = new System.Drawing.Size(161, 20);
            this.cbBussenisLayer.TabIndex = 5;
            this.cbBussenisLayer.Text = "Create BussenisLayer";
            this.cbBussenisLayer.UseVisualStyleBackColor = true;
            // 
            // btnGenrate
            // 
            this.btnGenrate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenrate.Location = new System.Drawing.Point(590, 244);
            this.btnGenrate.Name = "btnGenrate";
            this.btnGenrate.Size = new System.Drawing.Size(105, 36);
            this.btnGenrate.TabIndex = 6;
            this.btnGenrate.Text = "Genrate";
            this.btnGenrate.UseVisualStyleBackColor = true;
            this.btnGenrate.Click += new System.EventHandler(this.btnGenrate_Click);
            // 
            // rdSync
            // 
            this.rdSync.AutoSize = true;
            this.rdSync.Checked = true;
            this.rdSync.Location = new System.Drawing.Point(30, 244);
            this.rdSync.Name = "rdSync";
            this.rdSync.Size = new System.Drawing.Size(109, 20);
            this.rdSync.TabIndex = 7;
            this.rdSync.TabStop = true;
            this.rdSync.Text = "Sync Genrate";
            this.rdSync.UseVisualStyleBackColor = true;
            // 
            // rdAsync
            // 
            this.rdAsync.AutoSize = true;
            this.rdAsync.Location = new System.Drawing.Point(148, 244);
            this.rdAsync.Name = "rdAsync";
            this.rdAsync.Size = new System.Drawing.Size(116, 20);
            this.rdAsync.TabIndex = 8;
            this.rdAsync.Text = "Async Genrate";
            this.rdAsync.UseVisualStyleBackColor = true;
            // 
            // rdMulti
            // 
            this.rdMulti.AutoSize = true;
            this.rdMulti.Location = new System.Drawing.Point(281, 244);
            this.rdMulti.Name = "rdMulti";
            this.rdMulti.Size = new System.Drawing.Size(168, 20);
            this.rdMulti.TabIndex = 9;
            this.rdMulti.Text = "MultiThreading Genrate";
            this.rdMulti.UseVisualStyleBackColor = true;
            // 
            // frmPathFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 301);
            this.Controls.Add(this.rdMulti);
            this.Controls.Add(this.rdAsync);
            this.Controls.Add(this.rdSync);
            this.Controls.Add(this.btnGenrate);
            this.Controls.Add(this.cbBussenisLayer);
            this.Controls.Add(this.cbDataAccesLayer);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtBrowse);
            this.Controls.Add(this.cbSp);
            this.Controls.Add(this.label1);
            this.Name = "frmPathFiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPathFiles";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbSp;
        private System.Windows.Forms.TextBox txtBrowse;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.CheckBox cbDataAccesLayer;
        private System.Windows.Forms.CheckBox cbBussenisLayer;
        private System.Windows.Forms.Button btnGenrate;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.RadioButton rdSync;
        private System.Windows.Forms.RadioButton rdAsync;
        private System.Windows.Forms.RadioButton rdMulti;
    }
}