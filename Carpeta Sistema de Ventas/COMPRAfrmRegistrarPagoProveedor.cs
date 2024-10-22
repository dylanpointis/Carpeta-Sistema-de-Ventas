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
    public partial class COMPRAfrmRegistrarPagoProveedor : Form, IObserver
    {
        BEOrdenCompra ordenC = null;
        public COMPRAfrmRegistrarPagoProveedor(BEOrdenCompra ordenCompra)
        {
            ordenC = ordenCompra;

            IdiomaManager.GetInstance().archivoActual = "frmRegistrarPago";
            IdiomaManager.GetInstance().Agregar(this);

            InitializeComponent();
        }


        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        private void COMPRAfrmRegistrarPagoProveedor_Load(object sender, EventArgs e)
        {
            lblTotal.Text += ordenC.MontoTotal;
            lblProveedor.Text += ordenC.proveedor.Nombre;
            txtCBU.Text = ordenC.proveedor.CBU;
            txtBanco.Text = ordenC.proveedor.Banco;
            txtNumFactura.Focus();
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
            if(txtNumTransferencia.Text == "" || txtNumFactura.Text == "" || txtCBU.Text == "" || txtBanco.Text == "")
            {
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        //Para que no pueda escribir letras y mas de 12 digitos
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

                    if (texto.Length >= 20)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtNumTransferencia_KeyPress(object sender, KeyPressEventArgs e)
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

                    if (texto.Length >= 20)
                    {
                        e.Handled = true;
                    }
                }
            }
        }
    }
}
