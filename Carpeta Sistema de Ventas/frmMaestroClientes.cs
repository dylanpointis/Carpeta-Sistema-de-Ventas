using BE;
using BLL;
using Services;
using Services.Observer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

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
        BLLEvento bllEvento = new BLLEvento();
        Serializacion serializacion = new Serializacion();

        EnumModoAplicar modoOperacion;


        private void frmMaestroClientes_Load(object sender, EventArgs e)
        {
            grillaClientes.ColumnCount = 6;
            grillaClientes.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewDNI");
            grillaClientes.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewNombre");
            grillaClientes.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewApellido");
            grillaClientes.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewMail");
            grillaClientes.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewDireccion");

            grillaClientes.Columns[5].Name = "Borrado";

            grillaClientes.Columns[5].Visible = false;




            modoOperacion = EnumModoAplicar.Consulta;
            listaClientes = bllCliente.TraerListaCliente();
            ActualizarGrilla();
            ResetearBotones();
        }



        private void ActualizarGrilla()
        {
            grillaClientes.Rows.Clear();
            foreach (BECliente c in listaClientes)
            {
                grillaClientes.Rows.Add(c.DniCliente, c.Nombre, c.Apellido, c.Mail, c.Direccion, c.BorradoLogico);
            }

            grillaClientes.BindingContext = new BindingContext(); //ESTO ES PARA COLOREAR EN ROJO A LOS NO ACTIVOS. ASEGURA QUE SE LLENEN BIEN LOS DATOS DEL GRIDVIEW
            foreach (DataGridViewRow row in grillaClientes.Rows)
            {
                if (row.Cells[5].Value != null && row.Cells[5].Value.ToString() == "False")
                {
                    row.DefaultCellStyle.BackColor = Color.Crimson; //pone en rojo el background
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            listaClientes = bllCliente.TraerListaCliente();
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
                if (grillaClientes.CurrentRow.Cells[5].Value.ToString() == "True")
                {
                    LlenarCampos();
                    modoOperacion = EnumModoAplicar.Modificar;
                    BloquearBotones();
                    lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoModificar")}: {grillaClientes.CurrentRow.Cells[0].Value}";
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noSePuedeModificar"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); } //No se puede modificar a un cliente deshabilitado
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
                if (btnEliminar.Text == IdiomaManager.GetInstance().ConseguirTexto("deshabilitar")) //si dice deshabilitar
                {
                    modoOperacion = EnumModoAplicar.Eliminar;
                    BloquearBotones();
                    lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoEliminar")}: {grillaClientes.CurrentRow.Cells[0].Value}";
                }
                else //si dice habilitar
                {
                    modoOperacion = EnumModoAplicar.Activar;
                    BloquearBotones();
                    lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoActivar")}: {grillaClientes.CurrentRow.Cells[0].Value}";
                }
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
                        try
                        {
                            BECliente cli = new BECliente(Convert.ToInt32(txtDNI.Text), txtNombre.Text, txtApellido.Text, txtMail.Text, txtDireccion.Text);
                            bllCliente.RegistrarCliente(cli);
                            MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex){ MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    }
                    else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("llenarCampos"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                }
                else
                {
                    if (modoOperacion == EnumModoAplicar.Eliminar)
                    {
                        DialogResult resultado = MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("estaSeguro") + " " + grillaClientes.CurrentRow.Cells[0].Value + " ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultado == DialogResult.Yes)
                        {
                            int dniCliente = Convert.ToInt32(grillaClientes.CurrentRow.Cells[0].Value);

                            bllCliente.EliminarCliente(dniCliente);

                            bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Clientes", "Cliente eliminado", 3));
                            MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                        }
                    }
                    if (modoOperacion == EnumModoAplicar.Modificar)
                    {
                        if(grillaClientes.CurrentRow.Cells[5].Value.ToString() == "True") // si esta habilitado se puede modificar
                        {
                            if (!ValidarCampos()) { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("llenarCampos"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                            BECliente cliente = new BECliente(Convert.ToInt32(txtDNI.Text), txtNombre.Text, txtApellido.Text, txtMail.Text, Encriptador.EncriptarAES(txtDireccion.Text));
                            cliente.BorradoLogico = true;
                            bllCliente.ModificarCliente(cliente);

                            bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Clientes", "Cliente modificado", 4));
                            MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noSePuedeModificar"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); } //No se puede modificar a un cliente deshabilitado
                    }
                    if(modoOperacion == EnumModoAplicar.Activar)
                    {
                        DialogResult resultado = MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("estaSeguroHabilitar") + " " + grillaClientes.CurrentRow.Cells[0].Value + " ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultado == DialogResult.Yes)
                        {
                            int dni = Convert.ToInt32(grillaClientes.CurrentRow.Cells[0].Value);

                            bllCliente.HabilitarCliente(dni);

                            bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Clientes", "Cliente habilitado", 2));
                            MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
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
            listaClientes = lstConsulta;
            ActualizarGrilla();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearBotones();
            ActualizarGrilla();
        }

        private void BloquearBotones()
        {
            btnModificar.Enabled = false;
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;
            btnSerializar.Enabled = false;
            btnDeserializar.Enabled = false;
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
            btnSerializar.Enabled = true;
            btnDeserializar.Enabled = true;

            listaClientes = bllCliente.TraerListaCliente();
            listBoxArchivoSerializado.Items.Clear();
        }

        private void grillaClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(grillaClientes.SelectedRows.Count > 0)
            {
                int dni = Convert.ToInt32(grillaClientes.CurrentRow.Cells[0].Value);

                if (modoOperacion == EnumModoAplicar.Eliminar)
                {
                    lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoEliminar")}: {dni}";
                }
                if (modoOperacion == EnumModoAplicar.Modificar)
                {
                    if (grillaClientes.CurrentRow.Cells[5].Value.ToString() == "True")//Si esta habilitado se puede modificar
                    {
                        LlenarCampos();
                        lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoModificar")}: {dni}";
                    }
                    else //Si no esta habilitado no se puede modificar
                    {
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noSePuedeModificar"));
                        ResetearBotones();
                    }
                }

                BECliente cli = bllCliente.VerificarCliente(dni);
                if(cli.BorradoLogico == false)
                {
                    btnEliminar.Text = IdiomaManager.GetInstance().ConseguirTexto("habilitar");
                }
                else { btnEliminar.Text = IdiomaManager.GetInstance().ConseguirTexto("deshabilitar"); }
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



        //SERIALIZACION - DESERIALIZACION XML

        private void btnSerializar_Click(object sender, EventArgs e)
        {
            if (grillaClientes.Rows.Count > 0)
            {
                try
                {
                    string nombreArchivo = serializacion.SerializarClientes(listaClientes);
                    MostrarArchivoSerializado(nombreArchivo);
                    grillaClientes.Rows.Clear();
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exitoSerializacion"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bllEvento.RegistrarEvento((new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Clientes", "Archivo serializado", 4)));
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
                
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noHayRegistros"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); } //No hay ningun registro en la grilla para serializar
        }


        private void MostrarArchivoSerializado(string path)
        {
            listBoxArchivoSerializado.Items.Clear();
            string[] lineas = File.ReadAllLines(path);

            foreach (string linea in lineas)
            {
                listBoxArchivoSerializado.Items.Add(linea);
            }
        }


        private void btnDeserializar_Click(object sender, EventArgs e)
        {
            try
            {
                listaClientes = serializacion.Deseriaizar();

                MostrarDatosDeserializados(listaClientes);

                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exitoDeserializacion"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bllEvento.RegistrarEvento((new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Clientes", "Archivo deserializado", 5)));
                grillaClientes.Rows.Clear();
                ActualizarGrilla();
                
            }
            catch(Exception ex) { MessageBox.Show("Error: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void MostrarDatosDeserializados(List<BECliente> listaClientes)
        {
            listBoxArchivoSerializado.Items.Clear();
            foreach(BECliente cli in listaClientes)
            {
                listBoxArchivoSerializado.Items.Add($"{IdiomaManager.GetInstance().ConseguirTexto("gridViewDNI")}: {cli.DniCliente}");
                listBoxArchivoSerializado.Items.Add($"{IdiomaManager.GetInstance().ConseguirTexto("gridViewNombre")}: {cli.Nombre}");
                listBoxArchivoSerializado.Items.Add($"{IdiomaManager.GetInstance().ConseguirTexto("gridViewApellido")}: {cli.Apellido}");
                listBoxArchivoSerializado.Items.Add($"{IdiomaManager.GetInstance().ConseguirTexto("gridViewMail")}: {cli.Mail}");
                listBoxArchivoSerializado.Items.Add($"{IdiomaManager.GetInstance().ConseguirTexto("gridViewDireccion")}: {cli.Direccion}");
                listBoxArchivoSerializado.Items.Add($"--------------------------");
            }
        }
    }
}
