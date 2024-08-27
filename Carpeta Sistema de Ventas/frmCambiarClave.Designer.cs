namespace Carpeta_Sistema_de_Ventas
{
    partial class frmCambiarClave
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
            this.lblNombreUsuario = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtClaveActual = new System.Windows.Forms.TextBox();
            this.txtNuevaClave = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtConfirmar = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCambiarClave = new System.Windows.Forms.Button();
            this.btnMostrarClaveActual = new System.Windows.Forms.Button();
            this.btnMostrarClaveNueva = new System.Windows.Forms.Button();
            this.btnMostrarConfirmarClave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNombreUsuario
            // 
            this.lblNombreUsuario.AutoSize = true;
            this.lblNombreUsuario.Location = new System.Drawing.Point(393, 80);
            this.lblNombreUsuario.Name = "lblNombreUsuario";
            this.lblNombreUsuario.Size = new System.Drawing.Size(102, 13);
            this.lblNombreUsuario.TabIndex = 0;
            this.lblNombreUsuario.Text = "Nombre de usuario: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(384, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cambiar clave";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(360, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Clave actual";
            // 
            // txtClaveActual
            // 
            this.txtClaveActual.Location = new System.Drawing.Point(361, 144);
            this.txtClaveActual.Name = "txtClaveActual";
            this.txtClaveActual.PasswordChar = '*';
            this.txtClaveActual.Size = new System.Drawing.Size(183, 20);
            this.txtClaveActual.TabIndex = 3;
            // 
            // txtNuevaClave
            // 
            this.txtNuevaClave.Location = new System.Drawing.Point(361, 254);
            this.txtNuevaClave.Name = "txtNuevaClave";
            this.txtNuevaClave.PasswordChar = '*';
            this.txtNuevaClave.Size = new System.Drawing.Size(183, 20);
            this.txtNuevaClave.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(358, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Nueva clave";
            // 
            // txtConfirmar
            // 
            this.txtConfirmar.Location = new System.Drawing.Point(362, 313);
            this.txtConfirmar.Name = "txtConfirmar";
            this.txtConfirmar.PasswordChar = '*';
            this.txtConfirmar.Size = new System.Drawing.Size(182, 20);
            this.txtConfirmar.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(360, 297);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Confirmar clave";
            // 
            // btnCambiarClave
            // 
            this.btnCambiarClave.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnCambiarClave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambiarClave.ForeColor = System.Drawing.Color.White;
            this.btnCambiarClave.Location = new System.Drawing.Point(377, 369);
            this.btnCambiarClave.Name = "btnCambiarClave";
            this.btnCambiarClave.Size = new System.Drawing.Size(156, 32);
            this.btnCambiarClave.TabIndex = 8;
            this.btnCambiarClave.Text = "Cambiar clave";
            this.btnCambiarClave.UseVisualStyleBackColor = false;
            this.btnCambiarClave.Click += new System.EventHandler(this.btnCambiarClave_Click);
            // 
            // btnMostrarClaveActual
            // 
            this.btnMostrarClaveActual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMostrarClaveActual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMostrarClaveActual.Image = global::Carpeta_Sistema_de_Ventas.Properties.Resources.visible;
            this.btnMostrarClaveActual.Location = new System.Drawing.Point(550, 128);
            this.btnMostrarClaveActual.Name = "btnMostrarClaveActual";
            this.btnMostrarClaveActual.Size = new System.Drawing.Size(37, 36);
            this.btnMostrarClaveActual.TabIndex = 32;
            this.btnMostrarClaveActual.UseVisualStyleBackColor = true;
            this.btnMostrarClaveActual.Click += new System.EventHandler(this.btnMostrarClaveActual_Click);
            // 
            // btnMostrarClaveNueva
            // 
            this.btnMostrarClaveNueva.BackgroundImage = global::Carpeta_Sistema_de_Ventas.Properties.Resources.visible;
            this.btnMostrarClaveNueva.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMostrarClaveNueva.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMostrarClaveNueva.Location = new System.Drawing.Point(550, 238);
            this.btnMostrarClaveNueva.Name = "btnMostrarClaveNueva";
            this.btnMostrarClaveNueva.Size = new System.Drawing.Size(37, 36);
            this.btnMostrarClaveNueva.TabIndex = 33;
            this.btnMostrarClaveNueva.UseVisualStyleBackColor = true;
            this.btnMostrarClaveNueva.Click += new System.EventHandler(this.btnMostrarClaveNueva_Click);
            // 
            // btnMostrarConfirmarClave
            // 
            this.btnMostrarConfirmarClave.BackgroundImage = global::Carpeta_Sistema_de_Ventas.Properties.Resources.visible;
            this.btnMostrarConfirmarClave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMostrarConfirmarClave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMostrarConfirmarClave.Location = new System.Drawing.Point(550, 297);
            this.btnMostrarConfirmarClave.Name = "btnMostrarConfirmarClave";
            this.btnMostrarConfirmarClave.Size = new System.Drawing.Size(37, 36);
            this.btnMostrarConfirmarClave.TabIndex = 34;
            this.btnMostrarConfirmarClave.UseVisualStyleBackColor = true;
            this.btnMostrarConfirmarClave.Click += new System.EventHandler(this.btnMostrarConfirmarClave_Click);
            // 
            // frmCambiarClave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 529);
            this.Controls.Add(this.btnMostrarConfirmarClave);
            this.Controls.Add(this.btnMostrarClaveNueva);
            this.Controls.Add(this.btnMostrarClaveActual);
            this.Controls.Add(this.btnCambiarClave);
            this.Controls.Add(this.txtConfirmar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNuevaClave);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtClaveActual);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNombreUsuario);
            this.Name = "frmCambiarClave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCambiarClave";
            this.Load += new System.EventHandler(this.frmCambiarClave_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNombreUsuario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtClaveActual;
        private System.Windows.Forms.TextBox txtNuevaClave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtConfirmar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCambiarClave;
        private System.Windows.Forms.Button btnMostrarClaveActual;
        private System.Windows.Forms.Button btnMostrarClaveNueva;
        private System.Windows.Forms.Button btnMostrarConfirmarClave;
    }
}