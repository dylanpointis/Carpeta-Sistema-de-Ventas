﻿using BE;
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
                new SqlParameter("@Almacenamiento", prod.Almacenamiento)
            };
            dalCon.EjecutarProcAlmacenado("RegistrarProducto", parametros);
        }
        public void EliminarProducto(int idProd)
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
                new SqlParameter("@Almacenamiento", prod.Almacenamiento)
            };
            dalCon.EjecutarProcAlmacenado("ModificarProducto", parametros);
        }
    }
}
