using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Composite
{
    public abstract class PermisoCompuesto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        
        public PermisoCompuesto(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
        public abstract void AgregarHijo(PermisoCompuesto comp);

        public abstract IList<PermisoCompuesto> ObtenerHijos();
    }
}
