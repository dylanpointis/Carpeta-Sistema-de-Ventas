﻿using BE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DAL
{
    public class DALOrdenCompra
    {
        private DALConexion dalCon = new DALConexion();


        public int RegistrarOrdenCompra(BEOrdenCompra ordenCompra)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CUITProveedor", ordenCompra.proveedor.CUIT),
                new SqlParameter("@NumeroSolicitud", ordenCompra.NumeroSolicitudCompra),
                new SqlParameter("@FechaRegistro", ordenCompra.FechaRegistro.ToString("yyyy-MM-dd HH:mm")),
                new SqlParameter("@FechaEntrega", ordenCompra.FechaEntrega.ToString("yyyy-MM-dd HH:mm")),
                new SqlParameter("@Estado", ordenCompra.Estado),
                new SqlParameter("@NumeroTransferencia", ordenCompra.NumeroTransferencia),
                new SqlParameter("@MetodoPago", ordenCompra.MetodoPago),
                new SqlParameter("@MontoTotal", ordenCompra.MontoTotal),
                new SqlParameter("@CantidadTotal", ordenCompra.CantidadTotal),
                new SqlParameter("@NumeroFactura", ordenCompra.NumeroFactura)
            };

            int id = dalCon.EjecutarYTraerId("RegistrarOrdenCompra", parametros);
            return id;
        }


        public void RegistrarItemOrden(int numeroOrdenC, BEItemOrdenCompra item)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@NumeroOrdenCompra", numeroOrdenC),
                new SqlParameter("@CodigoProducto", item.Producto.CodigoProducto),
                new SqlParameter("@Cantidad", item.CantidadSolicitada),
                new SqlParameter("@PrecioCompra", item.PrecioCompra),
            };
            dalCon.EjecutarProcAlmacenado("RegistrarItemOrden", parametros);
        }

        public DataTable TraerListaOrdenes()
        {
            DataTable tabla = dalCon.TraerTabla("OrdenesCompra");
            return tabla;
        }

        public BEProveedor TraerProveedorOrden(int numOrden)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@NumeroOrdenCompra", numOrden)
            };
            DataTable tabla = dalCon.ConsultaProcAlmacenado("TraerProveedorOrden", parametros);


            BEProveedor proveedor = null;
            foreach (DataRow row in tabla.Rows)
            {
                    proveedor = new BEProveedor(
                    row[11].ToString(), //cuit
                    row[12].ToString(),  //nombre
                    row[13].ToString(),  //razonSocial
                    row[14].ToString(),  //email
                    row[15].ToString(),  //numTelefono
                    row[16].ToString(),  //direccion
                    row[17].ToString(),  //banco
                    row[18].ToString()   //cbu
                );
                break;
            }
            return proveedor;
        }

        public DataTable TraerProductosOrden(int numOrden)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@NumeroOrdenCompra", numOrden)
            };
            DataTable tabla = dalCon.ConsultaProcAlmacenado("TraerProductosOrden", parametros);
            return tabla;
        }

        public void MarcarOrdenEntregada(BEOrdenCompra ordenC)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@NumeroOrdenCompra", ordenC.NumeroOrdenCompra),
                new SqlParameter("@Estado", ordenC.Estado),
                new SqlParameter("@Fecha", ordenC.FechaEntrega.ToString("yyyy-MM-dd HH:mm"))
            };
            dalCon.EjecutarProcAlmacenado("MarcarOrdenEntregada", parametros);
        }

        //Esto es para que el digito verificador persista en la tabla Item_OrdenCompra 
        public DataTable traerTablaItemOrden()
        {
            return dalCon.TraerTabla("Item_OrdenCompra");
        }

        public void ModificarCantRecibidaItems(int numeroOrdenCompra, BEItemOrdenCompra item)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@NumeroOrdenCompra", numeroOrdenCompra),
                new SqlParameter("@CodigoProducto", item.Producto.CodigoProducto),
                new SqlParameter("@CantidadRecibida", item.CantidadRecibida),
                new SqlParameter("@NumFactura", item.NumFacturaRecepcion),
                new SqlParameter("@FechaRecepcion", item.FechaRecepcion),
            };
            dalCon.EjecutarProcAlmacenado("ModificarCantidadRecibidaItem", parametros);
        }

        public DataTable TraerOrdenesPendientes()//TRAE ORDENES PENDIENTES O ENTREGADAS PARCIALMENTE
        {
            SqlParameter[] parametros = new SqlParameter[]
            { };
            return dalCon.ConsultaProcAlmacenado("TraerOrdenesPendientes", parametros);
        }
    }
}
