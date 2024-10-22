using BE;
using BLL;
using Microsoft.VisualBasic;
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

namespace Carpeta_Sistema_de_Ventas
{
    public partial class COMPRAfrmGenerarOrdenCompra : Form, IObserver
    {
        public COMPRAfrmGenerarOrdenCompra()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().archivoActual = "frmGenerarOrdenCompra";
            IdiomaManager.GetInstance().Agregar(this);
        }
        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }



        BLLSolicitudCotizacion bllSolC = new BLLSolicitudCotizacion();
        BEOrdenCompra ordenC = new BEOrdenCompra("",0,0,"Pendiente", DateTime.Today, DateTime.Today, 0);
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
                    cmbProveedorFinal.Items.Add(proveedor.CUIT);
                }

                //traigo los productos de la solicitud
                List<BEItemSolicitud> listaItems = bllSolC.TraerItemsSolicitud(numSol);
                grillaItems.Rows.Clear();
                ordenC.itemsOrdenCompra.Clear();

                foreach (var item in listaItems)
                {
                    ordenC.itemsOrdenCompra.Add(new BEItemOrdenCompra(item.Producto, item.Cantidad,0));
                    grillaItems.Rows.Add(item.Producto.CodigoProducto, item.Producto.Modelo, item.Producto.Stock, item.Producto.StockMin, item.Producto.StockMax, item.Cantidad, 0);
                }
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (grillaItems.SelectedRows.Count > 0) 
            {
                CargarPrecioCompra();

            }
        }
        private void CargarPrecioCompra()
        {
            string codProd = grillaItems.CurrentRow.Cells[0].Value.ToString();
            string precioCompra = Interaction.InputBox(IdiomaManager.GetInstance().ConseguirTexto("ingresePrecio"));
            if (Regex.IsMatch(precioCompra.ToString(), @"^\d{1,5}(\.\d+)?$") && (Convert.ToDouble(precioCompra) > 0))  //COMPRUEBA CON REGEX QUE LA CANT INGRESADA ES UN NUMERO MENOR A 5 CIFRAS
            {
                //obtengo el item
                BEItemOrdenCompra item = ordenC.itemsOrdenCompra.FirstOrDefault(i => i.Producto.CodigoProducto == Convert.ToInt32(codProd));

                //modifica la cantidad a reponer
                ordenC.modificarPrecioItem(Convert.ToInt64(codProd), Convert.ToDouble(precioCompra));

                grillaItems.CurrentRow.Cells[6].Value = precioCompra;
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("numEntero")); }

        }



        private void btnModificarCant_Click(object sender, EventArgs e)
        {
            CargarCantidadItem();
        }
        private void CargarCantidadItem()
        {
            string codProd = grillaItems.CurrentRow.Cells[0].Value.ToString();
            string cantIngresada = Interaction.InputBox(IdiomaManager.GetInstance().ConseguirTexto("ingreseCant"));
            if (Regex.IsMatch(cantIngresada.ToString(), @"^\d{1,3}$") && (Convert.ToInt16(cantIngresada) > 0))  //COMPRUEBA CON REGEX QUE LA CANT INGRESADA ES UN NUMERO MENOR A 3 CIFRAS
            {
                //obtengo el item
                BEItemOrdenCompra item = ordenC.itemsOrdenCompra.FirstOrDefault(i => i.Producto.CodigoProducto == Convert.ToInt32(codProd));
                //me fijo si el stock actual + agregado  supera el stock Maximo del producto
                if ((Convert.ToInt16(cantIngresada)) + item.Producto.Stock > item.Producto.StockMax)
                {
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("superaStock"));
                }
                else
                {
                    //modifica la cantidad a reponer
                    ordenC.modificarCantidadItem(Convert.ToInt64(codProd), Convert.ToInt16(cantIngresada));

                    grillaItems.CurrentRow.Cells[5].Value = cantIngresada;
                }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("numEntero")); }
        }
    }
}
