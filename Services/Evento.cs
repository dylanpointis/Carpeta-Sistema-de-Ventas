using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public string NombreUsuario { get; set; }
        public string evento { get; set; }
        public string Modulo { get; set; }
        public int Criticidad { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }

        public Evento(string nombreUsuario, string modulo, string evento, int criticidad)
        {
            NombreUsuario = nombreUsuario;
            this.evento = evento;
            Modulo = modulo;
            Criticidad = criticidad;
        }
    }
}
