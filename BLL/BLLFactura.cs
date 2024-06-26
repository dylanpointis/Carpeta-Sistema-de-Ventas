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
        public void RegistrarFactura(BEFactura factura)
        {
            dalFac.RegistrarFactura(factura);
        }



        public void RegistrarItemFactura(BEFactura factura)
        {
            dalFac.RegistrarItemFactura(factura);
        }

        public int TraerUltimoNumTransaccion()
        {
            return dalFac.TraerUltimoIDFactura();
        }
    }
}
