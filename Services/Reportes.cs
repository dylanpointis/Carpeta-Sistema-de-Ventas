﻿using BE;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Services.Observer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Services
{
    public class Reportes
    {

        public static void GenerarReporteSolicitud(BESolicitudCotizacion solC, string paginahtml, Bitmap Logo, BEProveedor prov)
        {
            SaveFileDialog guardarArchivo = new SaveFileDialog();

            guardarArchivo.Filter = "PDF Files (*.pdf)|*.pdf";
            guardarArchivo.FileName = $"Solicitud {solC.NumSolicitud} - {prov.Nombre}_{DateTime.Now.ToString("yyyy-MM-dd")}.pdf";

            paginahtml = paginahtml.Replace("@NumSolicitud", solC.NumSolicitud.ToString());
            paginahtml = paginahtml.Replace("@fecha", solC.Fecha.ToString("yyyy-MM-dd HH:mm"));


            string filas = "";
            foreach (BEItemSolicitud item in solC.obtenerItems())
            {
                BEProducto prod = item.Producto;
                int cantidad = item.Cantidad;
                filas += "<tr>";
                filas += "<td>" + prod.CodigoProducto.ToString() + "</td>";
                filas += "<td>" + prod.Modelo + "</td>";
                filas += "<td>" + cantidad.ToString() + "</td>";
                filas += "</tr>";
            }

            paginahtml = paginahtml.Replace("@FILAS", filas);




            paginahtml = paginahtml.Replace("@CUIT", prov.CUIT);
            paginahtml = paginahtml.Replace("@NombreProveedor", prov.Nombre);
            paginahtml = paginahtml.Replace("@RazonSocial", prov.RazonSocial);



            //traducciones
            paginahtml = paginahtml.Replace("@textoNumSolicitud", IdiomaManager.GetInstance().ConseguirTexto("textoNumSolicitud"));
            paginahtml = paginahtml.Replace("@textoDetalleSolicitud", IdiomaManager.GetInstance().ConseguirTexto("textoDetalleSolicitud"));
            paginahtml = paginahtml.Replace("@textoDetalleProveedor", IdiomaManager.GetInstance().ConseguirTexto("textoDetalleProveedor"));
            paginahtml = paginahtml.Replace("@gridViewCodigo", IdiomaManager.GetInstance().ConseguirTexto("gridViewCodigo"));
            paginahtml = paginahtml.Replace("@gridViewModelo", IdiomaManager.GetInstance().ConseguirTexto("gridViewModelo"));
            paginahtml = paginahtml.Replace("@gridViewPrecioUnit", IdiomaManager.GetInstance().ConseguirTexto("gridViewPrecioUnit"));
            paginahtml = paginahtml.Replace("@textoCantidadSolicitada", IdiomaManager.GetInstance().ConseguirTexto("textoCantidadSolicitada"));
            paginahtml = paginahtml.Replace("@textoCantidadRecibida", IdiomaManager.GetInstance().ConseguirTexto("textoCantidadRecibida"));
            paginahtml = paginahtml.Replace("@gridViewCUIT", IdiomaManager.GetInstance().ConseguirTexto("gridViewCUIT"));
            paginahtml = paginahtml.Replace("@textoNombreProveedor", IdiomaManager.GetInstance().ConseguirTexto("gridViewNombre"));
            paginahtml = paginahtml.Replace("@gridViewRazonSocial", IdiomaManager.GetInstance().ConseguirTexto("gridViewRazonSocial"));
            paginahtml = paginahtml.Replace("@textoFecha", IdiomaManager.GetInstance().ConseguirTexto("textoFecha"));

            GuardarArchivoPDF(guardarArchivo, paginahtml, Logo);
        }


        public static void GenerarReporteOrden(BEOrdenCompra ordenC, string paginahtml, Bitmap Logo)
        {
            SaveFileDialog guardarArchivo = new SaveFileDialog();

            guardarArchivo.Filter = "PDF Files (*.pdf)|*.pdf";
            guardarArchivo.FileName = "OrdenCompra " + ordenC.NumeroOrdenCompra + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".pdf";

            paginahtml = paginahtml.Replace("@NumOrden", ordenC.NumeroOrdenCompra.ToString());
            paginahtml = paginahtml.Replace("@NroFactura", ordenC.NumeroFactura.ToString());
            paginahtml = paginahtml.Replace("@fecha", ordenC.FechaRegistro.ToString("yyyy-MM-dd HH:mm"));
            paginahtml = paginahtml.Replace("@FechaEntrega", ordenC.FechaEntrega.ToString("yyyy-MM-dd"));


            paginahtml = paginahtml.Replace("@CUIT", ordenC.proveedor.CUIT);
            paginahtml = paginahtml.Replace("@NombreProveedor", ordenC.proveedor.Nombre);
            paginahtml = paginahtml.Replace("@RazonSocial", ordenC.proveedor.RazonSocial);

            string filas = "";
            double subtotalfactura = 0;
            foreach (BEItemOrdenCompra item in ordenC.obtenerItems())
            {
                BEProducto prod = item.Producto;
                int cantidad = item.CantidadSolicitada;
                double subtotal = cantidad * item.PrecioCompra;
                subtotalfactura += subtotal;

                filas += "<tr>";
                filas += "<td>" + prod.CodigoProducto.ToString() + "</td>";
                filas += "<td>" + prod.Modelo + "</td>";
                filas += "<td>" + item.PrecioCompra.ToString("#,0.00", new System.Globalization.CultureInfo("es-ES")) + "</td>";
                filas += "<td>" + cantidad.ToString() + "</td>";
                filas += "</tr>";
            }

            paginahtml = paginahtml.Replace("@FILAS", filas);
            paginahtml = paginahtml.Replace("@Neto", subtotalfactura.ToString("#,0.00", new System.Globalization.CultureInfo("es-ES")));
            paginahtml = paginahtml.Replace("@Iva", (subtotalfactura * 0.21).ToString("#,0.00", new System.Globalization.CultureInfo("es-ES")));
            paginahtml = paginahtml.Replace("@Total", ordenC.MontoTotal.ToString("#,0.00", new System.Globalization.CultureInfo("es-ES")));
            paginahtml = paginahtml.Replace("@NumTransferencia", ordenC.NumeroTransferencia.ToString());

            //traducciones

            paginahtml = paginahtml.Replace("@textoNumOrden", IdiomaManager.GetInstance().ConseguirTexto("textoNumOrden"));
            paginahtml = paginahtml.Replace("@textoDetalleOrdenCompra", IdiomaManager.GetInstance().ConseguirTexto("textoDetalleOrdenCompra"));
            paginahtml = paginahtml.Replace("@textoDetalleProveedor", IdiomaManager.GetInstance().ConseguirTexto("textoDetalleProveedor"));
            paginahtml = paginahtml.Replace("@gridViewCodigo", IdiomaManager.GetInstance().ConseguirTexto("gridViewCodigo"));
            paginahtml = paginahtml.Replace("@gridViewModelo", IdiomaManager.GetInstance().ConseguirTexto("gridViewModelo"));
            paginahtml = paginahtml.Replace("@gridViewPrecioUnit", IdiomaManager.GetInstance().ConseguirTexto("gridViewPrecioUnit"));
            paginahtml = paginahtml.Replace("@textoCantidadSolicitada", IdiomaManager.GetInstance().ConseguirTexto("textoCantidadSolicitada"));
            paginahtml = paginahtml.Replace("@textoCantidadRecibida", IdiomaManager.GetInstance().ConseguirTexto("textoCantidadRecibida"));
            paginahtml = paginahtml.Replace("@gridViewCUIT", IdiomaManager.GetInstance().ConseguirTexto("gridViewCUIT"));
            paginahtml = paginahtml.Replace("@textoNombreProveedor", IdiomaManager.GetInstance().ConseguirTexto("textoNombreProveedor"));
            paginahtml = paginahtml.Replace("@gridViewRazonSocial", IdiomaManager.GetInstance().ConseguirTexto("gridViewRazonSocial"));
            paginahtml = paginahtml.Replace("@textoDetallePago", IdiomaManager.GetInstance().ConseguirTexto("textoDetallePago"));
            paginahtml = paginahtml.Replace("@textoNeto", IdiomaManager.GetInstance().ConseguirTexto("lblNeto"));
            paginahtml = paginahtml.Replace("@textoIVA", IdiomaManager.GetInstance().ConseguirTexto("lblIVA"));
            paginahtml = paginahtml.Replace("@textoMontoTotal", IdiomaManager.GetInstance().ConseguirTexto("lblTotal"));
            paginahtml = paginahtml.Replace("@textoNumeroTransferencia", IdiomaManager.GetInstance().ConseguirTexto("textoNumeroTransferencia"));
            paginahtml = paginahtml.Replace("@textoFactura", IdiomaManager.GetInstance().ConseguirTexto("textoFactura"));
            paginahtml = paginahtml.Replace("@textoFecha", IdiomaManager.GetInstance().ConseguirTexto("textoFecha"));
            paginahtml = paginahtml.Replace("@textfechaEntrega", IdiomaManager.GetInstance().ConseguirTexto("textoFechaEntrega"));

            GuardarArchivoPDF(guardarArchivo, paginahtml, Logo);
        }



        public static void GenerarReporteRecepcion(BEOrdenCompra ordenC, string paginahtml, Bitmap Logo)
        {
            SaveFileDialog guardarArchivo = new SaveFileDialog();

            guardarArchivo.Filter = "PDF Files (*.pdf)|*.pdf";
            guardarArchivo.FileName = ordenC.NumeroFactura + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".pdf";

            paginahtml = paginahtml.Replace("@NroFactura", ordenC.NumeroFactura.ToString());
            paginahtml = paginahtml.Replace("@NumOrden", ordenC.NumeroOrdenCompra.ToString());
            paginahtml = paginahtml.Replace("@fecha", ordenC.FechaRegistro.ToString("yyyy-MM-dd HH:mm"));
            paginahtml = paginahtml.Replace("@FechaEntrega", ordenC.FechaEntrega.ToString("yyyy-MM-dd HH:mm"));


            paginahtml = paginahtml.Replace("@CUIT", ordenC.proveedor.CUIT);
            paginahtml = paginahtml.Replace("@NombreProveedor", ordenC.proveedor.Nombre);
            paginahtml = paginahtml.Replace("@RazonSocial", ordenC.proveedor.RazonSocial);

            string filas = "";
            double subtotalfactura = 0;
            foreach (BEItemOrdenCompra item in ordenC.obtenerItems())
            {
                BEProducto prod = item.Producto;
                int cantidad = item.CantidadSolicitada;
                double subtotal = cantidad * item.PrecioCompra;
                subtotalfactura += subtotal;

                filas += "<tr>";
                filas += "<td>" + prod.CodigoProducto.ToString() + "</td>";
                filas += "<td>" + prod.Modelo + "</td>";
                filas += "<td>" + cantidad.ToString() + "</td>";
                filas += "<td>" + item.CantidadRecibida + "</td>";
                filas += "<td>" + item.NumFacturaRecepcion + "</td>";
                filas += "<td>" + item.FechaRecepcion + "</td>";
                filas += "</tr>";
            }

            paginahtml = paginahtml.Replace("@FILAS", filas);
            paginahtml = paginahtml.Replace("@Total", ordenC.MontoTotal.ToString("#,0.00", new System.Globalization.CultureInfo("es-ES")));
            paginahtml = paginahtml.Replace("@NumTransferencia", ordenC.NumeroTransferencia.ToString());

            //traducciones
          
            paginahtml = paginahtml.Replace("@textoDetalleOrdenCompra", IdiomaManager.GetInstance().ConseguirTexto("textoDetalleRecepcion"));
            paginahtml = paginahtml.Replace("@textoNumOrden", IdiomaManager.GetInstance().ConseguirTexto("textoNumOrden"));

            paginahtml = paginahtml.Replace("@textoDetalleProveedor", IdiomaManager.GetInstance().ConseguirTexto("textoDetalleProveedor"));
            paginahtml = paginahtml.Replace("@gridViewCodigo", IdiomaManager.GetInstance().ConseguirTexto("gridViewCodigo"));
            paginahtml = paginahtml.Replace("@gridViewModelo", IdiomaManager.GetInstance().ConseguirTexto("gridViewModelo"));
            paginahtml = paginahtml.Replace("@gridViewPrecioUnit", IdiomaManager.GetInstance().ConseguirTexto("gridViewPrecioUnit"));
            paginahtml = paginahtml.Replace("@textoCantidadSolicitada", IdiomaManager.GetInstance().ConseguirTexto("textoCantidadSolicitada"));
            paginahtml = paginahtml.Replace("@textoCantidadRecibida", IdiomaManager.GetInstance().ConseguirTexto("textoCantidadRecibida"));
            paginahtml = paginahtml.Replace("@gridViewCUIT", IdiomaManager.GetInstance().ConseguirTexto("gridViewCUIT"));
            paginahtml = paginahtml.Replace("@textoNombreProveedor", IdiomaManager.GetInstance().ConseguirTexto("textoNombreProveedor"));
            paginahtml = paginahtml.Replace("@gridViewRazonSocial", IdiomaManager.GetInstance().ConseguirTexto("gridViewRazonSocial"));
            paginahtml = paginahtml.Replace("@textoDetallePago", IdiomaManager.GetInstance().ConseguirTexto("textoDetallePago"));
            paginahtml = paginahtml.Replace("@textoNeto", IdiomaManager.GetInstance().ConseguirTexto("lblNeto"));
            paginahtml = paginahtml.Replace("@textoIVA", IdiomaManager.GetInstance().ConseguirTexto("lblIVA"));
            paginahtml = paginahtml.Replace("@textoMontoTotal", IdiomaManager.GetInstance().ConseguirTexto("lblTotal"));
            paginahtml = paginahtml.Replace("@textoNumeroTransferencia", IdiomaManager.GetInstance().ConseguirTexto("textoNumeroTransferencia"));
            paginahtml = paginahtml.Replace("@textoFactura", IdiomaManager.GetInstance().ConseguirTexto("textoFactura"));
            paginahtml = paginahtml.Replace("@textoFecha", IdiomaManager.GetInstance().ConseguirTexto("textoFecha"));
            paginahtml = paginahtml.Replace("@textfechaEntrega", IdiomaManager.GetInstance().ConseguirTexto("textoFechaEntrega"));
            paginahtml = paginahtml.Replace("@gridViewFacturaRecepcion", IdiomaManager.GetInstance().ConseguirTexto("gridViewFacturaRecepcion"));
            paginahtml = paginahtml.Replace("@gridViewFechaRecepcion", IdiomaManager.GetInstance().ConseguirTexto("gridViewFechaRecepcion"));

            GuardarArchivoPDF(guardarArchivo, paginahtml, Logo);
        }


        public static void GenerarReporteVenta(BEFactura fac, string paginahtml, Bitmap Logo)
        {
            SaveFileDialog guardarArchivo = new SaveFileDialog();

            guardarArchivo.Filter = "PDF Files (*.pdf)|*.pdf";
            guardarArchivo.FileName = fac.NumFactura + "-" + DateTime.Now.ToString("yyyy-MM-dd") + ".pdf";




            paginahtml = paginahtml.Replace("@NroFactura", fac.NumFactura.ToString());
            paginahtml = paginahtml.Replace("@Fecha", fac.Fecha.ToString("yyyy-MM-dd HH:mm"));

            paginahtml = paginahtml.Replace("@DNI", fac.clienteFactura.DniCliente.ToString());
            paginahtml = paginahtml.Replace("@Nombre", fac.clienteFactura.Nombre);
            paginahtml = paginahtml.Replace("@Apellido", fac.clienteFactura.Apellido);
            paginahtml = paginahtml.Replace("@Email", fac.clienteFactura.Mail);
            paginahtml = paginahtml.Replace("@Direccion", fac.clienteFactura.Direccion);


            string filas = "";
            double subtotalfactura = 0;
            foreach (BEItemFactura item in fac.obtenerListaItems())
            {
                BEProducto prod = item.producto;
                int cantidad = item.cantidad;
                double subtotal = cantidad * prod.Precio;
                subtotalfactura += subtotal;

                filas += "<tr>";
                filas += "<td>" + prod.CodigoProducto.ToString() + "</td>";
                filas += "<td>" + prod.Modelo + "</td>";
                filas += "<td>" + prod.Precio.ToString("#,0.00", new System.Globalization.CultureInfo("es-ES")) + "</td>";
                filas += "<td>" + cantidad.ToString() + "</td>";
                filas += "<td>" + subtotal.ToString("#,0.00", new System.Globalization.CultureInfo("es-ES")) + "</td>";
                filas += "</tr>";
            }

            paginahtml = paginahtml.Replace("@FILAS", filas);
            paginahtml = paginahtml.Replace("@Neto", subtotalfactura.ToString("#,0.00", new System.Globalization.CultureInfo("es-ES")));
            paginahtml = paginahtml.Replace("@Iva", fac.Impuesto.ToString("#,0.00", new System.Globalization.CultureInfo("es-ES")));
            paginahtml = paginahtml.Replace("@Total", fac.MontoTotal.ToString("#,0.00", new System.Globalization.CultureInfo("es-ES")));
            paginahtml = paginahtml.Replace("@Metodo", IdiomaManager.GetInstance().ConseguirTexto(fac.cobro.stringMetodoPago));

            paginahtml = paginahtml.Replace("@CantCuotas", fac.cobro.CantCuotas.ToString());
            if (fac.cobro.MarcaTarjeta != null)
            {
                paginahtml = paginahtml.Replace("@Marca", fac.cobro.MarcaTarjeta.ToString());
            }
            else { paginahtml = paginahtml.Replace("@Marca", "");}


            if (fac.cobro.AliasMP != null)
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


            GuardarArchivoPDF(guardarArchivo, paginahtml, Logo);
        }


        public static void GenerarReporteEventos(List<Evento> lista, string paginahtml, Bitmap Logo)
        {
            string filas = "";
            foreach (Evento ev in lista)
            {
                filas += "<tr>";
                filas += "<td>" + ev.CodEvento + "</td>";
                filas += "<td>" + ev.NombreUsuario + "</td>";
                filas += "<td>" + ev.Modulo + "</td>";
                filas += "<td>" + ev.evento + "</td>";
                filas += "<td>" + ev.Criticidad + "</td>";
                filas += "<td>" + ev.Fecha + "</td>";
                filas += "<td>" + ev.Hora + "</td>";
                filas += "</tr>";
            }

            SaveFileDialog guardarArchivo = new SaveFileDialog();

            guardarArchivo.Filter = "PDF Files (*.pdf)|*.pdf";
            guardarArchivo.FileName = "Eventos-" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".pdf";



            paginahtml = paginahtml.Replace("@FILAS", filas);
            paginahtml = paginahtml.Replace("@auditoriaFecha", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));



            paginahtml = paginahtml.Replace("@label2", IdiomaManager.GetInstance().ConseguirTexto("label2"));
            paginahtml = paginahtml.Replace("@textoFecha", IdiomaManager.GetInstance().ConseguirTexto("textoFecha"));
            paginahtml = paginahtml.Replace("@textoDetalleEvento", IdiomaManager.GetInstance().ConseguirTexto("textoDetalleEvento"));
            paginahtml = paginahtml.Replace("@textoIdEvento", IdiomaManager.GetInstance().ConseguirTexto("textoIdEvento"));
            paginahtml = paginahtml.Replace("@textoNombreUsuario", IdiomaManager.GetInstance().ConseguirTexto("textoNombreUsuario"));
            paginahtml = paginahtml.Replace("@textoModulo", IdiomaManager.GetInstance().ConseguirTexto("textoModulo"));
            paginahtml = paginahtml.Replace("@textoEvento", IdiomaManager.GetInstance().ConseguirTexto("textoEvento"));
            paginahtml = paginahtml.Replace("@textoCriticidad", IdiomaManager.GetInstance().ConseguirTexto("textoCriticidad"));
            paginahtml = paginahtml.Replace("@textoFecha", IdiomaManager.GetInstance().ConseguirTexto("textoFecha"));
            paginahtml = paginahtml.Replace("@textoHora", IdiomaManager.GetInstance().ConseguirTexto("textoHora"));



            GuardarArchivoPDF(guardarArchivo, paginahtml, Logo);
        }



        public static void GenerarReporteInteligente(DataGridView reporteTabla , string paginahtml, Bitmap Logo, string tipoReporte)
        {
            SaveFileDialog guardarArchivo = new SaveFileDialog();

            guardarArchivo.Filter = "PDF Files (*.pdf)|*.pdf";
            guardarArchivo.FileName = tipoReporte + " " + DateTime.Now.ToString("yyyy-MM-dd HH_mm") + ".pdf";

            paginahtml = paginahtml.Replace("@textoFecha", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            paginahtml = paginahtml.Replace("@textoDetalleReporte", tipoReporte);

            string columnasDinamicas = "";
            foreach (DataGridViewColumn column in reporteTabla.Columns)
            {
                columnasDinamicas += "<th>" + column.HeaderText + "</th>";
            }

            paginahtml = paginahtml.Replace("@COLUMNASDINAMICAS", columnasDinamicas);

            string filasDinamicas = "";
            foreach (DataGridViewRow row in reporteTabla.Rows)
            {
                filasDinamicas += "<tr>";
                for (int i = 0; i < reporteTabla.Columns.Count; i++) 
                {
                    filasDinamicas += "<td>" + row.Cells[i].Value.ToString() + "</td>";
                }

                filasDinamicas += "</tr>";
            }

            paginahtml = paginahtml.Replace("@FILASDINAMICAS", filasDinamicas);

            GuardarArchivoPDF(guardarArchivo ,paginahtml, Logo);
        }


        private static void GuardarArchivoPDF(SaveFileDialog guardarArchivo, string paginahtml, Bitmap Logo)
        {
            if (guardarArchivo.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(guardarArchivo.FileName, FileMode.Create))
                {
                    Document pdf = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter escritor = PdfWriter.GetInstance(pdf, stream);

                    pdf.Open();
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Logo, System.Drawing.Imaging.ImageFormat.Png);
                    img.ScaleToFit(80, 60);
                    img.Alignment = iTextSharp.text.Image.UNDERLYING;
                    img.SetAbsolutePosition(pdf.Right - 60, pdf.Top - 60);
                    pdf.Add(img);

                    using (StringReader lector = new StringReader(paginahtml))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(escritor, pdf, lector);
                    }

                    pdf.Close();
                    stream.Close();
                }
            }
        }


    }
}
