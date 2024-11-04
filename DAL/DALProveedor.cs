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

        public void EliminarProveedor(string cuit)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CUITProveedor", cuit)
            };
            dalCon.EjecutarProcAlmacenado("EliminarProveedor", parametros);
        }

        public void HabilitarProveedor(string cuit)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CUITProveedor", cuit)
            };
            dalCon.EjecutarProcAlmacenado("HabilitarProveedor", parametros);
        }

        public void ModificarProveedor(BEProveedor prov)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CUITProveedor", prov.CUIT),
                new SqlParameter("@Nombre", prov.Nombre),
                new SqlParameter("@RazonSocial", prov.RazonSocial),
                new SqlParameter("@Email", prov.Email),
                new SqlParameter("@NumTelefono", prov.NumTelefono),
                new SqlParameter("@CBU", prov.CBU),
                new SqlParameter("@Direccion", prov.Direccion),
                new SqlParameter("@Banco", prov.Banco)
            };

            dalCon.EjecutarProcAlmacenado("ModificarProveedor", parametros);
        }

        public void RegistrarProveedor(BEProveedor prov)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CUITProveedor", prov.CUIT),
                new SqlParameter("@Nombre", prov.Nombre),
                new SqlParameter("@RazonSocial", prov.RazonSocial),
                new SqlParameter("@Email", prov.Email),
                new SqlParameter("@NumTelefono", prov.NumTelefono),
                new SqlParameter("@CBU", prov.CBU),
                new SqlParameter("@Direccion", prov.Direccion),
                new SqlParameter("@Banco", prov.Banco)
            };

            dalCon.EjecutarProcAlmacenado("RegistrarProveedor", parametros);
        }

        public DataTable TraerListaProveedores()
        {
            DataTable tabla = dalCon.TraerTabla("Proveedores");
            return tabla;
        }

        public BEProveedor VerificarProveedor(string cUITProv, string cbu, string email)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CUITProveedor", cUITProv),
                new SqlParameter("@CBU", cbu),
                new SqlParameter("@Email", email)
            };
            DataTable tabla = dalCon.ConsultaProcAlmacenado("VerificarProveedor", parametros);

            BEProveedor prov = null;
            foreach (DataRow row in tabla.Rows)
            {
                prov = new BEProveedor(
                    row[0].ToString(), //cuit
                    row[1].ToString(),  //nombre
                    row[2].ToString(),  //razonSocial
                    row[3].ToString(),  //email
                    row[4].ToString(),  //numTelefono
                    row[5].ToString(),  //direccion
                    row[6].ToString(),   //banco
                    row[7].ToString()  //cBU
                );
                prov.BorradoLogico = Convert.ToBoolean(row[8]);
                break;
            }
            return prov;
        }
    }
}
