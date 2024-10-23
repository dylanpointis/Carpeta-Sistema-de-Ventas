using BE;
using BLL;
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
    public partial class COMPRAfrmRegistrarProveedor : Form
    {
        BLLProveedor bllProvedor = new BLLProveedor();
        bool preRegistro;
        BEProveedor provedor;
        public COMPRAfrmRegistrarProveedor(bool PreRegistro, BEProveedor prov)
        {
            preRegistro = PreRegistro;
            provedor = prov;
            InitializeComponent();
        }

        private void COMPRAfrmRegistrarProveedor_Load(object sender, EventArgs e)
        {
            if(preRegistro == true)
            {
                label6.Text = "Pre-Registrar proveedor";
                txtCBU.Enabled = false;
                txtBanco.Enabled = false;
                btnRegistrarProveedor.Text = "Pre-Registrar proveedor";
            }
            else
            {
                txtCUIT.Enabled = false;
                txtCUIT.Text = provedor.CUIT;
                txtNombre.Text = provedor.Nombre;
                txtRazonSocial.Text = provedor.RazonSocial;
                txtMail.Text = provedor.Email;
                txtNumTelefono.Text = provedor.NumTelefono;
                txtCBU.Text = provedor.CBU;
                txtDireccion.Text = provedor.Direccion; 
                txtBanco.Text = provedor.Banco;
            }
        }
        private void btnRegistrarProveedor_Click(object sender, EventArgs e)
        {
            BEProveedor prov = new BEProveedor
                (
                txtCUIT.Text,
                txtNombre.Text,
                txtRazonSocial.Text,
                txtMail.Text,
                txtNumTelefono.Text,
                txtCBU.Text, txtDireccion.Text, txtBanco.Text
                );
            try
            {
                if(preRegistro == true)
                {
                    bllProvedor.RegistrarProveedor(prov);
                    MessageBox.Show("Proveedor Pre-registrado con exito");
                }
                else
                {
                    bllProvedor.ModificarProveedor( prov );
                    MessageBox.Show("Proveedor registrado completamente con exito");
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

    }
}
