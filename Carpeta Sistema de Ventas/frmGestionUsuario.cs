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
            IdiomaManager.GetInstance().archivoActual = "frmGestionUsuario";
            IdiomaManager.GetInstance().Agregar(this);

        }

        public void ActualizarObserver()
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
            else
            {
                if (modoOperacion == EnumModoAplicar.Añadir)
                {
                    if (ValidarCampos())
                    {
                        BEUsuario usuarioEncontrado = bllUsuario.ValidarUsuario(txtNombreUsuario.Text, Convert.ToInt32(txtDNI.Text), txtEmail.Text);
                        if (usuarioEncontrado != null) //busca si existe un usuario con ese dni, email o nombre de usuario
                        {
                            MessageBox.Show(FormIdiomas.ConseguirTexto("yaExiste"));
                            return;
                        }
                        else
                        {
                            string clave = txtDNI.Text + txtApellido.Text; // CLAVE COMBINA DNI + APELLIDO
                            BEUsuario user = new BEUsuario(Convert.ToInt32(txtDNI.Text), txtNombre.Text, txtApellido.Text, txtEmail.Text, txtNombreUsuario.Text, Encriptador.EncriptarSHA256(clave), cmbRol.Text, false, true);
                            bllUsuario.RegistrarUsuario(user);
                            MessageBox.Show(FormIdiomas.ConseguirTexto("operacionExitosa"));
                        }
                    }
                    else { MessageBox.Show(FormIdiomas.ConseguirTexto("llenarCampos")); return; }
                }
                else if (modoOperacion == EnumModoAplicar.Modificar)
                {
                    if (ValidarCampos())
                    {
                        if (Convert.ToBoolean(grillaUsuarios.CurrentRow.Cells[8].Value) == true) //en caso de que este activo
                        {
                            /*Busca si esta bloqueado*/
                            int ultimoDNICliente = Convert.ToInt32(grillaUsuarios.CurrentRow.Cells[0].Value);
                            bool bloqueado = lstUsuarios.FirstOrDefault(u => u.DNI == ultimoDNICliente).Bloqueado;

                            BEUsuario user = new BEUsuario(Convert.ToInt32(txtDNI.Text), txtNombre.Text, txtApellido.Text, txtEmail.Text, txtNombreUsuario.Text, null, cmbRol.Text, bloqueado, true);

                            /*Actualiza en la bd buscando segun su ultimoDNI, esto por si quiere modificar su DNI actual*/
                            bllUsuario.ModificarUsuario(user, ultimoDNICliente);
                            MessageBox.Show(FormIdiomas.ConseguirTexto("operacionExitosa"));
                        }
                        else { MessageBox.Show(FormIdiomas.ConseguirTexto("noPuedeModificarse")); } //no puede modificarse un usuario inactivo
                    }
                    else { MessageBox.Show(FormIdiomas.ConseguirTexto("llenarCampos")); }
                }
                else if (modoOperacion == EnumModoAplicar.Eliminar)
                {
                    DialogResult resultado = MessageBox.Show($"{FormIdiomas.ConseguirTexto("estaSeguroEliminar")} {grillaUsuarios.CurrentRow.Cells[0].Value}?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        bllUsuario.EliminarUsuario(Convert.ToInt32(grillaUsuarios.CurrentRow.Cells[0].Value));
                        MessageBox.Show(FormIdiomas.ConseguirTexto("operacionExitosa"));
                    }
                }
                else if (modoOperacion == EnumModoAplicar.Activar)
                {
                    DialogResult resultado = MessageBox.Show($"{FormIdiomas.ConseguirTexto("estaSeguroActivar")} {grillaUsuarios.CurrentRow.Cells[0].Value}?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        bllUsuario.ActivarUsuario(Convert.ToInt32(grillaUsuarios.CurrentRow.Cells[0].Value));
                        MessageBox.Show(FormIdiomas.ConseguirTexto("operacionExitosa"));
                    }
                }
                else if (modoOperacion == EnumModoAplicar.Desbloquear)
                {
                    DialogResult resultado = MessageBox.Show($"{FormIdiomas.ConseguirTexto("estaSeguroDesbloquear")} {grillaUsuarios.CurrentRow.Cells[0].Value}?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        int DNI = Convert.ToInt32(grillaUsuarios.CurrentRow.Cells[0].Value);
                        bllUsuario.ModificarBloqueo(DNI, false);
                        //Cuando bloquea un usuario porque se olvido la clave, al desbloquearlo se le reseta a la clave por defecto
                        string apellido = grillaUsuarios.CurrentRow.Cells[2].Value.ToString();
                        string clave = DNI + apellido; // CLAVE COMBINA DNI + APELLIDO
                        bllUsuario.CambiarClave(DNI, Encriptador.EncriptarSHA256(clave));
                        MessageBox.Show(FormIdiomas.ConseguirTexto("operacionExitosa"));
                    }
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
                    modoOperacion = EnumModoAplicar.Añadir;
                    BloquearBotones();
                    lblMensaje.Text = FormIdiomas.ConseguirTexto("modoAñadir");
                }
                else
                {
                    MessageBox.Show(FormIdiomas.ConseguirTexto("yaExisteDNI"));
                    txtDNI.Focus();
                }
            }
            else
            {
                MessageBox.Show(FormIdiomas.ConseguirTexto("ingreseDNI"));
                txtDNI.Focus();
            }
        }



      

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if(grillaUsuarios.SelectedRows.Count > 0)
            {
                if (Convert.ToBoolean(grillaUsuarios.CurrentRow.Cells[8].Value) == true) //en caso de que este activo
                {
                    modoOperacion = EnumModoAplicar.Modificar;
                    BloquearBotones();

                    lblMensaje.Text = FormIdiomas.ConseguirTexto("modoModificar") + $" DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
                    txtDNI.Focus();
                    LlenarCamposConDatos();

                    btnModificar.Enabled = false;
                }
                else { MessageBox.Show(FormIdiomas.ConseguirTexto("noPuedeModificarse")); } //si esta inactivo no se puede modificar
            }
            else { MessageBox.Show(FormIdiomas.ConseguirTexto("seleccionarUsuario")); }
        }


        /*Este boton sirve tanto para Eliminar como para Activar*/
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (grillaUsuarios.SelectedRows.Count > 0)
            {
                if(Convert.ToBoolean(grillaUsuarios.CurrentRow.Cells[8].Value) == true) //en caso de que este activo
                {
                    modoOperacion = EnumModoAplicar.Eliminar;
                    BloquearBotones();
                    lblMensaje.Text = FormIdiomas.ConseguirTexto("modoEliminar") + $" DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
                }
                else //en caso de que este desactivado (quiere activarlo)
                {
                    modoOperacion = EnumModoAplicar.Activar;
                    BloquearBotones();
                    lblMensaje.Text = FormIdiomas.ConseguirTexto("modoActivar") + $" DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
                }
            }
            else { MessageBox.Show(FormIdiomas.ConseguirTexto("seleccionarUsuario")); }
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (grillaUsuarios.SelectedRows.Count > 0)
            {
                if (Convert.ToBoolean(grillaUsuarios.CurrentRow.Cells[7].Value))
                {
                    modoOperacion = EnumModoAplicar.Desbloquear;
                    BloquearBotones();
                    lblMensaje.Text = FormIdiomas.ConseguirTexto("modoDesbloquear") + $" DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
                }
                else { MessageBox.Show(FormIdiomas.ConseguirTexto("noEstaBloqueado")); }
            }
            else { MessageBox.Show(FormIdiomas.ConseguirTexto("seleccionarUsuario")); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearBotones();
        }

        private void ResetearBotones()
        {
            btnModificar.Enabled = true; btnEliminar.Enabled = true; btnDesbloquear.Enabled = true; btnAgregar.Enabled = true; btnResetearClave.Enabled = true;
            modoOperacion = EnumModoAplicar.Consulta;
            lblMensaje.Text = FormIdiomas.ConseguirTexto("lblMensaje");
            txtDNI.Enabled = true; txtNombre.Enabled = true; txtNombreUsuario.Enabled = true; txtEmail.Enabled = true; txtApellido.Enabled = true; cmbRol.Enabled = true;
            
            Actualizar();
            VaciarCampos();

            btnCancelar.Enabled = false;
        }

        private void BloquearBotones()
        {
            btnResetearClave.Enabled = false;
            if(modoOperacion == EnumModoAplicar.Añadir)
            {
                btnAgregar.Enabled = false;btnModificar.Enabled = false; btnEliminar.Enabled = false; btnDesbloquear.Enabled = false;
                btnCancelar.Enabled = true;
            }
            else if(modoOperacion == EnumModoAplicar.Modificar) 
            {
                btnAgregar.Enabled = false; btnEliminar.Enabled = false; btnDesbloquear.Enabled = false; btnModificar.Enabled = false;
                btnCancelar.Enabled = true;
            }
            else if(modoOperacion == EnumModoAplicar.Eliminar || modoOperacion == EnumModoAplicar.Activar)
            {
                btnCancelar.Enabled = true; btnAgregar.Enabled = false; btnModificar.Enabled = false; btnDesbloquear.Enabled = false;
                btnEliminar.Enabled = false;

                txtDNI.Enabled = false; txtNombre.Enabled = false; txtNombreUsuario.Enabled = false; txtEmail.Enabled = false; txtApellido.Enabled = false; cmbRol.Enabled = false;
            }
            else if(modoOperacion == EnumModoAplicar.Desbloquear)
            {
                btnDesbloquear.Enabled = false; btnEliminar.Enabled = false; btnDesbloquear.Enabled = false; btnModificar.Enabled = false; btnAgregar.Enabled = false;
                btnCancelar.Enabled = true;
                txtDNI.Enabled = false; txtNombre.Enabled = false; txtNombreUsuario.Enabled = false; txtEmail.Enabled = false; txtApellido.Enabled = false; cmbRol.Enabled = false;
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
                MessageBox.Show(FormIdiomas.ConseguirTexto("errorDNI"));
                return false;
            }
            /*Se fija si se llenaron los campos*/
            if (Convert.ToInt32(txtDNI.Text) == 0 || txtNombre.Text == "" || txtApellido.Text == "" || txtEmail.Text == "" || txtNombreUsuario.Text == "" || cmbRol.Text == "")
            {
                return false;
            }
                /*Se fija el formato del mail con una expresion regular*/
            if(!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show(FormIdiomas.ConseguirTexto("errorMail"));
                txtEmail.Focus();
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
        }


        /*Esto es para cuando cambia de seleccion en el gridview antes de darle a aplicar */
        private void grillaUsuarios_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (grillaUsuarios.SelectedRows.Count > 0)
            {
                if (Convert.ToBoolean(grillaUsuarios.CurrentRow.Cells[8].Value) == false)
                {
                    btnEliminar.Text = FormIdiomas.ConseguirTexto("btnActivar");
                }
                else { btnEliminar.Text = FormIdiomas.ConseguirTexto("btnEliminar"); }

                if (modoOperacion == EnumModoAplicar.Modificar)
                {
                    LlenarCamposConDatos();
                    lblMensaje.Text = FormIdiomas.ConseguirTexto("modoModificar") + $" DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
                }
                else if (modoOperacion == EnumModoAplicar.Eliminar)
                {
                    lblMensaje.Text = FormIdiomas.ConseguirTexto("modoEliminar") + $" DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
                }
                else if (modoOperacion == EnumModoAplicar.Desbloquear)
                {
                    if (Convert.ToBoolean(grillaUsuarios.CurrentRow.Cells[7].Value))
                    {
                        lblMensaje.Text = FormIdiomas.ConseguirTexto("modoDesbloquear") + $" DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
                    }
                    else
                    {
                        MessageBox.Show(FormIdiomas.ConseguirTexto("noEstaBloqueado"));
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

        }

        private void btnResetearClave_Click(object sender, EventArgs e)
        {
            if (grillaUsuarios.SelectedRows.Count > 0)
            {
                int DNI = Convert.ToInt32(grillaUsuarios.CurrentRow.Cells[0].Value);
                string apellido = grillaUsuarios.CurrentRow.Cells[2].Value.ToString();
                string clave = DNI + apellido;

                DialogResult resultado = MessageBox.Show($"{FormIdiomas.ConseguirTexto("estaSeguroClave")} {DNI}?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        bllUsuario.CambiarClave(DNI, Encriptador.EncriptarSHA256(clave));
                        MessageBox.Show(FormIdiomas.ConseguirTexto("operacionExitosa"));
                    }
                    catch (Exception ex) { MessageBox.Show("Error al modificar la clave"); }
                }
            }
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericUpDown numUpDown = sender as NumericUpDown;

            if (numUpDown != null)
            {
                if (!char.IsControl(e.KeyChar))
                {
                    string currentText = numUpDown.Text;

                    if (currentText.Length >= 9)
                    {
                        e.Handled = true;
                    }
                }
            }
        }
    }
}
