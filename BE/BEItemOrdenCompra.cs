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
        public int Cantidad { get; set; }
        public double PrecioCompra { get; set; }

        public BEItemOrdenCompra(BEProducto prod, int cant, double precioCompra)
        {
            this.Producto = prod;
            this.Cantidad = cant;
            PrecioCompra = precioCompra;
        }
    }
}
