using BE;
using BLL;
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

namespace Carpeta_Sistema_de_Ventas
{
    public partial class COMPRAfrmCorroborarRecepcion : Form, IObserver
    {
        public COMPRAfrmCorroborarRecepcion()
        {
            IdiomaManager.GetInstance().archivoActual = "frmCorroborarRecepcion";
            IdiomaManager.GetInstance().Agregar(this);
            InitializeComponent();
        }

        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        BLLOrdenCompra bllOrdenC = new BLLOrdenCompra();
        BLLProveedor bllProv = new BLLProveedor();
        BEOrdenCompra ordenC;

        private void COMPRAfrmCorroborarRecepcion_Load(object sender, EventArgs e)
        {
            grillaRecepcion.ColumnCount = 7;
            grillaRecepcion.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCodigo");
            grillaRecepcion.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewModelo");
            grillaRecepcion.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewStock");
            grillaRecepcion.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewSMin");
            grillaRecepcion.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewSMax");
            grillaRecepcion.Columns[5].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCantidadSolicitada");
            grillaRecepcion.Columns[6].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCantidadRecibida");


            
            List<BEOrdenCompra> listaOrd = bllOrdenC.TraerListaOrdenes();
            foreach (var ord in listaOrd)
            {
                if (ord.Estado == "Pendiente")
                {
                    cmbOrdenesCompra.Items.Add(+ord.NumeroOrdenCompra + "  |  " + ord.FechaEntrega);
                }
            }
        }

        private void cmbOrdenesCompra_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] partes = cmbOrdenesCompra.SelectedItem.ToString().Split(new string[] { "  |  " }, StringSplitOptions.RemoveEmptyEntries);
            int numOrden = Convert.ToInt32(partes[0].Trim());

            ordenC = new BEOrdenCompra(0,0,"Pendiente", DateTime.Now, DateTime.Now, 0); //CAMBIAR POR VALORES REALES
            ordenC.NumeroOrdenCompra = numOrden;

            //Muestra el detalle del proveedor
            BEProveedor prov = bllOrdenC.TraerProveedorOrden(numOrden);
            ordenC.proveedor = prov;

            lblNombreProv.Text = IdiomaManager.GetInstance().ConseguirTexto("lblNombreProv") + $"\n{prov.Nombre}";
            lblRazonSocial.Text = IdiomaManager.GetInstance().ConseguirTexto("lblRazonSocial") + $"\n{prov.RazonSocial}";
            lblMailProv.Text = IdiomaManager.GetInstance().ConseguirTexto("lblMailProv") + $"\n{prov.Email}";
            lblNumTel.Text = IdiomaManager.GetInstance().ConseguirTexto("lblNumTel") + $"\n{prov.NumTelefono}";
            lblCBU.Text = IdiomaManager.GetInstance().ConseguirTexto("lblCBU") + $"\n{prov.CBU}";
            lblCUIT.Text = IdiomaManager.GetInstance().ConseguirTexto("lblCUIT") + $"\n{prov.CUIT}";



            //Busca los items
            List<BEItemOrdenCompra> lista = bllOrdenC.TraerProductosOrden(numOrden);
            ordenC.itemsOrdenCompra = lista;

            label2.Text = IdiomaManager.GetInstance().ConseguirTexto("label2") + ordenC.CantidadTotal;
            ActualizarGrilla();

        }

        private void ActualizarGrilla()
        {
            grillaRecepcion.Rows.Clear();
            int cantTotalRecibida = 0;
            foreach (BEItemOrdenCompra item in ordenC.itemsOrdenCompra)
            {
                grillaRecepcion.Rows.Add(item.Producto.CodigoProducto, item.Producto.Modelo, item.Producto.Stock, item.Producto.StockMin, item.Producto.StockMax, item.CantidadSolicitada, item.CantidadRecibida);
                cantTotalRecibida += item.CantidadRecibida;
            }
            label1.Text = IdiomaManager.GetInstance().ConseguirTexto("label1") + cantTotalRecibida.ToString(); //muestra la cantidad total recibida
        }

        private void btnCargarCantidadRecibida_Click(object sender, EventArgs e)
        {
            if(grillaRecepcion.SelectedRows.Count > 0)
            {
                int cantRecibida = Convert.ToInt32(txtCantRecibida.Text);
                if (cantRecibida > 0)
                {
                    long codProd = Convert.ToInt64(grillaRecepcion.CurrentRow.Cells[0].Value);

                    BEItemOrdenCompra item = ordenC.itemsOrdenCompra.FirstOrDefault(i => i.Producto.CodigoProducto == codProd);
                    item.CantidadRecibida = cantRecibida;
                    ActualizarGrilla();
                }
            }

        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (ordenC.itemsOrdenCompra.TrueForAll(i => i.CantidadRecibida > 0))
            {
                try
                {
                    bllOrdenC.MarcarOrdenComoEntregada(ordenC);
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("error") + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
