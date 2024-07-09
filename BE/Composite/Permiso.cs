using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Composite
{
    public class Permiso : Componente
    {
        public override void AgregarHijo(Componente comp)
        {
        }

        public override void QuitarHijo(Componente comp)
        {
        }

        public override List<Componente> ObtenerHijos()
        {
            return new List<Componente>();
        }

    }
}
