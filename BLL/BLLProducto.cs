using BE;
using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLProducto
    {
        DALProducto dalProd = new DALProducto();
        public List<BEProducto> TraerListaProductos()
        {
            List<BEProducto> lista = new List<BEProducto> ();
            DataTable tabla = dalProd.TraerListaProducto();

            foreach (DataRow row in tabla.Rows)
            {
                BEProducto producto = new BEProducto(Convert.ToInt64(row[0]), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), Convert.ToDouble(row[5]), Convert.ToInt32(row[6]), Convert.ToInt32(row[7]));
                lista.Add(producto);
            }
            return lista;
        }

        public void ConsultarStock(BEProducto produto)
        {
            //dalProd.ModificarStock(produto, stock);
        }

        public void ModificarStock(BEProducto produto, int stock)
        {
            dalProd.ModificarStock(produto, stock);
        }

        public void RegistrarProducto(BEProducto prod)
        {
            dalProd.RegistrarProducto(prod);
        }

        public void EliminarProducto(long idProd)
        {
            dalProd.EliminarProducto(idProd);
        }

        public void ModificarProducto(BEProducto prod)
        {
            dalProd.ModificarProducto(prod);
        }
    }
}
