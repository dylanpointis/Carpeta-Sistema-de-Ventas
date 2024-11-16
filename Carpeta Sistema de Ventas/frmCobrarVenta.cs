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

            lblMontoTotal.Text = IdiomaManager.GetInstance().ConseguirTexto("lblMontoTotal") + _factura.MontoTotal.ToString();
            lblImpuesto.Text = IdiomaManager.GetInstance().ConseguirTexto("lblImpuesto") + _factura.Impuesto.ToString();
            lblNumeroFactura.Text = IdiomaManager.GetInstance().ConseguirTexto("lblNumeroFactura") + _factura.NumFactura.ToString();
            LlenarComboBoxMetodoPago();
            txtCantCuotas.Text = "1";
            txtCantCuotas.Enabled = false;
        }

        private void LlenarComboBoxMetodoPago()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Texto"); // La columna para el texto a mostrar
            dt.Columns.Add("Valor"); // La columna para el valor real

            dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Crédito"), EnumMetodoPago.Crédito);
            dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Débito"), EnumMetodoPago.Débito);
            dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Mercado Pago"), EnumMetodoPago.MercadoPago);
            dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Efectivo"), EnumMetodoPago.Efectivo);
            cmbMetodoPago.DataSource = dt;
            cmbMetodoPago.DisplayMember = "Texto";
            cmbMetodoPago.ValueMember = "Valor";

            cmbMarcaTarjeta.Enabled = false;
            txtAliasMP.Enabled = false;

            //var valoresEnum = Enum.GetNames(typeof(EnumMetodoPago));
            //foreach (var valor in valoresEnum)
            //{
            //    cmbMetodoPago.Items.Add(valor);
            //}
        }

        private void btnCobrarVenta_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                _factura.cobro.MetodoPago = (EnumMetodoPago)Enum.Parse(typeof(EnumMetodoPago), cmbMetodoPago.SelectedValue.ToString());
                _factura.cobro.stringMetodoPago = cmbMetodoPago.SelectedValue.ToString();
                _factura.cobro.ComentarioAdicional = txtComentarioAdicional.Text;

                List<BEFactura> facturasConMismoNumTransaccion = new List<BEFactura>();
                if (txtNumTransaccion.Text != "")
                {
                    _factura.cobro.NumTransaccionBancaria = Convert.ToInt32(txtNumTransaccion.Text);
                    facturasConMismoNumTransaccion = bllFactura.ConsultarFacturas(0, Convert.ToInt32(txtNumTransaccion.Text), 0); //busca si ya hay una factura con el mismo num transaccion
                }


                _factura.cobro.AliasMP = null;
                _factura.cobro.CantCuotas = 0;
                //si no encontro facturas con el mismo numero de transaccion
                if (facturasConMismoNumTransaccion.Count() == 0)
                {
                    if (_factura.cobro.MetodoPago == EnumMetodoPago.Débito) /*si es debito es una cuota*/
                    {
                        _factura.cobro.MarcaTarjeta = cmbMarcaTarjeta.Text;
                    }
                    else if (_factura.cobro.MetodoPago == EnumMetodoPago.MercadoPago)
                    {
                        _factura.cobro.AliasMP = txtAliasMP.Text;
                    }
                    else if(_factura.cobro.MetodoPago == EnumMetodoPago.Crédito)
                    {
                        _factura.cobro.MarcaTarjeta = cmbMarcaTarjeta.Text;
                        _factura.cobro.CantCuotas = Convert.ToInt16(txtCantCuotas.Text);
                    }
                    else //si es efectivo el num transaccion es null
                    {
                        _factura.cobro.NumTransaccionBancaria = null;
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
            if(txtNumTransaccion.Text == "" && cmbMetodoPago.Text != IdiomaManager.GetInstance().ConseguirTexto("Efectivo"))
            {
                return false;
            }

            /*Si el metodo de pago es Mercado pago debe solamente poner el aliasMp, no los datos de la tarejeta.*/
            if (cmbMetodoPago.Text != "")
            {
                if (cmbMetodoPago.Text != IdiomaManager.GetInstance().ConseguirTexto("Efectivo"))
                { 
                    if (cmbMetodoPago.Text == IdiomaManager.GetInstance().ConseguirTexto("Mercado Pago"))
                    {
                        if (txtAliasMP.Text == "")
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (cmbMarcaTarjeta.Text == "" || txtCantCuotas.Text == "")
                        {
                            return false;
                        }
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
            txtAliasMP.Enabled = false;
            txtNumTransaccion.Text = _factura.cobro.NumTransaccionBancaria.ToString();
            txtNumTransaccion.Enabled = true;
            txtCantCuotas.Enabled = false;
            if (cmbMetodoPago.Text == IdiomaManager.GetInstance().ConseguirTexto("Mercado Pago"))
            {
                txtAliasMP.Enabled = true;
                cmbMarcaTarjeta.Enabled = false;
                txtCantCuotas.Text = "0";
            }
            else if (cmbMetodoPago.Text == IdiomaManager.GetInstance().ConseguirTexto("Crédito"))
            {
                txtCantCuotas.Text = "1";
                cmbMarcaTarjeta.Enabled = true;
                txtCantCuotas.Enabled = true;
            }

            if (cmbMetodoPago.Text == IdiomaManager.GetInstance().ConseguirTexto("Débito")) //si es debito no se puede pagar con cuotas
            {
                cmbMarcaTarjeta.Enabled = true;
                txtCantCuotas.Text = "0";
            }

            if (cmbMetodoPago.Text == IdiomaManager.GetInstance().ConseguirTexto("Efectivo"))
            {
                txtNumTransaccion.Enabled = false;
                txtNumTransaccion.Text = "";;
                cmbMarcaTarjeta.Enabled= false;
                txtCantCuotas.Text = "0";
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
                    e.Handled = true;
                if (!char.IsControl(e.KeyChar))
                {
                    if (textBox.Text.Length >= 9)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

    }
}
