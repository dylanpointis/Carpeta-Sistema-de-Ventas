namespace Carpeta_Sistema_de_Ventas
{
    partial class COMPRAfrmGenerarSolicitudCotizacion
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
            this.label4 = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblNumTel = new System.Windows.Forms.Label();
            this.lblMail = new System.Windows.Forms.Label();
            this.lblRazonSocial = new System.Windows.Forms.Label();
            this.grillaProdBajoStock = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProveedor = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.grillaProveedores = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
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
            this.btnFinalizar.Enabled = false;
            this.btnFinalizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizar.ForeColor = System.Drawing.Color.White;
            this.btnFinalizar.Location = new System.Drawing.Point(12, 447);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(185, 52);
            this.btnFinalizar.TabIndex = 54;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.UseVisualStyleBackColor = false;
            // 
            // btnRegistrarProveedor
            // 
            this.btnRegistrarProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegistrarProveedor.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRegistrarProveedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarProveedor.ForeColor = System.Drawing.Color.Black;
            this.btnRegistrarProveedor.Location = new System.Drawing.Point(800, 462);
            this.btnRegistrarProveedor.Name = "btnRegistrarProveedor";
            this.btnRegistrarProveedor.Size = new System.Drawing.Size(133, 37);
            this.btnRegistrarProveedor.TabIndex = 28;
            this.btnRegistrarProveedor.Text = "Registrar nuevo";
            this.btnRegistrarProveedor.UseVisualStyleBackColor = false;
            // 
            // btnSeleccionarProveedor
            // 
            this.btnSeleccionarProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSeleccionarProveedor.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnSeleccionarProveedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionarProveedor.ForeColor = System.Drawing.Color.White;
            this.btnSeleccionarProveedor.Location = new System.Drawing.Point(585, 462);
            this.btnSeleccionarProveedor.Name = "btnSeleccionarProveedor";
            this.btnSeleccionarProveedor.Size = new System.Drawing.Size(138, 37);
            this.btnSeleccionarProveedor.TabIndex = 29;
            this.btnSeleccionarProveedor.Text = "Seleccionar";
            this.btnSeleccionarProveedor.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(582, 352);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 15);
            this.label4.TabIndex = 33;
            this.label4.Text = "Detalle Proveedor:";
            // 
            // lblNombre
            // 
            this.lblNombre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(582, 386);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(47, 13);
            this.lblNombre.TabIndex = 34;
            this.lblNombre.Text = "Nombre:";
            // 
            // lblNumTel
            // 
            this.lblNumTel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumTel.AutoSize = true;
            this.lblNumTel.Location = new System.Drawing.Point(797, 435);
            this.lblNumTel.Name = "lblNumTel";
            this.lblNumTel.Size = new System.Drawing.Size(103, 13);
            this.lblNumTel.TabIndex = 35;
            this.lblNumTel.Text = "Número de telefono:";
            // 
            // lblMail
            // 
            this.lblMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMail.AutoSize = true;
            this.lblMail.Location = new System.Drawing.Point(582, 435);
            this.lblMail.Name = "lblMail";
            this.lblMail.Size = new System.Drawing.Size(32, 13);
            this.lblMail.TabIndex = 36;
            this.lblMail.Text = "Mail: ";
            // 
            // lblRazonSocial
            // 
            this.lblRazonSocial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRazonSocial.AutoSize = true;
            this.lblRazonSocial.Location = new System.Drawing.Point(797, 386);
            this.lblRazonSocial.Name = "lblRazonSocial";
            this.lblRazonSocial.Size = new System.Drawing.Size(71, 13);
            this.lblRazonSocial.TabIndex = 37;
            this.lblRazonSocial.Text = "Razon social:";
            // 
            // grillaProdBajoStock
            // 
            this.grillaProdBajoStock.AllowUserToAddRows = false;
            this.grillaProdBajoStock.AllowUserToDeleteRows = false;
            this.grillaProdBajoStock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grillaProdBajoStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grillaProdBajoStock.Location = new System.Drawing.Point(12, 88);
            this.grillaProdBajoStock.MultiSelect = false;
            this.grillaProdBajoStock.Name = "grillaProdBajoStock";
            this.grillaProdBajoStock.ReadOnly = true;
            this.grillaProdBajoStock.Size = new System.Drawing.Size(487, 256);
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
            this.btnQuitar.Location = new System.Drawing.Point(12, 354);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(99, 30);
            this.btnQuitar.TabIndex = 63;
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.UseVisualStyleBackColor = false;
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
            // 
            // grillaProveedores
            // 
            this.grillaProveedores.AllowUserToAddRows = false;
            this.grillaProveedores.AllowUserToDeleteRows = false;
            this.grillaProveedores.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grillaProveedores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grillaProveedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grillaProveedores.Location = new System.Drawing.Point(552, 88);
            this.grillaProveedores.MultiSelect = false;
            this.grillaProveedores.Name = "grillaProveedores";
            this.grillaProveedores.ReadOnly = true;
            this.grillaProveedores.Size = new System.Drawing.Size(448, 244);
            this.grillaProveedores.TabIndex = 64;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(549, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 16);
            this.label5.TabIndex = 68;
            this.label5.Text = "Proveedores";
            // 
            // COMPRAfrmGenerarSolicitudCotizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(1025, 529);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtProveedor);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.grillaProveedores);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.lblRazonSocial);
            this.Controls.Add(this.grillaProdBajoStock);
            this.Controls.Add(this.lblMail);
            this.Controls.Add(this.lblNumTel);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.btnSeleccionarProveedor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRegistrarProveedor);
            this.Name = "COMPRAfrmGenerarSolicitudCotizacion";
            this.Text = "COMPRAfrmGenerarSolicitudCotizacion";
            this.Load += new System.EventHandler(this.COMPRAfrmGenerarSolicitudCotizacion_Load);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblNumTel;
        private System.Windows.Forms.Label lblMail;
        private System.Windows.Forms.Label lblRazonSocial;
        private System.Windows.Forms.DataGridView grillaProdBajoStock;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProveedor;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView grillaProveedores;
        private System.Windows.Forms.Label label5;
    }
}