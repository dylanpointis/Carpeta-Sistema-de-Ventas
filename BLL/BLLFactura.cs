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
    public class BLLFactura
    {
        DALFactura dalFac = new DALFactura();
        public int RegistrarFactura(BEFactura factura) //devuelve el num factura
        {
            return dalFac.RegistrarFactura(factura);
        }



        public void RegistrarItemFactura(BEFactura factura)
        {
            dalFac.RegistrarItemFactura(factura);
        }

        public int TraerUltimoNumTransaccion()
        {
            return dalFac.TraerUltimoNumTransaccion();
        }

        public List<BEFactura> TraerFacturas()
        {
            List<BEFactura> listaFac = dalFac.TraerFacturas();




            return listaFac;
        }

        public BEFactura TraerItemsFactura(BEFactura fac)
        {
            return dalFac.TraerItemsFactura(fac);
        }

        public List<BEFactura> ConsultarFacturas(int numfactura, int numtransaccion, int dni)
        {
            return dalFac.ConsultarFacturas(numfactura, numtransaccion, dni);
        }
    }
}
