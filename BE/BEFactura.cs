using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEFactura
    {
        public int NumFactura { get; set; }
        public DateTime Fecha { get; set; }
        public double MontoTotal { get; set; }
        public double Impuesto { get; set; }

        public BECobro cobro { get; set; } 
 


        private List<BEItemFactura> listaProductosAgregados{ get; set; }
        public BECliente clienteFactura { get; set; }


        public BEFactura()
        {
            listaProductosAgregados = new List<BEItemFactura>();
        }

        public List<BEItemFactura> obtenerListaItems()
        {
            return listaProductosAgregados;
        }

        public int cantidadItems()
        {
            return listaProductosAgregados.Count;
        }

        public void AgregarItem(BEItemFactura item)
        {
            BEItemFactura itemEncontrado = listaProductosAgregados.FirstOrDefault(i => i.producto.CodigoProducto == item.producto.CodigoProducto);
            if(itemEncontrado == null)
                listaProductosAgregados.Add(item);
        }
        
        public void QuitarItem(BEItemFactura item)
        {
            BEItemFactura itemEncontrado = listaProductosAgregados.FirstOrDefault(i => i.producto.CodigoProducto == i.producto.CodigoProducto);
            if(itemEncontrado != null)
                listaProductosAgregados.Remove(item);
        }
        public void RestablecerListaProductos(List<BEItemFactura> nuevaLista)
        {
            listaProductosAgregados = nuevaLista;
        }

        public double CalcularMonto()
        {
            //Calcular el total
            double total = 0;
            foreach (BEItemFactura item in listaProductosAgregados)
            {
                BEProducto prod = item.producto;
                int cantidad = item.cantidad;
                total += cantidad * prod.Precio;
            }
            return total;
        }
    }
}
