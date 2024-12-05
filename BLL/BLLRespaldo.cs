﻿using DAL;
using Services;
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
        private BLLEvento bllEv = new BLLEvento();
        public void RealizarBackUp(string ruta)
        {
            string nombreArchivo = $"AltaGama.BackUp_{DateTime.Now.ToString("ddMMyy_HHmm")}.bak";
            string rutaCompleta = System.IO.Path.Combine(ruta,nombreArchivo);
           

            dalRespaldo.RealizarBackUp(rutaCompleta);
            bllEv.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Respaldos", "Backup realizado", 1));
        }

        public void RealizarRestore(string ruta)
        {
            dalRespaldo.RealizarRestore(ruta);
            bllEv.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Respaldos", "Restore realizado", 1));
        }
    }
}
