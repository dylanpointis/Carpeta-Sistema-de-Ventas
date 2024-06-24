namespace Carpeta_Sistema_de_Ventas
{
    partial class frmCobrarVenta
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
            this.cmbMetodoPago = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbMarcaTarjeta = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCantCuotas = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCobrarVenta = new System.Windows.Forms.Button();
            this.txtComentarioAdicional = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblMontoTotal = new System.Windows.Forms.Label();
            this.lblImpuesto = new System.Windows.Forms.Label();
            this.txtAliasMP = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnConectar = new System.Windows.Forms.Button();
            this.txtNumTransaccion = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblNumeroFactura = new System.Windows.Forms.Label();
            this.txtNumTarjeta = new System.Windows.Forms.NumericUpDown();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantCuotas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumTarjeta)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbMetodoPago
            // 
            this.cmbMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMetodoPago.Enabled = false;
            this.cmbMetodoPago.FormattingEnabled = true;
            this.cmbMetodoPago.Location = new System.Drawing.Point(52, 160);
            this.cmbMetodoPago.Name = "cmbMetodoPago";
            this.cmbMetodoPago.Size = new System.Drawing.Size(145, 21);
            this.cmbMetodoPago.TabIndex = 0;
            this.cmbMetodoPago.SelectedIndexChanged += new System.EventHandler(this.cmbMetodoPago_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Método de pago";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 228);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Marca Tarjeta";
            // 
            // cmbMarcaTarjeta
            // 
            this.cmbMarcaTarjeta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMarcaTarjeta.Enabled = false;
            this.cmbMarcaTarjeta.FormattingEnabled = true;
            this.cmbMarcaTarjeta.Items.AddRange(new object[] {
            "Visa",
            "Cabal",
            "Mastercard",
            "Galicia"});
            this.cmbMarcaTarjeta.Location = new System.Drawing.Point(52, 244);
            this.cmbMarcaTarjeta.Name = "cmbMarcaTarjeta";
            this.cmbMarcaTarjeta.Size = new System.Drawing.Size(145, 21);
            this.cmbMarcaTarjeta.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(260, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Número de tarjeta";
            // 
            // txtCantCuotas
            // 
            this.txtCantCuotas.Enabled = false;
            this.txtCantCuotas.Location = new System.Drawing.Point(465, 245);
            this.txtCantCuotas.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtCantCuotas.Name = "txtCantCuotas";
            this.txtCantCuotas.Size = new System.Drawing.Size(144, 20);
            this.txtCantCuotas.TabIndex = 8;
            this.txtCantCuotas.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtCantCuotas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantCuotas_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(462, 228);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Cantidad cuotas";
            // 
            // btnCobrarVenta
            // 
            this.btnCobrarVenta.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnCobrarVenta.Enabled = false;
            this.btnCobrarVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCobrarVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCobrarVenta.ForeColor = System.Drawing.Color.White;
            this.btnCobrarVenta.Location = new System.Drawing.Point(52, 328);
            this.btnCobrarVenta.Name = "btnCobrarVenta";
            this.btnCobrarVenta.Size = new System.Drawing.Size(145, 43);
            this.btnCobrarVenta.TabIndex = 25;
            this.btnCobrarVenta.Text = "Registrar datos pago";
            this.btnCobrarVenta.UseVisualStyleBackColor = false;
            this.btnCobrarVenta.Click += new System.EventHandler(this.btnCobrarVenta_Click);
            // 
            // txtComentarioAdicional
            // 
            this.txtComentarioAdicional.Enabled = false;
            this.txtComentarioAdicional.Location = new System.Drawing.Point(465, 93);
            this.txtComentarioAdicional.Multiline = true;
            this.txtComentarioAdicional.Name = "txtComentarioAdicional";
            this.txtComentarioAdicional.Size = new System.Drawing.Size(207, 78);
            this.txtComentarioAdicional.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(462, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Comentario adicional";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(48, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 24);
            this.label6.TabIndex = 29;
            this.label6.Text = "Cobrar venta";
            // 
            // lblMontoTotal
            // 
            this.lblMontoTotal.AutoSize = true;
            this.lblMontoTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoTotal.Location = new System.Drawing.Point(272, 339);
            this.lblMontoTotal.Name = "lblMontoTotal";
            this.lblMontoTotal.Size = new System.Drawing.Size(87, 16);
            this.lblMontoTotal.TabIndex = 30;
            this.lblMontoTotal.Text = "Monto total:";
            // 
            // lblImpuesto
            // 
            this.lblImpuesto.AutoSize = true;
            this.lblImpuesto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImpuesto.Location = new System.Drawing.Point(273, 365);
            this.lblImpuesto.Name = "lblImpuesto";
            this.lblImpuesto.Size = new System.Drawing.Size(74, 16);
            this.lblImpuesto.TabIndex = 31;
            this.lblImpuesto.Text = "Impuesto:";
            // 
            // txtAliasMP
            // 
            this.txtAliasMP.Enabled = false;
            this.txtAliasMP.Location = new System.Drawing.Point(263, 161);
            this.txtAliasMP.Name = "txtAliasMP";
            this.txtAliasMP.Size = new System.Drawing.Size(144, 20);
            this.txtAliasMP.TabIndex = 33;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(260, 145);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "Alias mercado pago";
            // 
            // btnConectar
            // 
            this.btnConectar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnConectar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConectar.Location = new System.Drawing.Point(52, 78);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(145, 35);
            this.btnConectar.TabIndex = 34;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = false;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // txtNumTransaccion
            // 
            this.txtNumTransaccion.Enabled = false;
            this.txtNumTransaccion.Location = new System.Drawing.Point(263, 93);
            this.txtNumTransaccion.Name = "txtNumTransaccion";
            this.txtNumTransaccion.Size = new System.Drawing.Size(144, 20);
            this.txtNumTransaccion.TabIndex = 36;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(260, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 13);
            this.label10.TabIndex = 35;
            this.label10.Text = "Número de transacción";
            // 
            // lblNumeroFactura
            // 
            this.lblNumeroFactura.AutoSize = true;
            this.lblNumeroFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroFactura.Location = new System.Drawing.Point(273, 310);
            this.lblNumeroFactura.Name = "lblNumeroFactura";
            this.lblNumeroFactura.Size = new System.Drawing.Size(120, 16);
            this.lblNumeroFactura.TabIndex = 37;
            this.lblNumeroFactura.Text = "Número factura: ";
            // 
            // txtNumTarjeta
            // 
            this.txtNumTarjeta.Enabled = false;
            this.txtNumTarjeta.Location = new System.Drawing.Point(263, 245);
            this.txtNumTarjeta.Maximum = new decimal(new int[] {
            1569325055,
            23283064,
            0,
            0});
            this.txtNumTarjeta.Name = "txtNumTarjeta";
            this.txtNumTarjeta.Size = new System.Drawing.Size(144, 20);
            this.txtNumTarjeta.TabIndex = 38;
            this.txtNumTarjeta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumTarjeta_KeyPress);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelar.Enabled = false;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Location = new System.Drawing.Point(527, 328);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCancelar.Size = new System.Drawing.Size(145, 43);
            this.btnCancelar.TabIndex = 39;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmCobrarVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 412);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.txtNumTarjeta);
            this.Controls.Add(this.lblNumeroFactura);
            this.Controls.Add(this.txtNumTransaccion);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.txtAliasMP);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblImpuesto);
            this.Controls.Add(this.lblMontoTotal);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtComentarioAdicional);
            this.Controls.Add(this.btnCobrarVenta);
            this.Controls.Add(this.txtCantCuotas);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbMarcaTarjeta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbMetodoPago);
            this.Name = "frmCobrarVenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRegistrarFactura";
            this.Load += new System.EventHandler(this.frmCobrarVenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtCantCuotas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumTarjeta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbMetodoPago;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbMarcaTarjeta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtCantCuotas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCobrarVenta;
        private System.Windows.Forms.TextBox txtComentarioAdicional;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblMontoTotal;
        private System.Windows.Forms.Label lblImpuesto;
        private System.Windows.Forms.TextBox txtAliasMP;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.TextBox txtNumTransaccion;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblNumeroFactura;
        private System.Windows.Forms.NumericUpDown txtNumTarjeta;
        private System.Windows.Forms.Button btnCancelar;
    }
}