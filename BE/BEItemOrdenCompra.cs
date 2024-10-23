using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEItemOrdenCompra
    {
        public BEProducto Producto { get; set; }
        public int CantidadSolicitada { get; set; }
        public double PrecioCompra { get; set; }
        public int CantidadRecibida { get; set; }

        public BEItemOrdenCompra(BEProducto prod, int cantSolicitada, double precioCompra)
        {
            this.Producto = prod;
            this.CantidadSolicitada = cantSolicitada;
            PrecioCompra = precioCompra;
        }
    }
}
