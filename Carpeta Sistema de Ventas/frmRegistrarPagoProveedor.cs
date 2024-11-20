using BE;
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
    public partial class frmRegistrarPagoProveedor : Form, IObserver
    {
        BEOrdenCompra ordenC = null;
        public frmRegistrarPagoProveedor(BEOrdenCompra ordenCompra)
        {
            ordenC = ordenCompra;
            InitializeComponent();

            IdiomaManager.GetInstance().archivoActual = "frmRegistrarPago";
            IdiomaManager.GetInstance().Agregar(this);
        }


        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        private void COMPRAfrmRegistrarPagoProveedor_Load(object sender, EventArgs e)
        {
            lblTotal.Text += ordenC.MontoTotal.ToString("#,0.00", new System.Globalization.CultureInfo("es-ES")); ;
            lblProveedor.Text += ordenC.proveedor.Nombre;
            txtCBU.Text = ordenC.proveedor.CBU;
            txtBanco.Text = ordenC.proveedor.Banco;
        }

        private void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                try
                {
                    ordenC.NumeroFactura = Convert.ToInt64(txtNumFactura.Text);
                    ordenC.NumeroTransferencia = Convert.ToInt64(txtNumTransferencia.Text);
                    ordenC.MetodoPago = "Transferencia";
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                } 
                catch (Exception ex) { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("error") + ex.Message); }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("completeCampos")); }
        }

        private bool ValidarCampos()
        {
            if(txtNumTransferencia.Text == "" || txtCBU.Text == "" || txtBanco.Text == "" || txtNumFactura.Text == "")
            {
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        #region eventos form
        //Para que no pueda escribir letras y mas de 12 digitos

        private void txtNumTransferencia_KeyPress_1(object sender, KeyPressEventArgs e)
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

                    if (texto.Length >= 15)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtNumFactura_KeyPress(object sender, KeyPressEventArgs e)
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

                    if (texto.Length >= 15)
                    {
                        e.Handled = true;
                    }
                }
            }
        }
        private void frmRegistrarPagoProveedor_Shown(object sender, EventArgs e)
        {
            txtNumFactura.Focus();
        }

        //eventos para que cuando termine de escribir (presione ENTER) haga focus al otro textbox
        private void txtNumFactura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; //Evita el sonido de windows
                txtNumTransferencia.Focus();
            }
        }

        private void txtNumTransferencia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; //Evita el sonido de windows
                btnRegistrarPago.Focus();
            }
        }
        #endregion
    }
}
