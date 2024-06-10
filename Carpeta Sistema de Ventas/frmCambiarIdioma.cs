using Services;
using Services.Observer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmCambiarIdioma : Form, IObserver
    {
        public frmCambiarIdioma()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().Agregar(this);
            IdiomaManager.GetInstance().archivoActual = "frmCambiarIdioma";
        }

        public void ActualizarIdioma()
        {
            FormIdiomas.ActualizarControles(this);
        }

        private void btnCambiarIdioma_Click(object sender, EventArgs e)
        {
            if (SessionManager.IdiomaActual == "eng")
            {
                SessionManager.IdiomaActual = "esp";
            }
            else
            {
                SessionManager.IdiomaActual = "eng";
            }
        }

        private void frmCambiarIdioma_Load(object sender, EventArgs e)
        {

        }
    }
}
