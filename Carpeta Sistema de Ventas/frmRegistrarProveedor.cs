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
        BEProveedor proveedor;
        public frmRegistrarProveedor(bool PreRegistro, BEProveedor prov)
        {
            preRegistro = PreRegistro;
            proveedor = prov;
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
                proveedor = new BEProveedor("","","","","","","","");
            }
            else
            {
                txtCUIT.Enabled = false;
                txtCUIT.Text = proveedor.CUIT;
                txtNombre.Text = proveedor.Nombre;
                txtRazonSocial.Text = proveedor.RazonSocial;
                txtMail.Text = proveedor.Email;
                txtNumTelefono.Text = proveedor.NumTelefono;
                txtCBU.Text = proveedor.CBU;
                txtDireccion.Text = proveedor.Direccion; 
                txtBanco.Text = proveedor.Banco;
            }
        }
        private void btnRegistrarProveedor_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                proveedor.CUIT = txtCUIT.Text;
                proveedor.Nombre = txtNombre.Text;
                proveedor.RazonSocial = txtRazonSocial.Text;
                proveedor.Email = txtMail.Text;
                proveedor.NumTelefono = txtNumTelefono.Text;
                proveedor.CBU = txtCBU.Text;
                proveedor.Direccion = txtDireccion.Text;
                proveedor.Banco = txtBanco.Text;
                try
                {
                    if (preRegistro == true)
                    {
                        bllProvedor.RegistrarProveedor(proveedor);
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exitoPreRegistro"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        bllProvedor.ModificarProveedor(proveedor);
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exitoRegistro"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    btnRegistrarProveedor.Enabled = false;
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
            if (!Regex.IsMatch(txtCUIT.Text, @"^\d{2}-\d{8}-\d{1}$")) //CUIT FORMATO "XX-XXXXXXXX-X",
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("formatoCUIT"));
                txtMail.Focus();
                return false;
            }
            if (preRegistro == false && (txtCBU.Text == "" || txtBanco.Text == "")) // en el registro completo tiene que poner el cbu y banco
            {
                return false;
            }
            if (!Regex.IsMatch(txtMail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("errorMail"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMail.Focus();
                return false;
            }

            if (txtCBU.Text.Length != 22 && preRegistro ==false)
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("longCBU"));
                txtMail.Focus();
                return false;
            }
            if (!Regex.IsMatch(txtNumTelefono.Text, @"^\d+(\.\d+)?$"))
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("formatoNumTel"));
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
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

                    if (texto.Length >= 13)
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

                    if (texto.Length >= 22)
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
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (!char.IsControl(e.KeyChar))
                {
                    string texto = textBox.Text;

                    if (texto.Length >= 10)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        #endregion
    }
}
