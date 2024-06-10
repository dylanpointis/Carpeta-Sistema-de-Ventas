using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Composite
{
    public class Patente: PermisoCompuesto
    {

        public Patente(string nombre) : base(nombre)
        {

        }

        public override void AgregarHijo(PermisoCompuesto comp)
        {
        }

        public override IList<PermisoCompuesto> ObtenerHijos()
        {
            return null;
        }
    }
}
