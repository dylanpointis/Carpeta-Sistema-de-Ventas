using BE;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALEvento
    {
        DALConexion dalCon = new DALConexion();

        public DataTable FiltrarEventos(string nombreusuario, string modulo, string evento, string criticidad, string fechainicio, string fechafin)
        {
            SqlParameter[] parametros = new SqlParameter[]
             {
                new SqlParameter("@NombreUsuario", nombreusuario),
                new SqlParameter("@Modulo", modulo),
                new SqlParameter("@Evento", evento),
                new SqlParameter("@Criticidad", criticidad),
                new SqlParameter("@FechaInicio", fechainicio),
                new SqlParameter("@FechaFin", fechafin)
             };
            return dalCon.ConsultaProcAlmacenado("FiltrarEvento", parametros);
        }

        public void RegistrarEvento(Evento evento)
        {
            SqlParameter[] parametros = new SqlParameter[]
             {
                new SqlParameter("@NombreUsuario", evento.NombreUsuario),
                new SqlParameter("@Modulo", evento.Modulo),
                new SqlParameter("@Evento", evento.evento),
                new SqlParameter("@Criticidad", evento.Criticidad),
                new SqlParameter("@Fecha", evento.Fecha),
                new SqlParameter("@Hora", evento.Hora)
             };
            dalCon.EjecutarProcAlmacenado("RegistrarEvento", parametros);
        }

        public DataTable TraerListaEventos()
        {
            SqlParameter[] parametros = new SqlParameter[]
             { };
            return dalCon.ConsultaProcAlmacenado("TraerEventosUltimos3Dias", parametros);
        }
    }
}
