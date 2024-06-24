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
    public class DALCliente
    {
        DALConexion dalCon = new DALConexion();

        public void EliminarCliente(int dniCliente)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@DNI", dniCliente)
            };
            dalCon.EjecutarProcAlmacenado("EliminarCliente", parametros);
        }

        public void ModificarCliente(BECliente cliente)
        {
            SqlParameter[] parametros = new SqlParameter[]
             {
                new SqlParameter("@DNI", cliente.DniCliente),
                new SqlParameter("@Nombre", cliente.Nombre),
                new SqlParameter("@Apellido", cliente.Apellido),
                new SqlParameter("@Mail", cliente.Mail),
                new SqlParameter("@Direccion", cliente.Direccion)
             };
            dalCon.EjecutarProcAlmacenado("ModificarCliente", parametros);
        }

        public void RegistrarCliente(BECliente cliente)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@DNI", cliente.DniCliente),
                new SqlParameter("@Nombre", cliente.Nombre),
                new SqlParameter("@Apellido", cliente.Apellido),
                new SqlParameter("@Mail", cliente.Mail),
                new SqlParameter("@Direccion", cliente.Direccion)
            };
            dalCon.EjecutarProcAlmacenado("RegistrarCliente", parametros);
        }

        public DataTable TraerListaCliente()
        {
            DataTable tabla = dalCon.TraerTabla("Clientes");
            return tabla;
        }
    }
}
