using BE;
using BE.Composite;
using BLL;
using Services;
using Services.Observer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmMenu : Form, IObserver
    {
        public frmMenu()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().archivoActual = "frmMenu";
            IdiomaManager.GetInstance().Agregar((frmMenu)this);
            IdiomaManager.GetInstance().PrimeraVez = false;
        }


        BLLFamilia bllFamilia = new BLLFamilia();
        BEUsuario user;
        private void frmMenu_Load(object sender, EventArgs e)
        {
            user = SessionManager.GetInstance.ObtenerUsuario();
            btnSesion.Text = IdiomaManager.GetInstance().ConseguirTexto("btnSesion") + ": " + user.NombreUsuario;

            //deshabilita todos los controles inicialmente
            DeshabilitarControles();
            btnInicio.Enabled = true;
            SesionAyuda.Enabled = true;
            //recorre los permisos (permisos simples o familias) del rol usuario
            foreach (Componente componente in user.listaPermisosRol)
            {
                if (componente is Permiso)
                {
                    HabiilitarControl(componente.Nombre);
                }
                else if (componente is Familia)
                {
                    ProcesarFamilia((Familia)componente);
                }
            }
        }

        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles((frmMenu)this);
        }


        private void DeshabilitarControles()
        {
            foreach (ToolStripMenuItem control in menuStrip1.Items)
            {
                if(control.Name != "btnInicio") //Deshabilita todos menos el btnInicio
                {
                    control.Enabled = false;
                    foreach (ToolStripMenuItem item in control.DropDownItems)
                    {
                        item.Enabled = false;
                    }
                }
            }
        }

        private void HabiilitarControl(string nombreComponente)
        {
            foreach (ToolStripMenuItem control in menuStrip1.Items)
            {
                control.DropDownOpened += PonerTextoEnNegro;
                control.DropDownClosed += PonerTextoEnBlanco;

                //se fija si coincide el nombre del control con el permiso
                if (control.Name == nombreComponente)
                {
                    control.Enabled = true;
                }

                foreach (ToolStripMenuItem item in control.DropDownItems)
                {
                    //activa los botones de ayuda
                    if (item.Name == nombreComponente || item.Name == nombreComponente + "Ayuda")
                    {
                        item.Enabled = true;
                    }
                }
            }
        }


        private void PonerTextoEnNegro(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            item.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
        }
        private void PonerTextoEnBlanco(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            item.ForeColor = System.Drawing.Color.White;
        }

        private void ProcesarFamilia(Familia familia)
        {
            //Obtiene los hijos de la familia (permisos simples u otras familias)
            List<Componente> listaHijosFamilia = bllFamilia.TraerListaHijos(familia.Id);

            foreach (Componente hijo in listaHijosFamilia)
            {
                if (hijo is Permiso)
                {
                    HabiilitarControl(hijo.Nombre);
                }
                else if (hijo is Familia)
                {
                    //Funcion recursiva
                    ProcesarFamilia((Familia)hijo);
                }
            }

            //activar manualmente los botones de ayuda
           

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
            frmReportesVentas frm = new frmReportesVentas();
            AbrirForm(frm);
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

        private void generarSolicitudDeCotizaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new frmGenerarSolicitudCotizacion());
        }
        private void generarOrdenDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AbrirForm(new frmGenerarOrdenCompra());
        }

        private void corroborarRecepciónToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AbrirForm(new frmCorroborarRecepcion());
        }


        private void productosCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new frmAuditoriaCambios());
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new frmMaestroProveedores());
        }
        private void generarReporteComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new frmReporteCompras());
        }

        private void generarReporteInteligenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new frmReporteInteligente());
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("deseaCerrar"), IdiomaManager.GetInstance().ConseguirTexto("cerrar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                if (SessionManager.GetInstance.ObtenerUsuario() != null)
                {
                    BLLEvento bLLEvento = new BLLEvento();
                    bLLEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Sesiones", "Cierre sesión", 1));
                    SessionManager.GetInstance.LogOut();
                    this.Close();
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noHaySesion"), "", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }

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


        private void btnInicio_Click(object sender, EventArgs e)
        {
            if (formActivo != null)
                formActivo.Close();
        }

        private void btnAyudaVentas_Click(object sender, EventArgs e)
        {
            AbrirPDFAyuda("VENTASAyuda.pdf", Properties.Resources.VENTASAyuda);
        }

       

        private void AbrirPDFAyuda(string ruta, byte[] recursoPDF)
        {
            try
            {
                string nuevaRuta = Path.Combine(Path.GetTempPath(), ruta);

                File.WriteAllBytes(nuevaRuta, recursoPDF);

                Process.Start(new ProcessStartInfo(nuevaRuta) { UseShellExecute = true });
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void btnAyudaMaestroCliente_Click(object sender, EventArgs e)
        {
            AbrirPDFAyuda("MaestroClientes.pdf", Properties.Resources.MaestroClientes);
        }

        private void btnAyudaMaestroProveedor_Click(object sender, EventArgs e)
        {
            AbrirPDFAyuda("MaestroProveedor.pdf", Properties.Resources.MaestroProveedores);
        }

        private void btnAyudaMaestroProductos_Click(object sender, EventArgs e)
        {
            AbrirPDFAyuda("MaestroProductos.pdf", Properties.Resources.MaestroProductos);
        }

        private void btnAyudaMaestroProductosC_Click(object sender, EventArgs e)
        {
            AbrirPDFAyuda("MaestroProductosC.pdf", Properties.Resources.MaestroProductosC);
        }

        private void btnAyudaCambiarClave_Click(object sender, EventArgs e)
        {
            AbrirPDFAyuda("CambiarClave.pdf", Properties.Resources.CambiarClave);
        }

        private void btnAyudaCambiarIdioma_Click(object sender, EventArgs e)
        {
            AbrirPDFAyuda("CambiarIdioma.pdf", Properties.Resources.CambiarIdioma);
        }

        private void GenerarSolicitudCotizacionAyuda_Click(object sender, EventArgs e)
        {
            AbrirPDFAyuda("GenerarSolicitudCotizacion.pdf", Properties.Resources.GenerarSolicitudCotizacion);
        }

        private void GenerarOrdenCompraAyuda_Click(object sender, EventArgs e)
        {
            AbrirPDFAyuda("GenerarOrdenCompra.pdf", Properties.Resources.GenerarOrdenCompra);
        }

        private void CorroborarRecepcionAyuda_Click(object sender, EventArgs e)
        {
            AbrirPDFAyuda("CorroborarRecepcion.pdf", Properties.Resources.CorroborarRecepcion);
        }

        private void ReporteVentasAyuda_Click(object sender, EventArgs e)
        {
            AbrirPDFAyuda("ReporteVentas.pdf", Properties.Resources.ReporteVentas);
        }

        private void ReporteComprasAyuda_Click(object sender, EventArgs e)
        {
            AbrirPDFAyuda("ReporteCompras.pdf", Properties.Resources.ReporteCompras);
        }

        private void ReporteInteligenteAyuda_Click(object sender, EventArgs e)
        {
            AbrirPDFAyuda("ReporteInteligente.pdf", Properties.Resources.ReporteInteligente);
        }

        private void SesionAyuda_Click(object sender, EventArgs e)
        {
            AbrirPDFAyuda("SESIONAyuda.pdf", Properties.Resources.SESIONAyuda);
        }

        private void AdminAyuda_Click(object sender, EventArgs e)
        {
            AbrirPDFAyuda("ADMINAyuda.pdf", Properties.Resources.ADMINAyuda);
        }
    }
}
