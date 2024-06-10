using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALProducto
    {

        DALConexion dalCon = new DALConexion();

        public void ModificarStock(BEProducto produto, int stock)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                 new SqlParameter("@CodigoProducto", produto.CodigoProducto),
                 new SqlParameter("@Stock", stock)
           };

            dalCon.EjecutarProcAlmacenado("ModificarStock", parametros);
        }

        public DataTable TraerListaProducto()
        {
            DataTable tabla = dalCon.TraerTabla("Productos");
            return tabla;
        }
    }
}
