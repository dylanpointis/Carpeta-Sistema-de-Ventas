using BE;
using BLL;
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
    public partial class COMPRAfrmGenerarSolicitudCotizacion : Form
    {
        public COMPRAfrmGenerarSolicitudCotizacion()
        {
            InitializeComponent();
        }

        BLLProducto bllProd = new BLLProducto();
        BLLProveedor bllProv = new BLLProveedor();
        BESolicitudCotizacion solicitudCoti = new BESolicitudCotizacion("Pendiente", DateTime.Now);

        private void COMPRAfrmGenerarSolicitudCotizacion_Load(object sender, EventArgs e)
        {
            List<BEProducto> listaProd = bllProd.TraerListaProductos().Where(p => (p.Stock - p.StockMin) <= 10).ToList();
            grillaProdBajoStock.DataSource = listaProd;

            grillaProveedores.DataSource = bllProv.TraerListaProveedores();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void btnSeleccionarProveedor_Click(object sender, EventArgs e)
        {
            BEProveedor prov = new BEProveedor(grillaProveedores.CurrentRow.Cells[0].Value.ToString(), grillaProveedores.CurrentRow.Cells[1].Value.ToString(), 
                grillaProveedores.CurrentRow.Cells[2].Value.ToString(), grillaProveedores.CurrentRow.Cells[3].Value.ToString(), grillaProveedores.CurrentRow.Cells[4].Value.ToString(),
                grillaProveedores.CurrentRow.Cells[5].Value.ToString(), grillaProveedores.CurrentRow.Cells[6].Value.ToString());


            solicitudCoti.Proveedor = prov;



        }
    }
}
