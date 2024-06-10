using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALFactura
    {
        DALConexion dalCon = new DALConexion();
        public int RegistrarFactura(BEFactura factura)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@DNICliente", factura.clienteFactura.DniCliente),
                new SqlParameter("@Fecha", factura.Fecha.ToString("yyyy-MM-dd HH:mm"))
            };
            return dalCon.EjecutarYTraerId("RegistrarFactura", parametros);
        }

        public void RegistrarDatosPago(BEFactura factura)
        {
           SqlParameter[] parametros = new SqlParameter[]
           {
                new SqlParameter("@NumFactura", factura.NumFactura),
                new SqlParameter("@NumTransaccion", factura.NumTransaccionBancaria),
                new SqlParameter("@MontoTotal", factura.MontoTotal),
                new SqlParameter("@Impuesto", factura.Impuesto),
                new SqlParameter("@MetodoPago", factura.MetodoPago.ToString()),
                new SqlParameter("@MarcaTarjeta", factura.MarcaTarjeta),
                new SqlParameter("@NumTarjeta", factura.NumTarjeta),
                new SqlParameter("@CantCuotas", factura.CantCuotas),
                new SqlParameter("@AliasMP", factura.AliasMP),
                new SqlParameter("@ComentarioAdicional", factura.ComentarioAdicional)
           };
           dalCon.EjecutarProcAlmacenado("RegistrarDatosPago", parametros);
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
    }
}
