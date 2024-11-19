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
using Microsoft.VisualBasic.Logging;

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
            grillaItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            txtFechaEntrega.CustomFormat = Application.CurrentCulture.DateTimeFormat.ShortDatePattern;
            txtFechaEntrega.MinDate = DateTime.Today;
            txtFechaEntrega.Value = DateTime.Today.AddDays(7);

            TraerSolicitudesPendientes();
        }

        private void TraerSolicitudesPendientes()
        {
            List<BESolicitudCotizacion> listaSol = bllSolC.TraerListaSolicitudes();
            cmbSolicitudesCotizacion.Items.Clear();
            foreach (var sol in listaSol)
            {
                if (sol.Estado == "Pendiente")
                {
                    cmbSolicitudesCotizacion.Items.Add(+sol.NumSolicitud + "  |  " + sol.Fecha);
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
                    ordenC.AgregarItem(item.Producto, item.Cantidad, 0, 0, "");
                    grillaItems.Rows.Add(item.Producto.CodigoProducto, item.Producto.Modelo, item.Producto.Stock, item.Producto.StockMin, item.Producto.StockMax, item.Cantidad, 0);
                }
                btnFinalizar.Enabled = true;
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
                if (Regex.IsMatch(precioCompra.ToString(), @"^\d{1,8}(\.\d+)?$") && (Convert.ToDouble(precioCompra) > 0))  //COMPRUEBA CON REGEX QUE LA CANT INGRESADA ES UN NUMERO MENOR A 8 CIFRAS
                {
                    //obtengo el item
                    BEItemOrdenCompra item = ordenC.obtenerItems().FirstOrDefault(i => i.Producto.CodigoProducto == Convert.ToInt32(codProd));

                    //modifica el precio de compra
                    ordenC.modificarPrecioItem(Convert.ToInt64(codProd), Convert.ToDouble(precioCompra));
                    string preciocompraformato = Convert.ToDouble(precioCompra).ToString("#,0.00", new System.Globalization.CultureInfo("es-ES"));
                    grillaItems.CurrentRow.Cells[6].Value = preciocompraformato;
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("numEntero")); }
            }
        }



        private void btnModificarCant_Click(object sender, EventArgs e)
        {
            if (grillaItems.SelectedRows.Count > 0)
            {
                CargarCantidadItem();
                ActualizarLabelsTotal();
            }
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
                        ordenC.modificarCantidadItem(Convert.ToInt64(codProd), Convert.ToInt16(cantIngresada), 0, "", false);

                        grillaItems.CurrentRow.Cells[5].Value = cantIngresada;
                    }
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("numEnteroCant")); }
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

            iva = (montoNeto * 21) / 100;
            lblNeto.Text = IdiomaManager.GetInstance().ConseguirTexto("lblNeto") + montoNeto.ToString("#,0.00", new System.Globalization.CultureInfo("es-ES")); ;
            lblIVA.Text = IdiomaManager.GetInstance().ConseguirTexto("lblIVA") + iva.ToString("#,0.00", new System.Globalization.CultureInfo("es-ES")); ;
            lblTotal.Text = IdiomaManager.GetInstance().ConseguirTexto("lblTotal") + (montoNeto + iva).ToString("#,0.00", new System.Globalization.CultureInfo("es-ES")); ;

            ordenC.MontoTotal = montoNeto + iva;
        }





        private void btnRegistarPago_Click(object sender, EventArgs e)
        {
            if(ordenC.proveedor != null)
            {
                if (ordenC.obtenerItems().TrueForAll(i => i.CantidadSolicitada > 0 && i.PrecioCompra > 0))
                {
                    if (ordenC.proveedor.CBU != "" && ordenC.proveedor.Banco != "")// si el prov esta registrado completamente
                    {
                        frmRegistrarPagoProveedor form = new frmRegistrarPagoProveedor(ordenC);
                        form.ShowDialog();


                        //vuelve a cargar el idioma una vez que se cierra el form dialog
                        IdiomaManager.GetInstance().archivoActual = "frmGenerarOrdenCompra";
                        IdiomaManager.GetInstance().Agregar(this);
                        ActualizarLabelsTotal();
                        MostrarDetalleProveedor();
                        btnFinalizar.Enabled = true;
                    }
                    else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("provNoRegistradoCompleto"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("carguePreciosYCant"));}//Cargue todos los precios de compra y cants
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccioneProv"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
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
            if(provFinal.CBU != "" && provFinal.Banco != "") //si ya tiene el CBU y banco no hace falta registrarlo completamente
            {
                btnRegistrarProveedor.Enabled = false;
            }
            else {  btnRegistrarProveedor.Enabled = true;}
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
            if(ordenC.NumeroTransferencia > 0 && ordenC.NumeroFactura > 0) //si ya registro el pago
            {
                try
                {
                    ordenC.FechaEntrega = txtFechaEntrega.Value;

                    //logica Registrar orden de compra
                    int numeroOrdenC = bllOrdenC.RegistrarOrdenCompra(ordenC);
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //genera el reporte pdf
                    string paginahtml = Properties.Resources.htmlfacturacompra.ToString();
                    Reportes.GenerarReporteOrden(ordenC, paginahtml, Properties.Resources.logo);
                    btnFinalizar.Enabled = false;
                    TraerSolicitudesPendientes();
                }
                catch (Exception ex) { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("error") + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("debeRegistrarPago"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);}
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

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if(grillaItems.SelectedRows.Count > 0)
            {
                if (ordenC.obtenerItems().Count() > 1) //No puede eliminar el ultimo elemento que haya. No puede haber 0 items
                {
                    long codProd = Convert.ToInt64(grillaItems.CurrentRow.Cells[0].Value);
                    ordenC.QuitarItem(codProd);
                    foreach (DataGridViewRow row in grillaItems.Rows)
                    {
                        if (Convert.ToInt64(row.Cells[0].Value) == codProd)
                        {
                            grillaItems.Rows.Remove(row);
                        }
                    }
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("minimo1Producto"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }
    }
}
