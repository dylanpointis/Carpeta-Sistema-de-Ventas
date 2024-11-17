using BE;
using DAL;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLFactura
    {
        DALFactura dalFac = new DALFactura();
        BLLDigitoVerificador bllDV = new BLLDigitoVerificador();
        BLLEvento bLLEvento = new BLLEvento();
        BLLProducto bllProducto = new BLLProducto();

        public int RegistrarFactura(BEFactura factura) //devuelve el num factura
        {
            int numFac = dalFac.RegistrarFactura(factura);
            factura.NumFactura = numFac;
            bllDV.PersistirDV(TraerTablaFacturas());


            RegistrarItemFactura(factura);


            //reduce el stock
            foreach (var item in factura.listaProductosAgregados)
            {
                bllProducto.ModificarStock(item.producto, item.producto.Stock - item.cantidad);
            }


            //registra en la bitacora de eventos
            bLLEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Ventas", "Factura generada", 2));
            return numFac;
        }



        public void RegistrarItemFactura(BEFactura factura)
        {
            foreach (BEItemFactura item in factura.listaProductosAgregados)
            {
                dalFac.RegistrarItemFactura(factura, item);
            }
            bllDV.PersistirDV(bllDV.TraerTablaAConsultarDV("Item_Factura"));
        }

        public int TraerUltimoNumTransaccion()
        {
            return dalFac.TraerUltimoNumTransaccion();
        }

        public List<BEFactura> TraerFacturas()
        {
            DataTable tablaFac = dalFac.TraerFacturas();
            List<BEFactura> listaFacturas = new List<BEFactura>();

            foreach (DataRow row in tablaFac.Rows)
            {
                BECliente cli = new BECliente(Convert.ToInt32(row[11]), row[12].ToString(), row[13].ToString(), row[14].ToString(), Encriptador.DesencriptarAES(row[15].ToString()));

                int? valorNumTransaccion;
                if (row[2] == DBNull.Value) //si es DBNull lo pone como null
                {
                    valorNumTransaccion = null;
                }
                else { valorNumTransaccion = Convert.ToInt32(row[2]); }


                BECobro cobro = new BECobro() { NumTransaccionBancaria = valorNumTransaccion, MarcaTarjeta = row[7].ToString(), CantCuotas = Convert.ToInt32(row[8]), AliasMP = row[9].ToString(), ComentarioAdicional = row[10].ToString(), stringMetodoPago = row[6].ToString() };

                if (row[10] == DBNull.Value) //si es DBNull lo pone como null
                {
                    cobro.ComentarioAdicional = null;
                }
                else { cobro.ComentarioAdicional = row[10].ToString(); }
               

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

        public BEFactura TraerItemsFactura(BEFactura fac)
        {
            return dalFac.TraerItemsFactura(fac);
        }

        public List<BEFactura> ConsultarFacturas(int numfactura, int numtransaccion, int dni)
        {
            return dalFac.ConsultarFacturas(numfactura, numtransaccion, dni);
        }

        //Esto es para que el digito verificador persista en la tabla Item_Factura 
        private DataTable TraerTablaItems()
        {
            return dalFac.TraerTablaItems();
        }
        private DataTable TraerTablaFacturas()
        {
            return dalFac.TraerTablaFacturas();
        }

        public DataTable ReporteVentasGeneradas(string fechaInicio, string fechaFin, string tipo)
        {
            return dalFac.ReporteVentasGeneradas(fechaInicio, fechaFin, tipo);
        }

        public DataTable ReportePrecedirVentasPorProd()
        {
            return dalFac.ReportePrecedirVentasPorProd();
        }

    }
}
