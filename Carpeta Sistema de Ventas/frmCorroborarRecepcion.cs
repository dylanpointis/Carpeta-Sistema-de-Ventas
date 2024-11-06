using BE;
using BLL;
using Carpeta_Sistema_de_Ventas.Properties;
using Microsoft.VisualBasic;
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
    public partial class frmCorroborarRecepcion : Form, IObserver
    {
        public frmCorroborarRecepcion()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().archivoActual = "frmCorroborarRecepcion";
            IdiomaManager.GetInstance().Agregar(this);
        }

        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        BLLOrdenCompra bllOrdenC = new BLLOrdenCompra();
        BLLProducto bllProducto = new BLLProducto();
        BEOrdenCompra ordenC;
        List<BEOrdenCompra> listaOrdenesPendientes = new List<BEOrdenCompra>();

        private void COMPRAfrmCorroborarRecepcion_Load(object sender, EventArgs e)
        {
            grillaRecepcion.ColumnCount = 9;
            grillaRecepcion.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCodigo");
            grillaRecepcion.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewModelo");
            grillaRecepcion.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewStock");
            grillaRecepcion.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewSMin");
            grillaRecepcion.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewSMax");
            grillaRecepcion.Columns[5].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCantidadSolicitada");
            grillaRecepcion.Columns[6].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCantidadRecibida");
            grillaRecepcion.Columns[7].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewFacturaRecepcion");
            grillaRecepcion.Columns[8].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewFechaRecepcion");

            grillaRecepcion.Columns[2].Width = 50;
            grillaRecepcion.Columns[3].Width = 50;
            grillaRecepcion.Columns[4].Width = 50;
            txtCantRecibida.Text = "";
            txtNumFactura.Text = "";
            TraerOrdenesPendientes();
        }

        private void TraerOrdenesPendientes()
        {
            List<BEOrdenCompra> listaOrd = bllOrdenC.TraerOrdenesPendientes(); //TRAE ORDENES PENDIENTES O ENTREGADAS PARCIALMENTE
            cmbOrdenesCompra.Items.Clear();
            foreach (var ord in listaOrd)
            {
                listaOrdenesPendientes.Add(ord);
                cmbOrdenesCompra.Items.Add(ord.NumeroOrdenCompra + "  |  " + ord.FechaRegistro);
            }
        }

        private void cmbOrdenesCompra_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] partes = cmbOrdenesCompra.SelectedItem.ToString().Split(new string[] { "  |  " }, StringSplitOptions.RemoveEmptyEntries);
            int numOrden = Convert.ToInt32(partes[0].Trim());

            ordenC = listaOrdenesPendientes.FirstOrDefault(o => o.NumeroOrdenCompra == numOrden);
            if (ordenC != null)
            {
                string estado = "";
                if (ordenC.Estado == "Pendiente")
                {
                    estado = IdiomaManager.GetInstance().ConseguirTexto("pendiente");
                }
                else if (ordenC.Estado == "Parcialmente entregada")
                {
                    estado = IdiomaManager.GetInstance().ConseguirTexto("parcialmenteEntregada");
                }

                lblOrden.Text = IdiomaManager.GetInstance().ConseguirTexto("lblOrden") + $"{ordenC.NumeroOrdenCompra} | {IdiomaManager.GetInstance().ConseguirTexto("textoFactura")}: {ordenC.NumeroFactura} | {estado}";


                //Muestra el detalle del proveedor
                MostrarDetalleProveedor(numOrden);
                //Busca los items de la orden de compra y los carga en la variable interna
                BuscarItemsOrden(numOrden);
                btnFinalizar.Enabled = true;
            }
        }


        private void MostrarDetalleProveedor(int numOrden)
        {
            BEProveedor prov = bllOrdenC.TraerProveedorOrden(numOrden);
            ordenC.proveedor = prov;

            lblNombreProv.Text = IdiomaManager.GetInstance().ConseguirTexto("lblNombreProv") + $"\n{prov.Nombre}";
            lblRazonSocial.Text = IdiomaManager.GetInstance().ConseguirTexto("lblRazonSocial") + $"\n{prov.RazonSocial}";
            lblMailProv.Text = IdiomaManager.GetInstance().ConseguirTexto("lblMailProv") + $"\n{prov.Email}";
            lblNumTel.Text = IdiomaManager.GetInstance().ConseguirTexto("lblNumTel") + $"\n{prov.NumTelefono}";
            lblCBU.Text = IdiomaManager.GetInstance().ConseguirTexto("lblCBU") + $"\n{prov.CBU}";
            lblCUIT.Text = IdiomaManager.GetInstance().ConseguirTexto("lblCUIT") + $"\n{prov.CUIT}";
        }
        private void BuscarItemsOrden(int numOrden)
        {
            List<BEItemOrdenCompra> lista = bllOrdenC.TraerProductosOrden(numOrden);
            foreach (BEItemOrdenCompra item in lista)
            {
                ordenC.AgregarItem(item.Producto, item.CantidadSolicitada, item.CantidadRecibida, item.NumFacturaRecepcion, item.FechaRecepcion);
            }

            label2.Text = IdiomaManager.GetInstance().ConseguirTexto("label2") + ordenC.CantidadTotal;
            ActualizarGrilla();
        }

        private void ActualizarGrilla()
        {
            grillaRecepcion.Rows.Clear();
            int cantTotalRecibida = 0;
            foreach (BEItemOrdenCompra item in ordenC.obtenerItems())
            {
                grillaRecepcion.Rows.Add(item.Producto.CodigoProducto, item.Producto.Modelo, item.Producto.Stock, 
                    item.Producto.StockMin, item.Producto.StockMax, item.CantidadSolicitada, item.CantidadRecibida, item.NumFacturaRecepcion, item.FechaRecepcion);
                cantTotalRecibida += item.CantidadRecibida;
            }
            label1.Text = IdiomaManager.GetInstance().ConseguirTexto("label1") + cantTotalRecibida.ToString(); //muestra la cantidad total recibida
        }

        private void btnCargarCantidadRecibida_Click(object sender, EventArgs e)
        {
            if(grillaRecepcion.SelectedRows.Count > 0)
            {
                if(txtCantRecibida.Text == "" || txtNumFactura.Text == "" || txtCantRecibida.Text.Length > 4 || txtNumFactura.Text.Length > 16)
                {
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("ingreseCampos"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int cantRecibida = Convert.ToInt32(txtCantRecibida.Text);
                long numFactura = Convert.ToInt64(txtNumFactura.Text);
                if (cantRecibida >= 0 && numFactura > 0)
                {
                    long codProd = Convert.ToInt64(grillaRecepcion.CurrentRow.Cells[0].Value);

                    ordenC.modificarCantidadItem(codProd, cantRecibida, numFactura, DateTime.Now.ToString("yyyy-MM-dd HH:mm"),true);
                    ActualizarGrilla();
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("ingreseCampos"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccioneProd"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (ordenC.obtenerItems().TrueForAll(i => i.CantidadRecibida >= 0))
            {
                DialogResult resultado = MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("estaSeguro"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        bllOrdenC.ConfirmarRecepcion(ordenC);

                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exito"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Reportes.GenerarReporteRecepcion(ordenC, Properties.Resources.htmlfacturaRecepcion.ToString(), Properties.Resources.logo);
                        btnFinalizar.Enabled = false;
                        TraerOrdenesPendientes();
                    }
                    catch (Exception ex) { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("error") + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void txtCantRecibida_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtNumFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericUpDown numUpDown = sender as NumericUpDown;
            if (!char.IsControl(e.KeyChar))
            {
                string texto = numUpDown.Text;

                if (texto.Length >= 14)
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
