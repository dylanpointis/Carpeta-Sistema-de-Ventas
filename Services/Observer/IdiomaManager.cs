using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Windows.Forms;

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
            var fileName = Path.Combine("..","..","..",$"Idiomas\\{SessionManager.IdiomaActual}\\{archivoActual}-{SessionManager.IdiomaActual}.json");
            var jsonString = File.ReadAllText(fileName);
            textosDiccionario = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
        }

        public string ConseguirTexto(string nombreControl) //lee el diccionario y devuelve el texto segun la key (nombre del control)
        {
            if (textosDiccionario.ContainsKey(nombreControl))
            {
                return textosDiccionario[nombreControl];
            }
            else
            {
                return nombreControl;
            }
        }


        public bool PrimeraVez; //esto es para que solo actualice el menustrip la priemra vez

        public static void ActualizarControles(Control frm)
        {
            foreach (Control control in frm.Controls)
            {
                if (control is Button || control is Label)
                {
                    control.Text = IdiomaManager.GetInstance().ConseguirTexto(control.Name);
                }


                if (control is MenuStrip m && IdiomaManager.GetInstance().PrimeraVez == true) //solo cambia el menustrip la primera vez o cuando cambia el idioma
                {
                    foreach (ToolStripMenuItem item in m.Items)
                    {
                        if (item is ToolStripMenuItem toolStripMenuItem)
                        {
                            item.Text = IdiomaManager.GetInstance().ConseguirTexto(item.Name);

                            //al boton sesion le concatena el nombre del usuario
                            if(SessionManager.GetInstance.ObtenerUsuario() != null)
                            {
                                if (item.Name == "btnSesion") 
                                {
                                    item.Text = $"{IdiomaManager.GetInstance().ConseguirTexto(item.Name)}: {SessionManager.GetInstance.ObtenerUsuario().NombreUsuario}"; 
                                }
                            }

                            CambiarIdiomaMenuStrip(toolStripMenuItem.DropDownItems, frm);
                        }
                    }
                }




                if (control.Controls.Count > 0)
                {
                    ActualizarControles(control);
                }
            }
        }

        private static void CambiarIdiomaMenuStrip(ToolStripItemCollection items, Control frm)
        {
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripMenuItem item1)
                {
                    item.Text = IdiomaManager.GetInstance().ConseguirTexto(item.Name);

                    CambiarIdiomaMenuStrip(item1.DropDownItems, frm);
                }
            }
        }


    }
}
