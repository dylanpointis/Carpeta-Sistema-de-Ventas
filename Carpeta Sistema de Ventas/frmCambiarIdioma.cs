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
            IdiomaManager.ActualizarControles(this);
        }

        private void btnCambiarIdioma_Click(object sender, EventArgs e)
        {
            if(cmbIdioma.SelectedItem != null)
            {
                if (cmbIdioma.Text == IdiomaManager.GetInstance().ConseguirTexto("esp"))
                {
                    IdiomaManager.GetInstance().PrimeraVez = true; //esto es para que traduzca el menuStrip
                    SessionManager.IdiomaActual = "esp";
                    IdiomaManager.GetInstance().PrimeraVez = false;  //una vez traducido lo pone en false asi no se traduce de vuelta al cambiar de form
                }
                else
                {
                    IdiomaManager.GetInstance().PrimeraVez = true;
                    SessionManager.IdiomaActual = "eng";
                    IdiomaManager.GetInstance().PrimeraVez = false;
                }

                CargarComboBox();
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccionarIdioma")); }
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
