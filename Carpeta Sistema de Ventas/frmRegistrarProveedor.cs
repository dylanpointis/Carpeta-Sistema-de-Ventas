using BE;
using BLL;
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
    public partial class frmRegistrarProveedor : Form, IObserver
    {
        BLLProveedor bllProvedor = new BLLProveedor();
        bool preRegistro;
        BEProveedor provedor;
        public frmRegistrarProveedor(bool PreRegistro, BEProveedor prov)
        {
            preRegistro = PreRegistro;
            provedor = prov;
            IdiomaManager.GetInstance().archivoActual = "frmRegistrarProveedor";
            IdiomaManager.GetInstance().Agregar(this);
            InitializeComponent();
        }
        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        private void COMPRAfrmRegistrarProveedor_Load(object sender, EventArgs e)
        {
            if(preRegistro == true)
            {
                lblTitulo.Text = IdiomaManager.GetInstance().ConseguirTexto("textoPreRegistrar");
                txtCBU.Enabled = false;
                txtBanco.Enabled = false;
                btnRegistrarProveedor.Text = IdiomaManager.GetInstance().ConseguirTexto("textoPreRegistrar");
            }
            else
            {
                txtCUIT.Enabled = false;
                txtCUIT.Text = provedor.CUIT;
                txtNombre.Text = provedor.Nombre;
                txtRazonSocial.Text = provedor.RazonSocial;
                txtMail.Text = provedor.Email;
                txtNumTelefono.Text = provedor.NumTelefono;
                txtCBU.Text = provedor.CBU;
                txtDireccion.Text = provedor.Direccion; 
                txtBanco.Text = provedor.Banco;
            }
        }
        private void btnRegistrarProveedor_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                provedor.CUIT = txtCUIT.Text;
                provedor.Nombre = txtNombre.Text;
                provedor.RazonSocial = txtRazonSocial.Text;
                provedor.Email = txtMail.Text;
                provedor.NumTelefono = txtNumTelefono.Text;
                provedor.CBU = txtCBU.Text;
                provedor.Direccion = txtDireccion.Text;
                provedor.Banco = txtBanco.Text;
                try
                {
                    if (preRegistro == true)
                    {
                        bllProvedor.RegistrarProveedor(provedor);
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exitoPreRegistro"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        bllProvedor.ModificarProveedor(provedor);
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exitoRegistro"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Error); }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("llenarCampos"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private bool ValidarCampos()
        {
            if(txtCUIT.Text == "" || txtDireccion.Text == "" || txtNombre.Text == "" || txtRazonSocial.Text == "" || txtNumTelefono.Text == "" || txtMail.Text == "")
            {
                return false;
            }
            if(preRegistro == false && (txtCBU.Text == "" || txtBanco.Text == "")) // en el registro completo tiene que poner el cbu y banco
            {
                return false;
            }
            if (!Regex.IsMatch(txtMail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("errorMail"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMail.Focus();
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        #region eventosParaTextbox
        //Para que no pueda escribir letras. Solamente numeros o guion
        private void txtCUIT_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (!char.IsControl(e.KeyChar))
                {
                    string texto = textBox.Text;

                    if (texto.Length >= 14)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtCBU_KeyPress(object sender, KeyPressEventArgs e)
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

                    if (texto.Length >= 23)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtNumTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (!char.IsControl(e.KeyChar))
                {
                    string texto = textBox.Text;

                    if (texto.Length >= 23)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        #endregion
    }
}
