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
    public partial class frmAyuda : Form, IObserver
    {
        public frmAyuda()
        {
            InitializeComponent();
            IdiomaManager.GetInstance().archivoActual = "frmAyuda";
            IdiomaManager.GetInstance().Agregar(this);
        }

        public void ActualizarObserver()
        {
            IdiomaManager.ActualizarControles(this);
        }

        private void frmAyuda_Load(object sender, EventArgs e)
        {
            //label1.Text = "Sistema destinado a la venta de Celulares, permitiendo a los empleados y administradoresrealizar sus tareas diarias, persistiendo la\ninformación en la base de datos\n\nMenu Admin: Permite administrar los usuarios del sistema, y sus perfiles (roles). El administrador puede crear, eliminar o \nmodificar usuarios, asignandole un rol a cada uno. Como tambien crear, modifcar o eliminar roles (conjunto de permisos y familias)\n y familias (conjunto de permisos).\n\nMaestros: permite gestionar los productos y clientes, permitiendo crear, modificar o eliminarlos\n\nUsuarios: Permite a los usuarios realizar tareas basicas como cambiar su clave o idioma\n\nVentas: Permite a los usuarios asignados a la venta registrar una nueva factura en el sistema, cargando los productos seleccionados,\n seleccionado o registrando el cliente y registrar los datos del pago\n\nReportes: Permite visualizar las facturas de las ventas realizadas, viendo informacion del pago, cliente y productos comprados\n. Asi como tambien, generar un archivo PDF para la misma";
        }
    }
}
