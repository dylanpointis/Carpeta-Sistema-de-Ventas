using BE;
using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLProveedor
    {
        private DALProveedor dalProv = new DALProveedor();

        public List<BEProveedor> TraerListaProveedores()
        {
            List<BEProveedor> lista = new List<BEProveedor>();
            DataTable tabla = dalProv.TraerListaProveedores(); 

            foreach (DataRow row in tabla.Rows)
            {
                BEProveedor proveedor = new BEProveedor(
                    row[0].ToString(), //cuit
                    row[1].ToString(),  //nombre
                    row[2].ToString(),  //razonSocial
                    row[3].ToString(),  //email
                    row[4].ToString(),  //numTelefono
                    row[5].ToString(),  //cBU
                    row[6].ToString(),  //direccion
                    row[7].ToString()   //banco
                );

                lista.Add(proveedor);
            }

            return lista;
        }


        public BEProveedor VerificarProveedor(string CUITProv)
        {
            return dalProv.VerificarProveedor( CUITProv );
        }

    }
}
