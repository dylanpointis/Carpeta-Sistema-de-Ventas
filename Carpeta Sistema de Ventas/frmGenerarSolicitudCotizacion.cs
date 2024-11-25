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
    public partial class frmGenerarSolicitudCotizacion : Form, IObserver
    {
        public frmGenerarSolicitudCotizacion()
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
        List<BEProveedor> listaProv = new List<BEProveedor>();

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
            grillaProdBajoStock.Columns[0].Width = 60; grillaProdBajoStock.Columns[2].Width = 53;  grillaProdBajoStock.Columns[3].Width = 60;
            grillaProdBajoStock.Columns[4].Width = 60;
            grillaProdBajoStock.Columns[5].Width = 60;


            grillaProveedores.ColumnCount = 6;
            grillaProveedores.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCUIT");
            grillaProveedores.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewNombre");
            grillaProveedores.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewRazonSocial");
            grillaProveedores.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewEmail");
            grillaProveedores.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewNumTelefono");
            grillaProveedores.Columns[5].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewDireccion");


            listaProv = bllProv.TraerListaProveedores().Where(p => p.BorradoLogico == true).ToList();
            ActualizarGrilla();
            ActualizarGrillaProveedores();

        }

        private void ActualizarGrillaProveedores()
        {
            grillaProveedores.Rows.Clear();
            foreach (var prov in listaProv)
            {
                grillaProveedores.Rows.Add(prov.CUIT, prov.Nombre, prov.RazonSocial, prov.Email, prov.NumTelefono, prov.Direccion);
            }
        }

        private void ActualizarGrilla()
        {
            List<BEProducto> listaProdBajoStock = bllProd.TraerProductosBajoStock();
            foreach (BEProducto item in listaProdBajoStock)
            {
                grillaProdBajoStock.Rows.Add(item.CodigoProducto,item.Modelo, item.Marca, item.Stock, item.StockMin, item.StockMax);
                solicitudCoti.AgregarItem(item,0);
            }
        }


        private void btnSeleccionarProveedor_Click(object sender, EventArgs e)
        {
            if(grillaProveedores.SelectedRows.Count > 0)
            {
                BEProveedor prov = new BEProveedor(grillaProveedores.CurrentRow.Cells[0].Value.ToString(), grillaProveedores.CurrentRow.Cells[1].Value.ToString(),
                grillaProveedores.CurrentRow.Cells[2].Value.ToString(), grillaProveedores.CurrentRow.Cells[3].Value.ToString(),
                grillaProveedores.CurrentRow.Cells[4].Value.ToString(), grillaProveedores.CurrentRow.Cells[5].Value.ToString(),"", "");
                prov.BorradoLogico =true;

                solicitudCoti.AgregarProveedor(prov);
                cmbProveedoresSeleccionados.Items.Clear();
                foreach(BEProveedor p in solicitudCoti.obtenerProveedoresSolicitud())
                {
                    cmbProveedoresSeleccionados.Items.Add(p.Nombre);
                    cmbProveedoresSeleccionados.Text = p.Nombre;
                }
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado = MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("deseaFinalizar"), IdiomaManager.GetInstance().ConseguirTexto("btnFinalizar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    int idSolicitud = bLLSolicitudCotizacion.RegistrarSolicitudCotizacion(solicitudCoti);
                    solicitudCoti.NumSolicitud = idSolicitud;

                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //imprime el reporte uno para cada proveedor de la solicitud
                    foreach (BEProveedor prov in solicitudCoti.obtenerProveedoresSolicitud())
                    {
                        Reportes.GenerarReporteSolicitud(solicitudCoti, Properties.Resources.htmlsolicitudcotizacion.ToString(), Properties.Resources.logo, prov);
                    }


                    btnFinalizar.Enabled = false;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void btnRegistrarProveedor_Click(object sender, EventArgs e)
        {
            frmRegistrarProveedor form = new frmRegistrarProveedor(true, null);
            form.ShowDialog();

            //vuelve a cargar el idioma
            IdiomaManager.GetInstance().archivoActual = "frmGenerarSolicitudCotizacion";
            IdiomaManager.GetInstance().Agregar(this);
            listaProv = bllProv.TraerListaProveedores();
            ActualizarGrillaProveedores();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (grillaProdBajoStock.SelectedRows.Count > 0)
            {
                if (solicitudCoti.obtenerItems().Count() > 1) //No puede eliminar el ultimo elemento que haya. No puede haber 0 items
                {
                    long codProd = Convert.ToInt64(grillaProdBajoStock.CurrentRow.Cells[0].Value);
                    solicitudCoti.QuitarItem(codProd);

                    foreach (DataGridViewRow row in grillaProdBajoStock.Rows)
                    {
                        if (Convert.ToInt64(row.Cells[0].Value) == codProd)
                        {
                            grillaProdBajoStock.Rows.Remove(row);
                        }
                    }
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("minimo1Producto"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);}
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
            if (this.ClientSize.Width > 1300)
            {
                grillaProdBajoStock.Width = 787;
                grillaProdBajoStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                grillaProdBajoStock.Width = 487;
                grillaProdBajoStock.Height = 256;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listaProv = bllProv.TraerListaProveedores();
            List<BEProveedor> listaConsulta = new List<BEProveedor>();
          
            foreach(BEProveedor prov in listaProv)
            {
                if (prov.Nombre.ToLower().Contains(txtProveedor.Text.ToLower()))
                {
                    listaConsulta.Add(prov);
                }
                if (prov.CUIT == txtProveedor.Text.ToLower())
                {
                    listaConsulta.Add(prov);
                }
            }

            if (listaConsulta.Count > 0) 
            {
                listaProv = listaConsulta;
                ActualizarGrillaProveedores();
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noSeEncontraronCoincidencias")); }
            
        }
    }
}
