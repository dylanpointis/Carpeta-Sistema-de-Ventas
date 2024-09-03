using BLL;
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
    public partial class frmRespaldo : Form
    {
        public frmRespaldo()
        {
            InitializeComponent();
        }

        private BLLRespaldo bllRespaldo = new BLLRespaldo();


        private void frmRespaldo_Load(object sender, EventArgs e)
        {

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
                    MessageBox.Show("BackUp guardado exitosamente");
                    txtBackupRuta.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al realizar el back up: {ex.Message}");
                }
            }
            else { MessageBox.Show("Selecciona una ruta para guardar el archivo Back Up en el equipo"); }
        }
        #endregion

        #region Restore

        private void btnRestoreRuta_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Restauración realizada exitosamente");
                    txtRestoreRuta.Text = "";

                }catch(Exception ex) { MessageBox.Show($"Error al realizar la restauración: {ex.Message}"); }
            }
            else { MessageBox.Show("Seleccione la ruta del archivo .bak para realizar la restauración"); }

        }
        #endregion
    }
}
