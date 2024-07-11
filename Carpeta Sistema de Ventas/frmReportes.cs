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


            ActualizarGrilla();
        }

        private void ActualizarGrilla()
        {
            List<BEFactura> listaFac = bllFactura.TraerFacturas();

            grillaFacturas.Rows.Clear();
            foreach(BEFactura fac in listaFac)
            {
                grillaFacturas.Rows.Add(fac.NumFactura, fac.clienteFactura.DniCliente, fac.cobro.NumTransaccionBancaria,
                    fac.MontoTotal, fac.Impuesto, fac.Fecha, fac.cobro.stringMetodoPago, fac.cobro.MarcaTarjeta, fac.cobro.CantCuotas, fac.cobro.AliasMP,
                    fac.clienteFactura.Nombre, fac.clienteFactura.Apellido, fac.clienteFactura.Mail, fac.clienteFactura.Direccion);
            }
        }

        private void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            if (grillaFacturas.SelectedRows.Count > 0)
            {
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



                SaveFileDialog guardarArchivo = new SaveFileDialog();
                guardarArchivo.FileName = fac.NumFactura + "-" +DateTime.Now.ToString("yyyy-MM-dd") + ".pdf";


                string paginahtml = Properties.Resources.htmlfactura.ToString();


                paginahtml = paginahtml.Replace("@NroFactura", fac.NumFactura.ToString());
                paginahtml = paginahtml.Replace("@Fecha", fac.Fecha.ToString("dd/MM/yyyy HH:mm"));

                paginahtml = paginahtml.Replace("@DNI", fac.clienteFactura.DniCliente.ToString());
                paginahtml = paginahtml.Replace("@Nombre", fac.clienteFactura.Nombre);
                paginahtml = paginahtml.Replace("@Apellido", fac.clienteFactura.Apellido);
                paginahtml = paginahtml.Replace("@Email", fac.clienteFactura.Mail);
                paginahtml = paginahtml.Replace("@Direccion", fac.clienteFactura.Direccion);


                string filas = "";
                double subtotalfactura = 0;
                foreach (var item in fac.listaProductosAgregados)
                {
                    BEProducto prod = item.Item1;
                    int cantidad = item.Item2;
                    double subtotal = cantidad * prod.Precio;
                    subtotalfactura += subtotal;

                    filas += "<tr>";
                    filas += "<td>" + prod.CodigoProducto.ToString() + "</td>";
                    filas += "<td>" + prod.Modelo + "</td>";
                    filas += "<td>" + prod.Precio.ToString() + "</td>";
                    filas += "<td>" + cantidad.ToString() + "</td>";
                    filas += "<td>" + subtotal.ToString() + "</td>";
                    filas += "</tr>";
                }

                paginahtml = paginahtml.Replace("@FILAS", filas);
                paginahtml = paginahtml.Replace("@Neto", subtotalfactura.ToString());
                paginahtml = paginahtml.Replace("@Iva", fac.Impuesto.ToString());
                paginahtml = paginahtml.Replace("@Total", fac.MontoTotal.ToString());
                paginahtml = paginahtml.Replace("@Metodo", fac.cobro.stringMetodoPago);
                paginahtml = paginahtml.Replace("@Marca", fac.cobro.MarcaTarjeta.ToString());
                paginahtml = paginahtml.Replace("@CantCuotas", fac.cobro.CantCuotas.ToString());


                if(fac.cobro.AliasMP.ToString() != "")
                {
                    paginahtml = paginahtml.Replace("@Alias", fac.cobro.AliasMP.ToString());
                }
                else { paginahtml = paginahtml.Replace("@Alias", "-"); }


                //traducciones
                paginahtml = paginahtml.Replace("@textoDNI", IdiomaManager.GetInstance().ConseguirTexto("dgvDNI"));
                paginahtml = paginahtml.Replace("@textoNombre", IdiomaManager.GetInstance().ConseguirTexto("dgvNombre"));
                paginahtml = paginahtml.Replace("@textoApellido", IdiomaManager.GetInstance().ConseguirTexto("dgvApellido"));
                paginahtml = paginahtml.Replace("@textoEmail", IdiomaManager.GetInstance().ConseguirTexto("dgvEmail"));
                paginahtml = paginahtml.Replace("@textoDireccion", IdiomaManager.GetInstance().ConseguirTexto("dgvDireccion"));
                paginahtml = paginahtml.Replace("@textoCodigoproducto", IdiomaManager.GetInstance().ConseguirTexto("dgvCodigo"));
                paginahtml = paginahtml.Replace("@textoModelo", IdiomaManager.GetInstance().ConseguirTexto("dgvModelo"));
                paginahtml = paginahtml.Replace("@textoPrecio", IdiomaManager.GetInstance().ConseguirTexto("dgvPrecio"));
                paginahtml = paginahtml.Replace("@textoCantidad", IdiomaManager.GetInstance().ConseguirTexto("dgvCantidad"));
                paginahtml = paginahtml.Replace("@textoSubTotal", IdiomaManager.GetInstance().ConseguirTexto("dgvSubtotal"));
                paginahtml = paginahtml.Replace("@textoFactura", IdiomaManager.GetInstance().ConseguirTexto("dgvNumFactura"));
                paginahtml = paginahtml.Replace("@textoFecha", IdiomaManager.GetInstance().ConseguirTexto("htmlFechaVenta"));
                paginahtml = paginahtml.Replace("@textoDetalleCliente", IdiomaManager.GetInstance().ConseguirTexto("htmlDetalleCliente"));
                paginahtml = paginahtml.Replace("@textoDetalleProductos", IdiomaManager.GetInstance().ConseguirTexto("htmlDetalleProductos"));
                paginahtml = paginahtml.Replace("@textoDetallePago", IdiomaManager.GetInstance().ConseguirTexto("htmlDetallePago"));


                paginahtml = paginahtml.Replace("@textoNeto", IdiomaManager.GetInstance().ConseguirTexto("htmlNeto"));
                paginahtml = paginahtml.Replace("@textoIVA", IdiomaManager.GetInstance().ConseguirTexto("htmlIVA"));
                paginahtml = paginahtml.Replace("@textoMontoTotal", IdiomaManager.GetInstance().ConseguirTexto("htmlMontoTotal"));
                paginahtml = paginahtml.Replace("@textoMetodo", IdiomaManager.GetInstance().ConseguirTexto("dgvMetodo"));
                paginahtml = paginahtml.Replace("@textoMarca", IdiomaManager.GetInstance().ConseguirTexto("dgvMarca"));
                paginahtml = paginahtml.Replace("@textoAlias", IdiomaManager.GetInstance().ConseguirTexto("htmlAlias"));
                paginahtml = paginahtml.Replace("@textoCantCuotas", IdiomaManager.GetInstance().ConseguirTexto("dgvCantCuotas"));


                if (guardarArchivo.ShowDialog()== DialogResult.OK)
                {
                    using(FileStream stream = new FileStream(guardarArchivo.FileName, FileMode.Create))
                    {
                        Document pdf = new Document(PageSize.A4, 25, 25, 25, 25);

                        PdfWriter escritor = PdfWriter.GetInstance(pdf, stream);

                        pdf.Open();
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.logo, System.Drawing.Imaging.ImageFormat.Png);
                        img.ScaleToFit(80, 60);
                        img.Alignment = iTextSharp.text.Image.UNDERLYING;
                        img.SetAbsolutePosition(pdf.Right - 60, pdf.Top - 60);
                        pdf.Add(img);

                        using (StringReader lector = new StringReader(paginahtml))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(escritor, pdf,lector);
                        }


                        pdf.Close();
                        stream.Close();
                    }
                }


            }
            else
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccione"));
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if(txtNumFactura.Text != "")
            {
                BEFactura fac = bllFactura.TraerFacturas().FirstOrDefault(f => f.NumFactura == Convert.ToInt64(txtNumFactura.Text));

                grillaFacturas.Rows.Clear();

                grillaFacturas.Rows.Add(fac.NumFactura, fac.clienteFactura.DniCliente, fac.cobro.NumTransaccionBancaria,
                    fac.MontoTotal, fac.Impuesto, fac.Fecha, fac.cobro.stringMetodoPago, fac.cobro.MarcaTarjeta, fac.cobro.CantCuotas, fac.cobro.AliasMP,
                    fac.clienteFactura.Nombre, fac.clienteFactura.Apellido, fac.clienteFactura.Mail, fac.clienteFactura.Direccion);
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("ingrese")); }
            
           
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

                double total = 0;
                foreach (var item in fac.listaProductosAgregados)
                {
                    BEProducto prod = item.Item1;
                    int cantidad = item.Item2;
                    total = cantidad * prod.Precio;

                    grillaItems.Rows.Add(prod.CodigoProducto, prod.Modelo, cantidad, prod.Precio, total);
                }
            }
        }
    }
}
