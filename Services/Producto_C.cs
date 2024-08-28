using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Producto_C
    {
        public int idCambio {  get; set; }
        public string Fecha {  get; set; }
        public string Hora {  get; set; }
        
        public BEProducto Producto { get; set; } 
        
        public bool Activo { get; set; }

        public Producto_C(BEProducto prod, int idcambio, string fecha, string hora, bool activo) 
        { 
            this.idCambio = idcambio;
            this.Fecha = fecha;
            this.Hora = hora;
            this.Producto = prod;
            this.Activo = activo;
        }
    }
}
