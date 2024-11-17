using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;

namespace DAL
{
    public class DALDigitoVerificador
    {
        DALConexion dalCon = new DALConexion();
        public void PersistirDV(DV_Object dvObj)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@NombreTabla", dvObj.NombreTabla),
                new SqlParameter("@DVH", dvObj.DVH),
                new SqlParameter("@DVV", dvObj.DVV)
            };
            dalCon.EjecutarProcAlmacenado("PersistirDV", parametros);
        }

        public DataTable TraerTablaDV()
        {
            return dalCon.TraerTabla("DigitoVerificador");
        }

        public DataTable TraerTablaAConsultarDV(string tablaAConsultarDV)
        {
            return dalCon.TraerTabla(tablaAConsultarDV);
        }
    }
}
