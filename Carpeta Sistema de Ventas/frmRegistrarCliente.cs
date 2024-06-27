using BE;
using BLL;
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
    public partial class frmRegistrarCliente : Form
    {
        public frmRegistrarCliente()
        {
            InitializeComponent();
        }
        BLLCliente bllCliente = new BLLCliente();
        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                BECliente cliente = new BECliente(Convert.ToInt32(txtDNI.Text), txtNombre.Text, txtApellido.Text, txtMail.Text, txtDireccion.Text); 
                
                BECliente clienteEncontrado = bllCliente.VerificarCliente(cliente.DniCliente);
                if(clienteEncontrado == null)
                {
                    try
                    {
                        bllCliente.RegistrarCliente(cliente);
                        MessageBox.Show("Cliente registrado");
                    }
                    catch (Exception ex) { MessageBox.Show("Error al registrar al cliente"); }
                }
                else { MessageBox.Show("Ya existe un cliente con el DNI ingresado"); }
            }
            else { MessageBox.Show("Ingrese de vuelta los campos"); }
        }

        private bool ValidarDatos()
        {
            if (txtDNI.Text == "" && txtNombre.Text == "" && txtApellido.Text == "" && txtMail.Text == "" && txtDireccion.Text == "")
            {
                return false;
            }
            if (!Regex.IsMatch(txtDNI.Text, @"^\d{7,9}$"))
            {
                MessageBox.Show("El DNI debe contener solo números y tener entre 7 y 9 dígitos.");
                return false;
            }
            if (!Regex.IsMatch(txtMail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("El formato del correo electrónico no es válido.");
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
