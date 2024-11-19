namespace Carpeta_Sistema_de_Ventas
{
    partial class frmReporteInteligente
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
            this.grillaDatosReporte = new System.Windows.Forms.DataGridView();
            this.btnGenerarPDF = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbReporte = new System.Windows.Forms.ComboBox();
            this.btnGenerarReporte = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.fechaFin = new System.Windows.Forms.DateTimePicker();
            this.fechaInicio = new System.Windows.Forms.DateTimePicker();
            this.lblFechaFin = new System.Windows.Forms.Label();
            this.lblFechaInicio = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grillaDatosReporte)).BeginInit();
            this.SuspendLayout();
            // 
            // grillaDatosReporte
            // 
            this.grillaDatosReporte.AllowUserToAddRows = false;
            this.grillaDatosReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grillaDatosReporte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grillaDatosReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grillaDatosReporte.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grillaDatosReporte.Location = new System.Drawing.Point(22, 69);
            this.grillaDatosReporte.Name = "grillaDatosReporte";
            this.grillaDatosReporte.Size = new System.Drawing.Size(689, 424);
            this.grillaDatosReporte.TabIndex = 75;
            // 
            // btnGenerarPDF
            // 
            this.btnGenerarPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerarPDF.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnGenerarPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarPDF.ForeColor = System.Drawing.Color.White;
            this.btnGenerarPDF.Location = new System.Drawing.Point(771, 448);
            this.btnGenerarPDF.Name = "btnGenerarPDF";
            this.btnGenerarPDF.Size = new System.Drawing.Size(191, 45);
            this.btnGenerarPDF.TabIndex = 79;
            this.btnGenerarPDF.Text = "Generar PDF";
            this.btnGenerarPDF.UseVisualStyleBackColor = false;
            this.btnGenerarPDF.Click += new System.EventHandler(this.btnGenerarPDF_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 24);
            this.label1.TabIndex = 80;
            this.label1.Text = "Reporte";
            // 
            // cmbReporte
            // 
            this.cmbReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReporte.FormattingEnabled = true;
            this.cmbReporte.Location = new System.Drawing.Point(771, 93);
            this.cmbReporte.Name = "cmbReporte";
            this.cmbReporte.Size = new System.Drawing.Size(191, 21);
            this.cmbReporte.TabIndex = 81;
            this.cmbReporte.SelectedIndexChanged += new System.EventHandler(this.cmbReporte_SelectedIndexChanged);
            // 
            // btnGenerarReporte
            // 
            this.btnGenerarReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerarReporte.BackColor = System.Drawing.SystemColors.Control;
            this.btnGenerarReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarReporte.ForeColor = System.Drawing.Color.Black;
            this.btnGenerarReporte.Location = new System.Drawing.Point(771, 271);
            this.btnGenerarReporte.Name = "btnGenerarReporte";
            this.btnGenerarReporte.Size = new System.Drawing.Size(191, 45);
            this.btnGenerarReporte.TabIndex = 82;
            this.btnGenerarReporte.Text = "Generar Reporte";
            this.btnGenerarReporte.UseVisualStyleBackColor = false;
            this.btnGenerarReporte.Click += new System.EventHandler(this.btnGenerarReporte_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(768, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 83;
            this.label2.Text = "Reporte";
            // 
            // fechaFin
            // 
            this.fechaFin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fechaFin.CustomFormat = "yyyy-MM-dd";
            this.fechaFin.Location = new System.Drawing.Point(771, 217);
            this.fechaFin.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            this.fechaFin.Name = "fechaFin";
            this.fechaFin.Size = new System.Drawing.Size(191, 20);
            this.fechaFin.TabIndex = 102;
            this.fechaFin.Value = new System.DateTime(2024, 8, 20, 0, 0, 0, 0);
            this.fechaFin.ValueChanged += new System.EventHandler(this.fechaFin_ValueChanged);
            // 
            // fechaInicio
            // 
            this.fechaInicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fechaInicio.CustomFormat = "yyyy-MM-dd";
            this.fechaInicio.Location = new System.Drawing.Point(771, 154);
            this.fechaInicio.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            this.fechaInicio.Name = "fechaInicio";
            this.fechaInicio.Size = new System.Drawing.Size(191, 20);
            this.fechaInicio.TabIndex = 101;
            this.fechaInicio.Value = new System.DateTime(2024, 8, 20, 0, 0, 0, 0);
            this.fechaInicio.ValueChanged += new System.EventHandler(this.fechaInicio_ValueChanged);
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.Location = new System.Drawing.Point(768, 201);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(51, 13);
            this.lblFechaFin.TabIndex = 100;
            this.lblFechaFin.Text = "Fecha fin";
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.Location = new System.Drawing.Point(768, 138);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(64, 13);
            this.lblFechaInicio.TabIndex = 99;
            this.lblFechaInicio.Text = "Fecha inicio";
            // 
            // frmReporteInteligente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 535);
            this.Controls.Add(this.fechaFin);
            this.Controls.Add(this.fechaInicio);
            this.Controls.Add(this.lblFechaFin);
            this.Controls.Add(this.lblFechaInicio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGenerarReporte);
            this.Controls.Add(this.cmbReporte);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGenerarPDF);
            this.Controls.Add(this.grillaDatosReporte);
            this.Name = "frmReporteInteligente";
            this.Text = "frmReporteInteligente";
            this.Load += new System.EventHandler(this.frmReporteInteligente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grillaDatosReporte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grillaDatosReporte;
        private System.Windows.Forms.Button btnGenerarPDF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbReporte;
        private System.Windows.Forms.Button btnGenerarReporte;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker fechaFin;
        private System.Windows.Forms.DateTimePicker fechaInicio;
        private System.Windows.Forms.Label lblFechaFin;
        private System.Windows.Forms.Label lblFechaInicio;
    }
}