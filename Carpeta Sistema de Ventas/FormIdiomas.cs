using Services.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carpeta_Sistema_de_Ventas
{
    public static class FormIdiomas
    {
        public static void ActualizarControles(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                if (control is Button || control is Label)
                {
                    control.Text = IdiomaManager.GetInstance().ConseguirTexto(control.Name);
                }

                if (control.Controls.Count > 0)
                {
                    ActualizarControles(control);
                }
            }
        }

        public static string ConseguirTexto(string key)
        {
            return IdiomaManager.GetInstance().ConseguirTexto(key);
        }
    }
}
