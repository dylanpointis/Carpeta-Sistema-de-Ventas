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

            IdiomaManager.GetInstance().archivoActual = "frmCambiarIdioma";
            IdiomaManager.GetInstance().Agregar(this);
        }

        public void ActualizarObserver()
        {
            FormIdiomas.ActualizarControles(this);
        }

        private void btnCambiarIdioma_Click(object sender, EventArgs e)
        {
            if(cmbIdioma.SelectedItem != null)
            {
                if (cmbIdioma.Text == IdiomaManager.GetInstance().ConseguirTexto("esp"))
                {
                    SessionManager.IdiomaActual = "esp";
                }
                else
                {
                    SessionManager.IdiomaActual = "eng";
                }

                CargarComboBox();
            }
            else { MessageBox.Show("Seleccione un idioma"); }
        }

        private void frmCambiarIdioma_Load(object sender, EventArgs e)
        {
            CargarComboBox();
        }

        private void CargarComboBox()
        {
            cmbIdioma.Items.Clear();
            cmbIdioma.Items.Add(IdiomaManager.GetInstance().ConseguirTexto("esp"));
            cmbIdioma.Items.Add(IdiomaManager.GetInstance().ConseguirTexto("eng"));
        }
    }
}
