using BE;
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
using System.Net.Http.Headers;
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
        BLLDigitoVerificador bllDV = new BLLDigitoVerificador();
        BLLEvento bllEvento = new BLLEvento();
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (txtNombreUsuario.Text == "" || txtClave.Text == "")
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("llene"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bllDV.CompararDV(txtNombreUsuario.Text);
                bllUsuario.Login(txtNombreUsuario.Text, txtClave.Text); //LOGICA LOGIN
                this.Hide(); //oculta el formulario actual
                frmMenu frmMenu = new frmMenu();
                frmMenu.Show();

                frmMenu.FormClosing += CerrandoFormulario; //cuando se cierra el formulario menu ejecuta la funcion CerrandoFormulario que vuelve a mostrar el form 
            }
            catch (Exception ex)
            {
                // Muestra el mensaje de error desde la excepción.
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if(ex.Message == IdiomaManager.GetInstance().ConseguirTexto("inconsistenciaDV")) //"inconsistenciaDV" es el mensaje que muestra al usuario normal. No admin
                {
                    frmRepararDigitoVerificador form = new frmRepararDigitoVerificador();
                    form.ShowDialog();
                }
            }
        }




        


        private void CerrandoFormulario(object sender, FormClosingEventArgs e)
        {
            IdiomaManager.GetInstance().PrimeraVez = true; //hace el logout y registra el evento

            if (SessionManager.GetInstance.ObtenerUsuario() != null)
            {
                bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Sesiones", "Cierre sesión", 1, "", ""));
                SessionManager.GetInstance.LogOut();
            }

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
