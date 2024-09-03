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

        public void HabilitarCliente(int dni)
        {
            SqlParameter[] parametros = new SqlParameter[]
          {
                new SqlParameter("@DNI", dni)
          };
            dalCon.EjecutarProcAlmacenado("HabilitarCliente", parametros);
        }

        public void ModificarCliente(BECliente cliente)
        {
            SqlParameter[] parametros = new SqlParameter[]
             {
                new SqlParameter("@DNI", cliente.DniCliente),
                new SqlParameter("@Nombre", cliente.Nombre),
                new SqlParameter("@Apellido", cliente.Apellido),
                new SqlParameter("@Mail", cliente.Mail),
                new SqlParameter("@Direccion", cliente.Direccion),
                new SqlParameter("@Borrado", cliente.BorradoLogico)
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

        public BECliente VerificarCliente(int dniCliente)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@DNI", dniCliente)
            };
            DataTable tabla = dalCon.ConsultaProcAlmacenado("VerificarCliente", parametros);

            BECliente cliente = null;
            foreach(DataRow row in tabla.Rows) 
            {
                cliente = new BECliente(Convert.ToInt32(row[0]), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString());
                cliente.BorradoLogico = Convert.ToBoolean(row[5]);
                break;
            }
            return cliente;
        }

        public bool VerificarSiClienteTieneFacturas(int dniCliente)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@DNI", dniCliente)
            };
            DataTable tabla = dalCon.ConsultaProcAlmacenado("VerificarSiClienteTieneFacturas", parametros);

            if(tabla.Rows.Count > 0)
            {
                return true;
            }
            else { return false; }
        }
    }
}
