using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Composite
{
    public class Familia : Componente
    {
        private List<Componente> listaHijos = new List<Componente>();


        public override void AgregarHijo(Componente comp)
        {
            Componente permiso = listaHijos.FirstOrDefault(p => p.Id == comp.Id);
            if (permiso == null)
            {
                listaHijos.Add(comp);
            }
        }


        public override List<Componente> ObtenerHijos()
        {
            return listaHijos;
        }

        public override void QuitarHijo(Componente comp)
        {
            Componente permiso = listaHijos.FirstOrDefault(p => p.Id == comp.Id);
            if (permiso != null)
            {
                listaHijos.Remove(permiso);
            }
        }

    }
}
