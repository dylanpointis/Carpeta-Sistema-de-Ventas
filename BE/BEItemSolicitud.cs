using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEItemSolicitud
    {
        public BEProducto Producto { get; set; }
        public int Cantidad { get; set; }

        public BEItemSolicitud(BEProducto prod, int cant)
        {
            this.Producto = prod;
            this.Cantidad = cant;
        }
    }
}
