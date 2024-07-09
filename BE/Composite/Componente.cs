using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Composite
{
    public abstract class Componente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }

        public abstract void AgregarHijo(Componente comp);
        public abstract void QuitarHijo(Componente comp);

        public abstract List<Componente> ObtenerHijos();

    }
}
