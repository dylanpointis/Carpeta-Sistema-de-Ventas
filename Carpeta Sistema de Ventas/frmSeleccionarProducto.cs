﻿using BE;
using BLL;
using Microsoft.VisualBasic;
using Services.Observer;
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
    public partial class frmSeleccionarProducto : Form, IObserver
    {
        BLLProducto bllProductos = new BLLProducto();
        BEFactura _factura;
        public List<BEItemFactura> listaProductosInicial;
        public frmSeleccionarProducto(BEFactura factura)
        {
            _factura = factura;
            listaProductosInicial = new List<BEItemFactura>(_factura.obtenerListaItems()); // hace una copia de la lista incial al entrar al form
            InitializeComponent();


            IdiomaManager.GetInstance().archivoActual = "frmSeleccionarProducto";
            IdiomaManager.GetInstance().Agregar(this);
        }

        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this); ;
        }

        private void frmSeleccionarProducto_Load(object sender, EventArgs e)
        {
            ArmarColumnasTabla();
            ActualizarGrilla();

            if (_factura.cantidadItems() == 0) { btnConfirmar.Enabled = false; }
        }

        

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(grillaProductos.SelectedRows.Count > 0)
            {
                int cantStock = bllProductos.ConsultarStock(Convert.ToInt64(grillaProductos.CurrentRow.Cells[0].Value));
                if (cantStock > 0)
                {
                    BEProducto producto = new BEProducto(Convert.ToInt64(grillaProductos.CurrentRow.Cells[0].Value),
                        grillaProductos.CurrentRow.Cells[1].Value.ToString(), grillaProductos.CurrentRow.Cells[2].Value.ToString(),
                        grillaProductos.CurrentRow.Cells[3].Value.ToString(),grillaProductos.CurrentRow.Cells[4].Value.ToString(),
                        Convert.ToDouble(grillaProductos.CurrentRow.Cells[5].Value.ToString().Replace(".", "")),
                        Convert.ToInt32(grillaProductos.CurrentRow.Cells[6].Value), 0,0,
                        Convert.ToInt32(grillaProductos.CurrentRow.Cells[7].Value), true);
                   
                    if(YaEstaElProductoAgregado(producto.CodigoProducto) == false)
                    {
                        string cantIngresada = Interaction.InputBox(IdiomaManager.GetInstance().ConseguirTexto("ingreseCant"));
                        if (Regex.IsMatch(cantIngresada.ToString(), @"^\d{1,3}$") && (Convert.ToInt64(cantIngresada) > 0))  //COMPRUEBA CON REGEX QUE LA CANT INGRESADA ES UN NUMERO MENOR A 3 CIFRAS
                        {
                            if ((cantStock - Convert.ToInt32(cantIngresada) >= 0))
                            {
                                //chequea si no ingreso una cantidad mayor al stock disponible.
                                _factura.AgregarItem(new BEItemFactura(producto, Convert.ToInt32(cantIngresada)));
                                ActualizarGrilla();
                                btnConfirmar.Enabled = true;
                            }
                            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("superaStock")); }

                        }
                        else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("numEntero")); }
                    }
                    else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("yaEsta")); }
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noHayStock")); }
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (grillaProductosAgregados.SelectedRows.Count > 0)
            {
                long codigoProducto = Convert.ToInt64(grillaProductosAgregados.CurrentRow.Cells[0].Value);
                var productoAEliminar = _factura.obtenerListaItems().FirstOrDefault(p => p.producto.CodigoProducto == codigoProducto);

                _factura.QuitarItem(productoAEliminar);
                ActualizarGrilla();

                if (_factura.cantidadItems() == 0)
                    btnConfirmar.Enabled = false;
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if(_factura.cantidadItems() > 0)
            {
                DialogResult resultado = MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("deseaConfirmar"), IdiomaManager.GetInstance().ConseguirTexto("confirmarProductos"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noHayProdSeleccionados")); }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string consulta = txtBuscar.Text.ToLower();
            List<BEProducto> lstProductos = bllProductos.TraerListaProductos();
            List<BEProducto> lstProdEncontrados = new List<BEProducto>();

            /*Crea una lista con los productos encontrados. Concatena el codigo, descripcion y marca y luego con el metodo Contain se fija si contiene las letras de la consulta*/
            lstProdEncontrados = lstProductos
            .Where(p => p.CodigoProducto.ToString() == consulta || (p.Modelo.ToLower() + p.Marca.ToLower()).Contains(consulta))
            .ToList();

            grillaProductos.Rows.Clear();
            if (lstProdEncontrados.Count > 0)
            {
                foreach (BEProducto p in lstProdEncontrados)
                {
                    string formatoPrecio = p.Precio.ToString("#,0.00", new System.Globalization.CultureInfo("es-ES"));
                    grillaProductos.Rows.Add(p.CodigoProducto, p.Modelo, p.Descripcion, p.Marca, p.Color, formatoPrecio, p.Stock, p.Almacenamiento);
                }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noSeEncontraron")); ActualizarGrilla(); }
          
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarGrilla();
        }

        private void ActualizarGrilla()
        {
            grillaProductos.Rows.Clear();
            List<BEProducto> listprod = bllProductos.TraerListaProductos();
            foreach(BEProducto p in listprod)
            {
                if(p.BorradoLogico == true)
                {
                    string formatoPrecio = p.Precio.ToString("#,0.00", new System.Globalization.CultureInfo("es-ES"));
                    grillaProductos.Rows.Add(p.CodigoProducto, p.Modelo, p.Descripcion, p.Marca, p.Color, formatoPrecio, p.Stock, p.Almacenamiento);
                }
            }



            grillaProductosAgregados.Rows.Clear();
            if (_factura.cantidadItems() > 0)
            {
                foreach (var item in _factura.obtenerListaItems())
                {
                    BEProducto prod = item.producto;
                    int cantidad = item.cantidad;
                    string formatoPrecio = prod.Precio.ToString("#,0.00", new System.Globalization.CultureInfo("es-ES"));
                    string subtotal = (cantidad * prod.Precio).ToString("#,0.00", new System.Globalization.CultureInfo("es-ES"));
                    grillaProductosAgregados.Rows.Add(prod.CodigoProducto, prod.Modelo, cantidad, formatoPrecio, subtotal);
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



            lblNeto.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("lblNeto")} $" + neto.ToString("#,0.00", new System.Globalization.CultureInfo("es-ES"));
            lblIVA.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("lblIVA")}: $" + impuesto.ToString("#,0.00", new System.Globalization.CultureInfo("es-ES")); ;
            lblTotal.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("lblTotal")}: $" + (neto + impuesto).ToString("#,0.00", new System.Globalization.CultureInfo("es-ES")); ;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _factura.RestablecerListaProductos(listaProductosInicial); //Si cancela resetea la lista de productos a como estaba
            ActualizarLabelsTotal();
            this.Close();
        }


        private bool YaEstaElProductoAgregado(long idProd)
        {
            foreach (var item in _factura.obtenerListaItems())
            {
                BEProducto prod = item.producto;
                int cantidad = item.cantidad;
                if (prod.CodigoProducto == idProd)
                {
                    return true;
                }
            }
            return false;
        }




        private void ArmarColumnasTabla()
        {
            grillaProductosAgregados.ColumnCount = 5;
            grillaProductosAgregados.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCodigo");
            grillaProductosAgregados.Columns[2].Width = 58;
            grillaProductosAgregados.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewModelo");
            grillaProductosAgregados.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCantidad");
            grillaProductosAgregados.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewPrecio");
            grillaProductosAgregados.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewSubtotal");



            grillaProductos.ColumnCount = 8;
            grillaProductos.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCodigo");
            grillaProductos.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewModelo");
            grillaProductos.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewDescripcion");
            grillaProductos.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewMarca");
            grillaProductos.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewColor");
            grillaProductos.Columns[5].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewPrecio");
            grillaProductos.Columns[6].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewStock");
            grillaProductos.Columns[7].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewAlmacenamiento");


            grillaProductos.Columns[0].Width = 45;
            grillaProductos.Columns[1].Width = 45;
            //grillaProductosAgregados.Columns[2].Width = 58;
            grillaProductos.Columns[3].Width = 30;
            grillaProductos.Columns[4].Width = 25;
            grillaProductos.Columns[5].Width = 35;
            grillaProductos.Columns[6].Width = 20;
            grillaProductos.Columns[7].Width = 35;
            grillaProductosAgregados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

    }
}
