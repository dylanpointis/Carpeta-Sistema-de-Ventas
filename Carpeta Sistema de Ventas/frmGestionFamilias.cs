using BE.Composite;
using BE;
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
using Services.Observer;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmGestionFamilias : Form, IObserver
    {
        public frmGestionFamilias()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().archivoActual = "frmGestionFamilias";
            IdiomaManager.GetInstance().Agregar(this);
        }

        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        private BLLPermiso bllPermiso = new BLLPermiso();
        private BLLFamilia bllFamilia = new BLLFamilia();
        private BLLUsuario bllUsuario = new BLLUsuario();
        private Familia FamiliaConfigurada = new Familia();
        private EnumModoAplicar modoOperacion;


        private void frmGestionFamilias_Load(object sender, EventArgs e)
        {
            modoOperacion = EnumModoAplicar.Consulta;
            lblModoOperacion.Text = IdiomaManager.GetInstance().ConseguirTexto("lblModoOperacion");
            ActualizarListBoxPermisos();
            ActualizarComboBox();
        }

        private void ActualizarComboBox()
        {
            cmbFamilia.Items.Clear();
            List<Familia> listaFamilias = bllFamilia.TraerListaFamilias();
            foreach (var familia in listaFamilias)
            {
                if (familia is Familia)
                {
                    cmbFamilia.Items.Add($"{familia.Id} - {familia.Nombre}");
                }
            }
        }

        private void ActualizarListBoxPermisos()
        {
            listBoxPermisos.Items.Clear();
            List<Componente> listaPermisos = bllPermiso.TraerListaPermisos();
            foreach (var permiso in listaPermisos)
            {
                if (permiso is Permiso)
                {
                    listBoxPermisos.Items.Add($"{permiso.Id} - {permiso.Nombre} - {permiso.Tipo}");
                }
            }
        }

        private void btnAgregarPermiso_Click(object sender, EventArgs e)
        {
            if (listBoxPermisos.SelectedItems.Count > 0)
            {
                Componente permisoSeleccionado = TraerComponeneteDeListBox(listBoxPermisos);

                FamiliaConfigurada.AgregarHijo(permisoSeleccionado);
                ActualizarListBoxFamilia();

            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccionePermiso")); }
        }

        private void btnQuitarPermiso_Click(object sender, EventArgs e)
        {
            if (listBoxFamilia.SelectedItems.Count > 0)
            {
                Componente permisoSeleccionado = TraerComponeneteDeListBox(listBoxFamilia);

                FamiliaConfigurada.QuitarHijo(permisoSeleccionado);
                ActualizarListBoxFamilia();
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccioneQuitar")); }

        }

        private Componente TraerComponeneteDeListBox(ListBox listbox)
        {
            string[] partes = listbox.SelectedItem.ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            int id = int.Parse(partes[0].Trim());
            string nombre = partes[1].Trim();
            string tipo = partes[2].Trim();
            Componente componenteSeleccionado;
            if (tipo == "Simple")
            {
                componenteSeleccionado = new Permiso { Id = id, Nombre = nombre, Tipo = "Simple" };
            }
            else { componenteSeleccionado = new Familia { Id = id, Nombre = nombre, Tipo = "Familia" }; }
            return componenteSeleccionado;
        }

        private void ActualizarListBoxFamilia()
        {
            listBoxFamilia.Items.Clear();
            foreach (Componente permiso in FamiliaConfigurada.ObtenerHijos())
            {
                listBoxFamilia.Items.Add($"{permiso.Id} - {permiso.Nombre} - {permiso.Tipo}");
            }
        }


        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (txtNombreFamilia.Text != "")
            {
                Familia familia = bllFamilia.TraerListaFamilias().FirstOrDefault(r => r.Nombre == txtNombreFamilia.Text);
                if (familia == null)
                {
                    modoOperacion = EnumModoAplicar.Añadir;
                    lblModoOperacion.Text = IdiomaManager.GetInstance().ConseguirTexto("modoAñadir");

                    BloquearBotones();
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("yaOcupado")); }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("ingreseNombre")); txtNombreFamilia.Focus(); }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (cmbFamilia.SelectedItem != null)
            {
                string[] partes = cmbFamilia.SelectedItem.ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                int id = int.Parse(partes[0].Trim());
                string nombre = partes[1].Trim();
                txtNombreFamilia.Text = nombre;
                FamiliaConfigurada.Id = id;
                FamiliaConfigurada.Nombre = nombre;


                //Busca si algun usuario ya tiene la familia asignada a un Usuario
                BEUsuario yaEstaAsignado = null;
                List<BEUsuario> listaUsuarios = bllUsuario.TraerListaUsuarios();
                List<Componente> listaRol = bllFamilia.TraerListaPermisosRolSegunPermiso(id);
                foreach (var rol in listaRol)
                {
                    yaEstaAsignado = listaUsuarios.FirstOrDefault(u => u.Rol.Id == rol.Id);
                }
                if (yaEstaAsignado != null)
                {
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("yaEstaAsignadoModificar"));
                }
                else
                {
                    modoOperacion = EnumModoAplicar.Modificar;
                    lblModoOperacion.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoModificar")} {id}";
                    BloquearBotones();
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (cmbFamilia.SelectedItem != null)
            {
                string[] partes = cmbFamilia.SelectedItem.ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                int id = int.Parse(partes[0].Trim());

                FamiliaConfigurada.Id = id;

                BEUsuario yaEstaAsignado = null;
                List<BEUsuario> listaUsuarios = bllUsuario.TraerListaUsuarios();
                List<Componente> listaRol = bllFamilia.TraerListaPermisosRolSegunPermiso(id);
                foreach (var rol in listaRol)
                {
                    yaEstaAsignado = listaUsuarios.FirstOrDefault(u => u.Rol.Id == rol.Id);
                }
                if (yaEstaAsignado != null)
                {
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("yaEstaAsignadoEliminar"));
                }
                else
                {
                    modoOperacion = EnumModoAplicar.Eliminar;
                    lblModoOperacion.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoEliminar")} {id}";
                    BloquearBotones();
                }
            }

        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (modoOperacion == EnumModoAplicar.Eliminar)
            {
                DialogResult resultado = MessageBox.Show($"{IdiomaManager.GetInstance().ConseguirTexto("estaSeguro")} {FamiliaConfigurada.Id}?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    bllFamilia.EliminarHijos(FamiliaConfigurada.Id); //primero elimina sus hijos y luego la familia
                    bllFamilia.EliminarFamilia(FamiliaConfigurada.Id);
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"));
                }

            }
            else
            {
                if (FamiliaConfigurada.ObtenerHijos().Count() > 0)//PARA AGREGAR Y MODIFICAR FAMILIAS
                {
                    if (modoOperacion == EnumModoAplicar.Añadir) //agregar
                    {
                        if (txtNombreFamilia.Text != "")
                        {
                            Familia familia = bllFamilia.TraerListaFamilias().FirstOrDefault(r => r.Nombre == txtNombreFamilia.Text);
                            if (familia == null)
                            {
                                int idFamiliaCreada = bllFamilia.CrearFamilia(txtNombreFamilia.Text);
                                foreach (var permiso in FamiliaConfigurada.ObtenerHijos())
                                {
                                    bllFamilia.RegistrarHijos(idFamiliaCreada, permiso.Id);
                                }
                                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"));
                            }
                            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("yaOcupado")); }
                        }
                        else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("ingreseNombre")); txtNombreFamilia.Focus(); }
                    }
                    if (modoOperacion == EnumModoAplicar.Modificar) //modificar
                    {
                        FamiliaConfigurada.Nombre = txtNombreFamilia.Text;
                        bllFamilia.ModificarFamilia(FamiliaConfigurada); // cambia el nombre de la familia
                        bllFamilia.EliminarHijos(FamiliaConfigurada.Id); //elimina los hijos de la familia

                        foreach (var hijo in FamiliaConfigurada.ObtenerHijos())
                        {
                            bllFamilia.RegistrarHijos(FamiliaConfigurada.Id, hijo.Id);
                        }

                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"));
                    }
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("alMenosUno")); }
            }
            FamiliaConfigurada.ObtenerHijos().Clear();
            ActualizarListBoxFamilia();
            ActualizarComboBox();
            ResetearBotones();
        }


        private void BloquearBotones()
        {
            btnCrear.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearBotones();
            txtNombreFamilia.Text = "";
            FamiliaConfigurada.ObtenerHijos().Clear();
            ActualizarListBoxFamilia();
        }

        private void cmbFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFamilia.SelectedItem != null)
            {
                FamiliaConfigurada.ObtenerHijos().Clear();
                listBoxFamilia.Items.Clear();
                string[] partes = cmbFamilia.SelectedItem.ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                int id = int.Parse(partes[0].Trim());

                List<Componente> listaHijos = bllFamilia.TraerListaHijos(id);
                foreach (var hijo in listaHijos)
                {
                    listBoxFamilia.Items.Add($"{hijo.Id} - {hijo.Nombre} - {hijo.Tipo}");
                    FamiliaConfigurada.AgregarHijo(hijo);
                }
            }

        }

        private void ResetearBotones()
        {
            btnCrear.Enabled = true;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;

            cmbFamilia.SelectedItem = null;
            modoOperacion = EnumModoAplicar.Consulta;

            lblModoOperacion.Text = IdiomaManager.GetInstance().ConseguirTexto("lblModoOperacion");
        }
    }
}
