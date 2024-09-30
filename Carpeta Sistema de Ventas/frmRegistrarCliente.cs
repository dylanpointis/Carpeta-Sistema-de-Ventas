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
                BECliente cliente = new BECliente(Convert.ToInt32(txtDNI.Text), txtNombre.Text, txtApellido.Text, txtMail.Text, Encriptador.EncriptarAES(txtDireccion.Text)); 
                
                BECliente clienteEncontrado = bllCliente.VerificarCliente(cliente.DniCliente);
                if(clienteEncontrado == null)
                {
                    try
                    {
                        bllCliente.RegistrarCliente(cliente);

                        bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Clientes", "Cliente creado", 1, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.btnRegistrarCliente.Enabled = false;
                    }
                    catch (Exception ex) { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("error"), "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("yaExiste"), "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
