using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEItemFactura
    {
        public BEProducto producto { get; set; }
        public int cantidad { get; set; }

        public BEItemFactura(BEProducto prod, int cant)
        { 
            this.producto = prod;
            this.cantidad = cant;
        }
    }
}
