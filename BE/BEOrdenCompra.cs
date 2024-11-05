using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEOrdenCompra
    {
        public int NumeroOrdenCompra { get; set; }
        public long NumeroFactura { get; set; }

        public int NumeroSolicitudCompra { get; set; }

        public int CantidadTotal {  get; set; }
        public string Estado { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string MetodoPago { get; set; }
        public double MontoTotal { get; set; }
        public long NumeroTransferencia { get; set; }

        public BEProveedor proveedor { get; set; }


        private List<BEItemOrdenCompra> itemsOrdenCompra { get; set; }



        public BEOrdenCompra(int numSolicitud, int cantidadTotal, string estado, DateTime fechaEntrega, DateTime fechaRegistro, double montoTotal)
        {
            NumeroSolicitudCompra = numSolicitud;
            CantidadTotal = cantidadTotal;
            Estado = estado;
            FechaEntrega = fechaEntrega;
            FechaRegistro = fechaRegistro;
            MontoTotal = montoTotal;

            itemsOrdenCompra = new List<BEItemOrdenCompra>();
        }


        public void modificarCantidadItem(long codProd, int cant, long numFacturaRecep, string fechaRecep, bool boolRecepcion)
        {
            BEItemOrdenCompra item = itemsOrdenCompra.FirstOrDefault(p => p.Producto.CodigoProducto == codProd);
            if (boolRecepcion == false)
            {
                item.CantidadSolicitada = cant;
            }
            else //si es el formulario de corroborar recepcion modifca la cantidad recibida, no la solicitada
            { 
                item.CantidadRecibida = cant;
                item.NumFacturaRecepcion = numFacturaRecep;
                item.FechaRecepcion = fechaRecep;
            }
        }

        public void modificarPrecioItem(long codProd, double precioCompra)
        {
            BEItemOrdenCompra item = itemsOrdenCompra.FirstOrDefault(p => p.Producto.CodigoProducto == codProd);
            item.PrecioCompra = precioCompra;
        }


        public List<BEItemOrdenCompra> obtenerItems()
        {
            return itemsOrdenCompra;
        }

        public void AgregarItem(BEProducto producto, int cantSolicitada, int cantRecibida, long numFactura, string fechaRecepcion)
        {
            BEItemOrdenCompra itemEncontrado = itemsOrdenCompra.FirstOrDefault(i => i.Producto.CodigoProducto == producto.CodigoProducto);
            if(itemEncontrado == null)
            {
                BEItemOrdenCompra item = new BEItemOrdenCompra(producto, cantSolicitada, 0);
                item.CantidadRecibida = cantRecibida;
                item.NumFacturaRecepcion = numFactura;
                item.FechaRecepcion = fechaRecepcion;
                itemsOrdenCompra.Add(item);
            }
        }

        public void QuitarItem(long codProd)
        {
            BEItemOrdenCompra item = itemsOrdenCompra.FirstOrDefault(p => p.Producto.CodigoProducto == codProd);
            if(item != null)
            itemsOrdenCompra.Remove(item);
        }

    }
}
