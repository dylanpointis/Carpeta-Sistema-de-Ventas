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
            grillaEventos.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvId"); ;
            grillaEventos.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("textoNombreUsuario");
            grillaEventos.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("textoModulo");
            grillaEventos.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("textoEvento"); ;
            grillaEventos.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("textoCriticidad"); ;
            grillaEventos.Columns[5].Name = IdiomaManager.GetInstance().ConseguirTexto("textoFecha"); ;
            grillaEventos.Columns[6].Name = IdiomaManager.GetInstance().ConseguirTexto("textoHora"); ;

            grillaEventos.Columns[0].Width = 40;
            grillaEventos.Columns[4].Width = 40;
            grillaEventos.Columns[5].Width = 50;
            grillaEventos.Columns[6].Width = 50;
            ActualizarGrilla();
            LlenarComboBox();


            cmbModulo.SelectedItem = null;
            cmbEvento.SelectedItem = null;
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

            string modulo = "";
            string evento = "";


            if (cmbModulo.SelectedValue != null)
            {
                modulo = cmbModulo.SelectedValue.ToString();
            }
            else { modulo = ""; }

            if (cmbEvento.SelectedValue != null)
            {
                evento = cmbEvento.SelectedValue.ToString();
            }
            else { evento = ""; }


            listaEventos = bllEvento.FiltrarEventos(txtNombreUsuario.Text, modulo, evento, cmbCriticidad.Text, fechaInicial, fechaFinal);
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
            cmbModulo.SelectedItem = null;
            cmbEvento.SelectedItem = null;
            cmbCriticidad.SelectedItem = null;
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
                txtNombreUsuario.Text = user.NombreUsuario;
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string filas = "";
            foreach(DataGridViewRow row in grillaEventos.Rows)
            {
                string idevento = row.Cells[0].Value.ToString();
                string nombreusuario = row.Cells[1].Value.ToString();
                string modulo = row.Cells[2].Value.ToString();
                string eventodesc = row.Cells[3].Value.ToString();
                int criticidad = Convert.ToInt16(row.Cells[4].Value);
                string fecha = row.Cells[5].Value.ToString();
                string hora = row.Cells[6].Value.ToString();


                filas += "<tr>";
                filas += "<td>" + idevento + "</td>";
                filas += "<td>" + nombreusuario + "</td>";
                filas += "<td>" + modulo + "</td>";
                filas += "<td>" + eventodesc + "</td>";
                filas += "<td>" + criticidad + "</td>";
                filas += "<td>" + fecha + "</td>";
                filas += "<td>" + hora + "</td>";
                filas += "</tr>";
            }

            SaveFileDialog guardarArchivo = new SaveFileDialog();

            guardarArchivo.Filter = "PDF Files (*.pdf)|*.pdf";
            guardarArchivo.FileName = "Eventos-" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".pdf";

            string paginahtml = Properties.Resources.htmlauditoriaevento.ToString();


            paginahtml = paginahtml.Replace("@FILAS", filas);
            paginahtml = paginahtml.Replace("@auditoriaFecha", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
 


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
            //Crear el DataTable para cargar el comboBox
            DataTable dt = new DataTable();
            dt.Columns.Add("Texto"); // La columna para el texto a mostrar
            dt.Columns.Add("Valor"); // La columna para el valor real

            dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Sesiones"), "Sesiones");
            dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Gestión Usuarios"), "Gestión Usuarios");
            dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Gestión Perfiles"), "Gestión Perfiles");
            dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Ventas"), "Ventas");
            dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Productos"), "Productos");
            dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Clientes"), "Clientes");
            dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Respaldos"), "Respaldos");
            dt.Rows.Add("-", "");

            cmbModulo.DataSource = dt;
            cmbModulo.DisplayMember = "Texto"; // El texto que se mostrará
            cmbModulo.ValueMember = "Valor";   // El valor real

        }



        private void txtEvento_DropDown(object sender, EventArgs e)
        {
            if (cmbModulo.Text != "")
            {
                //Datatable para almacenar los eventos con su display y value real
                DataTable dt = new DataTable();
                dt.Columns.Add("Texto"); //texto mostrado en el combo
                dt.Columns.Add("Valor"); //valor real



                cmbEvento.DataSource= null;
                string modulo = cmbModulo.SelectedValue.ToString();

                switch (modulo)
                {
                    case "Sesiones":
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Inicio sesión"), "Inicio sesión");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Cierre sesión"), "Cierre sesión");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Cambio de clave"), "Cambio de clave");
                        break;
                    case "Gestión Usuarios":

                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Usuario creado"), "Usuario creado");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Usuario modificado"), "Usuario modificado");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Usuario eliminado"), "Usuario eliminado");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Usuario activado"), "Usuario activado");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Usuario desbloqueado"), "Usuario desbloqueado");
                        break;
                    case "Gestión Perfiles":
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Familia eliminada"), "Familia eliminada");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Familia creada"), "Familia creada");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Familia modificada"), "Familia modificada");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Perfil eliminado"), "Perfil eliminado");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Perfil creado"), "Perfil creado");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Perfil modificado"), "Perfil modificado");
                        break;
                    case "Ventas":
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Factura generada"), "Factura generada");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Impresión de factura"), "Impresión de factura");
                        break;
                    case "Productos":
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Producto creado"), "Producto creado");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Producto eliminado"), "Producto eliminado");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Producto habilitado"), "Producto habilitado");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Producto modificado"), "Producto modificado");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Stock reducidoo"), "Stock reducido");
                        break;
                    case "Clientes":
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Cliente creado"), "Cliente creado");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Cliente eliminado"), "Cliente eliminado");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Cliente habilitado"), "Cliente habilitado");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Cliente modificado"), "Cliente modificado");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Archivo serializado"), "Archivo serializado");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Archivo deserializado"), "Archivo deserializado");
                        break;
                    case "Respaldos":
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Backup realizado"), "Backup realizado");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Restore realizado"), "Restore realizado");
                        break;
                    default:
                        cmbEvento.DataSource = null;
                        break;
                }
                dt.Rows.Add("-", "");

                cmbEvento.DataSource = dt;
                cmbEvento.DisplayMember = "Texto";  // Lo que el usuario ve (traducido)
                cmbEvento.ValueMember = "Valor";      // El valor real (en español o código
            }
        }

        private void cmbModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbEvento.SelectedItem = null;
        }
    }
}
