using BE;
using BLL;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmSeleccionarProducto : Form
    {
        BLLProducto bllProductos = new BLLProducto();
        BEFactura _factura;
        public List<(BEProducto, int)> listaProductosInicial;
        public frmSeleccionarProducto(BEFactura factura)
        {
            _factura = factura;
            listaProductosInicial = new List<(BEProducto,int)>(_factura.listaProductosAgregados); // hace una copia de la lista incial al entrar al form
            InitializeComponent();
        }

        private void frmSeleccionarProducto_Load(object sender, EventArgs e)
        {
            grillaCarrito.ColumnCount = 5;
            grillaCarrito.Columns[0].Name = "Codigo producto";
            grillaCarrito.Columns[1].Name = "Descripcion";
            grillaCarrito.Columns[2].Name = "Cantidad";
            grillaCarrito.Columns[3].Name = "Precio";
            grillaCarrito.Columns[4].Name = "Subtotal";
           
            ActualizarGrilla();

            if(_factura.listaProductosAgregados.Count == 0) { btnConfirmar.Enabled = false; }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(grillaProductos.SelectedRows.Count > 0)
            {
                int cantStock = Convert.ToInt32(grillaProductos.CurrentRow.Cells[5].Value);
                if (cantStock > 0)
                {
                    BEProducto producto = new BEProducto(Convert.ToInt32(grillaProductos.CurrentRow.Cells[0].Value), grillaProductos.CurrentRow.Cells[1].Value.ToString(), grillaProductos.CurrentRow.Cells[2].Value.ToString(), grillaProductos.CurrentRow.Cells[3].Value.ToString(), Convert.ToDouble(grillaProductos.CurrentRow.Cells[4].Value), Convert.ToInt32(grillaProductos.CurrentRow.Cells[5].Value), Convert.ToInt32(grillaProductos.CurrentRow.Cells[6].Value));
                    string cantComprada = Interaction.InputBox("Ingrese la cantidad a vender");
                    if (Regex.IsMatch(cantComprada.ToString(), @"^\d+$"))  //COMPRUEBA CON REGEX QUE LA CANT INGRESADA ES UN NUMERO
                    { 

                        if(cantStock - Convert.ToInt32(cantComprada) >= 0)
                        {
                            _factura.listaProductosAgregados.Add((producto, Convert.ToInt32(cantComprada)));
                            ActualizarGrilla();
                            btnConfirmar.Enabled = true;
                        }
                        else { MessageBox.Show("La cantidad ingresada supera al stock disponbile"); }

                    }
                    else{ MessageBox.Show("Ingrese un número entero para la cantidad"); }
                }
                else { MessageBox.Show("No hay stock disponible del producto seleccionado"); }
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (grillaCarrito.SelectedRows.Count > 0)
            {
                int codigoProducto = Convert.ToInt32(grillaCarrito.CurrentRow.Cells[0].Value);
                var productoAEliminar = _factura.listaProductosAgregados.FirstOrDefault(p => p.Item1.CodigoProducto == codigoProducto);

                _factura.listaProductosAgregados.Remove(productoAEliminar);
                ActualizarGrilla();

                if (_factura.listaProductosAgregados.Count == 0)
                    btnConfirmar.Enabled = false;
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if(_factura.listaProductosAgregados.Count > 0)
            {
                DialogResult resultado = MessageBox.Show($"¿Desea confirmar los productos seleccionados?", "Confirmar productos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else { MessageBox.Show("No hay productos seleccionados"); }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string consulta = txtBuscar.Text.ToLower();
            List<BEProducto> lstProductos = bllProductos.TraerListaProductos();
            List<BEProducto> lstProdEncontrados = new List<BEProducto>();

            /*Crea una lista con los productos encontrados. Concatena el codigo, descripcion y marca y luego con el metodo Contain se fija si contiene las letras de la consulta*/
            lstProdEncontrados = lstProductos
            .Where(p => (p.CodigoProducto.ToString() + p.Descripcion.ToLower() + p.Marca.ToLower()).Contains(consulta))
            .ToList();

            if(lstProdEncontrados.Count > 0)
            {
                grillaProductos.DataSource = lstProdEncontrados;
            }
            else { MessageBox.Show("No se encontraron productos que coincidan con la búsqueda"); }
          
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarGrilla();
        }

        private void ActualizarGrilla()
        {
            grillaProductos.DataSource = bllProductos.TraerListaProductos();

            grillaCarrito.Rows.Clear();
            if (_factura.listaProductosAgregados.Count() > 0)
            {
                foreach (var item in _factura.listaProductosAgregados)
                {
                    BEProducto prod = item.Item1;
                    int cantidad = item.Item2;

                    grillaCarrito.Rows.Add(prod.CodigoProducto, prod.Descripcion, cantidad, prod.Precio, cantidad * prod.Precio);
                }
            }

            ActualizarLabelsTotal();
        }

        private void ActualizarLabelsTotal()
        {
            double neto = _factura.CalcularMonto();
            double impuesto = neto * 0.21;

            _factura.MontoTotal = (neto + impuesto);
            _factura.Impuesto = impuesto;


            lblNeto.Text = "Neto: $" + neto;
            lblIVA.Text = "I.V.A.: $" + impuesto;
            lblTotal.Text = "Total: $" + (neto + impuesto);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _factura.listaProductosAgregados = listaProductosInicial; //Si cancela resetea la lista de productos a como estaba
            ActualizarLabelsTotal();
            this.Close();
        }
    }
}
