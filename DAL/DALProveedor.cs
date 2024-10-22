using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALProveedor
    {
        private DALConexion dalCon = new DALConexion();

        public DataTable TraerListaProveedores()
        {
            DataTable tabla = dalCon.TraerTabla("Proveedores");
            return tabla;
        }

    }
}
