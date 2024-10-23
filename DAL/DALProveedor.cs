using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALProveedor
    {
        private DALConexion dalCon = new DALConexion();

        public DataTable TraerListaProveedores()
        {
            DataTable tabla = dalCon.TraerTabla("Proveedores");
            return tabla;
        }

        public BEProveedor VerificarProveedor(string cUITProv)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CUITProveedor", cUITProv)
            };
            DataTable tabla = dalCon.ConsultaProcAlmacenado("VerificarProveedor", parametros);

            BEProveedor prov = null;
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
                break;
            }
            return prov;
        }
    }
}
