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
    public partial class frmRespaldo : Form, IObserver
    {
        public frmRespaldo()
        {
            InitializeComponent(); 
            IdiomaManager.GetInstance().archivoActual = "frmRespaldo";
            IdiomaManager.GetInstance().Agregar(this);
        }

        private BLLRespaldo bllRespaldo = new BLLRespaldo();
        private BLLEvento bllEv = new BLLEvento();


        private void frmRespaldo_Load(object sender, EventArgs e)
        {
            btnRutaBackUp.Text = "";
            btnRutaRestore.Text = "";
        }


        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }


        #region BackUp
        private void btnRutaBackUp_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog buscadorCarpeta = new FolderBrowserDialog())
            {
                if (buscadorCarpeta.ShowDialog() == DialogResult.OK)
                {
                    txtBackupRuta.Text = buscadorCarpeta.SelectedPath;
                }
            }
        }

        private void btnRealizarBackUp_Click(object sender, EventArgs e)
        {
            if(txtBackupRuta.Text != "")
            {
                try
                {
                    bllRespaldo.RealizarBackUp(txtBackupRuta.Text);
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exitoBackUp"),"", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBackupRuta.Text = "";

                    bllEv.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Respaldos", "Backup realizado", 1, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("errorBackUp") + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccioneRutaBackUp"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        #endregion

        #region Restore
        private void btnRutaRestore_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog buscadorArchivo = new OpenFileDialog())
            {
                buscadorArchivo.Filter = "SQL Backup Files (*.bak)|*.bak";
                if (buscadorArchivo.ShowDialog() == DialogResult.OK)
                {
                    txtRestoreRuta.Text = buscadorArchivo.FileName;
                }
            }
        }

        private void btnRealizarRestore_Click(object sender, EventArgs e)
        {
            if(txtRestoreRuta.Text != "")
            {
                try
                {
                    bllRespaldo.RealizarRestore(txtRestoreRuta.Text);
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exitoRestore"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRestoreRuta.Text = "";


                    bllEv.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Respaldos", "Restore realizado", 1, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));
                }
                catch (Exception ex) { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("errorRestore") + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); } 
                }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccioneRutaRestore"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        #endregion
    }
}
