using BE;
using BLL;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmCambiarClave : Form
    {
        BEUsuario usuarioActual;
        frmMenu _frmParent;
        public frmCambiarClave(frmMenu frmParent)
        {
            usuarioActual = SessionManager.GetInstance.ObtenerUsuario();
            _frmParent = frmParent;
            InitializeComponent();

            txtClaveActual.KeyPress += textbox_KeyPress;
            txtNuevaClave.KeyPress += textbox_KeyPress;
            txtConfirmar.KeyPress += textbox_KeyPress;
        }

        BLLUsuario bllUsuario = new BLLUsuario();



        private void frmCambiarClave_Load(object sender, EventArgs e)
        {
            lblNombreUsuario.Text = "Nombre de usuario: " + usuarioActual.NombreUsuario;
        }

        private void btnCambiarClave_Click(object sender, EventArgs e)
        {

            if (txtClaveActual.Text != "" && txtNuevaClave.Text != "" && txtConfirmar.Text != "")
            {
                if (usuarioActual != null)
                {
                    if(txtNuevaClave.Text.Length >= 8 && txtConfirmar.Text.Length >= 8)
                    {
                        if (Encriptador.EncriptarSHA256(txtClaveActual.Text) == usuarioActual.Clave)
                        {
                            if (txtNuevaClave.Text == txtConfirmar.Text)
                            {
                                try
                                {
                                    bllUsuario.CambiarClave(usuarioActual.DNI, Encriptador.EncriptarSHA256(txtNuevaClave.Text));
                                    MessageBox.Show("Clave cambiada con exito");

                                    //Cierra sesion automaticamente
                                    SessionManager.GetInstance.LogOut();
                                    this.Close();
                                    _frmParent.Close();
                                }
                                catch (Exception ex) { MessageBox.Show("Error al cambiar la clave "); }

                            }
                            else { MessageBox.Show("Confirme la nueva clave"); }
                        }
                        else
                        {
                            MessageBox.Show("La clave actual ingresada no es correcta");
                        }
                    }
                    else { MessageBox.Show("La clave debe tener al menos 8 carácteres"); }
                   
                }
                else { MessageBox.Show("Debe iniciar una sesión en el sistema"); }
            }
            else { MessageBox.Show("Complete los campos"); }
        }


        bool ocultoClaveActual = true;
        private void btnMostrarClaveActual_Click(object sender, EventArgs e)
        {
            ocultoClaveActual = MostrarOcultarClave(ocultoClaveActual, txtClaveActual, btnMostrarClaveActual);
        }

        bool ocultoClaveNueva = true;
        private void btnMostrarClaveNueva_Click(object sender, EventArgs e)
        {
            ocultoClaveNueva = MostrarOcultarClave(ocultoClaveNueva, txtNuevaClave, btnMostrarClaveNueva);
        }

        bool ocultoConfirmarClave = true;
        private void btnMostrarConfirmarClave_Click(object sender, EventArgs e)
        {
            ocultoConfirmarClave = MostrarOcultarClave(ocultoConfirmarClave, txtConfirmar, btnMostrarConfirmarClave);
        }



        private bool MostrarOcultarClave(bool estaOculto, TextBox textBox, Button boton)
        {
            if (estaOculto == true)
            {
                textBox.PasswordChar = '\0';
                boton.BackgroundImage = Properties.Resources.invisible;
                return estaOculto = false;
            }
            else
            {
                textBox.PasswordChar = '*';
                boton.BackgroundImage = Properties.Resources.visible;
                return estaOculto = true;
            }
        }


        private void textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                if (!char.IsControl(e.KeyChar))
                {
                    string texto = textBox.Text;

                    if (texto.Length >= 40)
                    {
                        e.Handled = true;
                        MessageBox.Show("Máximo de 40 cáracteres alcanzado");
                    }
                }

            }
        }
    }
}
