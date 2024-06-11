using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALPermiso
    {
        DALConexion dalCon = new DALConexion();
        public DataTable TraerListaPermisos()
        {
            DataTable tabla = dalCon.TraerTabla("Permisos");
            return tabla;
        }
    }
}
