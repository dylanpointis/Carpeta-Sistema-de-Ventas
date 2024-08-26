using BE.Composite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALFamilia
    {
        DALConexion dalCon = new DALConexion();

        public DataTable TraerListaFamilias()
        {
            DataTable tabla = dalCon.TraerTabla("Permisos");
            return tabla;
        }

        #region Familia

        public int CrearFamilia(string NombreFamilia)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@NombreFamilia", NombreFamilia)
            };
            return dalCon.EjecutarYTraerId("CrearFamilia", parametros);
        }

        public void ModificarFamilia(Componente familiaAModificar)
        {

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CodPermiso", familiaAModificar.Id),
                new SqlParameter("@NombreFamilia", familiaAModificar.Nombre)
            };
            dalCon.EjecutarProcAlmacenado("ModificarFamilia", parametros);
        }


        public void RegistrarHijos(int idPadre, int idHijo)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CodPadre", idPadre),
                new SqlParameter("@CodHijo", idHijo)
            };
            dalCon.EjecutarProcAlmacenado("RegistrarHijosFamilia", parametros);
        }




        public List<Componente> TraerListaHijos(int idPadre)
        {
            SqlParameter[] parametros = new SqlParameter[]
             {
                 new SqlParameter("@CodPadre", idPadre)
             };
            DataTable tabla = dalCon.ConsultaProcAlmacenado("TraerListaHijos", parametros);


            List<Componente> lista = new List<Componente>();
            Componente permiso;
            foreach (DataRow dr in tabla.Rows)
            {
                if (Convert.ToBoolean(dr[3]) == false)
                {
                    permiso = new Permiso() { Id = Convert.ToInt32(dr[1]), Nombre = dr[2].ToString(), Tipo = "Simple" };
                }
                else { permiso = new Familia() { Id = Convert.ToInt32(dr[1]), Nombre = dr[2].ToString(), Tipo = "Familia" }; }
                lista.Add(permiso);
            }
            return lista;
        }



        public void EliminarHijos(int idPadre)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CodPadre", idPadre)
            };
            dalCon.EjecutarProcAlmacenado("EliminarHijos", parametros);
        }

        public void EliminarFamilia(int idFamilia)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CodFamilia", idFamilia)
            };
            dalCon.EjecutarProcAlmacenado("EliminarFamilia", parametros);
        }


        #endregion



        #region Roles

        public int CrearRol(string NombreRol)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@NombreRol", NombreRol)
            };
            return dalCon.EjecutarYTraerId("CrearRol", parametros);
        }

        public void EliminarPermisosRol(int idRol)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CodRol", idRol)
            };
            dalCon.EjecutarProcAlmacenado("EliminarPermisosRol", parametros);
        }


        public void EliminarRol(int idRol)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CodRol", idRol)
            };
            dalCon.EjecutarProcAlmacenado("EliminarRol", parametros);
        }

        public void RegistrarPermisosRol(int idRol, int idPermiso)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CodRol", idRol),
                new SqlParameter("@codPermiso", idPermiso)
            };
            dalCon.EjecutarProcAlmacenado("RegistrarRolPermiso", parametros);
        }

        public List<Componente> TraerListaPermisosRol(int idRol)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                 new SqlParameter("@CodRol", idRol)
            };
            DataTable tabla = dalCon.ConsultaProcAlmacenado("TraerListaPermisosRol", parametros);


            List<Componente> lista = new List<Componente>();
            Componente permiso;
            foreach (DataRow dr in tabla.Rows)
            {
                if (Convert.ToBoolean(dr[3]) == false)
                {
                    permiso = new Permiso() { Id = Convert.ToInt32(dr[1]), Nombre = dr[2].ToString(), Tipo = "Simple" };
                }
                else { permiso = new Familia() { Id = Convert.ToInt32(dr[1]), Nombre = dr[2].ToString(), Tipo = "Familia" }; }
                lista.Add(permiso);
            }
            return lista;
        }

        public DataTable TraerListaRoles()
        {
            DataTable tabla = dalCon.TraerTabla("Roles");
            return tabla;
        }


        public void ModificarRol(Componente rol)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CodRol", rol.Id),
                new SqlParameter("@Nombre", rol.Nombre)
            };
            dalCon.EjecutarProcAlmacenado("ModificarRol", parametros);
        }

        public List<Componente> TraerListaPermisosRolSegunPermiso(int idPermiso)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                 new SqlParameter("@CodPermiso", idPermiso)
            };
            DataTable tabla = dalCon.ConsultaProcAlmacenado("TraerListaPermisosRolSegunPermiso", parametros);


            List<Componente> lista = new List<Componente>();
            Componente rol = new Familia();
            foreach (DataRow dr in tabla.Rows)
            {
                rol.Id = Convert.ToInt32(dr[0]);
                lista.Add(rol);
            }
            return lista;
        }


        #endregion
    }
}
