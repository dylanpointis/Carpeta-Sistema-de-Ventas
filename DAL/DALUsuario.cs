using BE;
using BE.Composite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALUsuario
    {
        DALConexion dalCon = new DALConexion();

        public void ModificarBloqueo(int DNI, bool bloqueo)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                 new SqlParameter("@DNI", DNI),
                 new SqlParameter("@Bloqueo", bloqueo)
            };

            dalCon.EjecutarProcAlmacenado("ModificarBloquearUsuario", parametros);
        }

        public void ModificarUsuario(BEUsuario user)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@DNI", user.DNI),
                new SqlParameter("@Nombre", user.Nombre),
                new SqlParameter("@Apellido", user.Apellido),
                new SqlParameter("@Mail", user.Email),
                new SqlParameter("@NombreUsuario", user.NombreUsuario),
                new SqlParameter("@Rol", user.codRol),
                new SqlParameter("@Bloqueo", user.Bloqueado)
            };
            dalCon.EjecutarProcAlmacenado("ModificarUsuario", parametros);
        }

        public void EliminarUsuario(int DNICliente)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@DNI", DNICliente)
            };
            dalCon.EjecutarProcAlmacenado("EliminarUsuario", parametros);
        }

        public void RegistrarUsuario(BEUsuario user)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@DNI", user.DNI),
                new SqlParameter("@Nombre", user.Nombre),
                new SqlParameter("@Apellido", user.Apellido),
                new SqlParameter("@Mail", user.Email),
                new SqlParameter("@NombreUsuario", user.NombreUsuario),
                new SqlParameter("@Clave", user.Clave),
                new SqlParameter("@Rol", user.codRol)
            };
            dalCon.EjecutarProcAlmacenado("RegistrarUsuario", parametros);
        }

        public DataTable TraerListaUsuario()
        {
            DataTable tabla = dalCon.TraerTabla("Usuarios");
            return tabla;
        }

        public BEUsuario ValidarUsuario(string nombreusuario, int DNI, string Email)
        {
             SqlParameter[] parametros = new SqlParameter[]
             {
                 new SqlParameter("@NombreUsuario", nombreusuario),
                 new SqlParameter("@DNI", DNI),
                 new SqlParameter("@Email", Email)
             };


            DataTable tabla = dalCon.ConsultaProcAlmacenado("ValidarUsuario", parametros);

            BEUsuario user = null;
            foreach (DataRow dr in tabla.Rows)
            {
                user = new BEUsuario(Convert.ToInt32(dr[0]), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), Convert.ToInt32(dr[6]), Convert.ToBoolean(dr[7]), Convert.ToBoolean(dr[8]));
                user.ContFallidos = Convert.ToInt16(dr[9]);

                //busca el rol del usuario. Solamente carga el nombre del Rol
                Familia familia = new Familia();
                familia.Nombre = Convert.ToString(dr[11]);
                user.Rol = familia;
                break;
                //Solo agarra el primer registro que coincida nombreusuario
            }
            return user;
        }

        public void CambiarClave(int DNICliente, string clave)
        {
            SqlParameter[] parametros = new SqlParameter[]
          {
                new SqlParameter("@DNI", DNICliente),
                new SqlParameter("@Clave", clave)
          };
            dalCon.EjecutarProcAlmacenado("CambiarClaveUsuario", parametros);
        }

        public void ActivarUsuario(int DNICliente)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@DNI", DNICliente)
            };
            dalCon.EjecutarProcAlmacenado("ActivarUsuario", parametros);
        }

        public void ModificarContFallido(string nombreUsuario, int contClaveIncorrecta)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@NombreUsuario", nombreUsuario),
                new SqlParameter("@ContClaveIncorrecta", contClaveIncorrecta)
            };
            dalCon.EjecutarProcAlmacenado("ModificarContFallido", parametros);
        }
    }
}
