using BE.Composite;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLFamilia
    {
        DALFamilia dalFamilia = new DALFamilia();

        public List<Familia> TraerListaFamilias()
        {
            List<Familia> lista = new List<Familia>();
            DataTable tabla = dalFamilia.TraerListaFamilias();

            Familia permiso = null;
            foreach (DataRow row in tabla.Rows)
            {
                if (Convert.ToBoolean(row[2]) == true)
                {
                    permiso = new Familia() { Id = Convert.ToInt32(row[0]), Nombre = row[1].ToString(), Tipo = "Familia" };
                    lista.Add(permiso);
                }
            }
            return lista;
        }



        #region Familia

        public List<Componente> TraerListaHijos(int idPadre)
        {
            return dalFamilia.TraerListaHijos(idPadre);
        }

        public int CrearFamilia(string NombreFamilia)
        {
            return dalFamilia.CrearFamilia(NombreFamilia);
        }

        public void RegistrarHijos(int idPadre, int idHijo)
        {
            dalFamilia.RegistrarHijos(idPadre, idHijo);
        }

        public void ModificarFamilia(Componente familiaAModificar)
        {
            dalFamilia.ModificarFamilia(familiaAModificar);
        }


        public void EliminarHijos(int idPadre)
        {
            dalFamilia.EliminarHijos(idPadre);
        }

        public void EliminarFamilia(int idFamilia)
        {
            dalFamilia.EliminarFamilia(idFamilia);
        }


        #endregion



        #region Roles

        public int CrearRol(string NombreRol)
        {
            return dalFamilia.CrearRol(NombreRol);
        }

        public void RegistrarPermisosRol(int idRol, int idPermiso)
        {
            dalFamilia.RegistrarPermisosRol(idRol, idPermiso);
        }

        public List<Familia> TraerListaRoles()
        {
            List<Familia> lista = new List<Familia>();
            DataTable tabla = dalFamilia.TraerListaRoles();

            foreach (DataRow row in tabla.Rows)
            {
                Familia rol = new Familia() { Id = Convert.ToInt32(row[0]), Nombre = row[1].ToString() };
                lista.Add(rol);
            }
            return lista;
        }

        public void EliminarPermisosRol(int idRol)
        {
            dalFamilia.EliminarPermisosRol(idRol);
        }

        public List<Componente> TraerListaPermisosRol(int idRol)
        {
            return dalFamilia.TraerListaPermisosRol(idRol);
        }

        public void EliminarRol(int idRol)
        {
            dalFamilia.EliminarRol(idRol);
        }

        public void ModificarRol(Componente rol)
        {
            dalFamilia.ModificarRol(rol);
        }

        public List<Componente> TraerListaPermisosRolSegunPermiso(int idPermiso)
        {
            return dalFamilia.TraerListaPermisosRolSegunPermiso(idPermiso);
        }



        #endregion


    }
}
