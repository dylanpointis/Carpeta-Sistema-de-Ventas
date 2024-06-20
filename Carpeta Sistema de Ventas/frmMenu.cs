using BE;
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
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        class producto
        {
            public string nombre { get; set; }
            public float precio { get; set; }
        };


        private void frmMenu_Load(object sender, EventArgs e)
        {
            BEUsuario user = SessionManager.GetInstance.ObtenerUsuario();
            btnSesion.Text = $"Sesión: {user.NombreUsuario}";
        }
        Form formActivo = new Form();
        private void AbrirForm(Form form) //FUNCION PARA ABRIR FORMS DENTRO DEL MDIPARENT
        {
            if (formActivo != null)
            {
                formActivo.Close();
                if (formActivo is IObserver observer)
                {
                    IdiomaManager.GetInstance().Quitar(observer);
                }
            }
            formActivo = form;
            formActivo.MdiParent = this;
            formActivo.Dock = DockStyle.Fill;
            formActivo.ControlBox = false;
            formActivo.FormBorderStyle = FormBorderStyle.None;
            formActivo.Text = "";


            formActivo.Show();
        }

        private void gestiónDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGestionUsuario frm = new frmGestionUsuario();
            AbrirForm(frm);
        }
        private void cambiarClaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCambiarClave form = new frmCambiarClave(this);
            AbrirForm(form);
        }
        private void generarFacturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGenerarFactura form = new frmGenerarFactura();
            AbrirForm(form);
        }

        private void cambiarIdiomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCambiarIdioma frmCambiarIdioma = new frmCambiarIdioma();
            AbrirForm(frmCambiarIdioma);
        }



        private void gestiónDePerfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGestionRoles frmCambiarIdioma = new frmGestionRoles();
            AbrirForm(frmCambiarIdioma);
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMaestroProducto frmMaestroProducto = new frmMaestroProducto();
            AbrirForm(frmMaestroProducto);
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMaestroClientes frmMaestroClientes = new frmMaestroClientes();
            AbrirForm(frmMaestroClientes);
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Desea cerrar sesión?", "Cerrar sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                if (SessionManager.GetInstance.ObtenerUsuario() != null)
                {
                    SessionManager.GetInstance.LogOut();
                    this.Close();
                }
                else { MessageBox.Show("No hay una sesion iniciada"); }
            }

        }

        private void iniciarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BEUsuario user = SessionManager.GetInstance.ObtenerUsuario();
            if (user != null) 
            {
                MessageBox.Show("Ya hay una sesión iniciada");
            }
            else { this.Close(); }
        }
    }
}
