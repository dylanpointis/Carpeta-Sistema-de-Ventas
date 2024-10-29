using BE;
using BLL;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using Microsoft.VisualBasic;
using Services;
using Services.Observer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmGenerarOrdenCompra : Form, IObserver
    {
        public frmGenerarOrdenCompra()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().archivoActual = "frmGenerarOrdenCompra";
            IdiomaManager.GetInstance().Agregar(this);
        }
        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        BLLEvento bllEvento = new BLLEvento();
        BLLSolicitudCotizacion bllSolC = new BLLSolicitudCotizacion();
        BLLOrdenCompra bllOrdenC = new BLLOrdenCompra();
        BEProveedor provFinal;
        BEOrdenCompra ordenC = new BEOrdenCompra(0,0,"Pendiente", DateTime.Today, DateTime.Now, 0);
        private void COMPRAfrmGenerarOrdenCompra_Load(object sender, EventArgs e)
        {
            grillaItems.ColumnCount = 7;
            grillaItems.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCodigo");
            grillaItems.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewModelo");
            grillaItems.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewStock");
            grillaItems.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewSMin");
            grillaItems.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewSMax");
            grillaItems.Columns[5].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCantidadAReponer");
            grillaItems.Columns[6].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewPrecioUnit");

            txtFechaEntrega.CustomFormat = Application.CurrentCulture.DateTimeFormat.ShortDatePattern;
            txtFechaEntrega.Value = DateTime.Today;


            List<BESolicitudCotizacion> listaSol = bllSolC.TraerListaSolicitudes();
            foreach (var sol in listaSol)
            {
                if(sol.Estado == "Pendiente")
                {
                    cmbSolicitudesCotizacion.Items.Add(+ sol.NumSolicitud + "  |  " +sol.Fecha);
                }
            }
        }

        private void cmbSolicitudesCotizacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSolicitudesCotizacion.SelectedItem != null)
            {
                string[] partes = cmbSolicitudesCotizacion.SelectedItem.ToString().Split(new string[] { "  |  " }, StringSplitOptions.RemoveEmptyEntries);
                int numSol = int.Parse(partes[0].Trim());
                string fecha = partes[1].Trim();



                //cargo la solicitud en la variable local
                ordenC.NumeroSolicitudCompra = numSol;


                //traigo los proveedores de la solicitud
                List<BEProveedor> lista = bllSolC.TraerProveedoresSolicitud(numSol);
                cmbProveedorFinal.Items.Clear();
                cmbProveedorFinal.Text = "";
                foreach(var proveedor in lista)
                {
                    cmbProveedorFinal.Items.Add(proveedor.CUIT + "  |  " + proveedor.Nombre);
                }

                //traigo los productos de la solicitud
                List<BEItemSolicitud> listaItems = bllSolC.TraerItemsSolicitud(numSol);
                grillaItems.Rows.Clear();
                ordenC.obtenerItems().Clear();

                foreach (var item in listaItems)
                {
                    ordenC.AgregarItem(item.Producto, item.Cantidad, 0);
                    grillaItems.Rows.Add(item.Producto.CodigoProducto, item.Producto.Modelo, item.Producto.Stock, item.Producto.StockMin, item.Producto.StockMax, item.Cantidad, 0);
                }
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (grillaItems.SelectedRows.Count > 0) 
            {
                CargarPrecioCompra();
                ActualizarLabelsTotal();

            }
        }


        private void CargarPrecioCompra()
        {
            if (grillaItems.Rows.Count > 0)
            {
                string codProd = grillaItems.CurrentRow.Cells[0].Value.ToString();
                string precioCompra = Interaction.InputBox(IdiomaManager.GetInstance().ConseguirTexto("ingresePrecio"));
                if (Regex.IsMatch(precioCompra.ToString(), @"^\d{1,5}(\.\d+)?$") && (Convert.ToDouble(precioCompra) > 0))  //COMPRUEBA CON REGEX QUE LA CANT INGRESADA ES UN NUMERO MENOR A 5 CIFRAS
                {
                    //obtengo el item
                    BEItemOrdenCompra item = ordenC.obtenerItems().FirstOrDefault(i => i.Producto.CodigoProducto == Convert.ToInt32(codProd));

                    //modifica la cantidad a reponer
                    ordenC.modificarPrecioItem(Convert.ToInt64(codProd), Convert.ToDouble(precioCompra));

                    grillaItems.CurrentRow.Cells[6].Value = precioCompra;
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("numEntero")); }
            }
        }



        private void btnModificarCant_Click(object sender, EventArgs e)
        {
            CargarCantidadItem();
            ActualizarLabelsTotal();
        }
        private void CargarCantidadItem()
        {
            if (grillaItems.Rows.Count > 0)
            {
                string codProd = grillaItems.CurrentRow.Cells[0].Value.ToString();
                string cantIngresada = Interaction.InputBox(IdiomaManager.GetInstance().ConseguirTexto("ingreseCant"));
                if (Regex.IsMatch(cantIngresada.ToString(), @"^\d{1,3}$") && (Convert.ToInt16(cantIngresada) > 0))  //COMPRUEBA CON REGEX QUE LA CANT INGRESADA ES UN NUMERO MENOR A 3 CIFRAS
                {
                    //obtengo el item
                    BEItemOrdenCompra item = ordenC.obtenerItems().FirstOrDefault(i => i.Producto.CodigoProducto == Convert.ToInt32(codProd));
                    //me fijo si el stock actual + agregado  supera el stock Maximo del producto
                    if ((Convert.ToInt16(cantIngresada)) + item.Producto.Stock > item.Producto.StockMax)
                    {
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("superaStock"));
                    }
                    else
                    {
                        //modifica la cantidad a reponer
                        ordenC.modificarCantidadItem(Convert.ToInt64(codProd), Convert.ToInt16(cantIngresada), false);

                        grillaItems.CurrentRow.Cells[5].Value = cantIngresada;
                    }
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("numEntero")); }
            }
        }


        private void ActualizarLabelsTotal()
        {
            double montoNeto = 0;
            double iva = 0;

            foreach(BEItemOrdenCompra item in ordenC.obtenerItems())
            {
                //cant * precio compra
                montoNeto += item.CantidadSolicitada * item.PrecioCompra;
            }

            //foreach (DataGridViewRow row in grillaItems.Rows) 
            //{
            //    if (row.Cells[5].Value != null && row.Cells[6].Value != null)
            //    {
            //        //cant * precio compra
            //        montoNeto += Convert.ToInt32(row.Cells[5].Value) * Convert.ToDouble(row.Cells[6].Value);
            //    }

            //}
            iva = (montoNeto * 21) / 100;
            lblNeto.Text = IdiomaManager.GetInstance().ConseguirTexto("lblNeto") + montoNeto;
            lblIVA.Text = IdiomaManager.GetInstance().ConseguirTexto("lblIVA") + iva;
            lblTotal.Text = IdiomaManager.GetInstance().ConseguirTexto("lblTotal") + (montoNeto + iva);

            ordenC.MontoTotal = montoNeto + iva;
        }





        private void btnRegistarPago_Click(object sender, EventArgs e)
        {
            if(ordenC.proveedor != null)
            {
                if (ordenC.obtenerItems().TrueForAll(i => i.CantidadSolicitada > 0 && i.PrecioCompra > 0))
                {
                    if (ordenC.proveedor.CBU != "")// si el prov esta registrado completamente
                    {
                        frmRegistrarPagoProveedor form = new frmRegistrarPagoProveedor(ordenC);
                        form.ShowDialog();


                        //vuelve a cargar el idioma una vez que se cierra el form dialog
                        IdiomaManager.GetInstance().archivoActual = "frmGenerarOrdenCompra";
                        IdiomaManager.GetInstance().Agregar(this);
                        ActualizarLabelsTotal();
                        btnFinalizar.Enabled = true;
                    }
                    else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("provNoRegistradoCompleto"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("carguePreciosYCant"));}//Cargue todos los precios de compra y cants
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccioneProv")); }
        }

        private void cmbProveedorFinal_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<BEProveedor> lista = bllSolC.TraerProveedoresSolicitud(ordenC.NumeroSolicitudCompra);

            //separa por  "  |  "  el combobox
            string[] partes = cmbProveedorFinal.SelectedItem.ToString().Split(new string[] { "  |  " }, StringSplitOptions.RemoveEmptyEntries);
            string cuit = partes[0].Trim();

            //asigna el proveedor final seleccionado a la entidad ordenCompra
            provFinal = lista.Where(p => p.CUIT == cuit).FirstOrDefault();
            ordenC.proveedor = provFinal;

            //muestra el detalle del proveedor en pantalla
            MostrarDetalleProveedor();
            
        }

        private void MostrarDetalleProveedor()
        {
            lblNombreProv.Text = IdiomaManager.GetInstance().ConseguirTexto("lblNombreProv") + $"\n{provFinal.Nombre}";
            lblRazonSocial.Text = IdiomaManager.GetInstance().ConseguirTexto("lblRazonSocial") + $"\n{provFinal.RazonSocial}";
            lblMailProv.Text = IdiomaManager.GetInstance().ConseguirTexto("lblMailProv") + $"\n{provFinal.Email}";
            lblNumTel.Text = IdiomaManager.GetInstance().ConseguirTexto("lblNumTel") + $"\n{provFinal.NumTelefono}";
            lblCBU.Text = IdiomaManager.GetInstance().ConseguirTexto("lblCBU") + $"\n{provFinal.CBU}";
            lblCUIT.Text = IdiomaManager.GetInstance().ConseguirTexto("lblCUIT") + $"\n{provFinal.CUIT}";
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if(ordenC.NumeroFactura > 0 && ordenC.NumeroTransferencia > 0) //si ya registro el pago
            {
                
                    try
                    {
                        ordenC.FechaEntrega = txtFechaEntrega.Value;

                        int numeroOrdenC = bllOrdenC.RegistrarOrdenCompra(ordenC);

                        foreach (BEItemOrdenCompra item in ordenC.obtenerItems())
                        {
                            bllOrdenC.RegistrarItemOrden(numeroOrdenC, item);
                        }

                        bllSolC.ModificarEstadoSolicitud(ordenC.NumeroSolicitudCompra, "Cotizada"); //marca el estado de la solicitud en Cotizada

                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Compras", "Orden de compra generada", 5, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));

                        GenerarFactura();
                    }
                    catch (Exception ex) { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("error") + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }


        private void btnRegistrarProveedor_Click(object sender, EventArgs e)
        {
            if (provFinal != null)
            {
                frmRegistrarProveedor form = new frmRegistrarProveedor(false, provFinal);
                form.ShowDialog();  
                //vuelve a cargar el idioma
                IdiomaManager.GetInstance().archivoActual = "frmGenerarOrdenCompra";
                IdiomaManager.GetInstance().Agregar(this);
                MostrarDetalleProveedor();
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccioneProvParaRegistrarlo")); }
        }


        private void GenerarFactura()
        {
            SaveFileDialog guardarArchivo = new SaveFileDialog();

            guardarArchivo.Filter = "PDF Files (*.pdf)|*.pdf";
            guardarArchivo.FileName = ordenC.NumeroFactura + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".pdf";


            string paginahtml = Properties.Resources.htmlfacturacompra.ToString();


            paginahtml = paginahtml.Replace("@NroFactura", ordenC.NumeroFactura.ToString());
            paginahtml = paginahtml.Replace("@Fecha", ordenC.FechaRegistro.ToString("dd/MM/yyyy HH:mm"));
            paginahtml = paginahtml.Replace("@FechaEntrega", ordenC.FechaEntrega.ToString("dd/MM/yyyy HH:mm"));


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
                filas += "<td>" + item.PrecioCompra.ToString() + "</td>";
                filas += "<td>" + cantidad.ToString() + "</td>";
                filas += "<td>" + item.CantidadRecibida + "</td>";
                filas += "</tr>";
            }

            paginahtml = paginahtml.Replace("@FILAS", filas);
            paginahtml = paginahtml.Replace("@Neto", subtotalfactura.ToString());
            paginahtml = paginahtml.Replace("@Iva", (subtotalfactura * 0.21).ToString());
            paginahtml = paginahtml.Replace("@Total", ordenC.MontoTotal.ToString());
            paginahtml = paginahtml.Replace("@NumTransferencia", ordenC.NumeroTransferencia.ToString());

            //traducciones
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
            paginahtml = paginahtml.Replace("@textofechaEntrega", IdiomaManager.GetInstance().ConseguirTexto("textoFechaEntrega"));

            if (guardarArchivo.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(guardarArchivo.FileName, FileMode.Create))
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
                        XMLWorkerHelper.GetInstance().ParseXHtml(escritor, pdf, lector);
                    }


                    pdf.Close();
                    stream.Close();
                }
            }
        }
    }
}
