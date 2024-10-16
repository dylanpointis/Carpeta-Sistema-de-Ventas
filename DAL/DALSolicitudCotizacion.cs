using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALSolicitudCotizacion
    {
        private DALConexion dalCon = new DALConexion();

        public void RegistrarItemSolicitud(BEItemSolicitud item, int idSolicitud)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@NumeroSolicitud",idSolicitud),
                new SqlParameter("@CodigoProducto", item.Producto.CodigoProducto),
                new SqlParameter("@Cantidad", item.Cantidad)
            };

            dalCon.EjecutarProcAlmacenado("RegistrarItemSolicitud", parametros);
        }

        public void RegistrarProveedorSolicitud(BEProveedor prov, int idSolicitud)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@NumeroSolicitud",idSolicitud),
                new SqlParameter("@CUITProveedor", prov.CUIT)
            };

            dalCon.EjecutarProcAlmacenado("RegistrarProveedorSolicitud", parametros);
        }

        public int RegistrarSolicitudCotizacion(BESolicitudCotizacion solicitudCoti)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Estado",solicitudCoti.Estado),
                new SqlParameter("@Fecha",solicitudCoti.Fecha.ToString())
            };

            int id = dalCon.EjecutarYTraerId("RegistrarSolicitudCotizacion", parametros);
            return id;
        }
    }
}
