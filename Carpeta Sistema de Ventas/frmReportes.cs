using BE;
using BLL;
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
    public partial class frmReportes : Form, IObserver
    {
        public frmReportes()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().archivoActual = "frmReportes";
            IdiomaManager.GetInstance().Agregar(this);
        }

        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        BLLFactura bllFactura = new BLLFactura();

        private void frmReportes_Load(object sender, EventArgs e)
        {

            
            grillaFacturas.ColumnCount = 13;
            grillaFacturas.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvNumFactura");
            grillaFacturas.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvDNI");
            grillaFacturas.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvTran");
            grillaFacturas.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvTotal");
            grillaFacturas.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvImpuesto");
            grillaFacturas.Columns[5].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvFecha");
            grillaFacturas.Columns[6].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvMetodo");
            grillaFacturas.Columns[7].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvCantCuotas");
            grillaFacturas.Columns[8].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvAlias");
            grillaFacturas.Columns[9].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvNombre");
            grillaFacturas.Columns[10].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvApellido");
            grillaFacturas.Columns[11].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvEmail");
            grillaFacturas.Columns[12].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvDireccion");

            grillaFacturas.Columns[1].Width = 65;
            grillaFacturas.Columns[3].Width = 60;
            grillaFacturas.Columns[4].Width = 60;
            grillaFacturas.Columns[7].Width = 45;



            grillaItems.ColumnCount = 5;
            grillaItems.Columns[0].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvCodigo");
            grillaItems.Columns[1].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvModelo");
            grillaItems.Columns[2].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvCantidad");
            grillaItems.Columns[3].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvPrecio");
            grillaItems.Columns[4].Name = IdiomaManager.GetInstance().ConseguirTexto("dgvSubtotal");

            grillaItems.Columns[2].Width = 40;


            ActualizarGrilla();
        }

        private void ActualizarGrilla()
        {
            List<BEFactura> listaFac = bllFactura.TraerFacturas();

            grillaFacturas.Rows.Clear();
            foreach(BEFactura fac in listaFac)
            {
                grillaFacturas.Rows.Add(fac.NumFactura, fac.clienteFactura.DniCliente, fac.cobro.NumTransaccionBancaria,
                    fac.MontoTotal, fac.Impuesto, fac.Fecha, fac.cobro.stringMetodoPago, fac.cobro.CantCuotas, fac.cobro.AliasMP,
                    fac.clienteFactura.Nombre, fac.clienteFactura.Apellido, fac.clienteFactura.Mail, fac.clienteFactura.Direccion);
            }
        }

        private void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            if (grillaFacturas.SelectedRows.Count > 0)
            {

            }
            else
            {
                MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("seleccione"));
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if(txtNumFactura.Text != "")
            {
                BEFactura fac = bllFactura.TraerFacturas().FirstOrDefault(f => f.NumFactura == Convert.ToInt64(txtNumFactura.Text));

                grillaFacturas.Rows.Clear();

                grillaFacturas.Rows.Add(fac.NumFactura, fac.clienteFactura.DniCliente, fac.cobro.NumTransaccionBancaria,
                    fac.MontoTotal, fac.Impuesto, fac.Fecha, fac.cobro.stringMetodoPago, fac.cobro.CantCuotas, fac.cobro.AliasMP,
                    fac.clienteFactura.Nombre, fac.clienteFactura.Apellido, fac.clienteFactura.Mail, fac.clienteFactura.Direccion);
            }
            else { MessageBox.Show(IdiomaManager.GetInstance().ConseguirTexto("ingrese")); }
            
           
        }



        private void txtNumFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {

                    e.Handled = true;
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarGrilla();
        }

        private void grillaFacturas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grillaFacturas.SelectedRows.Count > 0)
            {
                grillaItems.Rows.Clear();
                long codigofactura = Convert.ToInt64(grillaFacturas.CurrentRow.Cells[0].Value);

                BEFactura fac = bllFactura.TraerFacturas().FirstOrDefault(f => f.NumFactura == codigofactura);

                fac = bllFactura.TraerItemsFactura(fac);

                double total = 0;
                foreach (var item in fac.listaProductosAgregados)
                {
                    BEProducto prod = item.Item1;
                    int cantidad = item.Item2;
                    total = cantidad * prod.Precio;

                    grillaItems.Rows.Add(prod.CodigoProducto, prod.Modelo, cantidad, prod.Precio, total);
                }
            }
        }
    }
}
