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
    public class DALFactura
    {
        DALConexion dalCon = new DALConexion();
        public void RegistrarFactura(BEFactura factura)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@DNICliente", factura.clienteFactura.DniCliente),
                new SqlParameter("@NumTransaccion", factura.cobro.NumTransaccionBancaria),
                new SqlParameter("@MontoTotal", factura.MontoTotal),
                new SqlParameter("@Impuesto", factura.Impuesto),
                new SqlParameter("@Fecha", factura.Fecha.ToString("yyyy-MM-dd HH:mm")),
                new SqlParameter("@MetodoPago", factura.cobro.MetodoPago.ToString()),
                new SqlParameter("@MarcaTarjeta", factura.cobro.MarcaTarjeta),
                new SqlParameter("@NumTarjeta", factura.cobro.NumTarjeta),
                new SqlParameter("@CantCuotas", factura.cobro.CantCuotas),
                new SqlParameter("@AliasMP", factura.cobro.AliasMP),
                new SqlParameter("@ComentarioAdicional", factura.cobro.ComentarioAdicional),
            };
            dalCon.EjecutarProcAlmacenado("RegistrarFactura", parametros);
        }


        public void RegistrarItemFactura(BEFactura factura)
        {
            foreach (var item in factura.listaProductosAgregados)
            {
                BEProducto prod = item.Item1;
                int cantidad = item.Item2;

                SqlParameter[] parametros = new SqlParameter[]
                {
                new SqlParameter("@NumFactura", factura.NumFactura),
                new SqlParameter("@CodigoProducto", prod.CodigoProducto),
                new SqlParameter("@Cant", cantidad),
                new SqlParameter("@SubTotal", prod.Precio * cantidad)
                };
                dalCon.EjecutarProcAlmacenado("RegistrarItemFactura", parametros);
            }
        }

        public int TraerUltimoIDFactura()
        {
            SqlParameter[] parametros = new SqlParameter[]
            { };

            DataTable tabla = dalCon.ConsultaProcAlmacenado("TraerUltimoIDFactura", parametros);
            int IdFactura = 0;
            foreach(DataRow row in tabla.Rows)
            {
                IdFactura = Convert.ToInt32(row[0]);
                break;
            }
            return IdFactura;
        }
    }
}
