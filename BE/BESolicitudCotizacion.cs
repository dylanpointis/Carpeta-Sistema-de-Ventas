using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BESolicitudCotizacion
    {
        public string Estado {  get; set; }
        public DateTime Fecha {  get; set; }

        public BEProveedor Proveedor { get; set; }


        public BESolicitudCotizacion(string estado, DateTime fecha)
        {
            Estado = estado;
            Fecha = fecha;
        }
    }
}
