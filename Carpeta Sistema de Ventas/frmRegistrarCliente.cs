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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmRegistrarCliente : Form, IObserver
    {
        public frmRegistrarCliente()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().archivoActual = "frmRegistrarCliente";
            IdiomaManager.GetInstance().Agregar(this);
        }
        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }


        BLLEvento bllEvento = new BLLEvento();
        BLLCliente bllCliente = new BLLCliente();
        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                try
                {
                    BECliente cli = new BECliente(Convert.ToInt32(txtDNI.Text), txtNombre.Text, txtApellido.Text, txtMail.Text, txtDireccion.Text);
                    bllCliente.RegistrarCliente(cli);
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnRegistrarCliente.Enabled = false;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("ingreseCampos"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private bool ValidarDatos()
        {
            if (txtDNI.Text == "" || txtNombre.Text == "" || txtApellido.Text == "" || txtMail.Text == "" || txtDireccion.Text == "")
            {
                return false;
            }
            if (!Regex.IsMatch(txtDNI.Text, @"^\d{7,9}$"))
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("dni7y9"));
                return false;
            }
            if (!Regex.IsMatch(txtMail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("formato"));
                return false;
            }
            return true;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Eventos formulario
        /*Evento para que no escriba mas de 9 digitos y solo Numeros*/
        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    
                    e.Handled = true;
                }
                if (!char.IsControl(e.KeyChar))
                {
                    string texto = textBox.Text;

                    if (texto.Length >= 9)
                    {
                        e.Handled = true;
                    }
                }
            }
        }


        private void frmRegistrarCliente_Shown(object sender, EventArgs e)
        {
            txtDNI.Focus();
        }
        //eventos para que al apretar enter avance al siguiente textbox
        private void txtDNI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; //Evita el sonido de windows
                txtNombre.Focus();
            }
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; //Evita el sonido de windows
                txtApellido.Focus();
            }
        }

        private void txtApellido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; //Evita el sonido de windows
                txtMail.Focus();
            }
        }

        private void txtMail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; //Evita el sonido de windows
                txtDireccion.Focus();
            }
        }
        private void txtDireccion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; //Evita el sonido de windows
                btnRegistrarCliente.Focus();
            }
        }
        #endregion

    }
}
