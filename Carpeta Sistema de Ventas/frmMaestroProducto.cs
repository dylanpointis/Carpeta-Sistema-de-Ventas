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
    public partial class frmMaestroProducto : Form, IObserver
    {
        List<BEProducto> listaProd = new List<BEProducto> ();
        BLLProducto bllProducto = new BLLProducto();
        BLLEvento bllEv = new BLLEvento();
        EnumModoAplicar modoOperacion;
        public frmMaestroProducto()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().archivoActual = "frmMaestroProducto";
            IdiomaManager.GetInstance().Agregar(this);
        }

        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        private void frmMaestroProducto_Load(object sender, EventArgs e)
        {
            modoOperacion = EnumModoAplicar.Consulta;

            grillaProductos.ColumnCount = 9;
            grillaProductos.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCodigo");
            grillaProductos.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewModelo");
            grillaProductos.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewDescripcion");
            grillaProductos.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewMarca");
            grillaProductos.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewColor");
            grillaProductos.Columns[5].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewPrecio");
            grillaProductos.Columns[6].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewStock");
            grillaProductos.Columns[7].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewAlmacenamiento");
            grillaProductos.Columns[8].Name = "Borrado";

            grillaProductos.Columns[8].Visible = false;



            ActualizarGrilla();
            ResetearBotones();
            grillaProductos.Columns[0].Width = 57;
            grillaProductos.Columns[3].Width = 55;
            grillaProductos.Columns[4].Width = 55;
            grillaProductos.Columns[5].Width = 55;
            grillaProductos.Columns[6].Width = 55;
            grillaProductos.Columns[7].Width = 55;
        }

        private void ActualizarGrilla()
        {
            grillaProductos.Rows.Clear();
            listaProd = bllProducto.TraerListaProductos();
            foreach (BEProducto p in listaProd)
            {
                grillaProductos.Rows.Add(p.CodigoProducto, p.Modelo, p.Descripcion, p.Marca, p.Color, p.Precio, p.Stock, p.Almacenamiento, p.BorradoLogico);
            }


            grillaProductos.BindingContext = new BindingContext(); //ESTO ES PARA COLOREAR EN ROJO A LOS NO ACTIVOS. ASEGURA QUE SE LLENEN BIEN LOS DATOS DEL GRIDVIEW
            foreach (DataGridViewRow row in grillaProductos.Rows)
            {
                if (row.Cells[8].Value != null && row.Cells[8].Value.ToString() == "False")
                {
                    row.DefaultCellStyle.BackColor = Color.Crimson; //pone en rojo el background
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarGrilla();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtCodigoProducto.Text != "" && txtCodigoProducto.Text != "0" && txtCodigoProducto.Text.Length <= 13)
            {
                BEProducto prodEncontrado = listaProd.FirstOrDefault(p => p.CodigoProducto == Convert.ToInt64(txtCodigoProducto.Text));
                if (prodEncontrado == null)
                {
                    modoOperacion = EnumModoAplicar.Añadir;
                    BloquearBotones();
                    lblMensaje.Text = IdiomaManager.GetInstance().ConseguirTexto("modoAñadir");
                    //lblMensaje.Text = FormIdiomas.ConseguirTexto("modoAñadir");
                }
                else
                {
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("yaExiste"));
                    txtCodigoProducto.Focus();
                }
            }
            else
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("ingreseCodigo"));
                txtCodigoProducto.Focus();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (grillaProductos.SelectedRows.Count > 0)
            {
                long codProd = Convert.ToInt64(grillaProductos.CurrentRow.Cells[0].Value);
                if (grillaProductos.CurrentRow.Cells[8].Value.ToString() == "True")
                {
                    LlenarCampos();
                    modoOperacion = EnumModoAplicar.Modificar;
                    BloquearBotones();
                    lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoModificar")}: {codProd}";
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noSePuedeModificar")); } //No se puede modifcar un producto deshabilitado
                
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccione")); }
        }

        //este boton sirve tanto para habilitar como deshabilitar un producto, dependiendo de lo que diga su texto
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (grillaProductos.SelectedRows.Count > 0)
            {
                if(btnEliminar.Text == IdiomaManager.GetInstance().ConseguirTexto("deshabilitar")) //si dice deshabilitar
                {
                    modoOperacion = EnumModoAplicar.Eliminar;
                    BloquearBotones();
                    lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoEliminar")}: {grillaProductos.CurrentRow.Cells[0].Value}";

                }
                else //si dice habilitar
                {
                    modoOperacion = EnumModoAplicar.Activar;
                    BloquearBotones();
                    lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoActivar")}: {grillaProductos.CurrentRow.Cells[0].Value}";
                }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccione")); }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (modoOperacion == EnumModoAplicar.Consulta)
            {
                ConsultarProductos();
            }
            else
            {
                if (modoOperacion == EnumModoAplicar.Añadir)
                {
                    if (ValidarCampos())
                    {
                        BEProducto prodEncontrado = listaProd.FirstOrDefault(p => p.CodigoProducto == Convert.ToInt64(txtCodigoProducto.Text));
                        if (prodEncontrado != null)
                        {
                            MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("yaExiste")) ; return;
                        }
                        else
                        {
                            BEProducto prod = new BEProducto(Convert.ToInt64(txtCodigoProducto.Text), txtModelo.Text, txtDescripcion.Text, cmbMarca.Text, txtColor.Text, Convert.ToDouble(txtPrecio.Text), Convert.ToInt32(txtStock.Text), Convert.ToInt32(txtAlmacenamiento.Text),true);
                            bllProducto.RegistrarProducto(prod);

                            bllEv.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Productos", "Producto creado", 3, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));
                            MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"));
                        }
                    }
                    else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("llenarCampos")); return; }
                }
                else
                {
                    if (modoOperacion == EnumModoAplicar.Eliminar)
                    {
                        DialogResult resultado = MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("estaSeguro") + " " + grillaProductos.CurrentRow.Cells[0].Value + " ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultado == DialogResult.Yes)
                        {
                            long idProd = Convert.ToInt64(grillaProductos.CurrentRow.Cells[0].Value);
                           
                            bllProducto.EliminarProducto(idProd);

                            bllEv.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Productos", "Producto eliminado", 2, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));
                            MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"));
                        }
                    }
                    if (modoOperacion == EnumModoAplicar.Activar)
                    {
                        DialogResult resultado = MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("estaSeguroHabilitar") + " " + grillaProductos.CurrentRow.Cells[0].Value + " ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultado == DialogResult.Yes)
                        {
                            long idProd = Convert.ToInt64(grillaProductos.CurrentRow.Cells[0].Value);

                            bllProducto.HabilitarProducto(idProd);

                            bllEv.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Productos", "Producto habilitado", 2, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));
                            MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"));
                        }

                    }


                    if (modoOperacion == EnumModoAplicar.Modificar)
                    {
                        if (grillaProductos.CurrentRow.Cells[8].Value.ToString() == "True")
                        {
                            BEProducto prod = new BEProducto(Convert.ToInt64(txtCodigoProducto.Text), txtModelo.Text, txtDescripcion.Text, cmbMarca.Text, txtColor.Text, Convert.ToDouble(txtPrecio.Text), Convert.ToInt32(txtStock.Text), Convert.ToInt32(txtAlmacenamiento.Text), true);
                            bllProducto.ModificarProducto(prod);

                            bllEv.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Productos", "Producto modificado", 3, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));
                            MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"));
                        }
                        else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noSePuedeModificar")); } //No se puede modifcar un producto deshabilitado
                    }
                }
                ResetearBotones();
                ActualizarGrilla();
            }

        }

        private void ConsultarProductos()
        {
            List<BEProducto> lstConsulta = new List<BEProducto>();
            foreach (BEProducto prod in listaProd)
            {
                if (prod.CodigoProducto.ToString() == txtCodigoProducto.Text || prod.Precio.ToString() == txtPrecio.Text || prod.Stock.ToString() == txtStock.Text || prod.Almacenamiento.ToString() == txtAlmacenamiento.Text || prod.Marca == cmbMarca.Text || prod.Color.ToLower() == txtColor.Text.ToLower())
                {
                    lstConsulta.Add(prod);
                }
                if (!string.IsNullOrWhiteSpace(txtModelo.Text))
                {
                    if (prod.Modelo.ToLower().Contains(txtModelo.Text.ToLower()) && !lstConsulta.Contains(prod))
                    {
                        lstConsulta.Add(prod);
                    }
                }
                if (!string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    if (prod.Descripcion.ToLower().Contains(txtDescripcion.Text.ToLower()) && !lstConsulta.Contains(prod))
                    {
                        lstConsulta.Add(prod);
                    }
                }
               
            }
            grillaProductos.Rows.Clear();

            foreach (BEProducto p in lstConsulta)
            {
                grillaProductos.Rows.Add(p.CodigoProducto, p.Modelo, p.Descripcion, p.Marca, p.Color, p.Precio, p.Stock, p.Almacenamiento);
            }
        }

        private void BloquearBotones()
        {
            btnModificar.Enabled = false;
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;
            if(modoOperacion == EnumModoAplicar.Modificar)
            {
                txtCodigoProducto.Enabled = false;
            }
            if (modoOperacion == EnumModoAplicar.Eliminar)
            {
                txtCodigoProducto.Enabled = false;
                txtModelo.Enabled = false;
                txtDescripcion.Enabled = false;
                txtPrecio.Enabled = false;
                txtStock.Enabled = false;
                txtAlmacenamiento.Enabled = false;
                cmbMarca.Enabled = false; 
                txtColor.Enabled = false;
            }
        }

       

        private void ResetearBotones()
        {
            modoOperacion = EnumModoAplicar.Consulta;
            lblMensaje.Text = IdiomaManager.GetInstance().ConseguirTexto("lblMensaje");
            txtCodigoProducto.Text = "";
            txtModelo.Text = "";
            txtDescripcion.Text = "";
            txtColor.Text = "";
            txtPrecio.Text = "0.00";
            txtStock.Text = "";
            txtAlmacenamiento.Text = "";
            cmbMarca.SelectedItem = null;

            txtCodigoProducto.Enabled = true;
            txtModelo.Enabled = true;
            txtDescripcion.Enabled = true;
            txtPrecio.Enabled = true;
            txtStock.Enabled = true;
            txtAlmacenamiento.Enabled = true;
            cmbMarca.Enabled = true;
            txtColor.Enabled = true;


            btnModificar.Enabled = true;
            btnAgregar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private bool ValidarCampos()
        {
            double precio = Convert.ToDouble(txtPrecio.Text);
            if(precio <= 0) 
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("precio"));
                return false;
            }
            if(txtCodigoProducto.Text == "" || txtModelo.Text == "" || txtDescripcion.Text == "" || txtColor.Text == "" || txtPrecio.Text == "" || txtStock.Text == "" || txtAlmacenamiento.Text == "" || cmbMarca.Text == "")
            {
                return false;
            }
            return true;
        }

        private void LlenarCampos()
        {
            txtCodigoProducto.Text = grillaProductos.CurrentRow.Cells[0].Value.ToString();
            txtModelo.Text = grillaProductos.CurrentRow.Cells[1].Value.ToString();
            txtDescripcion.Text = grillaProductos.CurrentRow.Cells[2].Value.ToString();
            cmbMarca.SelectedItem = grillaProductos.CurrentRow.Cells[3].Value.ToString();
            txtColor.Text = grillaProductos.CurrentRow.Cells[4].Value.ToString();
            txtPrecio.Text = grillaProductos.CurrentRow.Cells[5].Value.ToString();
            txtStock.Text = grillaProductos.CurrentRow.Cells[6].Value.ToString();
            txtAlmacenamiento.Text = grillaProductos.CurrentRow.Cells[7].Value.ToString();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearBotones();
        }

        private void grillaProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grillaProductos.SelectedRows.Count > 0)
            {
                long codProd = Convert.ToInt64(grillaProductos.CurrentRow.Cells[0].Value);
                if (modoOperacion == EnumModoAplicar.Eliminar)
                {
                    lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoEliminar")}: {codProd}";
                }
                if (modoOperacion == EnumModoAplicar.Activar)
                {
                    lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoActivar")}: {codProd}";
                }
                if (modoOperacion == EnumModoAplicar.Modificar)
                {
                    if (grillaProductos.CurrentRow.Cells[8].Value.ToString() == "True") //Si esta habilitado se puede modificar
                    {
                        LlenarCampos();
                        lblMensaje.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("modoModificar")}: {codProd}";
                    }
                    else // si no esta habilitado no se puede
                    {
                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noSePuedeModificar"));
                        ResetearBotones();
                    }
                }

                if (grillaProductos.CurrentRow.Cells[8].Value.ToString() == "False") 
                { 
                    btnEliminar.Text = IdiomaManager.GetInstance().ConseguirTexto("habilitar");
                } else { btnEliminar.Text = IdiomaManager.GetInstance().ConseguirTexto("deshabilitar"); }
            }
        }


        //evento para que no pueda escribir . , -
        private void txtCodigoProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericUpDown numUpDown = sender as NumericUpDown;
            if (!char.IsControl(e.KeyChar))
            {
                string texto = numUpDown.Text;

                if (texto.Length >= 13)
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

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericUpDown numUpDown = sender as NumericUpDown;
            if (!char.IsControl(e.KeyChar))
            {
                string texto = numUpDown.Text;

                if (texto.Length >= 3)
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

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericUpDown numUpDown = sender as NumericUpDown;
            if (!char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == '.' || e.KeyChar == '-')
                {
                    e.Handled = true;
                }
                if (numUpDown.Text.Length >= 9)
                {
                    e.Handled = true;
                }
            }
        }

        private void txtAlmacenamiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericUpDown numUpDown = sender as NumericUpDown;
            if (!char.IsControl(e.KeyChar))
            {
                string texto = numUpDown.Text;

                if (texto.Length >= 8)
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
