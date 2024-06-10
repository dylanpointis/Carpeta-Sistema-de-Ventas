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
    public partial class frmLogin : Form, IObserver
    {
        public frmLogin()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().Agregar(this);
            IdiomaManager.GetInstance().archivoActual = "frmLogin";
            SessionManager.IdiomaActual = "esp";
        }

        public void ActualizarIdioma()
        {
            FormIdiomas.ActualizarControles(this);
        }

        BLLUsuario bllUsuario = new BLLUsuario();
        int contClaveIncorrecta = 0;


        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (SessionManager.GetInstance.ObtenerUsuario() == null)
            {
                BEUsuario user = bllUsuario.ValidarUsuario(txtNombreUsuario.Text, 0, ""); //verifica si existe el usuario por el username

                if(user != null)
                {
                    if(user.Bloqueado == false && user.Activo == true)
                    {
                        if (user.Clave == Encriptador.EncriptarSHA256(txtClave.Text))
                        {
                            SessionManager.GetInstance.LogIn(user);


                            contClaveIncorrecta = 0;
                            this.Hide(); //oculta el formulario actual
                            frmMenu frmMenu = new frmMenu();
                            frmMenu.Show();

                            frmMenu.FormClosing += CerrandoFormulario; //cuando se cierra el formulario menu ejecuta la funcion CerrandoFormulario que vuelve a mostrar el form login

                        }
                        else
                        {
                            contClaveIncorrecta++;
                            MessageBox.Show("Clave incorrecta, vuelva a intentarlo");
                            if (contClaveIncorrecta == 3)
                            {
                                MessageBox.Show("Se ha bloqueado su usuario, comuníquese con el administrador");
                                bllUsuario.ModificarBloqueo(user.DNI, true);
                            }
                        }
                    }
                    else { MessageBox.Show("El usuario se encuentra Bloqueado o Desactivado, comuniquese con el administrador"); }
                   
                    
                }
                else { MessageBox.Show("No se encontró al usuario ingresado"); }
            }
            else { MessageBox.Show("Ya hay una sesión iniciada"); }
        }




        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtNombreUsuario.Text = "Admin";
            txtClave.Text = "clave123";
        }


        private void CerrandoFormulario(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            if (SessionManager.GetInstance.ObtenerUsuario() != null)
            {
                SessionManager.GetInstance.LogOut();
                MessageBox.Show("Se cerró la sesión");
            }
            else { MessageBox.Show("No hay una sesión iniciada"); }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        bool oculto = true;

        private void btnMostrarClave_Click(object sender, EventArgs e)
        {
            if(oculto == true)
            {
                txtClave.PasswordChar = '\0';
                btnMostrarClave.BackgroundImage = Properties.Resources.invisible;
                oculto = false;
            }
            else { 
                txtClave.PasswordChar = '*';
                oculto =true;
                btnMostrarClave.BackgroundImage = Properties.Resources.visible;
            }
        }

    }
}
