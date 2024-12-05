using BLL;
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
    public partial class frmRepararDigitoVerificador : Form, IObserver
    {
        BLLDigitoVerificador bllDV = new BLLDigitoVerificador();
        BLLRespaldo bllRespaldo = new BLLRespaldo();
        BLLEvento bllEvento = new BLLEvento();

        string NombreUsuarioAdmin;
        public frmRepararDigitoVerificador(string nombreUsuarioAdmin)
        {
            NombreUsuarioAdmin = nombreUsuarioAdmin;
            IdiomaManager.GetInstance().archivoActual = "frmRepararDigitoVerificador";
            IdiomaManager.GetInstance().Agregar(this);
            InitializeComponent();
        }

        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        private void btnRecalcularDV_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("estaSeguroRecalcular"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                try
                {
                    bllDV.RecalcularDV();
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exitoRecalcularDV"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
        private void btnRestaurarBD_Click(object sender, EventArgs e)
        {
            string ruta = "";
            using (OpenFileDialog buscadorArchivo = new OpenFileDialog())
            {
                buscadorArchivo.Filter = "SQL Backup Files (*.bak)|*.bak"; 
                if (buscadorArchivo.ShowDialog() == DialogResult.OK)
                {
                    ruta = buscadorArchivo.FileName;
                    try
                    {
                        bllRespaldo.RealizarRestore(ruta);
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exitoRestore"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex) { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("errorRestore") + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
