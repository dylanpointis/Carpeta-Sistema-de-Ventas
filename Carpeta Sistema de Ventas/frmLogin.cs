﻿using BE;
using BE.Composite;
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

        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtNombreUsuario.Text = "Admin";
            txtClave.Text = "clave123";
            btnMostrarClave.Text = "";
            txtNombreUsuario.Focus();
        }

        //Cuando carga por primera vez y cuando se vuelve a mostrar con el Metodo CerrandoFormulario() carga el archivo actual y lo agrega al sujeto
        private void frmLogin_VisibleChanged(object sender, EventArgs e)
        {
            IdiomaManager.GetInstance().archivoActual = "frmLogin";
            IdiomaManager.GetInstance().Agregar(this);
        }


        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        BLLUsuario bllUsuario = new BLLUsuario();
        BLLFamilia bllFamilia = new BLLFamilia();
        BLLEvento bllEvento = new BLLEvento();
      

        private void btnIniciar_Click(object sender, EventArgs e)
        {

            if(txtNombreUsuario.Text == "" || txtClave.Text == "")
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("llene"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (SessionManager.GetInstance.ObtenerUsuario() == null)
            {
                BEUsuario user = bllUsuario.ValidarUsuario(txtNombreUsuario.Text, 0, ""); //verifica si existe el usuario por el username

                if(user != null)
                {
                    if(user.Bloqueado == false && user.Activo == true)
                    {
                        int contClaveIncorrecta = user.ContFallidos;
                        if (user.Clave == Encriptador.EncriptarSHA256(txtClave.Text))
                        {
                            //trae los permisos segun su rol
                            List<Componente> listaPermisos = bllFamilia.TraerListaPermisosRol(user.codRol);
                            user.listaPermisosRol = listaPermisos;


                            SessionManager.GetInstance.LogIn(user);
                            IdiomaManager.GetInstance().PrimeraVez = true;

                            bllUsuario.ModificarContFallido(user.NombreUsuario, 0);

                            bllEvento.RegistrarEvento(new Evento(txtNombreUsuario.Text, "Sesiones", "Inicio sesión", 1, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));

                            this.Hide(); //oculta el formulario actual
                            frmMenu frmMenu = new frmMenu();
                            frmMenu.Show();

                            frmMenu.FormClosing += CerrandoFormulario; //cuando se cierra el formulario menu ejecuta la funcion CerrandoFormulario que vuelve a mostrar el form login

                        }
                        else
                        {
                            contClaveIncorrecta++;
                            bllUsuario.ModificarContFallido(user.NombreUsuario, contClaveIncorrecta);
                            MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("incorrecta"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (contClaveIncorrecta == 3)
                            {
                                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seBloqueo"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                bllUsuario.ModificarBloqueo(user.DNI, true);

                                bllEvento.RegistrarEvento(new Evento(txtNombreUsuario.Text, "Sesiones", "Usuario bloqueado", 1, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));
                            }
                        }
                    }
                    else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("estaBloqueado"), "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                   
                    
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noSeEncontro"), "", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("yaInicio")); }
        }




        


        private void CerrandoFormulario(object sender, FormClosingEventArgs e)
        {
            IdiomaManager.GetInstance().PrimeraVez = true; //hace el logout y registra el evento

            if (SessionManager.GetInstance.ObtenerUsuario() != null)
            {
                BLLEvento bLLEvento = new BLLEvento();
                bLLEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Sesiones", "Cierre sesión", 1, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));
            }

            SessionManager.GetInstance.LogOut();
            this.Show(); //vuelve a mostrar este form de login
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

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            IdiomaManager.GetInstance().Quitar(this);
        }








        //eventos para que cuando termine de escribir (presione ENTER) haga focus al otro textbox

        private void txtNombreUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; //Evita el sonido de windows
                txtClave.Focus();
            }
        }

        private void txtClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; //Evita el sonido de windows
                btnIniciar.Focus();
            }
        }
    }
}
