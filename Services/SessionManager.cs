using BE;
using Services.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SessionManager
    {
        private static SessionManager _instance; //Tiene que ser STATIC. Los atributos static son compartidos en todas las instancias de la clase. Mantienen el valor
        private BEUsuario _usuario;
        private static string _idiomaActual;
        private static Object _lock = new Object();


        private SessionManager() { }

        public static SessionManager GetInstance //Tiene que ser STATIC. Los metodos static pueden ser invocados sin crear un objeto de la clase SessionManager ()
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SessionManager();
                    }
                }
                return _instance;
            }
        }

        public void LogIn(BEUsuario user)
        {
            if (_usuario == null)
            {
                _usuario = user;
            }
        }

        public BEUsuario ObtenerUsuario()
        {
            return _usuario;
        }

        public void LogOut()
        {
            _usuario = null;
            _instance = null;
        }

        public static string IdiomaActual
        {
            get { return _idiomaActual; }
            set
            {
                _idiomaActual = value;
                IdiomaManager.GetInstance().CargarIdioma();
                IdiomaManager.GetInstance().Notificar();
            }
        }
    }
}
