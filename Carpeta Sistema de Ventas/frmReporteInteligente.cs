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
    public partial class frmReporteInteligente : Form, IObserver
    {
        public frmReporteInteligente()
        {
            InitializeComponent(); 
            IdiomaManager.GetInstance().archivoActual = "frmReporteInteligente";
            IdiomaManager.GetInstance().Agregar(this);
        }

        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        BLLProducto bllProd = new BLLProducto();
        BLLFactura bllFac = new BLLFactura();
        DataTable reporteSeleccionado = new DataTable();
        string detalleReporte = "";

        private void frmReporteInteligente_Load(object sender, EventArgs e)
        {
            ActualizarCombobox();

            fechaInicio.Format = DateTimePickerFormat.Custom;
            fechaInicio.CustomFormat = Application.CurrentCulture.DateTimeFormat.ShortDatePattern;

            fechaFin.Format = DateTimePickerFormat.Custom;
            fechaFin.CustomFormat = Application.CurrentCulture.DateTimeFormat.ShortDatePattern;
            fechaFin.Value = DateTime.Today; fechaInicio.Value = DateTime.Today.AddDays(-7); fechaFin.MaxDate = DateTime.Today; fechaInicio.MaxDate = DateTime.Today;
            fechaInicio.Visible = true; fechaFin.Visible = true; lblFechaInicio.Visible = true; lblFechaFin.Visible = true;
        }

        private void ActualizarGrilla()
        {
            grillaDatosReporte.Columns.Clear();
            grillaDatosReporte.Rows.Clear();

            foreach (DataColumn column in reporteSeleccionado.Columns)
            {
                grillaDatosReporte.Columns.Add(column.ColumnName, IdiomaManager.GetInstance().ConseguirTexto("gridView" + column.ColumnName));
            }

            foreach (DataRow row in reporteSeleccionado.Rows)
            {
                List<object> rowData = new List<object>();
                foreach (var item in row.ItemArray)
                {
                    rowData.Add(item);
                }

                grillaDatosReporte.Rows.Add(rowData.ToArray());
            }
        }

        private void ActualizarCombobox()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Texto"); // La columna para el texto a mostrar
            dt.Columns.Add("Valor"); // La columna para el valor real

            dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Ventas generadas por producto"), "Ventas generadas por producto");
            dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Ventas generadas por marca"), "Ventas generadas por marca");
            dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Ventas generadas por cliente"), "Ventas generadas por cliente");
            dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Predicción ventas por producto"), "Precedir ventas por producto");
            dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Predicción reposición de stock"), "Predecir reposición de stock");
            dt.Rows.Add(IdiomaManager.GetInstance().ConseguirTexto("Predecir ingresos"), "Predecir ingresos");
            cmbReporte.DataSource = dt;
            cmbReporte.DisplayMember = "Texto";
            cmbReporte.ValueMember = "Valor";
        }


        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            if (cmbReporte.SelectedValue.ToString() != "")
            {
                switch (cmbReporte.SelectedValue.ToString())
                {
                    case "Predecir reposición de stock":
                        reporteSeleccionado = bllProd.PredecirReposicionStock();
                        break;
                    case "Ventas generadas por producto":
                        reporteSeleccionado = bllFac.ReporteVentasGeneradas(fechaInicio.Value.ToString("yyyy-MM-dd"), fechaFin.Value.ToString("yyyy-MM-dd"), "VentasGeneradasPorProducto");
                        break;
                    case "Ventas generadas por marca":
                        reporteSeleccionado = bllFac.ReporteVentasGeneradas(fechaInicio.Value.ToString("yyyy-MM-dd"), fechaFin.Value.ToString("yyyy-MM-dd"), "VentasGeneradasPorMarca");
                        break;
                    case "Ventas generadas por cliente":
                        reporteSeleccionado = bllFac.ReporteVentasGeneradas(fechaInicio.Value.ToString("yyyy-MM-dd"), fechaFin.Value.ToString("yyyy-MM-dd"), "VentasGeneradasPorCliente");
                        break;
                    case "Precedir ventas por producto":
                        reporteSeleccionado = bllFac.ReportePrecedirVentasPorProd();
                        break;
                    case "Predecir ingresos":
                        reporteSeleccionado = bllFac.ReportePrecedirIngresos();
                        break;
                    default:
                        break;
                }
                detalleReporte = cmbReporte.Text; //esto es para mostrarlo traducido en el pdf
                ActualizarGrilla();
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccione"),"", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void cmbReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            fechaInicio.Visible = false; fechaFin.Visible = false; lblFechaInicio.Visible = false; lblFechaFin.Visible = false;
            if (cmbReporte.SelectedValue.ToString() == "Ventas generadas por producto" || cmbReporte.SelectedValue.ToString() == "Ventas generadas por marca" || cmbReporte.SelectedValue.ToString() == "Ventas generadas por cliente")
            {
                fechaInicio.Visible = true; fechaFin.Visible = true; lblFechaInicio.Visible = true; lblFechaFin.Visible = true;
            }
        }

        private void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            if(grillaDatosReporte.Rows.Count > 0)
            {
                Reportes.GenerarReporteInteligente(grillaDatosReporte, Properties.Resources.htmlreporteinteligente, Properties.Resources.logopng, detalleReporte);
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("genereReporte"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void fechaFin_ValueChanged(object sender, EventArgs e)
        {

            DateTime fechaInicial = fechaInicio.Value;
            DateTime fechaFinal = fechaFin.Value;

            if (fechaFinal < fechaInicial) //La fecha final no puede ser menor a la inicial
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("fechaFinalNoPuedeSerMenorAInicial"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fechaFin.Value = fechaInicial;
            }
        }

        private void fechaInicio_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaInicial = fechaInicio.Value;
            DateTime fechaFinal = fechaFin.Value;

            if (fechaInicial > fechaFinal) //La fecha inicial no puede ser mayor a la final
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("fechaInicioNoPuedeSerMayorAFinal"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fechaInicio.Value = fechaFinal;
            }
        }
    }
}
