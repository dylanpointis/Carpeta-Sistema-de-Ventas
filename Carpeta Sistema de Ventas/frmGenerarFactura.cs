﻿using System;
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
using Services;
using Services.Observer;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmGenerarFactura : Form, IObserver
    {
        private BLLCliente bllCliente = new BLLCliente();
        public BEFactura _factura;
        private BLLFactura bllFactura = new BLLFactura();
        private BLLProducto bllProducto = new BLLProducto();
        private BLLEvento bLLEvento = new BLLEvento();
        public frmGenerarFactura()
        {
            InitializeComponent();
            _factura = new BEFactura();
            IdiomaManager.GetInstance().archivoActual = "frmGenerarFactura";
            IdiomaManager.GetInstance().Agregar(this);
        }
        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        private void frmGenerarFactura_Load(object sender, EventArgs e)
        {
            grillaProductosAgregados.ColumnCount = 5;
            grillaProductosAgregados.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCodigo");
            grillaProductosAgregados.Columns[2].Width = 58;
            grillaProductosAgregados.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewDescripcion");
            grillaProductosAgregados.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCantidad");
            grillaProductosAgregados.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewPrecio");
            grillaProductosAgregados.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewSubtotal");


            grillaClientes.ColumnCount = 5;
            grillaClientes.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewDNI");
            grillaClientes.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewNombre");
            grillaClientes.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewApellido");
            grillaClientes.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewMail");
            grillaClientes.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewDireccion");
            grillaClientes.Columns[0].Width = 63;
            grillaClientes.RowHeadersWidth = 30;



            ActualizarGrillaProductos();
            ActualizarGrillaClientes();
        }

        private void ActualizarLabels()
        {
            lblNeto.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("lblNeto")} $" + _factura.CalcularMonto();
            lblIVA.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("lblIVA")}: $" + _factura.Impuesto;
            lblTotal.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("lblTotal")}: $" + _factura.MontoTotal;
        }


        private void btnSeleccionarProducto_Click(object sender, EventArgs e)
        {
            frmSeleccionarProducto form = new frmSeleccionarProducto(_factura);
            form.ShowDialog();

            //vuelve a cargar el idioma
            IdiomaManager.GetInstance().archivoActual = "frmGenerarFactura";
            IdiomaManager.GetInstance().Agregar(this);

            ActualizarGrillaProductos();
        }



        private void ActualizarGrillaProductos()
        {
            grillaProductosAgregados.Rows.Clear();
            if (_factura.listaProductosAgregados.Count() > 0)
            {
                foreach (BEItemFactura item in _factura.listaProductosAgregados)
                {
                    BEProducto prod = item.producto;
                    int cantidad = item.cantidad;

                    grillaProductosAgregados.Rows.Add(prod.CodigoProducto, prod.Modelo, cantidad, prod.Precio, cantidad * prod.Precio);
                }
            }

            ActualizarLabels();
        }

        /*SECCION CLIENTE*/

        private void ActualizarGrillaClientes()
        { 
            List<BECliente> list= bllCliente.TraerListaCliente();
            
            grillaClientes.Rows.Clear();
            foreach(BECliente c  in list)
            {
                if(c.BorradoLogico == true) // Si no esta borrado logicamente lo muestra
                {
                    grillaClientes.Rows.Add(c.DniCliente, c.Nombre, c.Apellido, c.Mail, c.Direccion);
                }
            }
        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            if(grillaClientes.SelectedRows.Count > 0)
            {
                BECliente cliente = new BECliente(Convert.ToInt32(grillaClientes.CurrentRow.Cells[0].Value), grillaClientes.CurrentRow.Cells[1].Value.ToString(), grillaClientes.CurrentRow.Cells[2].Value.ToString(), grillaClientes.CurrentRow.Cells[3].Value.ToString(), grillaClientes.CurrentRow.Cells[4].Value.ToString());
                lblNombreCliente.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("lblNombreCliente")} " + cliente.Nombre;
                lblApellidoCliente.Text = $"{ IdiomaManager.GetInstance().ConseguirTexto("lblApellidoCliente")}: " + cliente.Apellido;
                lblMailCliente.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("lblMailCliente")}: " + cliente.Mail;
                lblDNICliente.Text = $"{IdiomaManager.GetInstance().ConseguirTexto("lblDNICliente")}: " + cliente.DniCliente.ToString();

                _factura.clienteFactura = cliente;
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("clienteAgregado"));
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccioneCliente")); }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            string consulta = txtCliente.Text.ToLower();
            List<BECliente> lstClientes = bllCliente.TraerListaCliente();
            List<BECliente> lstClientesEncontrados = new List<BECliente>();

            /*Crea una lista con los clientes encontrados. //Busca por el dni exacto o si no: Concatena el nombre y apellido y luego con el metodo Contain se fija si contiene las letras de la consulta*/
            lstClientesEncontrados = lstClientes
            .Where(c => (c.DniCliente.ToString() == consulta) || (c.Nombre.ToLower() + c.Apellido.ToLower()).Contains(consulta))
            .ToList();

            grillaClientes.Rows.Clear();
            if (lstClientesEncontrados.Count > 0)
            {
                foreach (BECliente c in lstClientesEncontrados)
                {
                    grillaClientes.Rows.Add(c.DniCliente, c.Nombre, c.Apellido, c.Mail, c.Direccion);
                }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noSeEncontraron")); ActualizarGrillaClientes(); }
        }

        private void btnEliminarCliente_Click(object sender, EventArgs e)
        {
            if (_factura.clienteFactura != null)
            {
                DialogResult resultado = MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("deseaCliente"), IdiomaManager.GetInstance().ConseguirTexto("quitarCliente"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    _factura.clienteFactura = null;
                    lblNombreCliente.Text = IdiomaManager.GetInstance().ConseguirTexto("lblNombreCliente");
                    lblApellidoCliente.Text = IdiomaManager.GetInstance().ConseguirTexto("lblApellidoCliente");
                    lblMailCliente.Text = IdiomaManager.GetInstance().ConseguirTexto("lblMailCliente");
                    lblDNICliente.Text = IdiomaManager.GetInstance().ConseguirTexto("lblDNICliente");
                }
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("noHayCliente")); }
        }

        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            frmRegistrarCliente form = new frmRegistrarCliente();
            form.ShowDialog();


            //vuelve a cargar el idioma
            IdiomaManager.GetInstance().archivoActual = "frmGenerarFactura";
            IdiomaManager.GetInstance().Agregar(this);

            ActualizarGrillaClientes();
            ActualizarLabels();
        }

        private void btnCobrarVenta_Click(object sender, EventArgs e)
        {
            if(_factura.listaProductosAgregados.Count > 0 && _factura.clienteFactura != null)
            {
                _factura.Fecha = DateTime.Now;
                _factura.cobro = new BECobro();
                _factura.cobro.NumTransaccionBancaria = bllFactura.TraerUltimoNumTransaccion() + 1;

                frmCobrarVenta form = new frmCobrarVenta(_factura);
                form.ShowDialog();

                if (_factura.cobro != null)
                {
                    btnFinalizar.Enabled = true;
                }

                //vuelve a cargar el idioma
                IdiomaManager.GetInstance().archivoActual = "frmGenerarFactura";
                IdiomaManager.GetInstance().Agregar(this);

                ActualizarLabels();

            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("debeSeleccionar")); }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
        }

        private void btnFinalizar_Click_1(object sender, EventArgs e)
        {
            if(_factura.listaProductosAgregados.Count >0 && _factura.clienteFactura!= null && _factura.cobro != null)
            {
                DialogResult resultado = MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("deseaFinalizar"), IdiomaManager.GetInstance().ConseguirTexto("btnFinalizar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        _factura.NumFactura = bllFactura.RegistrarFactura(_factura);
                        bllFactura.RegistrarItemFactura(_factura);

                        //registra cada item de la factura
                        //registra en la bitacora de eventos
                        bLLEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Ventas", "Factura generada", 2, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));

                        foreach (var item in _factura.listaProductosAgregados)
                        {
                            BEProducto prod = item.producto;
                            int cantidad = item.cantidad;

                            bllProducto.ModificarStock(prod, prod.Stock - cantidad);
                            bLLEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Productos", "Stock reducido", 2, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm")));
                        }


                        MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("ventaFinalizada"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Enabled = false; // deshabilita los botones

                    }
                    catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

    }
}
