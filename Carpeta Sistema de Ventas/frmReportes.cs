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

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmReportes : Form, IObserver
    {
        public frmReportes()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().archivoActual = "frmReportes";
            IdiomaManager.GetInstance().Agregar(this);
        }

        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        BLLFactura bllFactura = new BLLFactura();
        BLLEvento bLLEvento = new BLLEvento();

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
                grillaFacturas.Rows.Add(fac.NumFactura, fac.clienteFactura.DniCliente, fac.cobro.NumTransaccionBancaria,
                    fac.MontoTotal, fac.Impuesto, fac.Fecha.ToString("yyyy-MM-dd HH:mm"), fac.cobro.stringMetodoPago, fac.cobro.MarcaTarjeta, fac.cobro.CantCuotas, fac.cobro.AliasMP,
                    fac.clienteFactura.Nombre, fac.clienteFactura.Apellido, fac.clienteFactura.Mail, fac.clienteFactura.Direccion);
            }
        }

        private void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            if (grillaFacturas.SelectedRows.Count > 0)
            {
                bLLEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Ventas", "Impresión de factura", 4));

                BECliente cli = new BECliente(Convert.ToInt32(grillaFacturas.CurrentRow.Cells[1].Value), grillaFacturas.CurrentRow.Cells[10].Value.ToString(), grillaFacturas.CurrentRow.Cells[11].Value.ToString(), grillaFacturas.CurrentRow.Cells[12].Value.ToString(), grillaFacturas.CurrentRow.Cells[13].Value.ToString());

                BECobro cobro = new BECobro() { NumTransaccionBancaria = Convert.ToInt32(grillaFacturas.CurrentRow.Cells[2].Value), MarcaTarjeta = grillaFacturas.CurrentRow.Cells[7].Value.ToString(), CantCuotas = Convert.ToInt32(grillaFacturas.CurrentRow.Cells[8].Value), AliasMP = grillaFacturas.CurrentRow.Cells[9].Value.ToString(), stringMetodoPago = grillaFacturas.CurrentRow.Cells[6].Value.ToString() };

                BEFactura fac = new BEFactura()
                {
                    NumFactura = Convert.ToInt32(grillaFacturas.CurrentRow.Cells[0].Value),
                    clienteFactura = cli,
                    cobro = cobro,
                    Fecha = Convert.ToDateTime(grillaFacturas.CurrentRow.Cells[5].Value),
                    MontoTotal = Convert.ToDouble(grillaFacturas.CurrentRow.Cells[3].Value),
                    Impuesto = Convert.ToDouble(grillaFacturas.CurrentRow.Cells[4].Value),
                };

                //busca y carga los items en la factura
                fac = bllFactura.TraerItemsFactura(fac);

                //genera el reporte pdf
                string paginahtml = Properties.Resources.htmlfactura.ToString();
                Reportes.GenerarReporteVenta(fac, paginahtml, Properties.Resources.logo);
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
                //Si no esta vacio convierte a int32, si no le pone 0
                int numfac = txtNumFactura.Text != "" ? Convert.ToInt32(txtNumFactura.Text) : 0;
                int numtran = txtNumTransaccion.Text != "" ? Convert.ToInt32(txtNumTransaccion.Text) : 0;
                int dni = txtDni.Text != "" ? Convert.ToInt32(txtDni.Text) : 0;



                List<BEFactura> listaFacturasConsulta = bllFactura.ConsultarFacturas(numfac, numtran, dni);

                grillaFacturas.Rows.Clear();

                foreach (BEFactura fac in listaFacturasConsulta)
                {
                    grillaFacturas.Rows.Add(fac.NumFactura, fac.clienteFactura.DniCliente, fac.cobro.NumTransaccionBancaria,
                    fac.MontoTotal, fac.Impuesto, fac.Fecha, fac.cobro.stringMetodoPago, fac.cobro.MarcaTarjeta, fac.cobro.CantCuotas, fac.cobro.AliasMP,
                    fac.clienteFactura.Nombre, fac.clienteFactura.Apellido, fac.clienteFactura.Mail, fac.clienteFactura.Direccion);
                }

                //txtNumFactura.Text = ""; txtNumTransaccion.Text = "";  txtDni.Text = "";
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

                BEFactura fac = bllFactura.TraerFacturas().FirstOrDefault(f => f.NumFactura == codigofactura);

                fac = bllFactura.TraerItemsFactura(fac);

                double subtotal = 0;
                foreach (BEItemFactura item in fac.listaProductosAgregados)
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
            else { grillaFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; }
        }
    }
}
