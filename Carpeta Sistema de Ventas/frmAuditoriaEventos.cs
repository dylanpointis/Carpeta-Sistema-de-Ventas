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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmAuditoriaEventos : Form, IObserver
    {
        public frmAuditoriaEventos()
        {
            InitializeComponent();

            fechaInicio.Format = DateTimePickerFormat.Custom;
            fechaInicio.CustomFormat = Application.CurrentCulture.DateTimeFormat.ShortDatePattern;
           
            fechaFin.Format = DateTimePickerFormat.Custom;
            fechaFin.CustomFormat = Application.CurrentCulture.DateTimeFormat.ShortDatePattern;
            fechaFin.Value = DateTime.Today; fechaInicio.Value = DateTime.Today.AddDays(-3);


            IdiomaManager.GetInstance().archivoActual = "frmAuditoriaEventos";
            IdiomaManager.GetInstance().Agregar(this);

        }
        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }





        private BLLEvento bllEvento = new BLLEvento();
        private List<Evento> listaEventos= new List<Evento>();
        private BLLUsuario bllUsuario = new BLLUsuario();
        private void frmAuditoriaEventos_Load(object sender, EventArgs e)
        {
            grillaEventos.ColumnCount = 7;
            grillaEventos.Columns[0].Name = "Id evento";
            grillaEventos.Columns[1].Name = "Nombre usuario";
            grillaEventos.Columns[2].Name = "Modulo";
            grillaEventos.Columns[3].Name = "Evento";
            grillaEventos.Columns[4].Name = "Criticidad";
            grillaEventos.Columns[5].Name = "Fecha";
            grillaEventos.Columns[6].Name = "Hora";

            grillaEventos.Columns[0].Width = 40;
            grillaEventos.Columns[4].Width = 40;
            grillaEventos.Columns[5].Width = 50;
            grillaEventos.Columns[6].Width = 50;
            ActualizarGrilla();
            LlenarComboBox();
        }


        private void ActualizarGrilla()
        {
            listaEventos = bllEvento.TraerListaEventos();
            grillaEventos.Rows.Clear();
            foreach (Evento ev in listaEventos)
            {
                grillaEventos.Rows.Add(ev.IdEvento,ev.NombreUsuario,ev.Modulo,ev.evento,ev.Criticidad,ev.Fecha,ev.Hora);
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarGrilla();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string fechaInicial = fechaInicio.Value.ToString("yyyy-MM-dd");
            string fechaFinal = fechaFin.Value.ToString("yyyy-MM-dd");
            


            listaEventos = bllEvento.FiltrarEventos(txtNombreUsuario.Text, txtModulo.Text, txtEvento.Text, txtCriticidad.Text, fechaInicial, fechaFinal);
            grillaEventos.Rows.Clear();

            foreach (Evento ev in listaEventos)
            {
                grillaEventos.Rows.Add(ev.IdEvento, ev.NombreUsuario, ev.Modulo, ev.evento, ev.Criticidad, ev.Fecha, ev.Hora);
            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ActualizarGrilla();
            fechaFin.Value = DateTime.Today; fechaInicio.Value = DateTime.Today.AddDays(-3);
            txtNombreUsuario.Text = "";
            txtModulo.SelectedItem = null;
            txtEvento.SelectedItem = null;
            txtCriticidad.SelectedItem = null;
        }

        private void grillaEventos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(grillaEventos.SelectedRows.Count > 0)
            {
                string nombreusuario = grillaEventos.CurrentRow.Cells[1].Value.ToString();
                BEUsuario user = bllUsuario.ValidarUsuario(nombreusuario, 0, "");

                lblNombre.Text = IdiomaManager.GetInstance().ConseguirTexto("lblNombre")+ " " + user.Nombre;
                lblApellido.Text = IdiomaManager.GetInstance().ConseguirTexto("lblApellido") + " " + user.Apellido;
                lblDNI.Text = IdiomaManager.GetInstance().ConseguirTexto("lblDNI") + " " + user.DNI;
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (grillaEventos.SelectedRows.Count > 0)
            {
                string nombreusuario = grillaEventos.CurrentRow.Cells[1].Value.ToString();
                string modulo = grillaEventos.CurrentRow.Cells[2].Value.ToString();
                string eventodesc = grillaEventos.CurrentRow.Cells[3].Value.ToString();
                int criticidad = Convert.ToInt16(grillaEventos.CurrentRow.Cells[4].Value);
                string fecha = grillaEventos.CurrentRow.Cells[5].Value.ToString();
                string hora = grillaEventos.CurrentRow.Cells[6].Value.ToString();

                Evento evento = new Evento(nombreusuario,modulo,eventodesc, criticidad,fecha,hora);
                evento.IdEvento = Convert.ToInt32(grillaEventos.CurrentRow.Cells[0].Value);

                SaveFileDialog guardarArchivo = new SaveFileDialog();
                guardarArchivo.FileName = evento.IdEvento + "-" + DateTime.Now.ToString("yyyy-MM-dd") + ".pdf";


                string paginahtml = Properties.Resources.htmlauditoriaevento.ToString();


                paginahtml = paginahtml.Replace("@auditoriaFecha", DateTime.Today.ToString("yyyy-MM-dd"));
                paginahtml = paginahtml.Replace("@IdEvento", evento.IdEvento.ToString());
                paginahtml = paginahtml.Replace("@NombreUsuario", evento.NombreUsuario);
                paginahtml = paginahtml.Replace("@Modulo", evento.Modulo);
                paginahtml = paginahtml.Replace("@Evento", evento.evento);
                paginahtml = paginahtml.Replace("@Criticidad", evento.Criticidad.ToString());
                paginahtml = paginahtml.Replace("@FechaEvento", evento.Fecha);
                paginahtml = paginahtml.Replace("@Hora", evento.Hora);


                paginahtml = paginahtml.Replace("@label2", IdiomaManager.GetInstance().ConseguirTexto("label2"));
                paginahtml = paginahtml.Replace("@textoFecha", IdiomaManager.GetInstance().ConseguirTexto("textoFecha"));
                paginahtml = paginahtml.Replace("@textoDetalleEvento", IdiomaManager.GetInstance().ConseguirTexto("textoDetalleEvento"));
                paginahtml = paginahtml.Replace("@textoIdEvento", IdiomaManager.GetInstance().ConseguirTexto("textoIdEvento"));
                paginahtml = paginahtml.Replace("@textoNombreUsuario", IdiomaManager.GetInstance().ConseguirTexto("textoNombreUsuario"));
                paginahtml = paginahtml.Replace("@textoModulo", IdiomaManager.GetInstance().ConseguirTexto("textoModulo"));
                paginahtml = paginahtml.Replace("@textoEvento", IdiomaManager.GetInstance().ConseguirTexto("textoEvento"));
                paginahtml = paginahtml.Replace("@textoCriticidad", IdiomaManager.GetInstance().ConseguirTexto("textoCriticidad"));
                paginahtml = paginahtml.Replace("@textoFecha", IdiomaManager.GetInstance().ConseguirTexto("textoFecha"));
                paginahtml = paginahtml.Replace("@textoHora", IdiomaManager.GetInstance().ConseguirTexto("textoHora"));



                if (guardarArchivo.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(guardarArchivo.FileName, FileMode.Create))
                    {
                        Document pdf = new Document(PageSize.A4, 25, 25, 25, 25);

                        PdfWriter escritor = PdfWriter.GetInstance(pdf, stream);

                        pdf.Open();
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.logo, System.Drawing.Imaging.ImageFormat.Png);
                        img.ScaleToFit(80, 60);
                        img.Alignment = iTextSharp.text.Image.UNDERLYING;
                        img.SetAbsolutePosition(pdf.Right - 60, pdf.Top - 60);
                        pdf.Add(img);

                        using (StringReader lector = new StringReader(paginahtml))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(escritor, pdf, lector);
                        }


                        pdf.Close();
                        stream.Close();
                    }
                }
            }
        }

        private void fechaInicio_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaInicial = fechaInicio.Value;
            DateTime fechaFinal = fechaFin.Value;

            if(fechaInicial > fechaFinal) //La fecha inicial no puede ser mayor a la final
            {  
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("fechaInicial"));
                fechaInicio.Value = fechaFinal;
            }
        }

        private void fechaFin_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaInicial = fechaInicio.Value;
            DateTime fechaFinal = fechaFin.Value;

            if (fechaFinal < fechaInicial) //La fecha final no puede ser menor a la inicial
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("fechaFinal"));
                fechaFin.Value = fechaInicial;
            }
        }


        private void LlenarComboBox()
        {

            txtModulo.Items.Add("Sesiones");
            txtModulo.Items.Add("Gestión Usuarios");
            txtModulo.Items.Add("Gestión Perfiles");
            txtModulo.Items.Add("Ventas");
            txtModulo.Items.Add("Productos");



            txtEvento.Items.Add("Inicio sesión");
            txtEvento.Items.Add("Cierre sesión");
            txtEvento.Items.Add("Factura generada");
        }



        private void txtEvento_DropDown(object sender, EventArgs e)
        {
            if (txtModulo.Text != "")
            {
                txtEvento.Items.Clear();
                string modulo = txtModulo.Text;

                switch (modulo)
                {
                    case "Sesiones":
                        txtEvento.Items.Add("Inicio sesión");
                        txtEvento.Items.Add("Cierre sesión");
                        txtEvento.Items.Add("Cambio de clave");
                        break;
                    case "Gestión Usuarios":
                        txtEvento.Items.Add("Usuario creado");
                        txtEvento.Items.Add("Usuario modificado");
                        txtEvento.Items.Add("Usuario eliminado");
                        txtEvento.Items.Add("Usuario activado");
                        txtEvento.Items.Add("Usuario desbloqueado");
                        break;
                    case "Gestión Perfiles":
                        txtEvento.Items.Add("Familia eliminada");
                        txtEvento.Items.Add("Familia creada");
                        txtEvento.Items.Add("Familia modificada");
                        txtEvento.Items.Add("Perfil eliminado");
                        txtEvento.Items.Add("Perfil creado");
                        txtEvento.Items.Add("Perfil modificado");
                        break;
                    case "Ventas":
                        txtEvento.Items.Add("Factura generada");
                        break;
                    case "Productos":
                        txtEvento.Items.Add("Producto creado");
                        txtEvento.Items.Add("Producto eliminado");
                        txtEvento.Items.Add("Producto modificado");
                        break;
                    case "Clientes":
                        txtEvento.Items.Add("Cliente creado");
                        txtEvento.Items.Add("Cliente eliminado");
                        txtEvento.Items.Add("Cliente modificado");
                        break;
                }
            }
        }
    }
}
