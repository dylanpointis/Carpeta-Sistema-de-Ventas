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

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmMaestroProducto : Form
    {
        List<BEProducto> listaProd = new List<BEProducto> ();
        BLLProducto bllProducto = new BLLProducto();
        EnumModoAplicar modoOperacion;
        public frmMaestroProducto()
        {
            InitializeComponent();
        }

        private void frmMaestroProducto_Load(object sender, EventArgs e)
        {
            modoOperacion = EnumModoAplicar.Consulta;
            ActualizarGrilla();
            ResetearBotones();
            grillaProductos.Columns[3].Width = 55;
            grillaProductos.Columns[4].Width = 55;
            grillaProductos.Columns[5].Width = 55;
            grillaProductos.Columns[6].Width = 55;
            grillaProductos.Columns[7].Width = 55;
        }

        private void ActualizarGrilla()
        {
            listaProd = bllProducto.TraerListaProductos();
            grillaProductos.DataSource = listaProd;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarGrilla();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtCodigoProducto.Text != "" && txtCodigoProducto.Text != "0" && txtCodigoProducto.Text.Length <= 13)
            {
                BEProducto prodEncontrado = listaProd.FirstOrDefault(p => p.CodigoProducto == Convert.ToInt32(txtCodigoProducto.Text));
                if (prodEncontrado == null)
                {
                    modoOperacion = EnumModoAplicar.Añadir;
                    BloquearBotones();
                    lblMensaje.Text = "Mensaje: Modo Añadir";
                    //lblMensaje.Text = FormIdiomas.ConseguirTexto("modoAñadir");
                }
                else
                {
                    MessageBox.Show("Ya existe un producto con ese codigo");
                    txtCodigoProducto.Focus();
                }
            }
            else
            {
                MessageBox.Show("Ingrese un Codigo para el producto, menor a 13 caracteres");
                txtCodigoProducto.Focus();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (grillaProductos.SelectedRows.Count > 0)
            {
                LlenarCampos();
                modoOperacion = EnumModoAplicar.Modificar;
                BloquearBotones();
                lblMensaje.Text = $"Modificar producto Código: {grillaProductos.CurrentRow.Cells[0].Value}";
            }
            else { MessageBox.Show("Seleccione un producto para modificar"); }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (grillaProductos.SelectedRows.Count > 0)
            {
                modoOperacion = EnumModoAplicar.Eliminar;
                BloquearBotones();
                lblMensaje.Text = $"Eliminar producto Código: {grillaProductos.CurrentRow.Cells[0].Value}";
            }
            else { MessageBox.Show("Seleccione un producto para eliminar"); }
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
                        BEProducto prodEncontrado = listaProd.FirstOrDefault(p => p.CodigoProducto == Convert.ToInt32(txtCodigoProducto.Text));
                        if (prodEncontrado != null)
                        {
                            MessageBox.Show("Ya existe un Producto con ese Codigo");
                        }
                        else
                        {
                            BEProducto prod = new BEProducto(Convert.ToInt32(txtCodigoProducto.Text), txtModelo.Text, txtDescripcion.Text, cmbMarca.Text, txtColor.Text, Convert.ToDouble(txtPrecio.Text), Convert.ToInt32(txtStock.Text), Convert.ToInt32(txtAlmacenamiento.Text));
                            bllProducto.RegistrarProducto(prod);
                            MessageBox.Show("Producto registrado exitosamente");
                        }
                    }
                    else { MessageBox.Show("Llene los campos"); return; }
                }
                else
                {
                    if (modoOperacion == EnumModoAplicar.Eliminar)
                    {
                        DialogResult resultado = MessageBox.Show($"¿Está seguro que desea eliminar al producto Código: {grillaProductos.CurrentRow.Cells[0].Value}?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultado == DialogResult.Yes)
                        {
                            int idProd = Convert.ToInt32(grillaProductos.CurrentRow.Cells[0].Value);
                            bllProducto.EliminarProducto(idProd);
                            MessageBox.Show("Producto eliminado");
                        }
                    }
                    if (modoOperacion == EnumModoAplicar.Modificar)
                    {
                        BEProducto prod = new BEProducto(Convert.ToInt32(txtCodigoProducto.Text),txtModelo.Text,txtDescripcion.Text,cmbMarca.Text,txtColor.Text, Convert.ToDouble(txtPrecio.Text),Convert.ToInt32(txtStock.Text), Convert.ToInt32(txtAlmacenamiento.Text));
                        bllProducto.ModificarProducto(prod);
                        MessageBox.Show("Producto modificado");
                    }
                    ResetearBotones();
                    ActualizarGrilla();
                }
            }

        }

        private void ConsultarProductos()
        {
            grillaProductos.DataSource = null;
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

            grillaProductos.DataSource = lstConsulta;
        }

        private void BloquearBotones()
        {
            btnModificar.Enabled = false;
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;
            if(modoOperacion == EnumModoAplicar.Modificar)
                txtCodigoProducto.Enabled=false;
        }

       

        private void ResetearBotones()
        {
            modoOperacion = EnumModoAplicar.Consulta;
            lblMensaje.Text = "Mensaje: Modo Consulta";
            txtCodigoProducto.Text = "";
            txtModelo.Text = "";
            txtDescripcion.Text = "";
            txtColor.Text = "";
            txtPrecio.Text = "0.00";
            txtStock.Text = "";
            txtAlmacenamiento.Text = "";
            cmbMarca.SelectedItem = null;

            txtCodigoProducto.Enabled = true;
            btnModificar.Enabled = true;
            btnAgregar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private bool ValidarCampos()
        {
            double precio = Convert.ToDouble(txtPrecio.Text);
            if(precio <= 0) 
            {
                MessageBox.Show("El precio debe ser mayor a 0");
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
                if (modoOperacion == EnumModoAplicar.Eliminar)
                {
                    lblMensaje.Text = $"Eliminar producto Código: {grillaProductos.CurrentRow.Cells[0].Value}";
                }
                if (modoOperacion == EnumModoAplicar.Modificar)
                {
                    LlenarCampos();
                    lblMensaje.Text = $"Modificar producto Código: {grillaProductos.CurrentRow.Cells[0].Value}";
                }
            }
        }
    }
}
