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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmMaestroClientes : Form, IObserver
    {
        public frmMaestroClientes()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().archivoActual = "frmMaestroClientes";
            IdiomaManager.GetInstance().Agregar(this);
        }

        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        List<BECliente> listaClientes = new List<BECliente>();
        BLLCliente bllCliente = new BLLCliente();
        EnumModoAplicar modoOperacion;


        private void frmMaestroClientes_Load(object sender, EventArgs e)
        {

            grillaClientes.ColumnCount = 5;
            grillaClientes.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewDNI");
            grillaClientes.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewNombre");
            grillaClientes.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewApellido");
            grillaClientes.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewMail");
            grillaClientes.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewDireccion");


            modoOperacion = EnumModoAplicar.Consulta;
            ActualizarGrilla();
            ResetearBotones();
        }



        private void ActualizarGrilla()
        {
            listaClientes = bllCliente.TraerListaCliente();
            grillaClientes.Rows.Clear();
            foreach (BECliente c in listaClientes)
            {
                grillaClientes.Rows.Add(c.DniCliente, c.Nombre, c.Apellido, c.Mail, c.Direccion);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarGrilla();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(txtDNI.Text != "" && txtDNI.Text != "0")
            {
                if (Regex.IsMatch(txtDNI.Text, @"^\d{7,9}$"))
                {
                    BECliente clienteEncontrado = bllCliente.VerificarCliente(Convert.ToInt32(txtDNI.Text));
                    if (clienteEncontrado == null)
                    {
                        modoOperacion = EnumModoAplicar.Añadir;
                        BloquearBotones();
                        lblMensaje.Text = IdiomaManager.GetInstance().ConseguirTexto("modoAñadir");
                    }
                    else
                    {
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("yaExiste"));
                        txtDNI.Focus();
                    }

                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("dni7y9")); }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("ingreseDNI")); }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (grillaClientes.SelectedRows.Count > 0)
            {
                LlenarCampos();
                modoOperacion = EnumModoAplicar.Modificar;
                BloquearBotones();
                lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoModificar")}: {grillaClientes.CurrentRow.Cells[0].Value}";
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccione")); }
        }

        private void LlenarCampos()
        {
            txtDNI.Text = grillaClientes.CurrentRow.Cells[0].Value.ToString();
            txtNombre.Text = grillaClientes.CurrentRow.Cells[1].Value.ToString();
            txtApellido.Text = grillaClientes.CurrentRow.Cells[2].Value.ToString();
            txtMail.Text = grillaClientes.CurrentRow.Cells[3].Value.ToString();
            txtDireccion.Text = grillaClientes.CurrentRow.Cells[4].Value.ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (grillaClientes.SelectedRows.Count > 0)
            {
                modoOperacion = EnumModoAplicar.Eliminar;
                BloquearBotones();
                lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoEliminar")}: {grillaClientes.CurrentRow.Cells[0].Value}";
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccione")); }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (modoOperacion == EnumModoAplicar.Consulta)
            {
                ConsultarClientes();
            }
            else
            {
                if (modoOperacion == EnumModoAplicar.Añadir)
                {
                    if (ValidarCampos())
                    {
                        BECliente clienteEncontrado = bllCliente.VerificarCliente(Convert.ToInt32(txtDNI.Text));
                        if (clienteEncontrado != null)
                        {
                            MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("yaExiste")); return;
                        }
                        else
                        {
                            BECliente cli = new BECliente(Convert.ToInt32(txtDNI.Text), txtNombre.Text, txtApellido.Text, txtMail.Text, Encriptador.EncriptarAES(txtDireccion.Text));
                            bllCliente.RegistrarCliente(cli);
                            MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"));
                        }
                    }
                    else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("llenarCampos")); return; }
                }
                else
                {
                    if (modoOperacion == EnumModoAplicar.Eliminar)
                    {
                        DialogResult resultado = MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("estaSeguro") + " " + grillaClientes.CurrentRow.Cells[0].Value + " ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultado == DialogResult.Yes)
                        {
                            int dniCliente = Convert.ToInt32(grillaClientes.CurrentRow.Cells[0].Value);

                            if(bllCliente.VerificarSiClienteTieneFacturas(dniCliente) == false)
                            {
                                bllCliente.EliminarCliente(dniCliente);
                                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"));
                            }
                            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("tieneFacturas")); }
                        }
                    }
                    if (modoOperacion == EnumModoAplicar.Modificar)
                    {
                        BECliente cliente = new BECliente(Convert.ToInt32(txtDNI.Text), txtNombre.Text, txtApellido.Text, txtMail.Text, Encriptador.EncriptarAES(txtDireccion.Text));
                        bllCliente.ModificarCliente(cliente);
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"));
                    }
                }

                ResetearBotones();
                ActualizarGrilla();
            }
        }

        private bool ValidarCampos()
        {
            if (txtDNI.Text == "" || txtNombre.Text == "" || txtApellido.Text == "" || txtMail.Text == "" || txtDireccion.Text == "")
            {
                return false;
            }
            if (!Regex.IsMatch(txtMail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("formato"));
                txtMail.Focus();
                return false;
            }
            return true;
        }

        private void ConsultarClientes()
        {
            grillaClientes.DataSource = null;
            List<BECliente> lstConsulta = new List<BECliente>();
            foreach (BECliente cli in listaClientes)
            {
                if (cli.DniCliente.ToString() == txtDNI.Text || cli.Mail.ToString() == txtMail.Text)
                {
                    lstConsulta.Add(cli);
                }
                if (!string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    if (cli.Nombre.ToLower().Contains(txtNombre.Text.ToLower()) && !lstConsulta.Contains(cli))
                    {
                        lstConsulta.Add(cli);
                    }
                }
                if (!string.IsNullOrWhiteSpace(txtApellido.Text))
                {
                    if (cli.Apellido.ToLower().Contains(txtApellido.Text.ToLower()) && !lstConsulta.Contains(cli))
                    {
                        lstConsulta.Add(cli);
                    }
                }
                if (!string.IsNullOrWhiteSpace(txtDireccion.Text))
                {
                    if (cli.Direccion.ToLower().Contains(txtDireccion.Text.ToLower()) && !lstConsulta.Contains(cli))
                    {
                        lstConsulta.Add(cli);
                    }
                }
            }

            grillaClientes.Rows.Clear();
            foreach(BECliente c in lstConsulta)
            {
                grillaClientes.Rows.Add(c.DniCliente, c.Nombre, c.Apellido, c.Mail, c.Direccion);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearBotones();
        }

        private void BloquearBotones()
        {
            btnModificar.Enabled = false;
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;
            if (modoOperacion == EnumModoAplicar.Modificar)
            {
                txtDNI.Enabled = false;
            }
            if (modoOperacion == EnumModoAplicar.Eliminar)
            {
                txtDNI.Enabled = false;
                txtNombre.Enabled = false;
                txtApellido.Enabled = false;
                txtMail.Enabled = false;
                txtDireccion.Enabled = false;
            }
        }
        private void ResetearBotones()
        {
            modoOperacion = EnumModoAplicar.Consulta;
            lblMensaje.Text = IdiomaManager.GetInstance().ConseguirTexto("lblMensaje");
            txtDNI.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtMail.Text = "";
            txtDireccion.Text = "";

            txtDNI.Enabled = true;
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtMail.Enabled = true;
            txtDireccion.Enabled = true;
            btnModificar.Enabled = true;
            btnAgregar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void grillaClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(grillaClientes.SelectedRows.Count > 0)
            {
                if (modoOperacion == EnumModoAplicar.Eliminar)
                {
                    lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoEliminar")}: {grillaClientes.CurrentRow.Cells[0].Value}";
                }
                if (modoOperacion == EnumModoAplicar.Modificar)
                {
                    LlenarCampos();
                    lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoModificar")}: {grillaClientes.CurrentRow.Cells[0].Value}";
                }
            }
        }

        /*EVENTO PARA QUE NO PUEDA ESCRIBIR DNI MAS DE 9 DIGITOS*/
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
