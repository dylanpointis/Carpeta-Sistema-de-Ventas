using BE;
using BLL;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Services.Observer;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmGestionUsuario : Form, IObserver
    {
        public frmGestionUsuario()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().Agregar(this);
            IdiomaManager.GetInstance().archivoActual = "frmGestionUsuario";

            /*
             * 
             * CON ESTO FUNCIONA PERO NO ME CONVENCE
             * 
            IdiomaManager.GetInstance().CargarIdioma();
            IdiomaManager.GetInstance().Notificar();*/
        }

        public void ActualizarIdioma()
        {
            FormIdiomas.ActualizarControles(this);
        }



        BLLUsuario bllUsuario = new BLLUsuario();
        List<BEUsuario> lstUsuarios = null;
        EnumModoAplicar modoOperacion;


        private void frmCrearUsuario_Load(object sender, EventArgs e)
        {
            btnCancelar.Enabled = false;
            modoOperacion = EnumModoAplicar.Consulta;
            txtDNI.Text = "";
            Actualizar();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (modoOperacion == EnumModoAplicar.Consulta)
            {
                List<BEUsuario> lstConsulta = new List<BEUsuario>();
                foreach (BEUsuario user in lstUsuarios)
                {
                    if (user.DNI.ToString() == txtDNI.Text || user.Nombre.ToLower() == txtNombre.Text.ToLower() || user.Apellido.ToLower() == txtApellido.Text.ToLower() || user.NombreUsuario.ToLower() == txtNombreUsuario.Text.ToLower() || user.Email == txtEmail.Text.ToLower() || user.Rol == cmbRol.Text)
                    {
                        lstConsulta.Add(user);
                    }
                }
                grillaUsuarios.DataSource = lstConsulta;
            }
            else if (modoOperacion == EnumModoAplicar.Añadir)
            {
                if (ValidarCampos())
                {
                    BEUsuario usuarioEncontrado = bllUsuario.ValidarUsuario(txtNombreUsuario.Text, Convert.ToInt32(txtDNI.Text), txtEmail.Text);
                    if (usuarioEncontrado != null)
                    {
                        MessageBox.Show("Ya existe un usuario con ese DNI, NombreUsuario o Email");
                    }
                    else
                    {
                        string clave = txtDNI.Text + txtApellido.Text; // CLAVE COMBINA DNI + APELLIDO
                        BEUsuario user = new BEUsuario(Convert.ToInt32(txtDNI.Text), txtNombre.Text, txtApellido.Text, txtEmail.Text, txtNombreUsuario.Text, Encriptador.EncriptarSHA256(clave), cmbRol.Text, false, true);
                        bllUsuario.RegistrarUsuario(user);
                        MessageBox.Show("Usuario registrado exitosamente");
                        ResetearBotones();
                    }
                }
                else { MessageBox.Show("Llene los campos"); }
            }
            else if (modoOperacion == EnumModoAplicar.Modificar)
            {
                if (ValidarCampos())
                {
                    /*Busca si esta bloqueado*/
                    int ultimoDNICliente = Convert.ToInt32(grillaUsuarios.CurrentRow.Cells[0].Value);
                    bool bloqueado = lstUsuarios.FirstOrDefault(u => u.DNI == ultimoDNICliente).Bloqueado;
                    bool activo = true;
                    if (cmbActivo.Text == "No activo")
                    {
                        activo = false;
                    }
                    BEUsuario user = new BEUsuario(Convert.ToInt32(txtDNI.Text), txtNombre.Text, txtApellido.Text, txtEmail.Text, txtNombreUsuario.Text, null, cmbRol.Text, bloqueado, activo);

                    /*Actualiza en la bd buscando segun su ultimoDNI, esto por si quiere modificar su DNI actual*/
                    bllUsuario.ModificarUsuario(user, ultimoDNICliente);
                    MessageBox.Show("Usuario modificado exitosamente");
                    ResetearBotones();
                }
                else { MessageBox.Show("Llene los campos"); }
            }
            else if (modoOperacion == EnumModoAplicar.Eliminar)
            {
                DialogResult resultado = MessageBox.Show($"¿Está seguro que desea eliminar al usuario DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}?", "Eliminar usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    bllUsuario.EliminarUsuario(Convert.ToInt32(grillaUsuarios.CurrentRow.Cells[0].Value));
                    Actualizar();
                    MessageBox.Show("Usuario eliminado exitosamente");
                }
                ResetearBotones();
            }
            else if (modoOperacion == EnumModoAplicar.Desbloquear)
            {
                DialogResult resultado = MessageBox.Show($"¿Está seguro que desea desbloquear al usuario DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}?", "Eliminar usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    int DNI = Convert.ToInt32(grillaUsuarios.CurrentRow.Cells[0].Value);
                    bllUsuario.ModificarBloqueo(DNI, false);
                    //Cuando bloquea un usuario porque se olvido la clave, al desbloquearlo se le reseta a la clave por defecto
                    string apellido = grillaUsuarios.CurrentRow.Cells[2].Value.ToString();
                    string clave = DNI + apellido; // CLAVE COMBINA DNI + APELLIDO
                    bllUsuario.CambiarClave(DNI, Encriptador.EncriptarSHA256(clave));

                    Actualizar();
                    MessageBox.Show("Usuario desbloqueado exitosamente exitosamente");
                }
                ResetearBotones();

            }
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(txtDNI.Text != "" && txtDNI.Text.Length <= 9)
            {
                BEUsuario user = bllUsuario.ValidarUsuario("",Convert.ToInt32(txtDNI.Text), "");
                if (user == null)
                {
                    BloquearBotones("Agregar");

                    modoOperacion = EnumModoAplicar.Añadir;
                    lblMensaje.Text = FormIdiomas.ConseguirTexto("modoAñadir");
                }
                else
                {
                    MessageBox.Show("Ya existe un usuario con ese DNI");
                    txtDNI.Focus();
                }
            }
            else
            {
                MessageBox.Show("Ingrese un DNI para agregar, menor a 9 caracteres");
                txtDNI.Focus();
            }
        }



      

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if(grillaUsuarios.SelectedRows.Count > 0)
            {
                BloquearBotones("Modificar");
                modoOperacion = EnumModoAplicar.Modificar;

                lblMensaje.Text = FormIdiomas.ConseguirTexto("modoModificar")+ $" DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
                txtDNI.Focus();
                LlenarCamposConDatos();

                btnModificar.Enabled = false;
            }
            else { MessageBox.Show("Seleccione un usuario para modificar"); }
        }



        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (grillaUsuarios.SelectedRows.Count > 0)
            {
                BloquearBotones("Eliminar");
                modoOperacion = EnumModoAplicar.Eliminar;
                lblMensaje.Text = FormIdiomas.ConseguirTexto("modoEliminar") + $" DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
            }
            else { MessageBox.Show("Seleccione un usuario para eliminar"); }
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (grillaUsuarios.SelectedRows.Count > 0)
            {
                if (Convert.ToBoolean(grillaUsuarios.CurrentRow.Cells[7].Value))
                {
                    modoOperacion = EnumModoAplicar.Desbloquear;
                    BloquearBotones("Desbloquear");
                    lblMensaje.Text = FormIdiomas.ConseguirTexto("modoDesbloquear") + $" DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
                }
                else { MessageBox.Show("El usuario seleccionado no está bloqueado"); }
            }
            else { MessageBox.Show("Seleccione un usuario para desbloquear"); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearBotones();
        }

        private void ResetearBotones()
        {
            btnModificar.Enabled = true; btnEliminar.Enabled = true; btnDesbloquear.Enabled = true; cmbActivo.Enabled = true; btnAgregar.Enabled = true; btnResetearClave.Enabled = true;
            modoOperacion = EnumModoAplicar.Consulta;
            lblMensaje.Text = FormIdiomas.ConseguirTexto("lblMensaje");
            txtDNI.Enabled = true; txtNombre.Enabled = true; txtNombreUsuario.Enabled = true; txtEmail.Enabled = true; txtApellido.Enabled = true; cmbRol.Enabled = true; cmbActivo.Enabled = true;
            
            Actualizar();
            VaciarCampos();

            btnCancelar.Enabled = false;
        }

        private void BloquearBotones(string Modo)
        {
            btnResetearClave.Enabled = false;
            if(Modo == "Agregar")
            {
                btnAgregar.Enabled = false;btnModificar.Enabled = false; btnEliminar.Enabled = false; btnDesbloquear.Enabled = false; cmbActivo.Enabled = false;
                btnCancelar.Enabled = true;
            }
            else if(Modo == "Modificar") 
            {
                btnAgregar.Enabled = false; btnEliminar.Enabled = false; btnDesbloquear.Enabled = false; btnModificar.Enabled = false;
                btnCancelar.Enabled = true;
            }
            else if(Modo == "Eliminar")
            {
                btnCancelar.Enabled = true; btnAgregar.Enabled = false; btnModificar.Enabled = false; btnDesbloquear.Enabled = false; cmbActivo.Enabled = false;
                btnEliminar.Enabled = false;

                txtDNI.Enabled = false; txtNombre.Enabled = false; txtNombreUsuario.Enabled = false; txtEmail.Enabled = false; txtApellido.Enabled = false; cmbRol.Enabled = false; cmbActivo.Enabled = false;
            }
            else if(Modo == "Desbloquear")
            {
                btnDesbloquear.Enabled = false; btnEliminar.Enabled = false; btnDesbloquear.Enabled = false; btnModificar.Enabled = false; btnAgregar.Enabled = false;
                btnCancelar.Enabled = true;
                txtDNI.Enabled = false; txtNombre.Enabled = false; txtNombreUsuario.Enabled = false; txtEmail.Enabled = false; txtApellido.Enabled = false; cmbRol.Enabled = false; cmbActivo.Enabled = false;
            }
        }


        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Actualizar();
        }


        private void Actualizar()
        {
            lstUsuarios = bllUsuario.TraerListaUsuarios();
            grillaUsuarios.DataSource = lstUsuarios;

            grillaUsuarios.Columns["Clave"].Visible = false; //quita la columna clave

            grillaUsuarios.BindingContext = new BindingContext(); //ESTO ES PARA COLOREAR EN ROJO A LOS NO ACTIVOS. ASEGURA QUE SE LLENEN BIEN LOS DATOS DEL GRIDVIEW*/
            foreach (DataGridViewRow row in grillaUsuarios.Rows)
            {
                if (Convert.ToBoolean(row.Cells[8].Value) == false)
                {
                    row.DefaultCellStyle.BackColor = Color.Crimson; //pone en rojo el background
                }
            }
        }


        private bool ValidarCampos()
        {

            if (!Regex.IsMatch(txtDNI.Text, @"^\d{7,9}$"))
            {
                MessageBox.Show("El DNI debe contener solo números y tener entre 7 y 9 dígitos.");
                return false;
            }
            /*Se fija si se llenaron los campos*/
            if (Convert.ToInt32(txtDNI.Text) == 0 && txtNombre.Text == "" && txtApellido.Text == "" && txtEmail.Text == "" && txtNombreUsuario.Text == "" && cmbRol.Text == "")
            {
                return false;
            }
                /*Se fija el formato del mail con una expresion regular*/
            if(!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Formato del mail incorrecto");
                return false;
            }
            return true;
        }

        private void VaciarCampos()
        {
            txtDNI.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            txtNombreUsuario.Text = "";
            cmbRol.SelectedItem = null;
            cmbActivo.SelectedItem = null;
        }


        /*Esto es para cuando cambia de seleccion en el gridview antes de darle a aplicar */
        private void grillaUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grillaUsuarios.SelectedRows.Count > 0)
            {
                if (modoOperacion == EnumModoAplicar.Modificar)
                {
                    LlenarCamposConDatos();
                    lblMensaje.Text = $"Mensaje: Modo Modificar DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
                }
                else if (modoOperacion == EnumModoAplicar.Eliminar)
                {
                    lblMensaje.Text = $"Mensaje: Modo Eliminar DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
                }
                else if (modoOperacion == EnumModoAplicar.Desbloquear)
                {
                    if (Convert.ToBoolean(grillaUsuarios.CurrentRow.Cells[7].Value))
                    {
                        lblMensaje.Text = $"Mensaje: Modo Desbloquear DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
                    }
                    else 
                    { 
                        MessageBox.Show("El usuario seleccionado no está bloqueado");
                        ResetearBotones();
                    }
                }
            }
        }


        private void LlenarCamposConDatos()
        {
            txtDNI.Text = grillaUsuarios.CurrentRow.Cells[0].Value.ToString();
            txtNombre.Text = grillaUsuarios.CurrentRow.Cells[1].Value.ToString();
            txtApellido.Text = grillaUsuarios.CurrentRow.Cells[2].Value.ToString();
            txtEmail.Text = grillaUsuarios.CurrentRow.Cells[3].Value.ToString();
            txtNombreUsuario.Text = grillaUsuarios.CurrentRow.Cells[4].Value.ToString();
            cmbRol.SelectedItem = grillaUsuarios.CurrentRow.Cells[6].Value.ToString();
            bool activo = Convert.ToBoolean(grillaUsuarios.CurrentRow.Cells[8].Value);
            if (activo)
            {
                cmbActivo.SelectedItem = "Activo";
            }
            else { cmbActivo.SelectedItem = "No activo"; }
        }

        private void btnResetearClave_Click(object sender, EventArgs e)
        {
            if (grillaUsuarios.SelectedRows.Count > 0)
            {
                int DNI = Convert.ToInt32(grillaUsuarios.CurrentRow.Cells[0].Value);
                string apellido = grillaUsuarios.CurrentRow.Cells[2].Value.ToString();
                string clave = DNI + apellido;

                DialogResult resultado = MessageBox.Show($"¿Está seguro que desea restablecer la clave de DNI: {DNI}?", "Restablecer clave", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        bllUsuario.CambiarClave(DNI, Encriptador.EncriptarSHA256(clave));
                        MessageBox.Show("Clave restablecida con exito");
                    }
                    catch (Exception ex) { MessageBox.Show("Error al modificar la clave"); }
                }
            }
        }
    }
}
