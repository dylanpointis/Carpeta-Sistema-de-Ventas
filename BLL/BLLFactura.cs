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


        public int RegistrarFactura(BEFactura factura) //devuelve el num factura
        {
            int numFac = dalFac.RegistrarFactura(factura);
            bllDV.PersistirDV(TraerTablaFacturas());
            return numFac;
        }



        public void RegistrarItemFactura(BEFactura factura)
        {
            dalFac.RegistrarItemFactura(factura);
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
    }
}
