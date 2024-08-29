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
using Microsoft.VisualBasic.ApplicationServices;
using BE.Composite;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmGestionUsuario : Form, IObserver
    {
        public frmGestionUsuario()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().archivoActual = "frmGestionUsuario";
            IdiomaManager.GetInstance().Agregar(this);


            grillaUsuarios.Columns.Add("DNI", IdiomaManager.GetInstance().ConseguirTexto("gridViewDNI"));
            grillaUsuarios.Columns.Add("Nombre", IdiomaManager.GetInstance().ConseguirTexto("gridViewNombre"));
            grillaUsuarios.Columns.Add("Apellido", IdiomaManager.GetInstance().ConseguirTexto("gridViewApellido"));
            grillaUsuarios.Columns.Add("Mail", IdiomaManager.GetInstance().ConseguirTexto("gridViewMail"));
            grillaUsuarios.Columns.Add("NombreUsuario", IdiomaManager.GetInstance().ConseguirTexto("gridViewNombreUsuario"));
            grillaUsuarios.Columns.Add("Rol", IdiomaManager.GetInstance().ConseguirTexto("gridViewRol"));
            grillaUsuarios.Columns.Add("Bloqueo", IdiomaManager.GetInstance().ConseguirTexto("gridViewBloqueo"));
            grillaUsuarios.Columns.Add("Activo", IdiomaManager.GetInstance().ConseguirTexto("gridViewActivo"));

            grillaUsuarios.Columns[6].Width = 60;
            grillaUsuarios.Columns[7].Width = 60;
        }

        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }


        BLLUsuario bllUsuario = new BLLUsuario();
        BLLFamilia bllFamilia = new BLLFamilia();
        BLLEvento bllEvento = new BLLEvento();

        List<Familia> listaRoles = new List<Familia>();
        List<BEUsuario> lstUsuarios = null;
        EnumModoAplicar modoOperacion;


        private void frmCrearUsuario_Load(object sender, EventArgs e)
        {
            listaRoles = bllFamilia.TraerListaRoles();
            btnCancelar.Enabled = false;
            modoOperacion = EnumModoAplicar.Consulta;
            txtDNI.Text = "";
            Actualizar();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (modoOperacion == EnumModoAplicar.Consulta)
            {
                grillaUsuarios.Rows.Clear();
                foreach (BEUsuario u in lstUsuarios)
                {
                    if (u.DNI.ToString() == txtDNI.Text || u.Nombre.ToLower() == txtNombre.Text.ToLower() || u.Apellido.ToLower() == txtApellido.Text.ToLower() || u.NombreUsuario.ToLower() == txtNombreUsuario.Text.ToLower() || u.Email == txtEmail.Text.ToLower() || u.Rol.Nombre == cmbRol.Text)
                    {
                        grillaUsuarios.Rows.Add(u.DNI, u.Nombre, u.Apellido, u.Email, u.NombreUsuario, u.Rol.Nombre, u.Bloqueado, u.Activo);
                    }
                }
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
                            MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("yaExiste"));
                            return;
                        }
                        else
                        {
                            Familia rol = listaRoles.FirstOrDefault(r => r.Nombre == cmbRol.Text); //BUSCA EL ROL
                            string clave = txtDNI.Text + txtApellido.Text; // CLAVE COMBINA DNI + APELLIDO
                            BEUsuario user = new BEUsuario(Convert.ToInt32(txtDNI.Text), txtNombre.Text, txtApellido.Text, txtEmail.Text, txtNombreUsuario.Text, Encriptador.EncriptarSHA256(clave), rol.Id, false, true);
                            bllUsuario.RegistrarUsuario(user);

                            bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Gestión usuarios", "Usuario creado", 1, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));

                            MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("operacionExitosa"));
                        }
                    }
                    else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("llenarCampos")); return; }
                }
                else if (modoOperacion == EnumModoAplicar.Modificar)
                {
                    if (ValidarCampos())
                    {
                            if (grillaUsuarios.CurrentRow.Cells[7].Value.ToString() != "False") //en caso de que este activo
                            {
                                int dni = Convert.ToInt32(txtDNI.Text);
                                bool bloqueado = lstUsuarios.FirstOrDefault(u => u.DNI == dni).Bloqueado;

                                List<BEUsuario> lstUsers = bllUsuario.TraerListaUsuarios();
                                BEUsuario usuarioEncontrado = lstUsers.FirstOrDefault(u => u.DNI != dni && (u.NombreUsuario == txtNombreUsuario.Text || u.Email == txtEmail.Text));
                                if (usuarioEncontrado != null) //busca si existe un usuario con ese  email o nombre de usuario
                                {
                                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("yaExisteMailUser"));
                                    return;
                                }
                                else
                                {
                                    Familia rol = listaRoles.FirstOrDefault(r => r.Nombre == cmbRol.Text); //BUSCA EL ROL

                                    BEUsuario user = new BEUsuario(dni, txtNombre.Text, txtApellido.Text, txtEmail.Text, txtNombreUsuario.Text, null, rol.Id, bloqueado, true);
                                    bllUsuario.ModificarUsuario(user);
                                    bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Gestión usuarios", "Usuario modificado", 1, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));
                                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("operacionExitosa"));
                                }

                            }
                            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noPuedeModificarse")); } //no puede modificarse un usuario inactivo                                             
                    }
                    else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("llenarCampos")); return; }
                }
                else if (modoOperacion == EnumModoAplicar.Eliminar)
                {
                    DialogResult resultado = MessageBox.Show($"{IdiomaManager.GetInstance().ConseguirTexto("estaSeguroEliminar")} {grillaUsuarios.CurrentRow.Cells[0].Value}?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        bllUsuario.EliminarUsuario(Convert.ToInt32(grillaUsuarios.CurrentRow.Cells[0].Value));
                        bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Gestión usuarios", "Usuario eliminado", 1, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("operacionExitosa"));
                    }
                }
                else if (modoOperacion == EnumModoAplicar.Activar)
                {
                    DialogResult resultado = MessageBox.Show($"{IdiomaManager.GetInstance().ConseguirTexto("estaSeguroActivar")} {grillaUsuarios.CurrentRow.Cells[0].Value}?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        bllUsuario.ActivarUsuario(Convert.ToInt32(grillaUsuarios.CurrentRow.Cells[0].Value));
                        bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Gestión usuarios", "Usuario activado", 1, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("operacionExitosa"));
                    }
                }
                else if (modoOperacion == EnumModoAplicar.Desbloquear)
                {
                    DialogResult resultado = MessageBox.Show($"{IdiomaManager.GetInstance().ConseguirTexto("estaSeguroDesbloquear")} {grillaUsuarios.CurrentRow.Cells[0].Value}?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        int DNI = Convert.ToInt32(grillaUsuarios.CurrentRow.Cells[0].Value);
                        string username = grillaUsuarios.CurrentRow.Cells[4].Value.ToString();

                        bllUsuario.ModificarBloqueo(DNI, false);
                        bllUsuario.ModificarContFallido(username, 0); //resetea el cont de intenos fallidos a 0

                        //Cuando bloquea un usuario porque se olvido la clave, al desbloquearlo se le reseta a la clave por defecto
                        string apellido = grillaUsuarios.CurrentRow.Cells[2].Value.ToString();
                        string clave = DNI + apellido; // CLAVE COMBINA DNI + APELLIDO
                        bllUsuario.CambiarClave(DNI, Encriptador.EncriptarSHA256(clave));
                        bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Gestión usuarios", "Usuario desbloqueado", 1, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("operacionExitosa"));
                    }
                }
                ResetearBotones();
            }
           
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(txtDNI.Text != "" && txtDNI.Text != "0" && txtDNI.Text.Length >= 7 && txtDNI.Text.Length <= 9)
            {
                BEUsuario user = bllUsuario.ValidarUsuario("",Convert.ToInt32(txtDNI.Text), "");
                if (user == null)
                {
                    modoOperacion = EnumModoAplicar.Añadir;
                    BloquearBotones();
                    lblMensaje.Text = IdiomaManager.GetInstance().ConseguirTexto("modoAñadir");
                }
                else
                {
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("yaExisteDNI"));
                    txtDNI.Focus();
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("ingreseDNI"));
                txtDNI.Focus();
            }
        }



      

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if(grillaUsuarios.SelectedRows.Count > 0)
            {
                if (grillaUsuarios.CurrentRow.Cells[7].Value.ToString() != IdiomaManager.GetInstance().ConseguirTexto("false")) //en caso de que este activo
                {
                    modoOperacion = EnumModoAplicar.Modificar;
                    BloquearBotones();

                    lblMensaje.Text = IdiomaManager.GetInstance().ConseguirTexto("modoModificar") + $" DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
                    txtDNI.Focus();
                    LlenarCamposConDatos();

                    btnModificar.Enabled = false;
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noPuedeModificarse")); } //si esta inactivo no se puede modificar
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccionarUsuario")); }
        }


        /*Este boton sirve tanto para Eliminar como para Activar*/
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (grillaUsuarios.SelectedRows.Count > 0)
            {
                if(grillaUsuarios.CurrentRow.Cells[7].Value.ToString() != IdiomaManager.GetInstance().ConseguirTexto("false")) //en caso de que este activo
                {
                    modoOperacion = EnumModoAplicar.Eliminar;
                    BloquearBotones();
                    lblMensaje.Text = IdiomaManager.GetInstance().ConseguirTexto("modoEliminar") + $" DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
                }
                else //en caso de que este desactivado (quiere activarlo)
                {
                    modoOperacion = EnumModoAplicar.Activar;
                    BloquearBotones();
                    lblMensaje.Text = IdiomaManager.GetInstance().ConseguirTexto("modoActivar") + $" DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
                }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccionarUsuario")); }
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (grillaUsuarios.SelectedRows.Count > 0)
            {
                if (grillaUsuarios.CurrentRow.Cells[6].Value.ToString() != IdiomaManager.GetInstance().ConseguirTexto("false"))
                {
                    modoOperacion = EnumModoAplicar.Desbloquear;
                    BloquearBotones();
                    lblMensaje.Text = IdiomaManager.GetInstance().ConseguirTexto("modoDesbloquear") + $" DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noEstaBloqueado")); }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccionarUsuario")); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearBotones();
        }

        private void ResetearBotones()
        {
            btnModificar.Enabled = true; btnEliminar.Enabled = true; btnDesbloquear.Enabled = true; btnAgregar.Enabled = true; btnResetearClave.Enabled = true;
            modoOperacion = EnumModoAplicar.Consulta;
            lblMensaje.Text = IdiomaManager.GetInstance().ConseguirTexto("lblMensaje");
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
                btnCancelar.Enabled = true; txtDNI.Enabled = false;
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
            grillaUsuarios.Rows.Clear();
            lstUsuarios = bllUsuario.TraerListaUsuarios();
            string boolActivo = "";
            string boolBloqueado = "";

            foreach(var u in lstUsuarios)
            {
                if(u.Activo == true)
                {
                    boolActivo = IdiomaManager.GetInstance().ConseguirTexto("true");
                }
                else { boolActivo = IdiomaManager.GetInstance().ConseguirTexto("false"); }

                if (u.Bloqueado == true)
                {
                    boolBloqueado = IdiomaManager.GetInstance().ConseguirTexto("true");
                }
                else { boolBloqueado = IdiomaManager.GetInstance().ConseguirTexto("false"); }


                grillaUsuarios.Rows.Add(u.DNI, u.Nombre, u.Apellido, u.Email, u.NombreUsuario, u.Rol.Nombre, boolBloqueado, boolActivo);

            }
           
            grillaUsuarios.BindingContext = new BindingContext(); //ESTO ES PARA COLOREAR EN ROJO A LOS NO ACTIVOS. ASEGURA QUE SE LLENEN BIEN LOS DATOS DEL GRIDVIEW
            foreach (DataGridViewRow row in grillaUsuarios.Rows)
            {
                if (row.Cells[7].Value != null && row.Cells[7].Value.ToString() == IdiomaManager.GetInstance().ConseguirTexto("false"))
                {
                    row.DefaultCellStyle.BackColor = Color.Crimson; //pone en rojo el background
                }
            }


            //llena el combobox de Roles
            cmbRol.Items.Clear();
            List<Familia> roles = bllFamilia.TraerListaRoles();
            foreach (var rol in roles)
            {
                cmbRol.Items.Add(rol.Nombre);
            }
        }


        private bool ValidarCampos()
        {

            if (!Regex.IsMatch(txtDNI.Text, @"^\d{7,9}$"))
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("errorDNI"));
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
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("errorMail"));
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
                if (grillaUsuarios.CurrentRow.Cells[7].Value.ToString() == IdiomaManager.GetInstance().ConseguirTexto("false"))
                {
                    btnEliminar.Text = IdiomaManager.GetInstance().ConseguirTexto("btnActivar");
                }
                else { btnEliminar.Text = IdiomaManager.GetInstance().ConseguirTexto("btnEliminar"); }

                if (modoOperacion == EnumModoAplicar.Modificar)
                {
                    LlenarCamposConDatos();
                    lblMensaje.Text = IdiomaManager.GetInstance().ConseguirTexto("modoModificar") + $" DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
                }
                else if (modoOperacion == EnumModoAplicar.Eliminar)
                {
                    lblMensaje.Text = IdiomaManager.GetInstance().ConseguirTexto("modoEliminar") + $" DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
                }
                else if (modoOperacion == EnumModoAplicar.Desbloquear)
                {
                    if (grillaUsuarios.CurrentRow.Cells[6].Value.ToString() != IdiomaManager.GetInstance().ConseguirTexto("false"))
                    {
                        lblMensaje.Text = IdiomaManager.GetInstance().ConseguirTexto("modoDesbloquear") + $" DNI: {grillaUsuarios.CurrentRow.Cells[0].Value}";
                    }
                    else
                    {
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noEstaBloqueado"));
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
            cmbRol.SelectedItem = grillaUsuarios.CurrentRow.Cells[5].Value.ToString();

            Familia rol = bllFamilia.TraerListaFamilias().FirstOrDefault(r => r.Nombre == cmbRol.Text);
        }

        private void btnResetearClave_Click(object sender, EventArgs e)
        {
            if (grillaUsuarios.SelectedRows.Count > 0)
            {
                int DNI = Convert.ToInt32(grillaUsuarios.CurrentRow.Cells[0].Value);
                string apellido = grillaUsuarios.CurrentRow.Cells[2].Value.ToString();
                string clave = DNI + apellido;

                DialogResult resultado = MessageBox.Show($"{IdiomaManager.GetInstance().ConseguirTexto("estaSeguroClave")} {DNI}?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        string username = grillaUsuarios.CurrentRow.Cells[4].Value.ToString();
                        bllUsuario.CambiarClave(DNI, Encriptador.EncriptarSHA256(clave));
                        bllUsuario.ModificarContFallido(username, 0);
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("operacionExitosa"));
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
                    string texto = numUpDown.Text;

                    if (texto.Length >= 9)
                    {
                        e.Handled = true;
                    }

                    /*no puede escribir . - ,*/
                    if (e.KeyChar == '.' || e.KeyChar == ',' || e.KeyChar == '-')
                    {
                        e.Handled = true;
                    }
                }
            }
        }
    }
}
