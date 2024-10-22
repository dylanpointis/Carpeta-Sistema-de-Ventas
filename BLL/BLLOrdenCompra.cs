using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLOrdenCompra
    {
        private DALOrdenCompra dalOrdC = new DALOrdenCompra();

        public void RegistrarItemOrden(int numeroOrdenC, BEItemOrdenCompra item)
        {
            dalOrdC.RegistrarItemOrden(numeroOrdenC,item);
        }

        public int RegistrarOrdenCompra(BEOrdenCompra ordenCompra)
        {
            return dalOrdC.RegistrarOrdenCompra(ordenCompra);
        }
    }
}
