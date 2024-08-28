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
            fechaFin.Value = DateTime.Today; fechaInicio.Value = DateTime.Today;
        }

        BLLCambio bllCambios = new BLLCambio();


        private void frmAuditoriaCambios_Load(object sender, EventArgs e)
        {
            grillaCambios.ColumnCount = 11;
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
            grillaCambios.Columns[10].Name = "Activo";



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
                grillaCambios.Rows.Add(prodC.Producto.CodigoProducto, prodC.Fecha, prodC.Hora, prodC.Producto.Modelo, prodC.Producto.Descripcion, prodC.Producto.Marca, prodC.Producto.Color, prodC.Producto.Precio, prodC.Producto.Stock, prodC.Producto.Almacenamiento, prodC.Activo);
            }
            
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarGrilla();
        }
    }
}
