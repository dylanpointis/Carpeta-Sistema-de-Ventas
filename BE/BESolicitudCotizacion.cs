using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BESolicitudCotizacion
    {
        public int NumSolicitud {  get; set; }
        public string Estado {  get; set; }
        public DateTime Fecha {  get; set; }

        private List<BEProveedor> ProveedoresSolicitud { get; set; }
        private List<BEItemSolicitud> itemsSolicitud { get; set; }


        public void AgregarProveedor(BEProveedor prov)
        {
            BEProveedor encontrado = ProveedoresSolicitud.FirstOrDefault(p => p.CUIT == prov.CUIT);
            if(encontrado == null)
            {
                ProveedoresSolicitud.Add(prov);
            }
        }

        public List<BEProveedor> obtenerProveedorSolicitud()
        {
            return ProveedoresSolicitud;
        }




        public void AgregarItem(BEProducto prod, int cant)
        {
            itemsSolicitud.Add(new BEItemSolicitud(prod, cant));
        }

        public void QuitarItem(long codProd)
        {
            BEItemSolicitud item = itemsSolicitud.FirstOrDefault(p => p.Producto.CodigoProducto == codProd);
            itemsSolicitud.Remove(item);
        }

        public void modificarCantidadItem(long codProd, int cant)
        {     
            BEItemSolicitud item = itemsSolicitud.FirstOrDefault(p => p.Producto.CodigoProducto == codProd);
            item.Cantidad = cant;

        }


        public List<BEItemSolicitud> obtenerItems()
        {
            return itemsSolicitud;
        }


        public BESolicitudCotizacion(string estado, DateTime fecha)
        {
            Estado = estado;
            Fecha = fecha;

            ProveedoresSolicitud = new List<BEProveedor>();
            itemsSolicitud = new List<BEItemSolicitud>();
        }
    }
}
