using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Composite
{
    public class Familia: PermisoCompuesto
    {
        private List<PermisoCompuesto> listaHijos;

        public Familia(int id, string nombre) : base(id,nombre)
        {
            listaHijos = new List<PermisoCompuesto>();
        }

        public override void AgregarHijo(PermisoCompuesto comp)
        {
            listaHijos.Add(comp);
        }

        public override IList<PermisoCompuesto> ObtenerHijos()
        {
            return listaHijos.ToArray();
        }
    }
}
