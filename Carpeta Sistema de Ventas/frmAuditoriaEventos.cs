﻿using BE;
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
            fechaFin.MaxDate = DateTime.Today; fechaInicio.MaxDate = DateTime.Today;
            IdiomaManager.GetInstance().archivoActual = "frmAuditoriaEventos";
            IdiomaManager.GetInstance().Agregar(this);
            listaEventos = bllEvento.TraerListaEventos();
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
            grillaEventos.Rows.Clear();
            foreach (Evento ev in listaEventos)
            {
                string ModuloTraducido = IdiomaManager.GetInstance().ConseguirTexto(ev.Modulo);
                string EventoTraducido = IdiomaManager.GetInstance().ConseguirTexto(ev.evento);
                grillaEventos.Rows.Add(ev.CodEvento,ev.NombreUsuario, ModuloTraducido, EventoTraducido, ev.Criticidad,ev.Fecha,ev.Hora);
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            listaEventos = bllEvento.TraerListaEventos();
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
            ActualizarGrilla();

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            listaEventos = bllEvento.TraerListaEventos();
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
            List<Evento> lista = new List<Evento>();

            foreach (DataGridViewRow row in grillaEventos.Rows)
            {
                string idevento = row.Cells[0].Value.ToString();
                string nombreusuario = row.Cells[1].Value.ToString();
                string modulo = row.Cells[2].Value.ToString();
                string eventodesc = row.Cells[3].Value.ToString();
                int criticidad = Convert.ToInt16(row.Cells[4].Value);
                string fecha = row.Cells[5].Value.ToString();
                string hora = row.Cells[6].Value.ToString();

                Evento ev = new Evento(nombreusuario, modulo, eventodesc, criticidad);
                ev.CodEvento = Convert.ToInt32(idevento);
                ev.Fecha = fecha;
                ev.Hora = hora;
                lista.Add(ev);
            }

            string paginahtml = Properties.Resources.htmlauditoriaevento.ToString();

            Reportes.GenerarReporteEventos(lista, paginahtml, Properties.Resources.logo);
        }

        private void fechaInicio_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaInicial = fechaInicio.Value;
            DateTime fechaFinal = fechaFin.Value;

            if(fechaInicial > fechaFinal) //La fecha inicial no puede ser mayor a la final
            {  
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("fechaInicial"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fechaInicio.Value = fechaFinal;
            }
        }

        private void fechaFin_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaInicial = fechaInicio.Value;
            DateTime fechaFinal = fechaFin.Value;

            if (fechaFinal < fechaInicial) //La fecha final no puede ser menor a la inicial
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("fechaFinal"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Compras"), "Compras");
            dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Proveedores"), "Proveedores");
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
                    case "Compras":
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Solicitud de cotización generada"), "Solicitud de cotización generada");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Orden de compra generada"), "Orden de compra generada");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Productos de orden recibidos"), "Productos de orden recibidos");
                        break;
                    case "Proveedores":
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Proveedor creado"), "Proveedor creado");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Proveedor habilitado"), "Proveedor habilitado");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Proveedor eliminado"), "Proveedor habilitado");
                        dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Proveedor modificado"), "Proveedor habilitado");
                        break;
                    default:
                        cmbEvento.DataSource = null;
                        break;
                }
                dt.Rows.Add("-", "");

                cmbEvento.DataSource = dt;
                cmbEvento.DisplayMember = "Texto";  // Lo que el usuario ve (traducido)
                cmbEvento.ValueMember = "Valor";      // El valor real (en español)
            }
        }

        private void cmbModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbEvento.SelectedItem = null;
        }
    }
}
