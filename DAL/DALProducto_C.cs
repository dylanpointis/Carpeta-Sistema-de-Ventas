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
    public class DALProducto_C
    {
        DALConexion dalCon = new DALConexion();

        public DataTable FiltrarCambios(string codProd, string modeloProd, string fechaInicial, string fechaFinal)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CodProd", codProd),
                new SqlParameter("@ModeloProd", modeloProd),
                new SqlParameter("@FechaInicio", fechaInicial),
                new SqlParameter("@FechaFin", fechaFinal),
            };
            return dalCon.ConsultaProcAlmacenado("FiltrarCambios", parametros);
        }

        public DataTable TraerListaCambios()
        {
            SqlParameter[] parametros = new SqlParameter[] { };
            return dalCon.ConsultaProcAlmacenado("TraerCambiosUltimoMes", parametros);
            
        }
    }
}
