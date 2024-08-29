using BLL;
using Services;
using Services.Observer;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Carpeta_Sistema_de_Ventas
{
    public partial class frmAuditoriaCambios : Form
    {
        public frmAuditoriaCambios()
        {
            InitializeComponent(); 
            fechaInicio.Format = DateTimePickerFormat.Custom;
            fechaInicio.CustomFormat = Application.CurrentCulture.DateTimeFormat.ShortDatePattern;

            fechaFin.Format = DateTimePickerFormat.Custom;
            fechaFin.CustomFormat = Application.CurrentCulture.DateTimeFormat.ShortDatePattern;
            fechaFin.Value = DateTime.Today; fechaInicio.Value = DateTime.Today.AddDays(-31);
        }

        BLLCambio bllCambios = new BLLCambio();


        private void frmAuditoriaCambios_Load(object sender, EventArgs e)
        {
            grillaCambios.ColumnCount = 12;
            grillaCambios.Columns[0].Name = "Codigo producto";
            grillaCambios.Columns[1].Name = "Fecha";
            grillaCambios.Columns[2].Name = "Hora";
            grillaCambios.Columns[3].Name = "Modelo";
            grillaCambios.Columns[4].Name = "Descripcion";
            grillaCambios.Columns[5].Name = "Marca";
            grillaCambios.Columns[6].Name = "Color";
            grillaCambios.Columns[7].Name = "Precio";
            grillaCambios.Columns[8].Name = "Stock";
            grillaCambios.Columns[9].Name = "Almacenamiento";
            grillaCambios.Columns[10].Name = "Borrado";
            grillaCambios.Columns[11].Name = "Activo";



            //grillaCambios.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewCodigo");
            //grillaCambios.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewFecha");
            //grillaCambios.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewHora");
            //grillaCambios.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewModelo");
            //grillaCambios.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewDescripcion");
            //grillaCambios.Columns[5].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewMarca");
            //grillaCambios.Columns[6].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewColor");
            //grillaCambios.Columns[7].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewPrecio");
            //grillaCambios.Columns[8].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewStock");
            //grillaCambios.Columns[9].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewAlmacenamiento");
            //grillaCambios.Columns[10].Name = IdiomaManager.GetInstance().ConseguirTexto("gridViewActivo");





            ActualizarGrilla();
        }

        private void ActualizarGrilla()
        {
            grillaCambios.Rows.Clear();
            List<Producto_C> listaCambios = bllCambios.TraerListaCambios();

            foreach(var prodC in listaCambios)
            {
                grillaCambios.Rows.Add(prodC.Producto.CodigoProducto, prodC.Fecha, prodC.Hora, prodC.Producto.Modelo, prodC.Producto.Descripcion, prodC.Producto.Marca, prodC.Producto.Color, prodC.Producto.Precio, prodC.Producto.Stock, prodC.Producto.Almacenamiento, prodC.Producto.BorradoLogico,prodC.Activo);
            }
            
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarGrilla();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string fechaInicial = fechaInicio.Value.ToString("yyyy-MM-dd");
            string fechaFinal = fechaFin.Value.ToString("yyyy-MM-dd");


            List<Producto_C> list = bllCambios.FiltrarCambios(txtCodigoProd.Text, txtModelo.Text, fechaInicial, fechaFinal);
            grillaCambios.Rows.Clear();

            foreach (Producto_C prodC in list)
            {
                grillaCambios.Rows.Add(prodC.Producto.CodigoProducto, prodC.Fecha, prodC.Hora, prodC.Producto.Modelo, prodC.Producto.Descripcion, prodC.Producto.Marca, prodC.Producto.Color, prodC.Producto.Precio, prodC.Producto.Stock, prodC.Producto.Almacenamiento, prodC.Producto.BorradoLogico, prodC.Activo);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtModelo.Text = ""; txtCodigoProd.Text = "";
            fechaFin.Value = DateTime.Today; fechaInicio.Value = DateTime.Today.AddDays(-31);
        }

        private void fechaInicio_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaInicial = fechaInicio.Value;
            DateTime fechaFinal = fechaFin.Value;

            if (fechaInicial > fechaFinal) //La fecha inicial no puede ser mayor a la final
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("fechaInicial"));
                fechaInicio.Value = fechaFinal;
            }
        }

        private void fechaFin_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaInicial = fechaInicio.Value;
            DateTime fechaFinal = fechaFin.Value;

            if (fechaFinal < fechaInicial) //La fecha final no puede ser menor a la inicial
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("fechaFinal"));
                fechaFin.Value = fechaInicial;
            }
        }
    }
}
