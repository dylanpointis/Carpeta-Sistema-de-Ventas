namespace Carpeta_Sistema_de_Ventas
{
    partial class frmGestionRoles
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
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNombreRol = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbRoles = new System.Windows.Forms.ComboBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnCrear = new System.Windows.Forms.Button();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.listBoxPermisoFamilia = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAgregarFamilia = new System.Windows.Forms.Button();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.btnAgregarPermiso = new System.Windows.Forms.Button();
            this.listBoxRol = new System.Windows.Forms.ListBox();
            this.listBoxFamilias = new System.Windows.Forms.ListBox();
            this.listBoxPermisos = new System.Windows.Forms.ListBox();
            this.btnGestionarFamilias = new System.Windows.Forms.Button();
            this.lblModoOperacion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(40, 420);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 15);
            this.label4.TabIndex = 84;
            this.label4.Text = "Permisos de la familia:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(596, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 83;
            this.label9.Text = "Roles";
            // 
            // txtNombreRol
            // 
            this.txtNombreRol.Location = new System.Drawing.Point(599, 118);
            this.txtNombreRol.Name = "txtNombreRol";
            this.txtNombreRol.Size = new System.Drawing.Size(146, 20);
            this.txtNombreRol.TabIndex = 82;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(596, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 81;
            this.label5.Text = "Nombre nuevo rol";
            // 
            // cmbRoles
            // 
            this.cmbRoles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoles.FormattingEnabled = true;
            this.cmbRoles.Location = new System.Drawing.Point(599, 78);
            this.cmbRoles.Name = "cmbRoles";
            this.cmbRoles.Size = new System.Drawing.Size(144, 21);
            this.cmbRoles.TabIndex = 80;
            this.cmbRoles.SelectedIndexChanged += new System.EventHandler(this.cmbRoles_SelectedIndexChanged);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Brown;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(904, 471);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(109, 34);
            this.btnCancelar.TabIndex = 79;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCrear
            // 
            this.btnCrear.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCrear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrear.Location = new System.Drawing.Point(412, 471);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(109, 34);
            this.btnCrear.TabIndex = 78;
            this.btnCrear.Text = "Crear nuevo";
            this.btnCrear.UseVisualStyleBackColor = false;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // btnAplicar
            // 
            this.btnAplicar.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicar.ForeColor = System.Drawing.Color.White;
            this.btnAplicar.Location = new System.Drawing.Point(781, 471);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(109, 34);
            this.btnAplicar.TabIndex = 77;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = false;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Location = new System.Drawing.Point(658, 471);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(109, 34);
            this.btnEliminar.TabIndex = 76;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificar.Location = new System.Drawing.Point(535, 471);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(109, 34);
            this.btnModificar.TabIndex = 75;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // listBoxPermisoFamilia
            // 
            this.listBoxPermisoFamilia.FormattingEnabled = true;
            this.listBoxPermisoFamilia.Location = new System.Drawing.Point(43, 436);
            this.listBoxPermisoFamilia.Name = "listBoxPermisoFamilia";
            this.listBoxPermisoFamilia.Size = new System.Drawing.Size(228, 82);
            this.listBoxPermisoFamilia.TabIndex = 74;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(595, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 20);
            this.label3.TabIndex = 73;
            this.label3.Text = "Configurar Rol";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(39, 276);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 72;
            this.label2.Text = "Familias";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, -2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 71;
            this.label1.Text = "Permisos";
            // 
            // btnAgregarFamilia
            // 
            this.btnAgregarFamilia.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAgregarFamilia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarFamilia.Location = new System.Drawing.Point(291, 305);
            this.btnAgregarFamilia.Name = "btnAgregarFamilia";
            this.btnAgregarFamilia.Size = new System.Drawing.Size(80, 30);
            this.btnAgregarFamilia.TabIndex = 70;
            this.btnAgregarFamilia.Text = "Agregar >>";
            this.btnAgregarFamilia.UseVisualStyleBackColor = false;
            this.btnAgregarFamilia.Click += new System.EventHandler(this.btnAgregarFamilia_Click);
            // 
            // btnQuitar
            // 
            this.btnQuitar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitar.Location = new System.Drawing.Point(496, 148);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(80, 30);
            this.btnQuitar.TabIndex = 69;
            this.btnQuitar.Text = "Quitar <<";
            this.btnQuitar.UseVisualStyleBackColor = false;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitarPermiso_Click);
            // 
            // btnAgregarPermiso
            // 
            this.btnAgregarPermiso.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAgregarPermiso.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarPermiso.Location = new System.Drawing.Point(291, 38);
            this.btnAgregarPermiso.Name = "btnAgregarPermiso";
            this.btnAgregarPermiso.Size = new System.Drawing.Size(80, 30);
            this.btnAgregarPermiso.TabIndex = 68;
            this.btnAgregarPermiso.Text = "Agregar >>";
            this.btnAgregarPermiso.UseVisualStyleBackColor = false;
            this.btnAgregarPermiso.Click += new System.EventHandler(this.btnAgregarPermiso_Click);
            // 
            // listBoxRol
            // 
            this.listBoxRol.FormattingEnabled = true;
            this.listBoxRol.Location = new System.Drawing.Point(599, 148);
            this.listBoxRol.Name = "listBoxRol";
            this.listBoxRol.Size = new System.Drawing.Size(228, 264);
            this.listBoxRol.TabIndex = 67;
            // 
            // listBoxFamilias
            // 
            this.listBoxFamilias.FormattingEnabled = true;
            this.listBoxFamilias.Location = new System.Drawing.Point(43, 296);
            this.listBoxFamilias.Name = "listBoxFamilias";
            this.listBoxFamilias.Size = new System.Drawing.Size(228, 121);
            this.listBoxFamilias.TabIndex = 66;
            this.listBoxFamilias.SelectedIndexChanged += new System.EventHandler(this.listBoxFamilias_SelectedIndexChanged);
            // 
            // listBoxPermisos
            // 
            this.listBoxPermisos.FormattingEnabled = true;
            this.listBoxPermisos.Location = new System.Drawing.Point(43, 21);
            this.listBoxPermisos.Name = "listBoxPermisos";
            this.listBoxPermisos.Size = new System.Drawing.Size(228, 251);
            this.listBoxPermisos.TabIndex = 65;
            // 
            // btnGestionarFamilias
            // 
            this.btnGestionarFamilias.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnGestionarFamilias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionarFamilias.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGestionarFamilias.Location = new System.Drawing.Point(866, 12);
            this.btnGestionarFamilias.Name = "btnGestionarFamilias";
            this.btnGestionarFamilias.Size = new System.Drawing.Size(147, 30);
            this.btnGestionarFamilias.TabIndex = 85;
            this.btnGestionarFamilias.Text = "Gestionar familias";
            this.btnGestionarFamilias.UseVisualStyleBackColor = false;
            this.btnGestionarFamilias.Click += new System.EventHandler(this.btnGestionarFamilias_Click);
            // 
            // lblModoOperacion
            // 
            this.lblModoOperacion.AutoSize = true;
            this.lblModoOperacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblModoOperacion.Location = new System.Drawing.Point(408, 444);
            this.lblModoOperacion.Name = "lblModoOperacion";
            this.lblModoOperacion.Size = new System.Drawing.Size(169, 24);
            this.lblModoOperacion.TabIndex = 86;
            this.lblModoOperacion.Text = "Modo operación:";
            // 
            // frmGestionRoles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(1025, 529);
            this.Controls.Add(this.lblModoOperacion);
            this.Controls.Add(this.btnGestionarFamilias);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtNombreRol);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbRoles);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.listBoxPermisoFamilia);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAgregarFamilia);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.btnAgregarPermiso);
            this.Controls.Add(this.listBoxRol);
            this.Controls.Add(this.listBoxFamilias);
            this.Controls.Add(this.listBoxPermisos);
            this.Name = "frmGestionRoles";
            this.Text = "frmGestionRoles";
            this.Load += new System.EventHandler(this.frmGestionRoles_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNombreRol;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbRoles;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.ListBox listBoxPermisoFamilia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAgregarFamilia;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Button btnAgregarPermiso;
        private System.Windows.Forms.ListBox listBoxRol;
        private System.Windows.Forms.ListBox listBoxFamilias;
        private System.Windows.Forms.ListBox listBoxPermisos;
        private System.Windows.Forms.Button btnGestionarFamilias;
        private System.Windows.Forms.Label lblModoOperacion;
    }
}