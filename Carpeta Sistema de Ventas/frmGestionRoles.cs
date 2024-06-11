using BLL;
using Services.Composite;
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
    public partial class frmGestionRoles : Form
    {
        public frmGestionRoles()
        {
            InitializeComponent();
        }
        BLLPermiso bllPermiso = new BLLPermiso();
        BLLRol bllRol = new BLLRol();

        private void frmGestionRoles_Load(object sender, EventArgs e)
        {
            ActualizarListaPermisos();
        }

        private void ActualizarListaPermisos()
        {
            List<Permiso> list = bllPermiso.TraerListaPermisos();
            listBoxPermisos.Items.Clear();
            foreach(Permiso permiso in list)
            {
                listBoxPermisos.Items.Add($"{permiso.Id} - {permiso.Nombre}");
            }
        }
    }
}
