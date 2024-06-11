using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Services.Observer
{
    public class IdiomaManager : ISubject
    {
        private List<IObserver> observersForms = new List<IObserver>();
        public  string archivoActual;
        private Dictionary<string, string> textosDiccionario;

        /*Singleton para que haya solo un idiomaManager*/
        private static IdiomaManager instancia;

        private IdiomaManager() { }

        public static IdiomaManager GetInstance()
        {
            if (instancia == null)
            {
                instancia = new IdiomaManager();
            }
            return instancia;
        }


        /*Metodos del patron Observer, clase Sujeto*/
        public void Agregar(IObserver observer)
        {
            observersForms.Add(observer);
            CargarIdiomaEnDiccionario();
            Notificar();
        }
        public void Quitar(IObserver observer)
        {
            observersForms.Remove(observer);
        }

        public void Notificar()
        {
            foreach (IObserver observer in observersForms)
            {
                observer.ActualizarObserver();
            }
        }


        /*Carga el idioma en el diccionario "textosDiccionario" con el archivo json*/
        public void CargarIdiomaEnDiccionario()
        {
            textosDiccionario = new Dictionary<string, string>();
            textosDiccionario.Clear();
            var fileName = $"Idiomas\\{SessionManager.IdiomaActual}\\{archivoActual}-{SessionManager.IdiomaActual}.json";
            var jsonString = File.ReadAllText(fileName);
            textosDiccionario = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
        }

        public string ConseguirTexto(string key) //lee el diccionario y devuelve el texto segun la key (nombre del control)
        {
            return textosDiccionario.ContainsKey(key) ? textosDiccionario[key] : key;
        }


    }
}
