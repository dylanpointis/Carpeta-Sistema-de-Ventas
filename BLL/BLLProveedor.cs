using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLProveedor
    {
        public DALProveedor dalProv = new DALProveedor();

        public List<BEProveedor> TraerListaProveedores()
        {
            List<BEProveedor> lista = new List<BEProveedor>();
            DataTable tabla = dalProv.TraerListaProveedores(); 

            foreach (DataRow row in tabla.Rows)
            {
                BEProveedor proveedor = new BEProveedor(
                    row[0].ToString(),  // Nombre
                    row[1].ToString(),  // RazonSocial
                    row[2].ToString(),  // Email
                    row[3].ToString(),  // NumTelefono
                    row[4].ToString(),  // CBU
                    row[5].ToString(),  // Direccion
                    row[6].ToString()   // Banco
                );

                lista.Add(proveedor);
            }

            return lista;
        }
    }
}
