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
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmReporteCompras : Form, IObserver
    {
        public frmReporteCompras()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().archivoActual = "frmReporteCompras";
            IdiomaManager.GetInstance().Agregar(this);
        }
        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        BLLOrdenCompra bllOrdenC = new BLLOrdenCompra();
        List<BEOrdenCompra> listaOrdenesCompra = new List<BEOrdenCompra>();
        BEOrdenCompra ordenSeleccionada;
        private void frmReporteCompras_Load(object sender, EventArgs e)
        {
            grillaOrdenes.ColumnCount = 10;
            grillaOrdenes.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewNumOrden");
            grillaOrdenes.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewNumFactura");
            grillaOrdenes.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCUIT");
            grillaOrdenes.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewFechaRegistro");
            grillaOrdenes.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewFechaEntrega");
            grillaOrdenes.Columns[5].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewEstado");
            grillaOrdenes.Columns[6].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewNumTransferencia");
            grillaOrdenes.Columns[7].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewMontoTotal");
            grillaOrdenes.Columns[8].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCantTotal");
            grillaOrdenes.Columns[9].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewNumSolicitud");
            grillaOrdenes.Columns[1].Width = 60;

            grillaItemsRecibidos.ColumnCount = 6;
            grillaItemsRecibidos.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCodigo");
            grillaItemsRecibidos.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewModelo");
            grillaItemsRecibidos.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCantidadSolicitada");
            grillaItemsRecibidos.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCantidadRecibida");
            grillaItemsRecibidos.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewFacturaRecepcion");
            grillaItemsRecibidos.Columns[5].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewFechaRecepcion");
            grillaItemsRecibidos.RowHeadersVisible = false;

            listaOrdenesCompra = bllOrdenC.TraerListaOrdenes().OrderByDescending(o => o.NumeroOrdenCompra).ToList();
            ActualizarGrilla();
        }

        private void ActualizarGrilla()
        {
            grillaOrdenes.Rows.Clear();
            foreach(BEOrdenCompra orden in listaOrdenesCompra)
            {
                orden.proveedor = bllOrdenC.TraerProveedorOrden(orden.NumeroOrdenCompra);
                grillaOrdenes.Rows.Add(orden.NumeroOrdenCompra, orden.NumeroFactura, orden.proveedor.CUIT, orden.FechaRegistro.ToString("yyyy-dd-MM HH:mm"), 
                    orden.FechaEntrega.ToString("yyyy-dd-MM HH:mm"), orden.Estado, orden.NumeroTransferencia, orden.MontoTotal, orden.CantidadTotal, orden.NumeroSolicitudCompra);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            listaOrdenesCompra = bllOrdenC.TraerListaOrdenes().OrderByDescending(o => o.NumeroOrdenCompra).ToList();
            ActualizarGrilla();
        }

        private void grillaOrdenes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grillaOrdenes.SelectedRows.Count > 0)
            {
                ordenSeleccionada = listaOrdenesCompra.FirstOrDefault(o => o.NumeroOrdenCompra == Convert.ToInt32(grillaOrdenes.CurrentRow.Cells[0].Value));
                BuscarItemsOrden(ordenSeleccionada.NumeroOrdenCompra);
            }
        }

        private void BuscarItemsOrden(int numOrden)
        {
            List<BEItemOrdenCompra> lista = bllOrdenC.TraerProductosOrden(numOrden);
            foreach (BEItemOrdenCompra item in lista)
            {
                ordenSeleccionada.AgregarItem(item.Producto, item.CantidadSolicitada, item.CantidadRecibida, item.NumFacturaRecepcion, item.FechaRecepcion);
            }

            grillaItemsRecibidos.Rows.Clear();
            foreach (BEItemOrdenCompra item in ordenSeleccionada.obtenerItems())
            {
                grillaItemsRecibidos.Rows.Add(item.Producto.CodigoProducto, item.Producto.Modelo, item.CantidadSolicitada, item.CantidadRecibida, item.NumFacturaRecepcion, item.FechaRecepcion);
            
            }
        }

        private void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            if (grillaOrdenes.SelectedRows.Count > 0)
            {
                Reportes.GenerarReporteRecepcion(ordenSeleccionada, Properties.Resources.htmlfacturaRecepcion, Properties.Resources.logopng);
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccione"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int numfac = txtNumFactura.Text != "" ? Convert.ToInt32(txtNumFactura.Text) : 0;
            int numord = txtNumOrden.Text != "" ? Convert.ToInt32(txtNumOrden.Text) : 0;
            int numtran = txtNumTransferencia.Text != "" ? Convert.ToInt32(txtNumTransferencia.Text) : 0;

            listaOrdenesCompra = listaOrdenesCompra.Where(o => (o.NumeroOrdenCompra == numord) || (o.NumeroFactura == numfac) || (o.NumeroTransferencia == numtran)).OrderByDescending(o => o.NumeroOrdenCompra).ToList();
            ActualizarGrilla();
        }

        #region EventosForm
        private void frmReporteCompras_Resize(object sender, EventArgs e)
        {
            if (this.ClientSize.Width > 1500)
            {
                grillaOrdenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else { grillaOrdenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; }
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
        private void txtNumFactura_KeyPress(object sender, KeyPressEventArgs e)
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


        private void txtNumTransferencia_KeyPress(object sender, KeyPressEventArgs e)
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
        #endregion
    }
}
