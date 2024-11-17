using BE;
using BLL;
using Services;
using Services.Observer;
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
    public partial class frmCambiarClave : Form, IObserver
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

            IdiomaManager.GetInstance().archivoActual = "frmCambiarClave";
            IdiomaManager.GetInstance().Agregar(this);

            btnMostrarClaveActual.Text = "";
            btnMostrarClaveNueva.Text = "";
            btnMostrarConfirmarClave.Text = "";
        }

        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }


        BLLUsuario bllUsuario = new BLLUsuario();


        private void frmCambiarClave_Load(object sender, EventArgs e)
        {
            lblNombreUsuario.Text += " " + usuarioActual.NombreUsuario;
        }

        private void btnCambiarClave_Click(object sender, EventArgs e)
        {
            if (txtClaveActual.Text != "" && txtNuevaClave.Text != "" && txtConfirmar.Text != "")
            {
                if (txtNuevaClave.Text == txtConfirmar.Text)
                {
                    try
                    {
                        bllUsuario.CambiarClave(usuarioActual.DNI, txtNuevaClave.Text, txtClaveActual.Text);
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Cierra sesion automaticamente
                        SessionManager.GetInstance.LogOut();
                        BLLEvento bLLEvento = new BLLEvento();
                        bLLEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Sesiones", "Cierre sesión", 1));
                        this.Close();
                        _frmParent.Close();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                } 
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("confirme"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("completar"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
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
