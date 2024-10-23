using BE;
using BLL;
using Microsoft.VisualBasic;
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

namespace Carpeta_Sistema_de_Ventas
{
    public partial class COMPRAfrmGenerarSolicitudCotizacion : Form, IObserver
    {
        public COMPRAfrmGenerarSolicitudCotizacion()
        {
            InitializeComponent();

            IdiomaManager.GetInstance().archivoActual = "frmGenerarSolicitudCotizacion";
            IdiomaManager.GetInstance().Agregar(this);
        }
        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        BLLEvento bllEv = new BLLEvento();
        BLLProducto bllProd = new BLLProducto();
        BLLProveedor bllProv = new BLLProveedor();
        BLLSolicitudCotizacion bLLSolicitudCotizacion = new BLLSolicitudCotizacion();

        BESolicitudCotizacion solicitudCoti = new BESolicitudCotizacion("Pendiente", DateTime.Now);

        private void COMPRAfrmGenerarSolicitudCotizacion_Load(object sender, EventArgs e)
        {
            grillaProdBajoStock.ColumnCount = 7;
            grillaProdBajoStock.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCodigo");
            grillaProdBajoStock.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewModelo");
            grillaProdBajoStock.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewMarca");
            grillaProdBajoStock.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewStock");
            grillaProdBajoStock.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewSMin");
            grillaProdBajoStock.Columns[5].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewSMax");
            grillaProdBajoStock.Columns[6].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCantidadAReponer");



            grillaProveedores.ColumnCount = 6;
            grillaProveedores.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCUIT");
            grillaProveedores.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewNombre");
            grillaProveedores.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewRazonSocial");
            grillaProveedores.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewEmail");
            grillaProveedores.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewNumTelefono");
            grillaProveedores.Columns[5].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewDireccion");

            ActualizarGrilla();

        }

        private void ActualizarGrilla()
        {
            List<BEProducto> listaProd = bllProd.TraerListaProductos().Where(p => (p.Stock - p.StockMin) <= 10).ToList();

            foreach (BEProducto item in listaProd)
            {
                grillaProdBajoStock.Rows.Add(item.CodigoProducto,item.Modelo, item.Marca, item.Stock, item.StockMin, item.StockMax);
                solicitudCoti.AgregarItem(item,0);
            }


            List<BEProveedor> listaProv = bllProv.TraerListaProveedores();
            foreach (var prov in listaProv) 
            {
                grillaProveedores.Rows.Add(prov.CUIT, prov.Nombre, prov.RazonSocial, prov.Email, prov.NumTelefono, prov.Direccion);
            }

        }


        private void btnSeleccionarProveedor_Click(object sender, EventArgs e)
        {
            if(grillaProveedores.SelectedRows.Count > 0)
            {
                BEProveedor prov = new BEProveedor(grillaProveedores.CurrentRow.Cells[0].Value.ToString(), grillaProveedores.CurrentRow.Cells[1].Value.ToString(),
                grillaProveedores.CurrentRow.Cells[2].Value.ToString(), grillaProveedores.CurrentRow.Cells[3].Value.ToString(),
                grillaProveedores.CurrentRow.Cells[4].Value.ToString(), "", grillaProveedores.CurrentRow.Cells[5].Value.ToString().ToLower(), "");


                solicitudCoti.AgregarProveedor(prov);
                cmbProveedoresSeleccionados.Items.Clear();
                foreach(BEProveedor p in solicitudCoti.obtenerProveedorSolicitud())
                {
                    cmbProveedoresSeleccionados.Items.Add(p.Nombre);
                    cmbProveedoresSeleccionados.Text = p.Nombre;
                }
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (solicitudCoti.obtenerItems().TrueForAll(i => i.Cantidad > 0))
            {
                try
                {
                    int idSolicitud = bLLSolicitudCotizacion.RegistrarSolicitudCotizacion(solicitudCoti);
                    solicitudCoti.NumSolicitud = idSolicitud;

                    foreach (BEItemSolicitud item in solicitudCoti.obtenerItems())
                    {
                        bLLSolicitudCotizacion.RegistrarItemSolicitud(item, solicitudCoti.NumSolicitud);
                    }

                    foreach (BEProveedor prov in solicitudCoti.obtenerProveedorSolicitud())
                    {
                        bLLSolicitudCotizacion.RegistrarProveedorSolicitud(prov, solicitudCoti.NumSolicitud);
                    }
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bllEv.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Compras", "Solicitud de cotización generada", 5, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));
                }
                catch (Exception ex) { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("error") + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("ingreseCantidades")); }
        }

        private void btnRegistrarProveedor_Click(object sender, EventArgs e)
        {
            COMPRAfrmRegistrarProveedor form = new COMPRAfrmRegistrarProveedor();
            form.ShowDialog();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (grillaProdBajoStock.SelectedRows.Count > 0)
            {
                long codProd = Convert.ToInt64(grillaProdBajoStock.CurrentRow.Cells[0].Value);
                solicitudCoti.QuitarItem(codProd);

                foreach(DataGridViewRow row in grillaProdBajoStock.Rows)
                {
                    if (Convert.ToInt64(row.Cells[0].Value) == codProd)
                    {
                        grillaProdBajoStock.Rows.Remove(row);
                    }
                }
            }
        }

        private void btnCargarCant_Click(object sender, EventArgs e)
        {
            if (grillaProdBajoStock.SelectedRows.Count > 0)
            {
                string codProd = grillaProdBajoStock.CurrentRow.Cells[0].Value.ToString();
                string cantIngresada = Interaction.InputBox(IdiomaManager.GetInstance().ConseguirTexto("ingreseCant"));
                if (Regex.IsMatch(cantIngresada.ToString(), @"^\d{1,3}$") && (Convert.ToInt16(cantIngresada) > 0))  //COMPRUEBA CON REGEX QUE LA CANT INGRESADA ES UN NUMERO MENOR A 3 CIFRAS
                {
                    //obtengo el item
                    BEItemSolicitud item = solicitudCoti.obtenerItems().FirstOrDefault(p => p.Producto.CodigoProducto == Convert.ToInt32(codProd));
                    //me fijo si el stock actual + agregado supera el stock Maximo del producto
                    if ((Convert.ToInt16(cantIngresada) + item.Producto.Stock) > item.Producto.StockMax)
                    {
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("superaStock"));
                    }
                    else
                    {
                        //modifica la cantidad a reponer
                        solicitudCoti.modificarCantidadItem(Convert.ToInt64(codProd), Convert.ToInt16(cantIngresada));

                        grillaProdBajoStock.CurrentRow.Cells[6].Value = cantIngresada;
                    }
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("numEntero")); }
            }
        }




        //esto es para que se vea bien cuando agrando o achico la pantalla

        private void COMPRAfrmGenerarSolicitudCotizacion_Resize(object sender, EventArgs e)
        {
            if (this.ClientSize.Width > 1500)
            {
                grillaProdBajoStock.Width = 787;
                grillaProdBajoStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                grillaProveedores.Width = 648;
            }
            else
            {

                grillaProdBajoStock.Width = 487;
                grillaProdBajoStock.Height = 256;
                grillaProveedores.Width = 448;
                grillaProveedores.Height = 244;
            }
        }
    }
}
