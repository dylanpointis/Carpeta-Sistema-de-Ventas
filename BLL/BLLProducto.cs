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
        BLLDigitoVerificador bllDV = new BLLDigitoVerificador();
        public List<BEProducto> TraerListaProductos()
        {
            List<BEProducto> lista = new List<BEProducto> ();
            DataTable tabla = dalProd.TraerListaProducto();

            foreach (DataRow row in tabla.Rows)
            {
                BEProducto producto = new BEProducto(Convert.ToInt64(row[0]), row[1].ToString(), row[2].ToString(),
                    row[3].ToString(), row[4].ToString(), Convert.ToDouble(row[5]), Convert.ToInt32(row[6]), 
                    Convert.ToInt32(row[7]), Convert.ToInt32(row[8]), Convert.ToInt32(row[9]), Convert.ToBoolean(row[10]));
                lista.Add(producto);
            }
            return lista;
        }

        //public void ConsultarStock(BEProducto produto)
        //{
        //    //dalProd.ModificarStock(produto, stock);
        //}

        public void ModificarStock(BEProducto producto, int stock)
        {
            if(producto.Stock - stock >= 0) 
            {
                dalProd.ModificarStock(producto, stock);
                bllDV.PersistirDV(dalProd.TraerListaProducto());
            }
        }

        public void RegistrarProducto(BEProducto prod)
        {
            dalProd.RegistrarProducto(prod);
            bllDV.PersistirDV(dalProd.TraerListaProducto());
        }

        public void EliminarProducto(long idProd)
        {
            dalProd.EliminarProducto(idProd);
            bllDV.PersistirDV(dalProd.TraerListaProducto());
        }


        public void HabilitarProducto(long idProd)
        {
            dalProd.HabilitarProducto(idProd);
            bllDV.PersistirDV(dalProd.TraerListaProducto());
        }


        public void ModificarProducto(BEProducto prod)
        {
            dalProd.ModificarProducto(prod);
            bllDV.PersistirDV(dalProd.TraerListaProducto());
        }

        //public bool VerificarSiProductoTieneFacturas(long codigoProducto)
        //{
        //    return dalProd.VerificarSiProductoTieneFacturas(codigoProducto);
        //}
    }
}
