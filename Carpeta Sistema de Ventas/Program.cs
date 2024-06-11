using BE;
using Services;
using Services.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carpeta_Sistema_de_Ventas
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IdiomaManager.GetInstance().archivoActual = "frmLogin";
            SessionManager.IdiomaActual = "esp";
            Application.Run(new frmLogin());

        }
    }
}
