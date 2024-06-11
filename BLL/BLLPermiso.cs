using DAL;
using Services.Composite;
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

        public List<Permiso> TraerListaPermisos()
        {
            List<Permiso> lista = new List<Permiso>();
            DataTable tabla = dalPermiso.TraerListaPermisos();

            foreach (DataRow row in tabla.Rows)
            {
                Permiso permiso = new Permiso(Convert.ToInt32(row[0]), row[1].ToString());
                lista.Add(permiso);
            }
            return lista;
        }
    }
}
