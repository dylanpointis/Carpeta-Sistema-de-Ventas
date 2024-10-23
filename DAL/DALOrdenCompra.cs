using BE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
                new SqlParameter("@Cantidad", item.CantidadSolicitada),
                new SqlParameter("@PrecioCompra", item.PrecioCompra),
            };
            dalCon.EjecutarProcAlmacenado("RegistrarItemOrden", parametros);
        }

        public DataTable TraerListaOrdenes()
        {
            DataTable tabla = dalCon.TraerTabla("OrdenesCompra");
            return tabla;
        }

        public BEProveedor TraerProveedorOrden(int numOrden)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@NumeroOrdenCompra", numOrden)
            };
            DataTable tabla = dalCon.ConsultaProcAlmacenado("TraerProveedorOrden", parametros);


            BEProveedor proveedor = null;
            foreach (DataRow row in tabla.Rows)
            {
                    proveedor = new BEProveedor(
                    row[11].ToString(), //cuit
                    row[12].ToString(),  //nombre
                    row[13].ToString(),  //razonSocial
                    row[14].ToString(),  //email
                    row[15].ToString(),  //numTelefono
                    row[16].ToString(),  //cBU
                    row[17].ToString(),  //direccion
                    row[18].ToString()   //banco
                );
                break;
            }
            return proveedor;
        }

        public DataTable TraerProductosOrden(int numOrden)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@NumeroOrdenCompra", numOrden)
            };
            DataTable tabla = dalCon.ConsultaProcAlmacenado("TraerProductosOrden", parametros);
            return tabla;
        }

        public void MarcarOrdenComoEntregada(BEOrdenCompra ordenC)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@NumeroOrdenCompra", ordenC.NumeroOrdenCompra)
            };
            dalCon.EjecutarProcAlmacenado("MarcarOrdenComoEntregada", parametros);


            foreach(BEItemOrdenCompra item in ordenC.itemsOrdenCompra)
            {
                parametros = new SqlParameter[]
                {
                    new SqlParameter("@NumeroOrdenCompra", ordenC.NumeroOrdenCompra),
                    new SqlParameter("@CodigoProducto", item.Producto.CodigoProducto),
                    new SqlParameter("@CantidadRecibida", item.CantidadRecibida)
                };
                dalCon.EjecutarProcAlmacenado("ModificarCantidadRecibidaItem", parametros);
            }

        }
    }
}
