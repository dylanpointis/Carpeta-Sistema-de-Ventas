using BE;
using Services;
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

        public List<BEFactura> ConsultarFacturas(int numfactura, int numtransaccion, int dni)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@NumFactura", numfactura),
                new SqlParameter("@NumTransaccion", numtransaccion),
                new SqlParameter("@DNICliente", dni),
            };

            DataTable tabla = dalCon.ConsultaProcAlmacenado("ConsultarFacturas", parametros);
            List<BEFactura> listaFacturas = new List<BEFactura>();

            foreach (DataRow row in tabla.Rows)
            {
                BECliente cli = new BECliente(Convert.ToInt32(row[11]), row[12].ToString(), row[13].ToString(), row[14].ToString(), Encriptador.DesencriptarAES(row[15].ToString()));

                BECobro cobro = new BECobro() { NumTransaccionBancaria = Convert.ToInt32(row[2]), MarcaTarjeta = row[7].ToString(), CantCuotas = Convert.ToInt32(row[8]), AliasMP = row[9].ToString(), ComentarioAdicional = row[10].ToString(), stringMetodoPago = row[6].ToString() };


                BEFactura fac = new BEFactura()
                {
                    NumFactura = Convert.ToInt32(row[0]),
                    clienteFactura = cli,
                    cobro = cobro,
                    Fecha = Convert.ToDateTime(row[5].ToString()),
                    MontoTotal = Convert.ToDouble(row[3]),
                    Impuesto = Convert.ToDouble(row[4]),
                };


                listaFacturas.Add(fac);
            }

            return listaFacturas;
        }

        public int RegistrarFactura(BEFactura factura)
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
                new SqlParameter("@CantCuotas", factura.cobro.CantCuotas),
                new SqlParameter("@AliasMP", factura.cobro.AliasMP),
                new SqlParameter("@ComentarioAdicional", factura.cobro.ComentarioAdicional),
            };
            return dalCon.EjecutarYTraerId("RegistrarFactura", parametros);
        }


        public void RegistrarItemFactura(BEFactura factura, BEItemFactura item)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@NumFactura", factura.NumFactura),
                new SqlParameter("@CodigoProducto", item.producto.CodigoProducto),
                new SqlParameter("@Cant", item.cantidad),
                new SqlParameter("@PrecioVenta", item.producto.Precio)
            };
            dalCon.EjecutarProcAlmacenado("RegistrarItemFactura", parametros);
        }

        public DataTable TraerFacturas()
        {
            SqlParameter[] parametros = new SqlParameter[]
            { };

            DataTable tabla = dalCon.ConsultaProcAlmacenado("TraerUltimas100Facturas", parametros);
            return tabla;
        }

        public BEFactura TraerItemsFactura(BEFactura fac)
        {
            //trae los items de la factura
            SqlParameter[] parametros = new SqlParameter[]
            {
                  new SqlParameter("@NumFactura", fac.NumFactura),
            };
            DataTable tablaItems = dalCon.ConsultaProcAlmacenado("TraerItemFactura", parametros);


            foreach (DataRow rowitem in tablaItems.Rows)
            {
                BEProducto prod = new BEProducto(Convert.ToInt64(rowitem[2]), rowitem[6].ToString(), null, null, null, Convert.ToDouble(rowitem[4]),0,0, 0, 0,true);
                int cant = Convert.ToInt32(rowitem[3]);

                fac.listaProductosAgregados.Add(new BEItemFactura(prod, cant));
            }

            return fac;
        }

        public int TraerUltimoNumTransaccion()
        {
            SqlParameter[] parametros = new SqlParameter[]
            { };

            DataTable tabla = dalCon.ConsultaProcAlmacenado("TraerUltimoNumTransaccion", parametros);
            int IdFactura = 0;
            foreach(DataRow row in tabla.Rows)
            {
                IdFactura = Convert.ToInt32(row[0]);
                break;
            }
            return IdFactura;
        }


        //Esto es para que el digito verificador persista en la tabla Item_Factura 
        public DataTable TraerTablaItems()
        {
            return dalCon.TraerTabla("Item_Factura");
        }
        public DataTable TraerTablaFacturas()
        {
            return dalCon.TraerTabla("Facturas");
        }

    }
}
