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
using static System.Net.Mime.MediaTypeNames;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmCobrarVenta : Form, IObserver
    {
        BEFactura _factura;
        BLLFactura bllFactura = new BLLFactura();
        public frmCobrarVenta(BEFactura factura)
        {
            InitializeComponent();
            _factura = factura;


            IdiomaManager.GetInstance().archivoActual = "frmCobrarVenta";
            IdiomaManager.GetInstance().Agregar(this);
        }

        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        private void frmCobrarVenta_Load(object sender, EventArgs e)
        {
            _factura.cobro = new BECobro();
            _factura.cobro.NumTransaccionBancaria = bllFactura.TraerUltimoNumTransaccion() + 1;

            var valoresEnum = Enum.GetNames(typeof(EnumMetodoPago));
            foreach (var valor in valoresEnum)
            {
                cmbMetodoPago.Items.Add(valor);
            }
            lblMontoTotal.Text = IdiomaManager.GetInstance().ConseguirTexto("lblMontoTotal") + _factura.MontoTotal.ToString();
            lblImpuesto.Text = IdiomaManager.GetInstance().ConseguirTexto("lblImpuesto") + _factura.Impuesto.ToString();
            lblNumeroFactura.Text = IdiomaManager.GetInstance().ConseguirTexto("lblNumeroFactura") + _factura.NumFactura.ToString();
        }

        private void btnCobrarVenta_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                //_factura.cobro.NumTransaccionBancaria = _factura.NumFactura;
                _factura.cobro.MetodoPago = (EnumMetodoPago)Enum.Parse(typeof(EnumMetodoPago), cmbMetodoPago.SelectedItem.ToString());
                _factura.cobro.stringMetodoPago = cmbMetodoPago.SelectedItem.ToString();
                _factura.cobro.ComentarioAdicional = txtComentarioAdicional.Text;
                _factura.cobro.NumTransaccionBancaria = Convert.ToInt32(txtNumTransaccion.Text);


                BLLFactura bllfac = new BLLFactura();
                List<BEFactura> facs = bllfac.ConsultarFacturas(0, Convert.ToInt32(txtNumTransaccion.Text), 0); //busca si ya hay una factura con el mismo num transaccion
                if (facs.Count() == 0)
                {
                    if (_factura.cobro.MetodoPago == EnumMetodoPago.Debito) /*si es debito es una cuota*/
                    {
                        _factura.cobro.CantCuotas = 1;
                    }
                    else if (_factura.cobro.MetodoPago == EnumMetodoPago.MercadoPago)
                    {
                        _factura.cobro.CantCuotas = 1;
                        _factura.cobro.AliasMP = txtAliasMP.Text;
                    }
                    else
                    {
                        _factura.cobro.AliasMP = null;
                        _factura.cobro.MarcaTarjeta = cmbMarcaTarjeta.Text;
                        _factura.cobro.CantCuotas = Convert.ToInt16(txtCantCuotas.Text);
                    }
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("ventaCobrada"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("numTransOcupado"), "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("llene"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
           
        }

        private bool ValidarCampos()
        {
            if(txtNumTransaccion.Text == "")
            {
                return false;
            }

            /*Si el metodo de pago es Mercado pago debe solamente poner el aliasMp, no los datos de la tarejeta.*/
            if (cmbMetodoPago.Text != "") 
            {
                if(cmbMetodoPago.Text == EnumMetodoPago.MercadoPago.ToString())
                {
                    if(txtAliasMP.Text == "")
                    {
                        return false;
                    }
                }
                else 
                {
                    if( cmbMarcaTarjeta.Text == "" || txtCantCuotas.Text == "")
                    {
                        return false;
                    }
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccioneMetodo"));
                return false;
            }
            return true; 
        }

        private void btnConectar_Click(object sender, EventArgs e) 
        {
            txtNumTransaccion.Enabled = true;
            txtNumTransaccion.Text = _factura.cobro.NumTransaccionBancaria.ToString();
            cmbMetodoPago.Enabled = true;
            txtAliasMP.Enabled = true;
            cmbMarcaTarjeta.Enabled = true;
            txtCantCuotas.Enabled = true;
            txtComentarioAdicional.Enabled = true;
            btnCobrarVenta.Enabled = true;
            btnConectar.Enabled = false;
        }

        private void cmbMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAliasMP.Text = "";

            if (cmbMetodoPago.Text == EnumMetodoPago.MercadoPago.ToString())
            {
                txtAliasMP.Enabled = true;
                cmbMarcaTarjeta.Enabled = false;
                txtCantCuotas.Enabled = false;
                txtCantCuotas.Text = "1";
            }
            else
            {
                txtAliasMP.Enabled = false;
                cmbMarcaTarjeta.Enabled = true;
                txtCantCuotas.Enabled = true;
            }

            if (cmbMetodoPago.Text == EnumMetodoPago.Debito.ToString()) //si es debito no se puede pagar con cuotas
            {
                txtCantCuotas.Text = "1";
                txtCantCuotas.Enabled = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }







        /*Evento para que no escriba mas de 16 digitos*/
        private void txtNumTarjeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericUpDown numUpDown = sender as NumericUpDown;

            if (numUpDown != null)
            {
                if (!char.IsControl(e.KeyChar))
                {
                    string texto = numUpDown.Text;

                    if (texto.Length >= 16)
                    {
                        e.Handled = true;
                    }
                }
                //para que no pueda ecribir , o .
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        /*Evento para que no escriba mas de 2 digitos*/
        private void txtCantCuotas_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericUpDown numUpDown = sender as NumericUpDown;

            if (numUpDown != null)
            {
                if (!char.IsControl(e.KeyChar))
                {
                    string texto = numUpDown.Text;

                    if (texto.Length >= 2)
                    {
                        e.Handled = true;
                    }
                }
                //para que no pueda ecribir , o .
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

            }
        }


        //para que pueda escribir solo numeros
        private void txtNumTransaccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {

                    e.Handled = true;
                }
            }

        }

    }
}
