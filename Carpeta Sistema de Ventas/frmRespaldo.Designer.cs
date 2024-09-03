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
            this.SuspendLayout();
            // 
            // txtBackupRuta
            // 
            this.txtBackupRuta.Location = new System.Drawing.Point(288, 132);
            this.txtBackupRuta.Name = "txtBackupRuta";
            this.txtBackupRuta.ReadOnly = true;
            this.txtBackupRuta.Size = new System.Drawing.Size(277, 20);
            this.txtBackupRuta.TabIndex = 3;
            // 
            // txtRestoreRuta
            // 
            this.txtRestoreRuta.Location = new System.Drawing.Point(288, 258);
            this.txtRestoreRuta.Name = "txtRestoreRuta";
            this.txtRestoreRuta.ReadOnly = true;
            this.txtRestoreRuta.Size = new System.Drawing.Size(277, 20);
            this.txtRestoreRuta.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(285, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Restore:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(285, 111);
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
            this.btnRealizarBackUp.Location = new System.Drawing.Point(288, 161);
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
            this.btnRealizarRestore.Location = new System.Drawing.Point(288, 289);
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
            this.btnRutaBackUp.Location = new System.Drawing.Point(581, 98);
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
            this.btnRutaRestore.Location = new System.Drawing.Point(581, 224);
            this.btnRutaRestore.Name = "btnRutaRestore";
            this.btnRutaRestore.Size = new System.Drawing.Size(54, 54);
            this.btnRutaRestore.TabIndex = 100;
            this.btnRutaRestore.UseVisualStyleBackColor = false;
            this.btnRutaRestore.Click += new System.EventHandler(this.btnRutaRestore_Click);
            // 
            // frmRespaldo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(988, 535);
            this.Controls.Add(this.btnRutaRestore);
            this.Controls.Add(this.btnRutaBackUp);
            this.Controls.Add(this.btnRealizarRestore);
            this.Controls.Add(this.btnRealizarBackUp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRestoreRuta);
            this.Controls.Add(this.txtBackupRuta);
            this.Name = "frmRespaldo";
            this.Text = "frmRespaldo";
            this.Load += new System.EventHandler(this.frmRespaldo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}