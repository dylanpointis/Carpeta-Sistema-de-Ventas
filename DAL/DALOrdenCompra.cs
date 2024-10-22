using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALOrdenCompra
    {
        private DALConexion dalCon = new DALConexion();


        public int RegistrarOrdenCompra(BEOrdenCompra ordenCompra)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CUITProveedor", ordenCompra.proveedor.CUIT),
                new SqlParameter("@NumeroSolicitud", ordenCompra.NumeroSolicitudCompra),
                new SqlParameter("@FechaRegistro", ordenCompra.FechaRegistro.ToString("dd/MM/yyyy HH:mm")),
                new SqlParameter("@FechaEntrega", ordenCompra.FechaEntrega.ToString("dd/MM/yyyy HH:mm")),
                new SqlParameter("@Estado", ordenCompra.Estado),
                new SqlParameter("@NumeroTransferencia", ordenCompra.NumeroTransferencia),
                new SqlParameter("@MetodoPago", ordenCompra.MetodoPago),
                new SqlParameter("@MontoTotal", ordenCompra.MontoTotal),
                new SqlParameter("@CantidadTotal", ordenCompra.CantidadTotal),
                new SqlParameter("@NumeroFactura", ordenCompra.NumeroFactura)
            };

            int id = dalCon.EjecutarYTraerId("RegistrarOrdenCompra", parametros);
            return id;
        }


        public void RegistrarItemOrden(int numeroOrdenC, BEItemOrdenCompra item)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@NumeroOrdenCompra", numeroOrdenC),
                new SqlParameter("@CodigoProducto", item.Producto.CodigoProducto),
                new SqlParameter("@Cantidad", item.Cantidad),
                new SqlParameter("@PrecioCompra", item.PrecioCompra),
            };
            dalCon.EjecutarProcAlmacenado("RegistrarItemOrden", parametros);
        }
    }
}
