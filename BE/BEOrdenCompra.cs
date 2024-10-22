using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEOrdenCompra
    {
        public int NumeroOrdenCompra { get; set; }
        public int NumeroFactura { get; set; }

        public string CUITProveedor { get; set; }
        public int NumeroSolicitudCompra { get; set; }

        public int CantidadTotal {  get; set; }
        public string Estado { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaRegistro { get; set; }
        public double MontoTotal { get; set; }
        public int NumeroTransferencia { get; set; }



        public List<BEItemOrdenCompra> itemsOrdenCompra { get; set; }



        public BEOrdenCompra(string cuitProv, int numSolicitud, int cantidadTotal, string estado, DateTime fechaEntrega, DateTime fechaRegistro, double montoTotal)
        {
            CUITProveedor = cuitProv;
            NumeroSolicitudCompra = numSolicitud;
            CantidadTotal = cantidadTotal;
            Estado = estado;
            FechaEntrega = fechaEntrega;
            FechaRegistro = fechaRegistro;
            MontoTotal = montoTotal;

            itemsOrdenCompra = new List<BEItemOrdenCompra>();
        }


        public void modificarCantidadItem(long codProd, int cant)
        {
            BEItemOrdenCompra item = itemsOrdenCompra.FirstOrDefault(p => p.Producto.CodigoProducto == codProd);
            item.Cantidad = cant;

        }

        public void modificarPrecioItem(long codProd, double precioCompra)
        {
            BEItemOrdenCompra item = itemsOrdenCompra.FirstOrDefault(p => p.Producto.CodigoProducto == codProd);
            item.PrecioCompra = precioCompra;
        }
    }
}
