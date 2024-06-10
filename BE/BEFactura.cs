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
        public int NumTransaccionBancaria { get; set; }
        public DateTime Fecha { get; set; }
        public EnumMetodoPago MetodoPago { get; set; }
        public string AliasMP { get; set; } //puede ser Null
        public double MontoTotal { get; set; }
        public double Impuesto { get; set; }
        public string MarcaTarjeta { get; set; }
        public string NumTarjeta { get; set; } //encriptada reversiblemente
        public int CantCuotas { get; set; }
        public string ComentarioAdicional { get; set; } //puede ser Null


        public List<(BEProducto, int)> listaProductosAgregados{ get; set; }
        public BECliente clienteFactura { get; set; }


        public BEFactura()
        {
            listaProductosAgregados = new List<(BEProducto, int)>();
        }

        public double CalcularMonto()
        {
            //Calcular el total
            double total = 0;
            foreach (var item in listaProductosAgregados)
            {
                BEProducto prod = item.Item1;
                int cantidad = item.Item2;
                total += cantidad * prod.Precio;
            }
            return total;
        }
    }
}
