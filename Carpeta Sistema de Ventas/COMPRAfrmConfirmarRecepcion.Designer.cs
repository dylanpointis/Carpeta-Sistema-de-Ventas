namespace Carpeta_Sistema_de_Ventas
{
    partial class COMPRAfrmConfirmarRecepcion
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.CodProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantidadActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantMinima = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantAReponer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantRecibida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.btnCambiarClave = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNumTel = new System.Windows.Forms.Label();
            this.lblCBU = new System.Windows.Forms.Label();
            this.lblApellidoCliente = new System.Windows.Forms.Label();
            this.lblDNICliente = new System.Windows.Forms.Label();
            this.lblNombreCliente = new System.Windows.Forms.Label();
            this.lblMailCliente = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(22, 81);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(173, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(19, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 30);
            this.label6.TabIndex = 68;
            this.label6.Text = "Ordenes de compra\r\npendientes de entrega:";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodProducto,
            this.CantidadActual,
            this.CantMinima,
            this.CantMax,
            this.CantAReponer,
            this.CantRecibida});
            this.dataGridView2.Location = new System.Drawing.Point(249, 43);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(726, 336);
            this.dataGridView2.TabIndex = 69;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // CodProducto
            // 
            this.CodProducto.HeaderText = "CodProducto";
            this.CodProducto.Name = "CodProducto";
            // 
            // CantidadActual
            // 
            this.CantidadActual.HeaderText = "CantidadActual";
            this.CantidadActual.Name = "CantidadActual";
            // 
            // CantMinima
            // 
            this.CantMinima.HeaderText = "CantMinima";
            this.CantMinima.Name = "CantMinima";
            // 
            // CantMax
            // 
            this.CantMax.HeaderText = "CantMax";
            this.CantMax.Name = "CantMax";
            // 
            // CantAReponer
            // 
            this.CantAReponer.HeaderText = "Cantidad solicitada";
            this.CantAReponer.Name = "CantAReponer";
            // 
            // CantRecibida
            // 
            this.CantRecibida.HeaderText = "Cantidad recibida";
            this.CantRecibida.Name = "CantRecibida";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(248, 394);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 72;
            this.label5.Text = "Cantidad recibida:";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(249, 418);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(144, 20);
            this.numericUpDown2.TabIndex = 71;
            // 
            // btnCambiarClave
            // 
            this.btnCambiarClave.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnCambiarClave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambiarClave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCambiarClave.ForeColor = System.Drawing.Color.White;
            this.btnCambiarClave.Location = new System.Drawing.Point(427, 394);
            this.btnCambiarClave.Name = "btnCambiarClave";
            this.btnCambiarClave.Size = new System.Drawing.Size(163, 44);
            this.btnCambiarClave.TabIndex = 70;
            this.btnCambiarClave.Text = "Cargar";
            this.btnCambiarClave.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.button1.Enabled = false;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(854, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 52);
            this.button1.TabIndex = 73;
            this.button1.Text = "Finalizar";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblNumTel);
            this.groupBox1.Controls.Add(this.lblCBU);
            this.groupBox1.Controls.Add(this.lblApellidoCliente);
            this.groupBox1.Controls.Add(this.lblDNICliente);
            this.groupBox1.Controls.Add(this.lblNombreCliente);
            this.groupBox1.Controls.Add(this.lblMailCliente);
            this.groupBox1.Location = new System.Drawing.Point(22, 219);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 172);
            this.groupBox1.TabIndex = 75;
            this.groupBox1.TabStop = false;
            // 
            // lblNumTel
            // 
            this.lblNumTel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumTel.AutoSize = true;
            this.lblNumTel.Location = new System.Drawing.Point(99, 80);
            this.lblNumTel.Name = "lblNumTel";
            this.lblNumTel.Size = new System.Drawing.Size(103, 13);
            this.lblNumTel.TabIndex = 65;
            this.lblNumTel.Text = "Número de telefono:";
            // 
            // lblCBU
            // 
            this.lblCBU.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCBU.AutoSize = true;
            this.lblCBU.Location = new System.Drawing.Point(6, 140);
            this.lblCBU.Name = "lblCBU";
            this.lblCBU.Size = new System.Drawing.Size(35, 13);
            this.lblCBU.TabIndex = 60;
            this.lblCBU.Text = "CBU: ";
            // 
            // lblApellidoCliente
            // 
            this.lblApellidoCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApellidoCliente.AutoSize = true;
            this.lblApellidoCliente.Location = new System.Drawing.Point(99, 140);
            this.lblApellidoCliente.Name = "lblApellidoCliente";
            this.lblApellidoCliente.Size = new System.Drawing.Size(35, 13);
            this.lblApellidoCliente.TabIndex = 57;
            this.lblApellidoCliente.Text = "CUIT:";
            // 
            // lblDNICliente
            // 
            this.lblDNICliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDNICliente.AutoSize = true;
            this.lblDNICliente.Location = new System.Drawing.Point(99, 27);
            this.lblDNICliente.Name = "lblDNICliente";
            this.lblDNICliente.Size = new System.Drawing.Size(71, 13);
            this.lblDNICliente.TabIndex = 59;
            this.lblDNICliente.Text = "Razon social:";
            // 
            // lblNombreCliente
            // 
            this.lblNombreCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNombreCliente.AutoSize = true;
            this.lblNombreCliente.Location = new System.Drawing.Point(6, 27);
            this.lblNombreCliente.Name = "lblNombreCliente";
            this.lblNombreCliente.Size = new System.Drawing.Size(47, 13);
            this.lblNombreCliente.TabIndex = 56;
            this.lblNombreCliente.Text = "Nombre:";
            // 
            // lblMailCliente
            // 
            this.lblMailCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMailCliente.AutoSize = true;
            this.lblMailCliente.Location = new System.Drawing.Point(6, 80);
            this.lblMailCliente.Name = "lblMailCliente";
            this.lblMailCliente.Size = new System.Drawing.Size(32, 13);
            this.lblMailCliente.TabIndex = 58;
            this.lblMailCliente.Text = "Mail: ";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(22, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 15);
            this.label4.TabIndex = 74;
            this.label4.Text = "Detalle Proveedor:";
            // 
            // COMPRAfrmConfirmarRecepcion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 529);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.btnCambiarClave);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox1);
            this.Name = "COMPRAfrmConfirmarRecepcion";
            this.Text = "COMPRAfrmConfirmarRecepcion";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantidadActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantMinima;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantAReponer;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantRecibida;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button btnCambiarClave;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblNumTel;
        private System.Windows.Forms.Label lblCBU;
        private System.Windows.Forms.Label lblApellidoCliente;
        private System.Windows.Forms.Label lblDNICliente;
        private System.Windows.Forms.Label lblNombreCliente;
        private System.Windows.Forms.Label lblMailCliente;
        private System.Windows.Forms.Label label4;
    }
}