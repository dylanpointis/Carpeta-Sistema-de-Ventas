namespace Carpeta_Sistema_de_Ventas
{
    partial class COMPRAfrmGenerarOrdenCompra
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
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.grillaItems = new System.Windows.Forms.DataGridView();
            this.btnCargar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRegistarPago = new System.Windows.Forms.Button();
            this.cmbSolicitudesCotizacion = new System.Windows.Forms.ComboBox();
            this.lblRazonSocial = new System.Windows.Forms.Label();
            this.lblMailProv = new System.Windows.Forms.Label();
            this.lblCUIT = new System.Windows.Forms.Label();
            this.lblNombreProv = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNumTel = new System.Windows.Forms.Label();
            this.lblCBU = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRegistrarProveedor = new System.Windows.Forms.Button();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.cmbProveedorFinal = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnModificarCant = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grillaItems)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 24);
            this.label3.TabIndex = 47;
            this.label3.Text = "Solicitudes de cotización:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Location = new System.Drawing.Point(345, 435);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(144, 20);
            this.numericUpDown1.TabIndex = 49;
            // 
            // grillaItems
            // 
            this.grillaItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grillaItems.Location = new System.Drawing.Point(312, 78);
            this.grillaItems.Name = "grillaItems";
            this.grillaItems.Size = new System.Drawing.Size(701, 316);
            this.grillaItems.TabIndex = 50;
            // 
            // btnCargar
            // 
            this.btnCargar.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnCargar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCargar.ForeColor = System.Drawing.Color.White;
            this.btnCargar.Location = new System.Drawing.Point(514, 423);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(156, 32);
            this.btnCargar.TabIndex = 51;
            this.btnCargar.Text = "Cargar precio de compra";
            this.btnCargar.UseVisualStyleBackColor = false;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(308, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 24);
            this.label1.TabIndex = 52;
            this.label1.Text = "Productos a reponer:";
            // 
            // btnRegistarPago
            // 
            this.btnRegistarPago.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRegistarPago.BackColor = System.Drawing.SystemColors.Control;
            this.btnRegistarPago.Enabled = false;
            this.btnRegistarPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistarPago.ForeColor = System.Drawing.Color.Black;
            this.btnRegistarPago.Location = new System.Drawing.Point(742, 417);
            this.btnRegistarPago.Name = "btnRegistarPago";
            this.btnRegistarPago.Size = new System.Drawing.Size(132, 52);
            this.btnRegistarPago.TabIndex = 53;
            this.btnRegistarPago.Text = "Registrar pago";
            this.btnRegistarPago.UseVisualStyleBackColor = false;
            // 
            // cmbSolicitudesCotizacion
            // 
            this.cmbSolicitudesCotizacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSolicitudesCotizacion.FormattingEnabled = true;
            this.cmbSolicitudesCotizacion.Location = new System.Drawing.Point(16, 100);
            this.cmbSolicitudesCotizacion.Name = "cmbSolicitudesCotizacion";
            this.cmbSolicitudesCotizacion.Size = new System.Drawing.Size(217, 21);
            this.cmbSolicitudesCotizacion.TabIndex = 54;
            this.cmbSolicitudesCotizacion.SelectedIndexChanged += new System.EventHandler(this.cmbSolicitudesCotizacion_SelectedIndexChanged);
            // 
            // lblRazonSocial
            // 
            this.lblRazonSocial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRazonSocial.AutoSize = true;
            this.lblRazonSocial.Location = new System.Drawing.Point(181, 25);
            this.lblRazonSocial.Name = "lblRazonSocial";
            this.lblRazonSocial.Size = new System.Drawing.Size(71, 13);
            this.lblRazonSocial.TabIndex = 59;
            this.lblRazonSocial.Text = "Razon social:";
            // 
            // lblMailProv
            // 
            this.lblMailProv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMailProv.AutoSize = true;
            this.lblMailProv.Location = new System.Drawing.Point(2, 78);
            this.lblMailProv.Name = "lblMailProv";
            this.lblMailProv.Size = new System.Drawing.Size(32, 13);
            this.lblMailProv.TabIndex = 58;
            this.lblMailProv.Text = "Mail: ";
            // 
            // lblCUIT
            // 
            this.lblCUIT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCUIT.AutoSize = true;
            this.lblCUIT.Location = new System.Drawing.Point(181, 138);
            this.lblCUIT.Name = "lblCUIT";
            this.lblCUIT.Size = new System.Drawing.Size(35, 13);
            this.lblCUIT.TabIndex = 57;
            this.lblCUIT.Text = "CUIT:";
            // 
            // lblNombreProv
            // 
            this.lblNombreProv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNombreProv.AutoSize = true;
            this.lblNombreProv.Location = new System.Drawing.Point(2, 25);
            this.lblNombreProv.Name = "lblNombreProv";
            this.lblNombreProv.Size = new System.Drawing.Size(47, 13);
            this.lblNombreProv.TabIndex = 56;
            this.lblNombreProv.Text = "Nombre:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 228);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 15);
            this.label4.TabIndex = 55;
            this.label4.Text = "Detalle Proveedor:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblNumTel);
            this.groupBox1.Controls.Add(this.lblCBU);
            this.groupBox1.Controls.Add(this.lblCUIT);
            this.groupBox1.Controls.Add(this.lblRazonSocial);
            this.groupBox1.Controls.Add(this.lblNombreProv);
            this.groupBox1.Controls.Add(this.lblMailProv);
            this.groupBox1.Location = new System.Drawing.Point(16, 256);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 172);
            this.groupBox1.TabIndex = 60;
            this.groupBox1.TabStop = false;
            // 
            // lblNumTel
            // 
            this.lblNumTel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumTel.AutoSize = true;
            this.lblNumTel.Location = new System.Drawing.Point(181, 78);
            this.lblNumTel.Name = "lblNumTel";
            this.lblNumTel.Size = new System.Drawing.Size(103, 13);
            this.lblNumTel.TabIndex = 65;
            this.lblNumTel.Text = "Número de telefono:";
            // 
            // lblCBU
            // 
            this.lblCBU.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCBU.AutoSize = true;
            this.lblCBU.Location = new System.Drawing.Point(2, 138);
            this.lblCBU.Name = "lblCBU";
            this.lblCBU.Size = new System.Drawing.Size(35, 13);
            this.lblCBU.TabIndex = 60;
            this.lblCBU.Text = "CBU: ";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(345, 486);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(144, 20);
            this.numericUpDown2.TabIndex = 61;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(342, 415);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 62;
            this.label2.Text = "Precio de compra";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(345, 470);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 63;
            this.label5.Text = "Cantidad a reponer";
            // 
            // btnRegistrarProveedor
            // 
            this.btnRegistrarProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegistrarProveedor.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRegistrarProveedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarProveedor.ForeColor = System.Drawing.Color.Black;
            this.btnRegistrarProveedor.Location = new System.Drawing.Point(16, 446);
            this.btnRegistrarProveedor.Name = "btnRegistrarProveedor";
            this.btnRegistrarProveedor.Size = new System.Drawing.Size(100, 37);
            this.btnRegistrarProveedor.TabIndex = 64;
            this.btnRegistrarProveedor.Text = "Registrar completamente";
            this.btnRegistrarProveedor.UseVisualStyleBackColor = false;
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFinalizar.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnFinalizar.Enabled = false;
            this.btnFinalizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizar.ForeColor = System.Drawing.Color.White;
            this.btnFinalizar.Location = new System.Drawing.Point(880, 417);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(121, 52);
            this.btnFinalizar.TabIndex = 65;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.UseVisualStyleBackColor = false;
            // 
            // cmbProveedorFinal
            // 
            this.cmbProveedorFinal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProveedorFinal.FormattingEnabled = true;
            this.cmbProveedorFinal.Location = new System.Drawing.Point(16, 193);
            this.cmbProveedorFinal.Name = "cmbProveedorFinal";
            this.cmbProveedorFinal.Size = new System.Drawing.Size(217, 21);
            this.cmbProveedorFinal.TabIndex = 66;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 15);
            this.label6.TabIndex = 67;
            this.label6.Text = "Proveedor final:";
            // 
            // btnModificarCant
            // 
            this.btnModificarCant.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnModificarCant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificarCant.ForeColor = System.Drawing.Color.White;
            this.btnModificarCant.Location = new System.Drawing.Point(514, 474);
            this.btnModificarCant.Name = "btnModificarCant";
            this.btnModificarCant.Size = new System.Drawing.Size(156, 32);
            this.btnModificarCant.TabIndex = 68;
            this.btnModificarCant.Text = "Modificar cantidad a reponer";
            this.btnModificarCant.UseVisualStyleBackColor = false;
            this.btnModificarCant.Click += new System.EventHandler(this.btnModificarCant_Click);
            // 
            // COMPRAfrmGenerarOrdenCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 529);
            this.Controls.Add(this.btnModificarCant);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbProveedorFinal);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.btnRegistrarProveedor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbSolicitudesCotizacion);
            this.Controls.Add(this.btnRegistarPago);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.grillaItems);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label3);
            this.Name = "COMPRAfrmGenerarOrdenCompra";
            this.Text = "COMPRAfrmGenerarOrdenCompra";
            this.Load += new System.EventHandler(this.COMPRAfrmGenerarOrdenCompra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grillaItems)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.DataGridView grillaItems;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRegistarPago;
        private System.Windows.Forms.ComboBox cmbSolicitudesCotizacion;
        private System.Windows.Forms.Label lblRazonSocial;
        private System.Windows.Forms.Label lblMailProv;
        private System.Windows.Forms.Label lblCUIT;
        private System.Windows.Forms.Label lblNombreProv;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCBU;
        private System.Windows.Forms.Button btnRegistrarProveedor;
        private System.Windows.Forms.Label lblNumTel;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.ComboBox cmbProveedorFinal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnModificarCant;
    }
}