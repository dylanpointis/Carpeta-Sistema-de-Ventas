using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLRespaldo
    {
        private DALRespaldo dalRespaldo = new DALRespaldo();

        public void RealizarBackUp(string ruta)
        {
            string nombreArchivo = $"AltaGama.BackUp_{DateTime.Now.ToString("ddMMyy_HHmm")}.bak";
            string rutaCompleta = System.IO.Path.Combine(ruta,nombreArchivo);
           

            dalRespaldo.RealizarBackUp(rutaCompleta);
        }

        public void RealizarRestore(string ruta)
        {
            dalRespaldo.RealizarRestore(ruta);
        }
    }
}
