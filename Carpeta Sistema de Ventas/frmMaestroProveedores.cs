using BE;
using BLL;
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
    public partial class frmMaestroProveedores : Form, IObserver
    {
        public frmMaestroProveedores()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().archivoActual = "frmMaestroProveedores";
            IdiomaManager.GetInstance().Agregar(this);
        }

        List<BEProveedor> listaProvs = new List<BEProveedor>();
        BLLProveedor bllProv = new BLLProveedor();
        EnumModoAplicar modoOperacion;

        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        private void frmMaestroProveedores_Load(object sender, EventArgs e)
        {
            grillaProveedores.ColumnCount = 9;
            grillaProveedores.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCUIT");
            grillaProveedores.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewNombre");
            grillaProveedores.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewRazonSocial");
            grillaProveedores.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewNumTelefono");
            grillaProveedores.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewEmail");
            grillaProveedores.Columns[5].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewDireccion");
            grillaProveedores.Columns[6].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewBanco");
            grillaProveedores.Columns[7].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCBU");
            grillaProveedores.Columns[8].Name = "Borrado";
            grillaProveedores.Columns[8].Visible = false;
            modoOperacion = EnumModoAplicar.Consulta;
            listaProvs = bllProv.TraerListaProveedores();
            ActualizarGrilla();
            ResetearBotones();
        }

      
        private void ActualizarGrilla()
        {
            grillaProveedores.Rows.Clear();
            foreach (BEProveedor p in listaProvs)
            {
                grillaProveedores.Rows.Add(p.CUIT, p.Nombre, p.RazonSocial, p.NumTelefono, p.Email, p.Direccion, p.Banco, p.CBU, p.BorradoLogico);
            }

            grillaProveedores.BindingContext = new BindingContext(); //ESTO ES PARA COLOREAR EN ROJO A LOS NO ACTIVOS. ASEGURA QUE SE LLENEN BIEN LOS DATOS DEL GRIDVIEW
            foreach (DataGridViewRow row in grillaProveedores.Rows)
            {
                if (row.Cells[8].Value != null && row.Cells[8].Value.ToString() == "False")
                {
                    row.DefaultCellStyle.BackColor = Color.Crimson; //pone en rojo el background
                }
           }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtCUIT.Text != "" && txtCUIT.Text != "0")
            {
                if(Regex.IsMatch(txtCUIT.Text, @"^\d{2}-\d{8}-\d{1}$")) //CUIT FORMATO "XX-XXXXXXXX-X",
                {
                    BEProveedor provEncontrado = bllProv.VerificarProveedor(txtCUIT.Text, "","");
                    if (provEncontrado == null)
                    {
                        modoOperacion = EnumModoAplicar.Añadir;
                        BloquearBotones();
                        lblMensaje.Text = IdiomaManager.GetInstance().ConseguirTexto("modoAñadir");
                    }
                    else
                    {
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("yaExisteCUIT"));
                        txtCUIT.Focus();
                    }
                }
                else
                {
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("formatoCUIT"));
                    txtMail.Focus();
                }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("ingreseCUIT")); }
        }

        private void BloquearBotones()
        {
            btnModificar.Enabled = false;
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;
            if (modoOperacion == EnumModoAplicar.Modificar)
            {
                txtCUIT.Enabled = false;
            }
            if (modoOperacion == EnumModoAplicar.Eliminar)
            {
                txtCUIT.Enabled = false;
                txtNombre.Enabled = false;
                txtRazonSocial.Enabled = false;
                txtMail.Enabled = false;
                txtDireccion.Enabled = false;
                txtBanco.Enabled = false;
                txtCBU.Enabled = false;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (grillaProveedores.SelectedRows.Count > 0)
            {
                if (grillaProveedores.CurrentRow.Cells[8].Value.ToString() == "True")
                {
                    LlenarCampos();
                    modoOperacion = EnumModoAplicar.Modificar;
                    BloquearBotones();
                    lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoModificar")}: {grillaProveedores.CurrentRow.Cells[0].Value}";
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noSePuedeModificar"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); } //No se puede modificar a un cliente deshabilitado
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccione")); }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (grillaProveedores.SelectedRows.Count > 0)
            {
                if (btnEliminar.Text == IdiomaManager.GetInstance().ConseguirTexto("deshabilitar")) //si dice deshabilitar
                {
                    modoOperacion = EnumModoAplicar.Eliminar;
                    BloquearBotones();
                    lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoEliminar")}: {grillaProveedores.CurrentRow.Cells[0].Value}";
                }
                else //si dice habilitar
                {
                    modoOperacion = EnumModoAplicar.Activar;
                    BloquearBotones();
                    lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoActivar")}: {grillaProveedores.CurrentRow.Cells[0].Value}";
                }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccione")); }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (modoOperacion == EnumModoAplicar.Consulta)
            {
                ConsultarProveedores();
            }
            else
            {
                if (modoOperacion == EnumModoAplicar.Añadir)
                {
                    if (ValidarCampos())
                    {
                        try
                        {
                            BEProveedor prov = new BEProveedor(txtCUIT.Text, txtNombre.Text, txtRazonSocial.Text, txtMail.Text, txtNumTelefono.Text, txtDireccion.Text, txtBanco.Text, txtCBU.Text);
                            bllProv.RegistrarProveedor(prov);
                            MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    }
                    else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("llenarCampos"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                }
                else
                {
                    if (modoOperacion == EnumModoAplicar.Eliminar)
                    {
                        DialogResult resultado = MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("estaSeguro") + " " + grillaProveedores.CurrentRow.Cells[0].Value + " ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultado == DialogResult.Yes)
                        {
                            string cuit = grillaProveedores.CurrentRow.Cells[0].Value.ToString();

                            bllProv.EliminarProveedor(cuit);

                            //bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Clientes", "Cliente eliminado", 3));
                            MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                    if (modoOperacion == EnumModoAplicar.Modificar)
                    {
                        if (grillaProveedores.CurrentRow.Cells[8].Value.ToString() == "True") // si esta habilitado se puede modificar
                        {
                            if (!ValidarCampos()) { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("llenarCampos"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                            try
                            {
                                BEProveedor prov = new BEProveedor(txtCUIT.Text, txtNombre.Text, txtRazonSocial.Text, txtMail.Text, txtNumTelefono.Text, txtDireccion.Text, txtBanco.Text, txtCBU.Text);
                                bllProv.ModificarProveedor(prov);
                                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch(Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                        }
                        else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noSePuedeModificar"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); } //No se puede modificar a un cliente deshabilitado
                    }
                    if (modoOperacion == EnumModoAplicar.Activar)
                    {
                        DialogResult resultado = MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("estaSeguroHabilitar") + " " + grillaProveedores.CurrentRow.Cells[0].Value + " ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultado == DialogResult.Yes)
                        {
                            string cuit = grillaProveedores.CurrentRow.Cells[0].Value.ToString();

                            bllProv.HabilitarProveedor(cuit);

                            MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                ResetearBotones();
                ActualizarGrilla();
            }
        }

        private void ConsultarProveedores()
        {
            List<BEProveedor> lstConsulta = new List<BEProveedor>();
            foreach (BEProveedor p in listaProvs)
            {
                if (p.CUIT.ToString() == txtCUIT.Text || p.Email.ToString() == txtMail.Text)
                    lstConsulta.Add(p);

                if (!string.IsNullOrWhiteSpace(txtCBU.Text))
                {
                    if (p.CBU.ToString() == txtCBU.Text)
                        lstConsulta.Add(p);
                }
                if (!string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    if (p.Nombre.ToLower().Contains(txtNombre.Text.ToLower()) && !lstConsulta.Contains(p))
                        lstConsulta.Add(p);
                }
                if (!string.IsNullOrWhiteSpace(txtRazonSocial.Text))
                {
                    if (p.RazonSocial.ToLower().Contains(txtRazonSocial.Text.ToLower()) && !lstConsulta.Contains(p))
                        lstConsulta.Add(p);
                }
                if (!string.IsNullOrWhiteSpace(txtDireccion.Text))
                {
                    if (p.Direccion.ToLower().Contains(txtDireccion.Text.ToLower()) && !lstConsulta.Contains(p))
                        lstConsulta.Add(p);
                }
                if (!string.IsNullOrWhiteSpace(txtBanco.Text))
                {
                    if (p.Banco.ToLower().Contains(txtBanco.Text.ToLower()) && !lstConsulta.Contains(p))
                        lstConsulta.Add(p);
                }
            }

            grillaProveedores.Rows.Clear();
            listaProvs = lstConsulta;
            ActualizarGrilla();
        }

        private bool ValidarCampos()
        {
            if (txtCUIT.Text == "" || txtNombre.Text == "" || txtRazonSocial.Text == "" || txtMail.Text == "" || txtDireccion.Text == "" || txtNumTelefono.Text == "")
            {
                return false;
            }

            if(!string.IsNullOrWhiteSpace(txtCBU.Text) && txtCBU.Text.Length != 22)
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("longCBU"));
                txtMail.Focus();
                return false;
            }

            if (!Regex.IsMatch(txtCUIT.Text, @"^\d{2}-\d{8}-\d{1}$")) //CUIT FORMATO "XX-XXXXXXXX-X",
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("formatoCUIT"));
                txtMail.Focus();
                return false;
            }
            if (!Regex.IsMatch(txtMail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("formatoMail"));
                txtMail.Focus();
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearBotones();
        }



        private void LlenarCampos()
        {
            txtCUIT.Text = grillaProveedores.CurrentRow.Cells[0].Value.ToString();
            txtNombre.Text = grillaProveedores.CurrentRow.Cells[1].Value.ToString();
            txtRazonSocial.Text = grillaProveedores.CurrentRow.Cells[2].Value.ToString();
            txtNumTelefono.Text = grillaProveedores.CurrentRow.Cells[3].Value.ToString();
            txtMail.Text = grillaProveedores.CurrentRow.Cells[4].Value.ToString();
            txtDireccion.Text = grillaProveedores.CurrentRow.Cells[5].Value.ToString();
            txtBanco.Text = grillaProveedores.CurrentRow.Cells[6].Value.ToString();
            txtCBU.Text = grillaProveedores.CurrentRow.Cells[7].Value.ToString();
        }
        private void ResetearBotones()
        {
            modoOperacion = EnumModoAplicar.Consulta;
            lblMensaje.Text = IdiomaManager.GetInstance().ConseguirTexto("lblMensaje");
            txtCUIT.Text = "";
            txtNombre.Text = "";
            txtRazonSocial.Text = "";
            txtMail.Text = "";
            txtDireccion.Text = "";
            txtCBU.Text = "";
            txtBanco.Text = "";
            txtNumTelefono.Text = "";
            txtCUIT.Enabled = true;
            txtNombre.Enabled = true;
            txtRazonSocial.Enabled = true;
            txtMail.Enabled = true;
            txtDireccion.Enabled = true;
            txtCBU.Enabled = true;
            txtBanco.Enabled = true;
            btnModificar.Enabled = true;
            btnAgregar.Enabled = true;
            btnEliminar.Enabled = true;

            listaProvs = bllProv.TraerListaProveedores();
        }



        //EVENTOS

        private void grillaProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grillaProveedores.SelectedRows.Count > 0)
            {
                string CUIT = grillaProveedores.CurrentRow.Cells[0].Value.ToString();

                if (modoOperacion == EnumModoAplicar.Eliminar)
                {
                    lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoEliminar")}: {CUIT}";
                }
                if (modoOperacion == EnumModoAplicar.Modificar)
                {
                    if (grillaProveedores.CurrentRow.Cells[8].Value.ToString() == "True")//Si esta habilitado se puede modificar
                    {
                        LlenarCampos();
                        lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoModificar")}: {CUIT}";
                    }
                    else //Si no esta habilitado no se puede modificar
                    {
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noSePuedeModificar"));
                        ResetearBotones();
                    }
                }

                BEProveedor prov = bllProv.VerificarProveedor(CUIT, "","");
                if (prov.BorradoLogico == false)
                {
                    btnEliminar.Text = IdiomaManager.GetInstance().ConseguirTexto("habilitar");
                }
                else { btnEliminar.Text = IdiomaManager.GetInstance().ConseguirTexto("deshabilitar"); }
            }
        }

        private void txtCUIT_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (!char.IsControl(e.KeyChar))
                {
                    string texto = textBox.Text;

                    if (texto.Length >= 13)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            listaProvs = bllProv.TraerListaProveedores();
            ActualizarGrilla();
        }

        private void txtNumTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (!char.IsControl(e.KeyChar))
                {
                    string texto = textBox.Text;

                    if (texto.Length >= 10)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtCBU_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (!char.IsControl(e.KeyChar))
                {
                    string texto = textBox.Text;

                    if (texto.Length >= 22)
                    {
                        e.Handled = true;
                    }
                }
            }
        }
    }
}
