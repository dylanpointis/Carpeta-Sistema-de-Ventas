using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALCambio
    {
        DALConexion dalCon = new DALConexion();
        public DataTable TraerListaCambios()
        {
            SqlParameter[] parametros = new SqlParameter[]
                { };
            return dalCon.ConsultaProcAlmacenado("TraerCambiosUltimoMes", parametros);
            
        }
    }
}
