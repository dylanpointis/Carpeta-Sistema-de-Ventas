using BE;
using DAL;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLOrdenCompra
    {
        private DALOrdenCompra dalOrdC = new DALOrdenCompra();
        private BLLDigitoVerificador bllDV = new BLLDigitoVerificador();
        private BLLSolicitudCotizacion bllSolC = new BLLSolicitudCotizacion();
        private BLLProducto bllProducto = new BLLProducto();
        private BLLEvento bllEvento = new BLLEvento();

        public void ConfirmarRecepcion(BEOrdenCompra ordenC)
        {
            ordenC.FechaEntrega = DateTime.Now;
            int cantTotalRecibida = ordenC.obtenerItems().Sum(i => i.CantidadRecibida);
            if (cantTotalRecibida < ordenC.CantidadTotal)
            {
                ordenC.Estado = "Entregada parcialmente";
            }
            else { ordenC.Estado = "Entregada"; }
            MarcarOrdenEntregada(ordenC);

            //modificar el stock de cada producto
            foreach (var item in ordenC.obtenerItems())
            {
                //suma el stock actual + el recibido
                item.Producto.Stock += item.CantidadRecibida;
                bllProducto.ModificarProducto(item.Producto);
            }

            bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Compras", "Productos de orden recibidos", 4));
        }

        public void MarcarOrdenEntregada(BEOrdenCompra ordenC)
        {
            dalOrdC.MarcarOrdenEntregada(ordenC); //marca como entregada y carga el numero de factura
            bllDV.PersistirDV(dalOrdC.TraerListaOrdenes());
            bllDV.PersistirDV(dalOrdC.traerTablaItemOrden());
        }

        public void RegistrarItemOrden(int numeroOrdenC, BEItemOrdenCompra item)
        {
            dalOrdC.RegistrarItemOrden(numeroOrdenC,item);
        }

        public int RegistrarOrdenCompra(BEOrdenCompra ordenCompra)
        {
            ordenCompra.CantidadTotal = ordenCompra.obtenerItems().Sum(i => i.CantidadSolicitada);

            ordenCompra.NumeroOrdenCompra = dalOrdC.RegistrarOrdenCompra(ordenCompra);
            bllDV.PersistirDV(dalOrdC.TraerListaOrdenes());

            //registra cada item
            foreach (BEItemOrdenCompra item in ordenCompra.obtenerItems())
            {
                RegistrarItemOrden(ordenCompra.NumeroOrdenCompra, item);
            }
            bllDV.PersistirDV(traerTablaItemOrden());

            bllSolC.ModificarEstadoSolicitud(ordenCompra.NumeroSolicitudCompra, "Cotizada"); //marca el estado de la solicitud como Cotizada

            //registra el evento
            bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Compras", "Orden de compra generada", 5));

            return ordenCompra.NumeroOrdenCompra;
        }

        public List<BEOrdenCompra> TraerListaOrdenes()
        {
            List<BEOrdenCompra> lista = new List<BEOrdenCompra>();
            DataTable tabla = dalOrdC.TraerListaOrdenes();

            foreach (DataRow row in tabla.Rows)
            {
                int numSolicitud = Convert.ToInt32(row[2].ToString());
                int cantidadTotal = Convert.ToInt32(row[9].ToString());
                string estado = row[5].ToString();
                DateTime fechaEntrega = Convert.ToDateTime(row[4]);
                DateTime fechaRegistro = Convert.ToDateTime(row[3]);
                double montoTotal = Convert.ToDouble(row[8]);

                BEOrdenCompra ordenCompra = new BEOrdenCompra(numSolicitud, cantidadTotal, estado, fechaEntrega, fechaRegistro, montoTotal);
                ordenCompra.NumeroOrdenCompra = Convert.ToInt32(row[0]);
                ordenCompra.NumeroTransferencia = Convert.ToInt32(row[6]);
                lista.Add(ordenCompra);
            }
            return lista;
        }

        public List<BEItemOrdenCompra> TraerProductosOrden(int numOrden)
        {
            DataTable tabla = dalOrdC.TraerProductosOrden(numOrden);
            List<BEItemOrdenCompra> lista = new List<BEItemOrdenCompra>();


            foreach (DataRow row in tabla.Rows)
            {
                BEProducto prod = new BEProducto(
                    Convert.ToInt64(row[6]),
                    row[7].ToString(),
                    row[8].ToString(),
                    row[9].ToString(),
                    row[10].ToString(),
                    Convert.ToDouble(row[11]),
                    Convert.ToInt32(row[12]),
                    Convert.ToInt32(row[13]),
                    Convert.ToInt32(row[14]),
                    Convert.ToInt32(row[15]),
                    Convert.ToBoolean(row[16])
                    );

                BEItemOrdenCompra item = new BEItemOrdenCompra(prod, Convert.ToInt32(row[3]), Convert.ToInt32(row[4]));
                item.CantidadRecibida = Convert.ToInt32(row[5]);

                lista.Add(item);
            }

            return lista;
        }

        public BEProveedor TraerProveedorOrden(int numOrden)
        {
            List<BEProveedor> lista = new List<BEProveedor>();
            BEProveedor prov = dalOrdC.TraerProveedorOrden(numOrden);
            return prov;
        }

        //este metodo es para traer la tabla Item_OrdenesCompra para persistirla en digito verificador

        private DataTable traerTablaItemOrden()
        {
            return dalOrdC.traerTablaItemOrden();
        }
    }
}
