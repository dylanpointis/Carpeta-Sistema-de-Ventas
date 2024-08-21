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
    public partial class frmAuditoriaEventos : Form
    {
        public frmAuditoriaEventos()
        {
            InitializeComponent();

            fechaInicio.Format = DateTimePickerFormat.Custom;
            fechaInicio.CustomFormat = Application.CurrentCulture.DateTimeFormat.ShortDatePattern;
            fechaFin.Format = DateTimePickerFormat.Custom;
            fechaFin.CustomFormat = Application.CurrentCulture.DateTimeFormat.ShortDatePattern;

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

            ActualizarGrilla();
            LlenarComboBox();
        }

        private void LlenarComboBox()
        {
            txtEvento.Items.Add("Inicio sesión");
            txtEvento.Items.Add("Cierre sesión");





            txtModulo.Items.Add("Login");
            txtModulo.Items.Add("Gestión Usuarios");
            txtModulo.Items.Add("Gestión Perfiles");
            txtModulo.Items.Add("Ventas");
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
            


            List<Evento> list = bllEvento.FiltrarEventos(txtNombreUsuario.Text, txtModulo.Text, txtEvento.Text, txtCriticidad.Text, fechaInicial, fechaFinal);
            grillaEventos.Rows.Clear();

            foreach (Evento ev in list)
            {
                grillaEventos.Rows.Add(ev.IdEvento, ev.NombreUsuario, ev.Modulo, ev.evento, ev.Criticidad, ev.Fecha, ev.Hora);
            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            fechaInicio.Value = DateTime.Today;
            fechaFin.Value = DateTime.Today;
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

                lblNombre.Text = "Nombre: "+ user.Nombre;
                lblApellido.Text = "Apellido: " + user.Apellido;
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
    }
}
