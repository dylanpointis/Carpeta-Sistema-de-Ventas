using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLFactura
    {
        DALFactura dalFac = new DALFactura();
        public int RegistrarFactura(BEFactura factura)
        {
            return dalFac.RegistrarFactura(factura);
        }

        public void RegistrarDatosPago(BEFactura factura)
        {
            dalFac.RegistrarDatosPago(factura);
        }

        public void RegistrarItemFactura(BEFactura factura)
        {
            dalFac.RegistrarItemFactura(factura);
        }
    }
}
