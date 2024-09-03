using BE;
using BE.Composite;
using BLL;
using Services;
using Services.Observer;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmMenu : Form, IObserver
    {
        public frmMenu()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().archivoActual = "frmMenu";
            IdiomaManager.GetInstance().Agregar(this);
            IdiomaManager.GetInstance().PrimeraVez = false;
        }


        BLLFamilia bllFamilia = new BLLFamilia();

        private void frmMenu_Load(object sender, EventArgs e)
        {
            BEUsuario user = SessionManager.GetInstance.ObtenerUsuario();
            btnSesion.Text = IdiomaManager.GetInstance().ConseguirTexto("btnSesion") + ": " + SessionManager.GetInstance.ObtenerUsuario().NombreUsuario;


            //Deshabilita los controles
            Admin.Enabled = false; Maestros.Enabled = false; Usuarios.Enabled = false; Ventas.Enabled = false; Compras.Enabled = false; Reportes.Enabled = false; Ayuda.Enabled = false;


            /*Se fija los permisos del usuario*/
            List<Componente> listaHijosFamilia = new List<Componente>();
            foreach (Componente componente in user.listaPermisosRol)
            {
                if (componente is Permiso) //si es un permiso simple deshabilita el bootn
                {
                    foreach (ToolStripMenuItem control in this.menuStrip1.Items)
                    {
                        if (control.Name == componente.Nombre)
                        {
                            control.Enabled = true;
                        }
                    }
                }

                if (componente is Familia) // si es familia recorre sus hijos y deshabilita los botones
                {
                    listaHijosFamilia = bllFamilia.TraerListaHijos(componente.Id);

                    foreach (Componente hijo in listaHijosFamilia)
                    {
                        foreach (ToolStripMenuItem control in this.menuStrip1.Items)
                        {
                            if (control.Name == hijo.Nombre)
                            {
                                control.Enabled = true;
                            }
                            foreach (ToolStripMenuItem item in control.DropDownItems)
                            {
                                if (item.Name == hijo.Nombre)
                                {
                                    item.Enabled = true;
                                }
                            }
                        }
                    }
                }
            }

        }




        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
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
            frmGestionRoles frmRoles = new frmGestionRoles();
            AbrirForm(frmRoles);
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


        private void generarReporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportes frm = new frmReportes();
            AbrirForm(frm);
        }

        private void Ayuda_Click(object sender, EventArgs e)
        {
            frmAyuda ayuda = new frmAyuda();
            AbrirForm(ayuda);
        }
        private void eventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAuditoriaEventos frm = new frmAuditoriaEventos();
            AbrirForm(frm);
        }

        private void respaldosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new frmRespaldo());
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("deseaCerrar"), IdiomaManager.GetInstance().ConseguirTexto("cerrar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                if (SessionManager.GetInstance.ObtenerUsuario() != null)
                {
                    BLLEvento bLLEvento = new BLLEvento();
                    bLLEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Sesiones", "Cierre sesión", 1, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));
                    SessionManager.GetInstance.LogOut();
                    this.Close();
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noHaySesion")); }
            }

        }

        private void productosCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new frmAuditoriaCambios());
        }


        private void iniciarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BEUsuario user = SessionManager.GetInstance.ObtenerUsuario();
            if (user != null)
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("haySesion"));
            }
            else { this.Close(); }
        }

    }
}
