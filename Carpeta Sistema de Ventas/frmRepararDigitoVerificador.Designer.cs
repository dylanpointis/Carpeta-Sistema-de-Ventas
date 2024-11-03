namespace Carpeta_Sistema_de_Ventas
{
    partial class frmRepararDigitoVerificador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRepararDigitoVerificador));
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnRecalcularDV = new System.Windows.Forms.Button();
            this.btnRestaurarBD = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(61, 31);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(289, 48);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Inconsistencia presente en la \r\nBase de datos del sistema";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRecalcularDV
            // 
            this.btnRecalcularDV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecalcularDV.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnRecalcularDV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecalcularDV.ForeColor = System.Drawing.Color.White;
            this.btnRecalcularDV.Location = new System.Drawing.Point(125, 115);
            this.btnRecalcularDV.Name = "btnRecalcularDV";
            this.btnRecalcularDV.Size = new System.Drawing.Size(138, 37);
            this.btnRecalcularDV.TabIndex = 30;
            this.btnRecalcularDV.Text = "RECALCULAR DV";
            this.btnRecalcularDV.UseVisualStyleBackColor = false;
            this.btnRecalcularDV.Click += new System.EventHandler(this.btnRecalcularDV_Click);
            // 
            // btnRestaurarBD
            // 
            this.btnRestaurarBD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestaurarBD.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnRestaurarBD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestaurarBD.ForeColor = System.Drawing.Color.White;
            this.btnRestaurarBD.Location = new System.Drawing.Point(125, 158);
            this.btnRestaurarBD.Name = "btnRestaurarBD";
            this.btnRestaurarBD.Size = new System.Drawing.Size(138, 37);
            this.btnRestaurarBD.TabIndex = 31;
            this.btnRestaurarBD.Text = "RESTAURAR BD";
            this.btnRestaurarBD.UseVisualStyleBackColor = false;
            this.btnRestaurarBD.Click += new System.EventHandler(this.btnRestaurarBD_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.BackColor = System.Drawing.SystemColors.Control;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.Black;
            this.btnSalir.Location = new System.Drawing.Point(125, 201);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(138, 37);
            this.btnSalir.TabIndex = 32;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // frmRepararDigitoVerificador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 339);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnRestaurarBD);
            this.Controls.Add(this.btnRecalcularDV);
            this.Controls.Add(this.lblTitulo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRepararDigitoVerificador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnRecalcularDV;
        private System.Windows.Forms.Button btnRestaurarBD;
        private System.Windows.Forms.Button btnSalir;
    }
}