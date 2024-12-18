﻿using BE;
using BLL;
using Services;
using Services.Observer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmAuditoriaCambios : Form, IObserver
    {
        public frmAuditoriaCambios()
        {
            InitializeComponent(); 
            fechaInicio.Format = DateTimePickerFormat.Custom;
            fechaInicio.CustomFormat = Application.CurrentCulture.DateTimeFormat.ShortDatePattern;

            fechaFin.Format = DateTimePickerFormat.Custom;
            fechaFin.CustomFormat = Application.CurrentCulture.DateTimeFormat.ShortDatePattern;
            fechaFin.Value = DateTime.Today; fechaInicio.Value = DateTime.Today.AddDays(-31);
            fechaFin.MaxDate = DateTime.Today; fechaInicio.MaxDate = DateTime.Today;

            IdiomaManager.GetInstance().archivoActual = "frmAuditoriaCambios";
            IdiomaManager.GetInstance().Agregar(this);
        }

        BLLProducto_C bllCambios = new BLLProducto_C();
        BLLProducto bllProd = new BLLProducto();
        List<BEProducto_C> listaCambios = new List<BEProducto_C>();

        private void frmAuditoriaCambios_Load(object sender, EventArgs e)
        {
            grillaCambios.ColumnCount = 14;



            grillaCambios.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvCodProd");
            grillaCambios.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvFecha");
            grillaCambios.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvHora");
            grillaCambios.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvModelo");
            grillaCambios.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvDescripcion");
            grillaCambios.Columns[5].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvMarca");
            grillaCambios.Columns[6].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvColor");
            grillaCambios.Columns[7].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvPrecio");
            grillaCambios.Columns[8].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvStock");
            grillaCambios.Columns[9].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvSMin");
            grillaCambios.Columns[10].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvSMax");
            grillaCambios.Columns[11].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvAlmacenamiento");
            grillaCambios.Columns[12].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvBorrado");
            grillaCambios.Columns[13].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvActivo");


            listaCambios = bllCambios.TraerListaCambios();
            ActualizarGrilla();
        }


        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }
        private void ActualizarGrilla()
        {
            grillaCambios.Rows.Clear();

            foreach(var prodC in listaCambios)
            {
                string activo = ""; //El elemento activo es el que tiene el estado actual del producto, los antiguos no son activos
                if (prodC.Activo == true)
                {
                    activo = IdiomaManager.GetInstance().ConseguirTexto("boolTrue");
                }
                else { activo = IdiomaManager.GetInstance().ConseguirTexto("boolFalse"); }

                string borrado = "";
                if(prodC.Producto.BorradoLogico == true) //Si el bool BorradoLogico es true es porque el producto esta habilitado (no borrado)
                {
                    borrado = IdiomaManager.GetInstance().ConseguirTexto("boolHabilitado");
                }
                else { borrado = IdiomaManager.GetInstance().ConseguirTexto("boolBorrado"); }

                grillaCambios.Rows.Add(prodC.Producto.CodigoProducto, prodC.Fecha, prodC.Hora, prodC.Producto.Modelo, prodC.Producto.Descripcion, prodC.Producto.Marca, prodC.Producto.Color, prodC.Producto.Precio, prodC.Producto.Stock, prodC.Producto.StockMin, prodC.Producto.StockMax, prodC.Producto.Almacenamiento, borrado, activo);
            }



            grillaCambios.BindingContext = new BindingContext(); //ESTO ES PARA COLOREAR EN CELESTE A LOS ACTIVOS. ASEGURA QUE SE LLENEN BIEN LOS DATOS DEL GRIDVIEW
            foreach (DataGridViewRow row in grillaCambios.Rows)
            {
                if (row.Cells[13].Value.ToString() == IdiomaManager.GetInstance().ConseguirTexto("boolTrue"))
                {
                    row.DefaultCellStyle.BackColor = Color.PowderBlue; //pone en celeste el estado activo del producto
                }
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            listaCambios = bllCambios.TraerListaCambios();
            ActualizarGrilla();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string fechaInicial = fechaInicio.Value.ToString("yyyy-MM-dd");
            string fechaFinal = fechaFin.Value.ToString("yyyy-MM-dd");


            listaCambios = bllCambios.FiltrarCambios(txtCodigoProd.Text, txtModelo.Text, fechaInicial, fechaFinal);
            ActualizarGrilla();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtModelo.Text = ""; txtCodigoProd.Text = "";
            fechaFin.Value = DateTime.Today; fechaInicio.Value = DateTime.Today.AddDays(-31);
            listaCambios = bllCambios.TraerListaCambios();
            ActualizarGrilla();
        }

        private void fechaInicio_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaInicial = fechaInicio.Value;
            DateTime fechaFinal = fechaFin.Value;

            if (fechaInicial > fechaFinal) //La fecha inicial no puede ser mayor a la final
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("fechaInicial"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fechaInicio.Value = fechaFinal;
            }
        }

        private void fechaFin_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaInicial = fechaInicio.Value;
            DateTime fechaFinal = fechaFin.Value;

            if (fechaFinal < fechaInicial) //La fecha final no puede ser menor a la inicial
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("fechaFinal"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fechaFin.Value = fechaInicial;
            }
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            if (grillaCambios.SelectedRows.Count > 0) 
            {
                if (grillaCambios.CurrentRow.Cells[13].Value.ToString() == IdiomaManager.GetInstance().ConseguirTexto("boolFalse"))
                {
                    long codProd = Convert.ToInt64(grillaCambios.CurrentRow.Cells[0].Value);
                    string modelo = grillaCambios.CurrentRow.Cells[3].Value.ToString();
                    string descripcion = grillaCambios.CurrentRow.Cells[4].Value.ToString();
                    string marca = grillaCambios.CurrentRow.Cells[5].Value.ToString();
                    string color = grillaCambios.CurrentRow.Cells[6].Value.ToString();
                    double precio = Convert.ToDouble(grillaCambios.CurrentRow.Cells[7].Value);
                    int stock = Convert.ToInt32(grillaCambios.CurrentRow.Cells[8].Value);
                    int stockmin = Convert.ToInt32(grillaCambios.CurrentRow.Cells[9].Value);
                    int stockmax = Convert.ToInt32(grillaCambios.CurrentRow.Cells[10].Value);
                    int alm = Convert.ToInt32(grillaCambios.CurrentRow.Cells[11].Value);

               
                    bool borrado = grillaCambios.CurrentRow.Cells[12].Value.ToString() == IdiomaManager.GetInstance().ConseguirTexto("boolHabilitado") ? true : false;
                    
                    //si dice "Habilitado" es porque no esta borrado entonces se le pone true al bool.

                    BEProducto prod = new BEProducto(codProd, modelo, descripcion, marca, color, precio, stock,stockmin,stockmax, alm, borrado);

                    bllProd.ModificarProducto(prod);
                    ActualizarGrilla();
                    MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("exitoActivado"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("yaEstaActivado"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); } //tiene True en el bool Activado
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccionaGrid"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        }

        private void txtCodigoProd_KeyPress(object sender, KeyPressEventArgs e)
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

                    if (texto.Length >= 14)
                    {
                        e.Handled = true;
                    }
                }
            }
        }
    }
}
