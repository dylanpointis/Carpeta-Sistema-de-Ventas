using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmGenerarFactura : Form
    {
        private BLLCliente bllCliente = new BLLCliente();
        public BEFactura _factura;
        private BLLFactura bllFactura = new BLLFactura();
        private BLLProducto bllProducto = new BLLProducto();
        public frmGenerarFactura()
        {
            InitializeComponent();
            _factura = new BEFactura();
        }

        private void frmGenerarFactura_Load(object sender, EventArgs e)
        {
            grillaProductosAgregados.ColumnCount = 5;
            grillaProductosAgregados.Columns[0].Name = "Codigo producto";
            grillaProductosAgregados.Columns[2].Width = 58;
            grillaProductosAgregados.Columns[1].Name = "Descripcion";
            grillaProductosAgregados.Columns[2].Name = "Cantidad";
            grillaProductosAgregados.Columns[3].Name = "Precio";
            grillaProductosAgregados.Columns[4].Name = "Subtotal";

            ActualizarGrillaProductos();
            ActualizarGrillaClientes();
        }

        private void btnSeleccionarProducto_Click(object sender, EventArgs e)
        {
            frmSeleccionarProducto form = new frmSeleccionarProducto(_factura);
            form.ShowDialog();
            ActualizarGrillaProductos();
        }



        private void ActualizarGrillaProductos()
        {
            grillaProductosAgregados.Rows.Clear();
            if (_factura.listaProductosAgregados.Count() > 0)
            {
                foreach (var item in _factura.listaProductosAgregados)
                {
                    BEProducto prod = item.Item1;
                    int cantidad = item.Item2;

                    grillaProductosAgregados.Rows.Add(prod.CodigoProducto, prod.Modelo, cantidad, prod.Precio, cantidad * prod.Precio);
                }
            }

            lblNeto.Text = "Neto: $" + _factura.CalcularMonto();
            lblIVA.Text = "I.V.A.: $" + _factura.Impuesto;
            lblTotal.Text = "Total: $" + _factura.MontoTotal;
        }

        /*SECCION CLIENTE*/

        private void ActualizarGrillaClientes()
        { 
            grillaClientes.DataSource = bllCliente.TraerListaCliente();
        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            if(grillaClientes.SelectedRows.Count > 0)
            {
                BECliente cliente = new BECliente(Convert.ToInt32(grillaClientes.CurrentRow.Cells[0].Value), grillaClientes.CurrentRow.Cells[1].Value.ToString(), grillaClientes.CurrentRow.Cells[2].Value.ToString(), grillaClientes.CurrentRow.Cells[3].Value.ToString(), grillaClientes.CurrentRow.Cells[4].Value.ToString());
                lblNombreCliente.Text = "Nombre: " + cliente.Nombre;
                lblApellidoCliente.Text = "Apellido: " + cliente.Apellido;
                lblMailCliente.Text = "Mail: " + cliente.Mail;
                lblDNICliente.Text = "DNI: " + cliente.DniCliente.ToString();

                _factura.clienteFactura = cliente;
                MessageBox.Show("Cliente agregado a la factura");
            }
            else { MessageBox.Show("Seleccione un cliente para agregarlo a la factura"); }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            string consulta = txtCliente.Text.ToLower();
            List<BECliente> lstClientes = bllCliente.TraerListaCliente();
            List<BECliente> lstClientesEncontrados = new List<BECliente>();

            /*Crea una lista con los clientes encontrados. Concatena el dni, nombre y apellido y luego con el metodo Contain se fija si contiene las letras de la consulta*/
            lstClientesEncontrados = lstClientes
            .Where(c => (c.DniCliente.ToString() + c.Nombre.ToLower() + c.Apellido.ToLower()).Contains(consulta))
            .ToList();

            if (lstClientesEncontrados.Count > 0)
            {
                grillaClientes.DataSource = lstClientesEncontrados;
            }
            else { MessageBox.Show("No se encontraron productos que coincidan con la búsqueda"); }
        }

        private void btnEliminarCliente_Click(object sender, EventArgs e)
        {
            if (_factura.clienteFactura != null)
            {
                DialogResult resultado = MessageBox.Show($"¿Desea quitar el cliente seleccionado?", "Quitar cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    _factura.clienteFactura = null;
                    lblNombreCliente.Text = "Nombre: ";
                    lblApellidoCliente.Text = "Apellido: ";
                    lblMailCliente.Text = "Mail: ";
                    lblDNICliente.Text = "DNI: ";
                }
            }
            else { MessageBox.Show("No hay un cliente agreado a la factura"); }
        }

        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            frmRegistrarCliente form = new frmRegistrarCliente();
            form.ShowDialog();
            ActualizarGrillaClientes();
        }
        /*
        private void btnActualizarGrillaCliente_Click(object sender, EventArgs e)
        {
            ActualizarGrillaClientes();
        }*/

        private void btnCobrarVenta_Click(object sender, EventArgs e)
        {
            if(_factura.listaProductosAgregados.Count > 0 && _factura.clienteFactura != null)
            {
                _factura.Fecha = DateTime.Now;
                _factura.NumFactura = bllFactura.TraerUltimoNumTransaccion() + 1;

                frmCobrarVenta form = new frmCobrarVenta(_factura);
                form.ShowDialog();

                if (_factura.cobro != null)
                {
                    btnFinalizar.Enabled = true;
                }
            }
            else { MessageBox.Show("Debe seleccionar al menos un producto y un cliente"); }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
        }

        private void btnFinalizar_Click_1(object sender, EventArgs e)
        {
            if(_factura.listaProductosAgregados.Count >0 && _factura.clienteFactura!= null && _factura.cobro != null)
            {
                DialogResult resultado = MessageBox.Show($"¿Desea finalizar la venta?", "Finalizar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    bllFactura.RegistrarFactura(_factura);
                    bllFactura.RegistrarItemFactura(_factura); //registra cada item de la factura


                    foreach (var item in _factura.listaProductosAgregados)
                    {
                        BEProducto prod = item.Item1;
                        int cantidad = item.Item2;

                        bllProducto.ModificarStock(prod, prod.Stock - cantidad);
                    }
                    MessageBox.Show("Venta Finalizada");
                    this.Enabled = false; // deshabilita los botones
                }
            }
        }
    }
}
