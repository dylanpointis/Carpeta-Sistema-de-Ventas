namespace Carpeta_Sistema_de_Ventas
{
    partial class frmCorroborarRecepcion
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
            this.label6 = new System.Windows.Forms.Label();
            this.grillaRecepcion = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCantRecibida = new System.Windows.Forms.NumericUpDown();
            this.btnCargar = new System.Windows.Forms.Button();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNumTel = new System.Windows.Forms.Label();
            this.lblCBU = new System.Windows.Forms.Label();
            this.lblCUIT = new System.Windows.Forms.Label();
            this.lblRazonSocial = new System.Windows.Forms.Label();
            this.lblNombreProv = new System.Windows.Forms.Label();
            this.lblMailProv = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbOrdenesCompra = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOrden = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNumFactura = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.grillaRecepcion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantRecibida)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumFactura)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(19, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 30);
            this.label6.TabIndex = 68;
            this.label6.Text = "Ordenes de compra\r\npendientes de entrega:";
            // 
            // grillaRecepcion
            // 
            this.grillaRecepcion.AllowUserToAddRows = false;
            this.grillaRecepcion.AllowUserToDeleteRows = false;
            this.grillaRecepcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grillaRecepcion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grillaRecepcion.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grillaRecepcion.Location = new System.Drawing.Point(265, 43);
            this.grillaRecepcion.MultiSelect = false;
            this.grillaRecepcion.Name = "grillaRecepcion";
            this.grillaRecepcion.ReadOnly = true;
            this.grillaRecepcion.Size = new System.Drawing.Size(748, 327);
            this.grillaRecepcion.TabIndex = 69;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(264, 407);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 16);
            this.label5.TabIndex = 72;
            this.label5.Text = "Cantidad recibida:";
            // 
            // txtCantRecibida
            // 
            this.txtCantRecibida.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCantRecibida.Location = new System.Drawing.Point(265, 431);
            this.txtCantRecibida.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.txtCantRecibida.Name = "txtCantRecibida";
            this.txtCantRecibida.Size = new System.Drawing.Size(144, 20);
            this.txtCantRecibida.TabIndex = 71;
            this.txtCantRecibida.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantRecibida_KeyPress);
            // 
            // btnCargar
            // 
            this.btnCargar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCargar.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnCargar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCargar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargar.ForeColor = System.Drawing.Color.White;
            this.btnCargar.Location = new System.Drawing.Point(636, 407);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(163, 44);
            this.btnCargar.TabIndex = 70;
            this.btnCargar.Text = "Cargar";
            this.btnCargar.UseVisualStyleBackColor = false;
            this.btnCargar.Click += new System.EventHandler(this.btnCargarCantidadRecibida_Click);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinalizar.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnFinalizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizar.ForeColor = System.Drawing.Color.White;
            this.btnFinalizar.Location = new System.Drawing.Point(892, 399);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(121, 52);
            this.btnFinalizar.TabIndex = 73;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.UseVisualStyleBackColor = false;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.lblNumTel);
            this.groupBox1.Controls.Add(this.lblCBU);
            this.groupBox1.Controls.Add(this.lblCUIT);
            this.groupBox1.Controls.Add(this.lblRazonSocial);
            this.groupBox1.Controls.Add(this.lblNombreProv);
            this.groupBox1.Controls.Add(this.lblMailProv);
            this.groupBox1.Location = new System.Drawing.Point(22, 219);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 232);
            this.groupBox1.TabIndex = 75;
            this.groupBox1.TabStop = false;
            // 
            // lblNumTel
            // 
            this.lblNumTel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumTel.AutoSize = true;
            this.lblNumTel.Location = new System.Drawing.Point(98, 78);
            this.lblNumTel.Name = "lblNumTel";
            this.lblNumTel.Size = new System.Drawing.Size(103, 13);
            this.lblNumTel.TabIndex = 65;
            this.lblNumTel.Text = "Número de telefono:";
            // 
            // lblCBU
            // 
            this.lblCBU.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCBU.AutoSize = true;
            this.lblCBU.Location = new System.Drawing.Point(5, 138);
            this.lblCBU.Name = "lblCBU";
            this.lblCBU.Size = new System.Drawing.Size(35, 13);
            this.lblCBU.TabIndex = 60;
            this.lblCBU.Text = "CBU: ";
            // 
            // lblCUIT
            // 
            this.lblCUIT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCUIT.AutoSize = true;
            this.lblCUIT.Location = new System.Drawing.Point(5, 199);
            this.lblCUIT.Name = "lblCUIT";
            this.lblCUIT.Size = new System.Drawing.Size(35, 13);
            this.lblCUIT.TabIndex = 57;
            this.lblCUIT.Text = "CUIT:";
            // 
            // lblRazonSocial
            // 
            this.lblRazonSocial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRazonSocial.AutoSize = true;
            this.lblRazonSocial.Location = new System.Drawing.Point(98, 25);
            this.lblRazonSocial.Name = "lblRazonSocial";
            this.lblRazonSocial.Size = new System.Drawing.Size(71, 13);
            this.lblRazonSocial.TabIndex = 59;
            this.lblRazonSocial.Text = "Razon social:";
            // 
            // lblNombreProv
            // 
            this.lblNombreProv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNombreProv.AutoSize = true;
            this.lblNombreProv.Location = new System.Drawing.Point(5, 25);
            this.lblNombreProv.Name = "lblNombreProv";
            this.lblNombreProv.Size = new System.Drawing.Size(47, 13);
            this.lblNombreProv.TabIndex = 56;
            this.lblNombreProv.Text = "Nombre:";
            // 
            // lblMailProv
            // 
            this.lblMailProv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMailProv.AutoSize = true;
            this.lblMailProv.Location = new System.Drawing.Point(5, 78);
            this.lblMailProv.Name = "lblMailProv";
            this.lblMailProv.Size = new System.Drawing.Size(32, 13);
            this.lblMailProv.TabIndex = 58;
            this.lblMailProv.Text = "Mail: ";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(22, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 15);
            this.label4.TabIndex = 74;
            this.label4.Text = "Detalle Proveedor:";
            // 
            // cmbOrdenesCompra
            // 
            this.cmbOrdenesCompra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrdenesCompra.FormattingEnabled = true;
            this.cmbOrdenesCompra.Location = new System.Drawing.Point(12, 91);
            this.cmbOrdenesCompra.Name = "cmbOrdenesCompra";
            this.cmbOrdenesCompra.Size = new System.Drawing.Size(217, 21);
            this.cmbOrdenesCompra.TabIndex = 66;
            this.cmbOrdenesCompra.SelectedIndexChanged += new System.EventHandler(this.cmbOrdenesCompra_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(262, 498);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 16);
            this.label1.TabIndex = 76;
            this.label1.Text = "Cantidad total recibida:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(262, 472);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 16);
            this.label2.TabIndex = 77;
            this.label2.Text = "Cantidad total solicitada: ";
            // 
            // lblOrden
            // 
            this.lblOrden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrden.AutoSize = true;
            this.lblOrden.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrden.Location = new System.Drawing.Point(265, 13);
            this.lblOrden.Name = "lblOrden";
            this.lblOrden.Size = new System.Drawing.Size(68, 20);
            this.lblOrden.TabIndex = 78;
            this.lblOrden.Text = "Orden: ";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(424, 407);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 16);
            this.label3.TabIndex = 80;
            this.label3.Text = "Número de factura:";
            // 
            // txtNumFactura
            // 
            this.txtNumFactura.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumFactura.Location = new System.Drawing.Point(425, 431);
            this.txtNumFactura.Maximum = new decimal(new int[] {
            1569325055,
            23283064,
            0,
            0});
            this.txtNumFactura.Name = "txtNumFactura";
            this.txtNumFactura.Size = new System.Drawing.Size(144, 20);
            this.txtNumFactura.TabIndex = 79;
            this.txtNumFactura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumFactura_KeyPress);
            // 
            // frmCorroborarRecepcion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 529);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNumFactura);
            this.Controls.Add(this.lblOrden);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbOrdenesCompra);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCantRecibida);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.grillaRecepcion);
            this.Controls.Add(this.label6);
            this.Name = "frmCorroborarRecepcion";
            this.Text = "COMPRAfrmConfirmarRecepcion";
            this.Load += new System.EventHandler(this.COMPRAfrmCorroborarRecepcion_Load);
            this.Resize += new System.EventHandler(this.frmCorroborarRecepcion_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.grillaRecepcion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantRecibida)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumFactura)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView grillaRecepcion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown txtCantRecibida;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblNumTel;
        private System.Windows.Forms.Label lblCBU;
        private System.Windows.Forms.Label lblCUIT;
        private System.Windows.Forms.Label lblRazonSocial;
        private System.Windows.Forms.Label lblNombreProv;
        private System.Windows.Forms.Label lblMailProv;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbOrdenesCompra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOrden;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtNumFactura;
    }
}