namespace Carpeta_Sistema_de_Ventas
{
    partial class frmRespaldo
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
            this.txtBackupRuta = new System.Windows.Forms.TextBox();
            this.txtRestoreRuta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRealizarBackUp = new System.Windows.Forms.Button();
            this.btnRealizarRestore = new System.Windows.Forms.Button();
            this.btnRutaBackUp = new System.Windows.Forms.Button();
            this.btnRutaRestore = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBackupRuta
            // 
            this.txtBackupRuta.Location = new System.Drawing.Point(47, 64);
            this.txtBackupRuta.Name = "txtBackupRuta";
            this.txtBackupRuta.ReadOnly = true;
            this.txtBackupRuta.Size = new System.Drawing.Size(277, 20);
            this.txtBackupRuta.TabIndex = 3;
            // 
            // txtRestoreRuta
            // 
            this.txtRestoreRuta.Location = new System.Drawing.Point(47, 190);
            this.txtRestoreRuta.Name = "txtRestoreRuta";
            this.txtRestoreRuta.ReadOnly = true;
            this.txtRestoreRuta.Size = new System.Drawing.Size(277, 20);
            this.txtRestoreRuta.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(44, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Restore:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "Backup:";
            // 
            // btnRealizarBackUp
            // 
            this.btnRealizarBackUp.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnRealizarBackUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRealizarBackUp.ForeColor = System.Drawing.Color.White;
            this.btnRealizarBackUp.Location = new System.Drawing.Point(47, 93);
            this.btnRealizarBackUp.Name = "btnRealizarBackUp";
            this.btnRealizarBackUp.Size = new System.Drawing.Size(108, 32);
            this.btnRealizarBackUp.TabIndex = 97;
            this.btnRealizarBackUp.Text = "Guardar Backup";
            this.btnRealizarBackUp.UseVisualStyleBackColor = false;
            this.btnRealizarBackUp.Click += new System.EventHandler(this.btnRealizarBackUp_Click);
            // 
            // btnRealizarRestore
            // 
            this.btnRealizarRestore.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnRealizarRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRealizarRestore.ForeColor = System.Drawing.Color.White;
            this.btnRealizarRestore.Location = new System.Drawing.Point(47, 221);
            this.btnRealizarRestore.Name = "btnRealizarRestore";
            this.btnRealizarRestore.Size = new System.Drawing.Size(108, 32);
            this.btnRealizarRestore.TabIndex = 98;
            this.btnRealizarRestore.Text = "Realizar Restore";
            this.btnRealizarRestore.UseVisualStyleBackColor = false;
            this.btnRealizarRestore.Click += new System.EventHandler(this.btnRealizarRestore_Click);
            // 
            // btnRutaBackUp
            // 
            this.btnRutaBackUp.BackColor = System.Drawing.SystemColors.Control;
            this.btnRutaBackUp.BackgroundImage = global::Carpeta_Sistema_de_Ventas.Properties.Resources.foldericon2;
            this.btnRutaBackUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRutaBackUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRutaBackUp.Location = new System.Drawing.Point(340, 30);
            this.btnRutaBackUp.Name = "btnRutaBackUp";
            this.btnRutaBackUp.Size = new System.Drawing.Size(54, 54);
            this.btnRutaBackUp.TabIndex = 99;
            this.btnRutaBackUp.UseVisualStyleBackColor = false;
            this.btnRutaBackUp.Click += new System.EventHandler(this.btnRutaBackUp_Click);
            // 
            // btnRutaRestore
            // 
            this.btnRutaRestore.BackColor = System.Drawing.SystemColors.Control;
            this.btnRutaRestore.BackgroundImage = global::Carpeta_Sistema_de_Ventas.Properties.Resources.foldericon2;
            this.btnRutaRestore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRutaRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRutaRestore.Location = new System.Drawing.Point(340, 156);
            this.btnRutaRestore.Name = "btnRutaRestore";
            this.btnRutaRestore.Size = new System.Drawing.Size(54, 54);
            this.btnRutaRestore.TabIndex = 100;
            this.btnRutaRestore.UseVisualStyleBackColor = false;
            this.btnRutaRestore.Click += new System.EventHandler(this.btnRutaRestore_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnRutaRestore);
            this.groupBox1.Controls.Add(this.btnRutaBackUp);
            this.groupBox1.Controls.Add(this.btnRealizarRestore);
            this.groupBox1.Controls.Add(this.btnRealizarBackUp);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtRestoreRuta);
            this.groupBox1.Controls.Add(this.txtBackupRuta);
            this.groupBox1.Location = new System.Drawing.Point(241, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(452, 280);
            this.groupBox1.TabIndex = 101;
            this.groupBox1.TabStop = false;
            // 
            // frmRespaldo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(988, 535);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmRespaldo";
            this.Text = "frmRespaldo";
            this.Load += new System.EventHandler(this.frmRespaldo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtBackupRuta;
        private System.Windows.Forms.TextBox txtRestoreRuta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRealizarBackUp;
        private System.Windows.Forms.Button btnRealizarRestore;
        private System.Windows.Forms.Button btnRutaBackUp;
        private System.Windows.Forms.Button btnRutaRestore;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}