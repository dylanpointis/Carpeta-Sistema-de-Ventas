using BE.Composite;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLPermiso
    {

        DALPermiso dalPermiso = new DALPermiso();


        public List<Componente> TraerListaPermisos()
        {
            List<Componente> lista = new List<Componente>();
            DataTable tabla = dalPermiso.TraerListaPermisos();

            Componente permiso = null;
            foreach (DataRow row in tabla.Rows)
            {
                if (Convert.ToBoolean(row[2]) == false)
                {
                    permiso = new Permiso() { Id = Convert.ToInt32(row[0]), Nombre = row[1].ToString(), Tipo = "Simple" };
                    lista.Add(permiso);
                }
            }
            return lista;
        }



      


    }
}
