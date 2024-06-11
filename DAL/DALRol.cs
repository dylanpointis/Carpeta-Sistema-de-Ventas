using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALRol
    {
        DALConexion dalCon = new DALConexion();
        public DataTable TraerListaRoles()
        {
            DataTable tabla = dalCon.TraerTabla("Roles");
            return tabla;
        }


    }
}
