using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEProducto
    {
        public int CodigoProducto{ get; set; }
        public string Modelo { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set;}
        public string Color { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
        public int Almacenamiento { get; set; }

        public BEProducto(int codigoProducto, string modelo, string descripcion, string marca, string color, double precio, int stock, int almacenamiento) 
        {
            this.CodigoProducto = codigoProducto;
            this.Modelo = modelo;
            this.Descripcion = descripcion;
            this.Marca = marca;
            this.Color = color;
            this.Precio = precio;
            this.Stock = stock;
            this.Almacenamiento = almacenamiento;
        }

    }
}
