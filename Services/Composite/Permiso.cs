using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Composite
{
    public class Permiso: PermisoCompuesto
    {

        public Permiso(int id, string nombre) : base(id, nombre)
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
