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
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmReportesVentas : Form, IObserver
    {
        public frmReportesVentas()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().archivoActual = "frmReportesVentas";
            IdiomaManager.GetInstance().Agregar(this);
        }

        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        BLLFactura bllFactura = new BLLFactura();
        BLLEvento bLLEvento = new BLLEvento();
        BEFactura facturaSeleccionada;

        private void frmReportes_Load(object sender, EventArgs e)
        {
            grillaFacturas.ColumnCount = 14;
            grillaFacturas.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvNumFactura");
            grillaFacturas.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvDNI");
            grillaFacturas.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvTran");
            grillaFacturas.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvTotal");
            grillaFacturas.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvImpuesto");
            grillaFacturas.Columns[5].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvFecha");
            grillaFacturas.Columns[6].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvMetodo");
            grillaFacturas.Columns[7].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvMarca");
            grillaFacturas.Columns[8].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvCantCuotas");
            grillaFacturas.Columns[9].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvAlias");
            grillaFacturas.Columns[10].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvNombre");
            grillaFacturas.Columns[11].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvApellido");
            grillaFacturas.Columns[12].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvEmail");
            grillaFacturas.Columns[13].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvDireccion");

            grillaFacturas.Columns[0].Width = 50;
            grillaFacturas.Columns[1].Width = 65;
            grillaFacturas.Columns[3].Width = 60;
            grillaFacturas.Columns[4].Width = 60;
            grillaFacturas.Columns[7].Width = 45;



            grillaItems.ColumnCount = 5;
            grillaItems.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvCodigo");
            grillaItems.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvModelo");
            grillaItems.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvCantidad");
            grillaItems.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvPrecio");
            grillaItems.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvSubtotal");

            grillaItems.Columns[2].Width = 40;
            grillaItems.RowHeadersVisible = false;

            ActualizarGrilla();
        }

        private void ActualizarGrilla()
        {
            List<BEFactura> listaFac = bllFactura.TraerFacturas();

            grillaFacturas.Rows.Clear();
            foreach(BEFactura fac in listaFac)
            {
                string metodoPagoTraducido = IdiomaManager.GetInstance().ConseguirTexto(fac.cobro.stringMetodoPago);

                grillaFacturas.Rows.Add(fac.NumFactura, fac.clienteFactura.DniCliente, fac.cobro.NumTransaccionBancaria,
                    fac.MontoTotal, fac.Impuesto, fac.Fecha.ToString("yyyy-MM-dd HH:mm"), metodoPagoTraducido, fac.cobro.MarcaTarjeta, fac.cobro.CantCuotas, fac.cobro.AliasMP,
                    fac.clienteFactura.Nombre, fac.clienteFactura.Apellido, fac.clienteFactura.Mail, fac.clienteFactura.Direccion);
            }
        }

        private void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            if (grillaFacturas.SelectedRows.Count > 0)
            {
                bLLEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Ventas", "Impresión de factura", 4));

                //genera el reporte pdf
                string paginahtml = Properties.Resources.htmlfactura.ToString();
                Reportes.GenerarReporteVenta(facturaSeleccionada, paginahtml, Properties.Resources.logo);
            }
            else
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccione"));
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtNumFactura.Text == "" && txtNumTransaccion.Text == "" && txtDni.Text == "")
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("ingrese"));
            }
            else
            {
                if (!Regex.IsMatch(txtNumFactura.Text, @"^(\d+)?$") || !Regex.IsMatch(txtNumTransaccion.Text, @"^(\d+)?$") || !Regex.IsMatch(txtDni.Text, @"^(\d+)?$"))
                {
                    return; //si no son numeros no continua
                }
                //Si no esta vacio convierte a int32, si no le pone 0
                int numfac = txtNumFactura.Text != "" ? Convert.ToInt32(txtNumFactura.Text) : 0;
                int numtran = txtNumTransaccion.Text != "" ? Convert.ToInt32(txtNumTransaccion.Text) : 0;
                int dni = txtDni.Text != "" ? Convert.ToInt32(txtDni.Text) : 0;



                List<BEFactura> listaFacturasConsulta = bllFactura.ConsultarFacturas(numfac, numtran, dni);

                grillaFacturas.Rows.Clear();

                foreach (BEFactura fact in listaFacturasConsulta)
                {
                    grillaFacturas.Rows.Add(fact.NumFactura, fact.clienteFactura.DniCliente, fact.cobro.NumTransaccionBancaria,
                    fact.MontoTotal, fact.Impuesto, fact.Fecha, fact.cobro.stringMetodoPago, fact.cobro.MarcaTarjeta, fact.cobro.CantCuotas, fact.cobro.AliasMP,
                    fact.clienteFactura.Nombre, fact.clienteFactura.Apellido, fact.clienteFactura.Mail, fact.clienteFactura.Direccion);
                }

                //txtNumFactura.Text = ""; txtNumTransaccion.Text = "";  txtDni.Text = "";
            }
        }



        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarGrilla();
        }

        private void grillaFacturas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grillaFacturas.SelectedRows.Count > 0)
            {
                grillaItems.Rows.Clear();
                long codigofactura = Convert.ToInt64(grillaFacturas.CurrentRow.Cells[0].Value);

                facturaSeleccionada = bllFactura.TraerFacturas().FirstOrDefault(f => f.NumFactura == codigofactura);
                if(facturaSeleccionada.cobro.ComentarioAdicional != null)
                {
                    lblComentarioAdicional.Text = IdiomaManager.GetInstance().ConseguirTexto("comentarioAdicional") + facturaSeleccionada.cobro.ComentarioAdicional;
                }
                else { lblComentarioAdicional.Text = ""; }
               

                facturaSeleccionada = bllFactura.TraerItemsFactura(facturaSeleccionada);

                double subtotal = 0;
                foreach (BEItemFactura item in facturaSeleccionada.listaProductosAgregados)
                {
                    BEProducto prod = item.producto;
                    int cantidad = item.cantidad;
                    subtotal += cantidad * prod.Precio;

                    grillaItems.Rows.Add(prod.CodigoProducto, prod.Modelo, cantidad, prod.Precio, cantidad * prod.Precio);
                }
                grillaItems.Rows.Add("","","","",subtotal);
            }
        }

        //esto es para que se vea bien cuando agrando o achico la pantalla
        private void frmReportes_Resize(object sender, EventArgs e)
        {
            if (this.ClientSize.Width > 1500)
            {
                grillaFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else { grillaFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; grillaFacturas.AllowUserToResizeColumns = true; }
        }

        private void txtNumFactura_KeyPress_1(object sender, KeyPressEventArgs e)
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
