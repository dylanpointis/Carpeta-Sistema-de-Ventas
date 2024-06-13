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
                _factura.NumTransaccionBancaria = _factura.NumFactura;
                _factura.MetodoPago = (EnumMetodoPago)Enum.Parse(typeof(EnumMetodoPago), cmbMetodoPago.SelectedItem.ToString());
                _factura.ComentarioAdicional = txtComentarioAdicional.Text;
                _factura.CantCuotas = 1;
                if (_factura.MetodoPago == EnumMetodoPago.MercadoPago)
                {
                    _factura.AliasMP = txtAliasMP.Text;
                }
                else
                {
                    _factura.AliasMP = null;
                    _factura.NumTarjeta = Encriptador.EncriptarAES(txtNumTarjeta.Text); //TIENE QUE ENCRIPTARSE REVERSIBLEMENTE
                    _factura.MarcaTarjeta = cmbMarcaTarjeta.Text;
                    _factura.CantCuotas = Convert.ToInt16(txtCantCuotas.Text);
                }
                bllFactura.RegistrarDatosPago(_factura);
                bllFactura.RegistrarItemFactura(_factura); //registra cada item de la factura


                foreach (var item in _factura.listaProductosAgregados)
                {
                    BEProducto prod = item.Item1;
                    int cantidad = item.Item2;

                    bllProducto.ModificarStock(prod, prod.Stock - cantidad);
                }

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
            }
            else
            {
                txtAliasMP.Enabled = false;
                txtNumTarjeta.Enabled = true;
                cmbMarcaTarjeta.Enabled = true;
                txtCantCuotas.Enabled = true;
            }
        }
    }
}
