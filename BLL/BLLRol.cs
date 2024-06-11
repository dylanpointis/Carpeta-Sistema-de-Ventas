using BE;
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
    public class BLLRol
    {
        DALRol dalRol = new DALRol();
        public List<Familia> TraerListaRoles()
        {
            List<Familia> lista = new List<Familia>();
            DataTable tabla = dalRol.TraerListaRoles();

            foreach (DataRow row in tabla.Rows)
            {
                Familia rol = new Familia(Convert.ToInt32(row[0]), row[1].ToString());
                lista.Add(rol);
            }
            return lista;
        }
    }
}
