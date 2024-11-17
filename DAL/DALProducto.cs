using BE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALProducto
    {

        DALConexion dalCon = new DALConexion();


        public void RegistrarProducto(BEProducto prod)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CodigoProducto", prod.CodigoProducto),
                new SqlParameter("@Modelo", prod.Modelo),
                new SqlParameter("@Descripcion", prod.Descripcion),
                new SqlParameter("@Marca", prod.Marca),
                new SqlParameter("@Color", prod.Color),
                new SqlParameter("@Precio", prod.Precio),
                new SqlParameter("@Stock", prod.Stock),
                new SqlParameter("@StockMinimo", prod.StockMin),
                new SqlParameter("@StockMaximo", prod.StockMax),
                new SqlParameter("@Almacenamiento", prod.Almacenamiento)
            };
            dalCon.EjecutarProcAlmacenado("RegistrarProducto", parametros);
        }
        public void EliminarProducto(long idProd)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                 new SqlParameter("@CodigoProducto", idProd)
            };

            dalCon.EjecutarProcAlmacenado("EliminarProducto", parametros);
        }
        public void ModificarStock(BEProducto produto, int stock)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                 new SqlParameter("@CodigoProducto", produto.CodigoProducto),
                 new SqlParameter("@Stock", stock)
            };

            dalCon.EjecutarProcAlmacenado("ModificarStock", parametros);
        }


        public DataTable TraerListaProducto()
        {
            DataTable tabla = dalCon.TraerTabla("Productos");
            return tabla;
        }

        public void ModificarProducto(BEProducto prod)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CodigoProducto", prod.CodigoProducto),
                new SqlParameter("@Modelo", prod.Modelo),
                new SqlParameter("@Descripcion", prod.Descripcion),
                new SqlParameter("@Marca", prod.Marca),
                new SqlParameter("@Color", prod.Color),
                new SqlParameter("@Precio", prod.Precio),
                new SqlParameter("@Stock", prod.Stock),
                new SqlParameter("@StockMinimo", prod.StockMin),
                new SqlParameter("@StockMaximo", prod.StockMax),
                new SqlParameter("@Almacenamiento", prod.Almacenamiento),
                new SqlParameter("@Borrado", prod.BorradoLogico),

            };
            dalCon.EjecutarProcAlmacenado("ModificarProducto", parametros);
        }

        //public bool VerificarSiProductoTieneFacturas(long codigoProducto)
        //{
        //    SqlParameter[] parametros = new SqlParameter[]
        //    {
        //        new SqlParameter("@CodigoProducto", codigoProducto)
        //    };
        //    DataTable tabla = dalCon.ConsultaProcAlmacenado("VerificarSiProductoTieneFacturas", parametros);

        //    if (tabla.Rows.Count > 0)
        //    {
        //        return true;
        //    }
        //    else { return false; }
        //}

        public void HabilitarProducto(long codProd)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                 new SqlParameter("@CodigoProducto", codProd)
            };
            dalCon.EjecutarProcAlmacenado("HabilitarProducto", parametros);
        }

        public int ConsultarStock(long codProd)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                 new SqlParameter("@CodigoProducto", codProd)
            };
            return dalCon.EjecutarYTraerId("ConsultarStock", parametros);
        }

        public DataTable TraerProductosBajoStock()
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
            };
            return dalCon.ConsultaProcAlmacenado("TraerProductosBajoStock", parametros);
        }

        public DataTable PredecirReposicionStock()
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
            };
            return dalCon.ConsultaProcAlmacenado("ReporteInteligenteStock", parametros);
        }
    }
}
