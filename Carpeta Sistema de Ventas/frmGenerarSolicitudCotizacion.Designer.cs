namespace Carpeta_Sistema_de_Ventas
{
    partial class frmGenerarSolicitudCotizacion
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
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.btnRegistrarProveedor = new System.Windows.Forms.Button();
            this.btnSeleccionarProveedor = new System.Windows.Forms.Button();
            this.grillaProdBajoStock = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProveedor = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.grillaProveedores = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbProveedoresSeleccionados = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCargarCant = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grillaProdBajoStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grillaProveedores)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(374, 29);
            this.label3.TabIndex = 46;
            this.label3.Text = "Generar solicitud de cotización";
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFinalizar.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnFinalizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizar.ForeColor = System.Drawing.Color.White;
            this.btnFinalizar.Location = new System.Drawing.Point(12, 423);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(185, 52);
            this.btnFinalizar.TabIndex = 54;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.UseVisualStyleBackColor = false;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnRegistrarProveedor
            // 
            this.btnRegistrarProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegistrarProveedor.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRegistrarProveedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarProveedor.ForeColor = System.Drawing.Color.Black;
            this.btnRegistrarProveedor.Location = new System.Drawing.Point(745, 351);
            this.btnRegistrarProveedor.Name = "btnRegistrarProveedor";
            this.btnRegistrarProveedor.Size = new System.Drawing.Size(133, 37);
            this.btnRegistrarProveedor.TabIndex = 28;
            this.btnRegistrarProveedor.Text = "Registrar nuevo";
            this.btnRegistrarProveedor.UseVisualStyleBackColor = false;
            this.btnRegistrarProveedor.Click += new System.EventHandler(this.btnRegistrarProveedor_Click);
            // 
            // btnSeleccionarProveedor
            // 
            this.btnSeleccionarProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSeleccionarProveedor.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnSeleccionarProveedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionarProveedor.ForeColor = System.Drawing.Color.White;
            this.btnSeleccionarProveedor.Location = new System.Drawing.Point(530, 351);
            this.btnSeleccionarProveedor.Name = "btnSeleccionarProveedor";
            this.btnSeleccionarProveedor.Size = new System.Drawing.Size(138, 37);
            this.btnSeleccionarProveedor.TabIndex = 29;
            this.btnSeleccionarProveedor.Text = "Seleccionar";
            this.btnSeleccionarProveedor.UseVisualStyleBackColor = false;
            this.btnSeleccionarProveedor.Click += new System.EventHandler(this.btnSeleccionarProveedor_Click);
            // 
            // grillaProdBajoStock
            // 
            this.grillaProdBajoStock.AllowUserToAddRows = false;
            this.grillaProdBajoStock.AllowUserToDeleteRows = false;
            this.grillaProdBajoStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grillaProdBajoStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grillaProdBajoStock.Location = new System.Drawing.Point(12, 88);
            this.grillaProdBajoStock.MultiSelect = false;
            this.grillaProdBajoStock.Name = "grillaProdBajoStock";
            this.grillaProdBajoStock.ReadOnly = true;
            this.grillaProdBajoStock.Size = new System.Drawing.Size(504, 256);
            this.grillaProdBajoStock.TabIndex = 58;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 20);
            this.label2.TabIndex = 56;
            this.label2.Text = "Productos bajos de stock";
            // 
            // btnQuitar
            // 
            this.btnQuitar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuitar.BackColor = System.Drawing.Color.Firebrick;
            this.btnQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitar.ForeColor = System.Drawing.Color.White;
            this.btnQuitar.Location = new System.Drawing.Point(136, 359);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(99, 30);
            this.btnQuitar.TabIndex = 63;
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.UseVisualStyleBackColor = false;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(732, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 67;
            this.label1.Text = "Buscar proveedores:";
            // 
            // txtProveedor
            // 
            this.txtProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProveedor.Location = new System.Drawing.Point(735, 62);
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.Size = new System.Drawing.Size(164, 20);
            this.txtProveedor.TabIndex = 66;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.Black;
            this.btnBuscar.Location = new System.Drawing.Point(923, 45);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(78, 37);
            this.btnBuscar.TabIndex = 65;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // grillaProveedores
            // 
            this.grillaProveedores.AllowUserToAddRows = false;
            this.grillaProveedores.AllowUserToDeleteRows = false;
            this.grillaProveedores.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grillaProveedores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grillaProveedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grillaProveedores.Location = new System.Drawing.Point(532, 88);
            this.grillaProveedores.MultiSelect = false;
            this.grillaProveedores.Name = "grillaProveedores";
            this.grillaProveedores.ReadOnly = true;
            this.grillaProveedores.Size = new System.Drawing.Size(469, 244);
            this.grillaProveedores.TabIndex = 64;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(529, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 16);
            this.label5.TabIndex = 68;
            this.label5.Text = "Proveedores";
            // 
            // cmbProveedoresSeleccionados
            // 
            this.cmbProveedoresSeleccionados.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProveedoresSeleccionados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProveedoresSeleccionados.FormattingEnabled = true;
            this.cmbProveedoresSeleccionados.Location = new System.Drawing.Point(531, 447);
            this.cmbProveedoresSeleccionados.Name = "cmbProveedoresSeleccionados";
            this.cmbProveedoresSeleccionados.Size = new System.Drawing.Size(228, 21);
            this.cmbProveedoresSeleccionados.TabIndex = 69;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(528, 423);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(178, 16);
            this.label4.TabIndex = 70;
            this.label4.Text = "Proveedores seleccionados";
            // 
            // btnCargarCant
            // 
            this.btnCargarCant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCargarCant.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnCargarCant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCargarCant.ForeColor = System.Drawing.Color.White;
            this.btnCargarCant.Location = new System.Drawing.Point(12, 358);
            this.btnCargarCant.Name = "btnCargarCant";
            this.btnCargarCant.Size = new System.Drawing.Size(118, 30);
            this.btnCargarCant.TabIndex = 71;
            this.btnCargarCant.Text = "Cargar cantidad";
            this.btnCargarCant.UseVisualStyleBackColor = false;
            this.btnCargarCant.Click += new System.EventHandler(this.btnCargarCant_Click);
            // 
            // frmGenerarSolicitudCotizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(1025, 529);
            this.Controls.Add(this.btnCargarCant);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbProveedoresSeleccionados);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtProveedor);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.grillaProveedores);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.grillaProdBajoStock);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.btnSeleccionarProveedor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRegistrarProveedor);
            this.Name = "frmGenerarSolicitudCotizacion";
            this.Text = "COMPRAfrmGenerarSolicitudCotizacion";
            this.Load += new System.EventHandler(this.COMPRAfrmGenerarSolicitudCotizacion_Load);
            this.Resize += new System.EventHandler(this.COMPRAfrmGenerarSolicitudCotizacion_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.grillaProdBajoStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grillaProveedores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.Button btnRegistrarProveedor;
        private System.Windows.Forms.Button btnSeleccionarProveedor;
        private System.Windows.Forms.DataGridView grillaProdBajoStock;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProveedor;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView grillaProveedores;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbProveedoresSeleccionados;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCargarCant;
    }
}