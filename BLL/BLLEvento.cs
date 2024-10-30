using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using System.Data;

namespace BLL
{
    public class BLLEvento
    {
        DALEvento dalEv = new DALEvento();

        public void RegistrarEvento(Evento evento)
        {
            evento.Fecha = DateTime.Today.ToString("yyyy-MM-dd");
            evento.Hora = DateTime.Now.ToString("HH:mm");
            dalEv.RegistrarEvento(evento);
        }


        public List<Evento> TraerListaEventos()
        {
            List<Evento> lista = new List<Evento>();
            DataTable tabla = dalEv.TraerListaEventos();

            foreach (DataRow row in tabla.Rows)
            {
                Evento evento = new Evento(row[1].ToString(), row[2].ToString(), row[3].ToString(), Convert.ToInt32(row[4]), row[5].ToString(), row[6].ToString());
                evento.IdEvento = Convert.ToInt32(row[0]);
                lista.Add(evento);
            }
            return lista;
        }



        public List<Evento> FiltrarEventos(string nombreusuario, string modulo, string evento, string criticidad, string fechainicio, string fechafin)
        {
            List<Evento> lista = new List<Evento>();
            DataTable tabla = dalEv.FiltrarEventos(nombreusuario, modulo, evento, criticidad, fechainicio, fechafin);

            foreach (DataRow row in tabla.Rows)
            {
                Evento e = new Evento(row[1].ToString(), row[2].ToString(), row[3].ToString(), Convert.ToInt32(row[4]), row[5].ToString(), row[6].ToString());
                e.IdEvento = Convert.ToInt32(row[0]);
                lista.Add(e);
            }
            return lista;
        }
    }
}
