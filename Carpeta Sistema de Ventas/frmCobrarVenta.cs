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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmCobrarVenta : Form
    {
        BLLFactura bllFactura = new BLLFactura();
        BLLProducto bllProducto = new BLLProducto();
        BEFactura _factura;
        public Cobro cobroDatos = new Cobro();
        public frmCobrarVenta(BEFactura factura)
        {
            InitializeComponent();
            _factura = factura;
        }

        private void frmCobrarVenta_Load(object sender, EventArgs e)
        {
            var valoresEnum = Enum.GetNames(typeof(EnumMetodoPago));
            foreach (var valor in valoresEnum)
            {
                cmbMetodoPago.Items.Add(valor);
            }
            lblMontoTotal.Text = "Monto total: " + _factura.MontoTotal.ToString();
            lblImpuesto.Text = "Impuesto: " + _factura.Impuesto.ToString();
            lblNumeroFactura.Text = "Número de factura: " + _factura.NumFactura.ToString();
        }

        private void btnCobrarVenta_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                cobroDatos.NumTransaccionBancaria = _factura.NumFactura;
                cobroDatos.MetodoPago = (EnumMetodoPago)Enum.Parse(typeof(EnumMetodoPago), cmbMetodoPago.SelectedItem.ToString());
                cobroDatos.ComentarioAdicional = txtComentarioAdicional.Text;
                
                if(cobroDatos.MetodoPago == EnumMetodoPago.Debito) /*si es debito es una cuota*/
                {
                    cobroDatos.CantCuotas = 1;
                }

                if (cobroDatos.MetodoPago == EnumMetodoPago.MercadoPago)
                {
                    cobroDatos.CantCuotas = 1;
                    cobroDatos.AliasMP = txtAliasMP.Text;
                }
                else
                {
                    cobroDatos.AliasMP = null;
                    cobroDatos.NumTarjeta = Encriptador.EncriptarAES(txtNumTarjeta.Text); //TIENE QUE ENCRIPTARSE REVERSIBLEMENTE
                    cobroDatos.MarcaTarjeta = cmbMarcaTarjeta.Text;
                    cobroDatos.CantCuotas = Convert.ToInt16(txtCantCuotas.Text);
                }
               

                _factura.cobro = cobroDatos;
                MessageBox.Show("Venta cobrada");
                this.Close();
            }
            else { MessageBox.Show("Complete los campos"); }
           
        }

        private bool ValidarCampos()
        {

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
                    if(txtNumTarjeta.Text == "0" || cmbMarcaTarjeta.Text == "" || txtCantCuotas.Text == "")
                    {
                        return false;
                    }

                    if (!Regex.IsMatch(txtNumTarjeta.Text, @"^\d{14,16}$"))
                    {
                        MessageBox.Show("El numero de tarjeta debe contener solo números y tener entre 14 y 16 dígitos.");
                        return false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un metodo de pago");
                return false;
            }
            return true; 
        }

        private void btnConectar_Click(object sender, EventArgs e) 
        { 
            txtNumTransaccion.Text = _factura.NumFactura.ToString();
            cmbMetodoPago.Enabled = true;
            txtNumTarjeta.Enabled = true;
            txtAliasMP.Enabled = true;
            cmbMarcaTarjeta.Enabled = true;
            txtCantCuotas.Enabled = true;
            txtComentarioAdicional.Enabled = true;
            btnCobrarVenta.Enabled = true;
        }

        private void cmbMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAliasMP.Text = "";

            if (cmbMetodoPago.Text == EnumMetodoPago.MercadoPago.ToString())
            {
                txtAliasMP.Enabled = true;
                txtNumTarjeta.Enabled = false;
                cmbMarcaTarjeta.Enabled = false;
                txtCantCuotas.Enabled = false;
                txtCantCuotas.Text = "1";
            }
            else
            {
                txtAliasMP.Enabled = false;
                txtNumTarjeta.Enabled = true;
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
                    string currentText = numUpDown.Text;

                    if (currentText.Length >= 16)
                    {
                        e.Handled = true;
                    }
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
                    string currentText = numUpDown.Text;

                    if (currentText.Length >= 2)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

    }
}
