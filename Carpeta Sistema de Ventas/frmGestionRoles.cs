using BE;
using BE.Composite;
using BLL;
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
    public partial class frmGestionRoles : Form, IObserver
    {
        public frmGestionRoles()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().archivoActual = "frmGestionRoles";
            IdiomaManager.GetInstance().Agregar(this);
        }

        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }


        BLLPermiso bllPermiso = new BLLPermiso();
        BLLFamilia bllFamilia = new BLLFamilia();
        BLLUsuario bllUsuario = new BLLUsuario();
        private Familia RolConfigurado = new Familia();


        private Familia rolAModifcarOEliminar = new Familia();
        EnumModoAplicar modoOperacion;

        private void frmGestionRoles_Load(object sender, EventArgs e)
        {
            modoOperacion = EnumModoAplicar.Consulta;
            lblModoOperacion.Text = IdiomaManager.GetInstance().ConseguirTexto("lblModoOperacion");
            ActualizarListBoxPermisosYFamilias();
            ActualizarComboBox();
        }

        private void btnAgregarPermiso_Click(object sender, EventArgs e)
        {
            if (listBoxPermisos.SelectedItems.Count > 0)
            {
                Componente permisoSeleccionado = TraerComponeneteDeListBox(listBoxPermisos);
                if (!ExisteConflicto(permisoSeleccionado))
                {
                    RolConfigurado.AgregarHijo(permisoSeleccionado);
                    ActualizarListBoxRol();
                }
            }
            else { MessageBox.Show(lblModoOperacion.Text = IdiomaManager.GetInstance().ConseguirTexto("seleccionePermiso")); }
        }

        private void btnAgregarFamilia_Click(object sender, EventArgs e)
        {
            if (listBoxFamilias.SelectedItems.Count > 0)
            {
                Componente permisoSeleccionado = TraerComponeneteDeListBox(listBoxFamilias);
                if (!ExisteConflicto(permisoSeleccionado))
                {
                    RolConfigurado.AgregarHijo(permisoSeleccionado);
                    ActualizarListBoxRol();
                }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccioneFamilia")); }
        }

        private void btnQuitarPermiso_Click(object sender, EventArgs e)
        {
            if (listBoxRol.SelectedItems.Count > 0)
            {
                Componente permisoSeleccionado = TraerComponeneteDeListBox(listBoxRol);

                RolConfigurado.QuitarHijo(permisoSeleccionado);
                ActualizarListBoxRol();
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

        private bool ExisteConflicto(Componente permisoSeleccionado)
        {
            if (permisoSeleccionado is Permiso) // si es simple
            {
                Componente comp = bllFamilia.VerificarSiEstaEnFamilia(permisoSeleccionado.Id);
                if (comp != null)
                {
                    //logica pra saber si el permiso ya esta en una familia seleccionada
                    Componente yaEstaEnElRol = RolConfigurado.ObtenerHijos().FirstOrDefault(p => p.Id == comp.Id);
                    if (yaEstaEnElRol != null)
                    {
                        MessageBox.Show($"{IdiomaManager.GetInstance().ConseguirTexto("yaEstaEnFamilia")} {comp.Id}");
                        return true;
                    }
                }
            }
            else// si es familia
            {
                //logica para saber si la familia seleccionada tiene algun permiso  que ya se selecciono
                List<Componente> listaHijos = bllFamilia.TraerListaHijos(permisoSeleccionado.Id);
                foreach (var hijo in listaHijos)
                {
                    Componente yaEstaEnElRol = RolConfigurado.ObtenerHijos().FirstOrDefault(p => p.Id == hijo.Id);
                    if (yaEstaEnElRol != null)
                    {
                        MessageBox.Show($"{IdiomaManager.GetInstance().ConseguirTexto("yaTienePermiso")} {hijo.Id} {IdiomaManager.GetInstance().ConseguirTexto("yaSeleccionado")}");
                        return true;
                    }
                }

            }
            return false;
        }



        private void ActualizarListBoxRol()
        {
            listBoxRol.Items.Clear();
            foreach (Componente permiso in RolConfigurado.ObtenerHijos())
            {
                listBoxRol.Items.Add($"{permiso.Id} - {permiso.Nombre} - {permiso.Tipo}");
            }
        }





        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (txtNombreRol.Text != "")
            {
                Familia rol = bllFamilia.TraerListaRoles().FirstOrDefault(r => r.Nombre == txtNombreRol.Text);
                if(rol == null)
                {
                    modoOperacion = EnumModoAplicar.Añadir;
                    lblModoOperacion.Text = IdiomaManager.GetInstance().ConseguirTexto("modoAñadir");

                    BloquearBotones();
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("yaOcupado")); }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("ingreseNombre")); txtNombreRol.Focus(); }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {

            if (modoOperacion == EnumModoAplicar.Eliminar)
            {
                DialogResult resultado = MessageBox.Show($"{IdiomaManager.GetInstance().ConseguirTexto("estaSeguro")} {rolAModifcarOEliminar.Id}?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    bllFamilia.EliminarPermisosRol(rolAModifcarOEliminar.Id);
                    bllFamilia.EliminarRol(rolAModifcarOEliminar.Id);

                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"));
                }
            }
            if (RolConfigurado.ObtenerHijos().Count() > 0)
            {
                if (modoOperacion == EnumModoAplicar.Añadir)
                {
                    if (txtNombreRol.Text != "")
                    {
                        Familia rol = bllFamilia.TraerListaRoles().FirstOrDefault(r => r.Nombre == txtNombreRol.Text);
                        if (rol == null)
                        {
                            int idRolCreado = bllFamilia.CrearRol(txtNombreRol.Text);
                            foreach (var permiso in RolConfigurado.ObtenerHijos())
                            {
                                bllFamilia.RegistrarPermisosRol(idRolCreado, permiso.Id);
                            }
                            MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"));
                        }
                        else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("yaOcupado")); }

                    }
                    else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("ingreseNombre")); txtNombreRol.Focus(); }
                }
                if (modoOperacion == EnumModoAplicar.Modificar)
                {
                    rolAModifcarOEliminar.Nombre = txtNombreRol.Text;
                    bllFamilia.EliminarPermisosRol(rolAModifcarOEliminar.Id); /*Elima los permisos hijos del Rol*/
                    bllFamilia.ModificarRol(rolAModifcarOEliminar); //le cambia el nombre al Rol
                    foreach (var permiso in RolConfigurado.ObtenerHijos())
                    {
                        bllFamilia.RegistrarPermisosRol(rolAModifcarOEliminar.Id, permiso.Id); //Registra de vuelta los permisos del rol
                    }
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"));
                }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("alMenosUno")); }
            RolConfigurado.ObtenerHijos().Clear();
            ActualizarListBoxRol();
            ActualizarComboBox();
            ResetearBotones();
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (cmbRoles.SelectedItem != null)
            {
                string[] partes = cmbRoles.SelectedItem.ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                int id = int.Parse(partes[0].Trim());
                string nombre = partes[1].Trim();
                txtNombreRol.Text = nombre;

                rolAModifcarOEliminar.Id = id;

                List<BEUsuario> listaUsuarios = bllUsuario.TraerListaUsuarios(); //Busca la lista de usuarios
                BEUsuario yaEstaAsignado = listaUsuarios.FirstOrDefault(u => u.Rol.Id == rolAModifcarOEliminar.Id); //Se fija si el rol ya fue asignado a un usuario
                if (yaEstaAsignado != null)
                {
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("yaEstaAsignadoModificar"));
                }
                else
                {
                    if (txtNombreRol.Text != "")
                    {
                        modoOperacion = EnumModoAplicar.Modificar;
                        lblModoOperacion.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoModificar")} {id}";
                        BloquearBotones();
                    }
                    else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("ingreseNombre")); txtNombreRol.Focus(); }
                }
            }
            else { MessageBox.Show("Seleccione un Rol en el comboBox"); }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (cmbRoles.SelectedItem != null)
            {
                string[] partes = cmbRoles.SelectedItem.ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                int id = int.Parse(partes[0].Trim());

                rolAModifcarOEliminar.Id = id;

                List<BEUsuario> listaUsuarios = bllUsuario.TraerListaUsuarios(); //Busca la lista de usuarios
                BEUsuario yaEstaAsignado = listaUsuarios.FirstOrDefault(u => u.Rol.Id == rolAModifcarOEliminar.Id); //Se fija si el rol ya fue asignado a un usuario
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
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccioneComboBox")); }

        }

        private void ResetearBotones()
        {
            btnCrear.Enabled = true;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;

            cmbRoles.SelectedItem = null;
            modoOperacion = EnumModoAplicar.Consulta;

            lblModoOperacion.Text = IdiomaManager.GetInstance().ConseguirTexto("lblModoOperacion");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearBotones();
            txtNombreRol.Text = "";
            RolConfigurado.ObtenerHijos().Clear();
            ActualizarListBoxRol();

        }



        private void btnGestionarFamilias_Click(object sender, EventArgs e)
        {

            frmGestionFamilias form = new frmGestionFamilias();
            form.ShowDialog();

            //vuelve a cargar el idioma
            IdiomaManager.GetInstance().archivoActual = "frmGestionRoles";
            IdiomaManager.GetInstance().Agregar(this);

            ActualizarListBoxPermisosYFamilias();
        }

        private void ActualizarListBoxPermisosYFamilias()
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


            listBoxFamilias.Items.Clear();
            List<Familia> listaFamilias = bllFamilia.TraerListaFamilias();
            foreach (var permiso in listaFamilias)
            {
                if (permiso is Familia)
                {
                    listBoxFamilias.Items.Add($"{permiso.Id} - {permiso.Nombre} - {permiso.Tipo}");
                }
            }

        }
        private void ActualizarComboBox()
        {
            cmbRoles.Items.Clear();
            List<Familia> listaRoles = bllFamilia.TraerListaRoles();
            foreach (var rol in listaRoles)
            {
                cmbRoles.Items.Add($"{rol.Id} - {rol.Nombre}");
            }
        }


        private void listBoxFamilias_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxPermisoFamilia.Items.Clear();
            if (listBoxFamilias.SelectedItems.Count > 0)
            {
                Componente familiaSeleccionada = TraerComponeneteDeListBox(listBoxFamilias);
                List<Componente> lista = bllFamilia.TraerListaHijos(familiaSeleccionada.Id);

                foreach (Componente permiso in lista)
                {
                    listBoxPermisoFamilia.Items.Add($"{permiso.Id} - {permiso.Nombre} - {permiso.Tipo}");
                }
            }
        }

        private void BloquearBotones()
        {
            btnCrear.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

        }

        private void cmbRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRoles.SelectedItem != null)
            {
                RolConfigurado.ObtenerHijos().Clear();
                listBoxRol.Items.Clear();
                string[] partes = cmbRoles.SelectedItem.ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                int id = int.Parse(partes[0].Trim());
                string nombre = partes[1].Trim();
                Familia rol = new Familia() { Id = id, Nombre = nombre };
                List<Componente> lista = bllFamilia.TraerListaPermisosRol(rol.Id);

                foreach (Componente permiso in lista)
                {
                    listBoxRol.Items.Add($"{permiso.Id} - {permiso.Nombre} - {permiso.Tipo}");
                    RolConfigurado.AgregarHijo(permiso);
                }
            }
        }
    }
}
