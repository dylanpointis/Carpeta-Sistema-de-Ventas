﻿namespace Carpeta_Sistema_de_Ventas
{
    partial class frmGestionFamilias
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblModoOperacion = new System.Windows.Forms.Label();
            this.btnCrear = new System.Windows.Forms.Button();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnQuitarPermiso = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAgregarPermiso = new System.Windows.Forms.Button();
            this.listBoxPermisos = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNombreFamilia = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbFamilia = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxFamilia = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 20);
            this.label2.TabIndex = 95;
            this.label2.Text = "Gestionar Familias";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Location = new System.Drawing.Point(607, 380);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(109, 34);
            this.btnCancelar.TabIndex = 94;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblModoOperacion
            // 
            this.lblModoOperacion.AutoSize = true;
            this.lblModoOperacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblModoOperacion.Location = new System.Drawing.Point(111, 438);
            this.lblModoOperacion.Name = "lblModoOperacion";
            this.lblModoOperacion.Size = new System.Drawing.Size(169, 24);
            this.lblModoOperacion.TabIndex = 93;
            this.lblModoOperacion.Text = "Modo operación:";
            // 
            // btnCrear
            // 
            this.btnCrear.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCrear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrear.Location = new System.Drawing.Point(115, 380);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(109, 34);
            this.btnCrear.TabIndex = 92;
            this.btnCrear.Text = "Crear nuevo";
            this.btnCrear.UseVisualStyleBackColor = false;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // btnAplicar
            // 
            this.btnAplicar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicar.Location = new System.Drawing.Point(484, 380);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(109, 34);
            this.btnAplicar.TabIndex = 91;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = false;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Location = new System.Drawing.Point(361, 380);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(109, 34);
            this.btnEliminar.TabIndex = 90;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificar.Location = new System.Drawing.Point(238, 380);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(109, 34);
            this.btnModificar.TabIndex = 89;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnQuitarPermiso
            // 
            this.btnQuitarPermiso.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnQuitarPermiso.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitarPermiso.Location = new System.Drawing.Point(267, 122);
            this.btnQuitarPermiso.Name = "btnQuitarPermiso";
            this.btnQuitarPermiso.Size = new System.Drawing.Size(80, 30);
            this.btnQuitarPermiso.TabIndex = 88;
            this.btnQuitarPermiso.Text = "Quitar <<";
            this.btnQuitarPermiso.UseVisualStyleBackColor = false;
            this.btnQuitarPermiso.Click += new System.EventHandler(this.btnQuitarPermiso_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 87;
            this.label1.Text = "Permisos";
            // 
            // btnAgregarPermiso
            // 
            this.btnAgregarPermiso.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAgregarPermiso.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarPermiso.Location = new System.Drawing.Point(267, 86);
            this.btnAgregarPermiso.Name = "btnAgregarPermiso";
            this.btnAgregarPermiso.Size = new System.Drawing.Size(80, 30);
            this.btnAgregarPermiso.TabIndex = 86;
            this.btnAgregarPermiso.Text = "Agregar >>";
            this.btnAgregarPermiso.UseVisualStyleBackColor = false;
            this.btnAgregarPermiso.Click += new System.EventHandler(this.btnAgregarPermiso_Click);
            // 
            // listBoxPermisos
            // 
            this.listBoxPermisos.FormattingEnabled = true;
            this.listBoxPermisos.Location = new System.Drawing.Point(19, 85);
            this.listBoxPermisos.Name = "listBoxPermisos";
            this.listBoxPermisos.Size = new System.Drawing.Size(228, 264);
            this.listBoxPermisos.TabIndex = 85;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(484, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 84;
            this.label9.Text = "Familia";
            // 
            // txtNombreFamilia
            // 
            this.txtNombreFamilia.Location = new System.Drawing.Point(487, 79);
            this.txtNombreFamilia.Name = "txtNombreFamilia";
            this.txtNombreFamilia.Size = new System.Drawing.Size(146, 20);
            this.txtNombreFamilia.TabIndex = 83;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(484, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 13);
            this.label5.TabIndex = 82;
            this.label5.Text = "Nombre nuevo familia";
            // 
            // cmbFamilia
            // 
            this.cmbFamilia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFamilia.FormattingEnabled = true;
            this.cmbFamilia.Location = new System.Drawing.Point(487, 39);
            this.cmbFamilia.Name = "cmbFamilia";
            this.cmbFamilia.Size = new System.Drawing.Size(144, 21);
            this.cmbFamilia.TabIndex = 81;
            this.cmbFamilia.SelectedIndexChanged += new System.EventHandler(this.cmbFamilia_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(483, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 20);
            this.label3.TabIndex = 80;
            this.label3.Text = "Configurar Familia";
            // 
            // listBoxFamilia
            // 
            this.listBoxFamilia.FormattingEnabled = true;
            this.listBoxFamilia.Location = new System.Drawing.Point(487, 109);
            this.listBoxFamilia.Name = "listBoxFamilia";
            this.listBoxFamilia.Size = new System.Drawing.Size(228, 264);
            this.listBoxFamilia.TabIndex = 79;
            // 
            // frmGestionFamilias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(763, 480);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.lblModoOperacion);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnQuitarPermiso);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAgregarPermiso);
            this.Controls.Add(this.listBoxPermisos);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtNombreFamilia);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbFamilia);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBoxFamilia);
            this.Name = "frmGestionFamilias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmGestionFamilias_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblModoOperacion;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnQuitarPermiso;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAgregarPermiso;
        private System.Windows.Forms.ListBox listBoxPermisos;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNombreFamilia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbFamilia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxFamilia;
    }
}