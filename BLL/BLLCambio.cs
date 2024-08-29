using BE;
using DAL;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLCambio
    {
        DALCambio dalCambio = new DALCambio();

        public List<Producto_C> FiltrarCambios(string codProd, string modeloProd, string fechaInicial, string fechaFinal)
        {

            List<Producto_C> lista = new List<Producto_C>();
            DataTable tabla = dalCambio.FiltrarCambios(codProd, modeloProd, fechaInicial, fechaFinal);

            foreach(DataRow row in tabla.Rows)
            {
                BEProducto prod = new BEProducto(Convert.ToInt64(row[1]), row[4].ToString(), row[5].ToString(),
                    row[6].ToString(), row[7].ToString(), Convert.ToDouble(row[8]), Convert.ToInt32(row[9]), Convert.ToInt32(row[10]), Convert.ToBoolean(row[11]));



                Producto_C prodC = new Producto_C(prod, Convert.ToInt32(row[0]), row[2].ToString(), row[3].ToString(), Convert.ToBoolean(row[12]));


                lista.Add(prodC);

            }

            return lista;

        }

        public List<Producto_C> TraerListaCambios()
        {
            DataTable tabla = dalCambio.TraerListaCambios();
            List<Producto_C> listaCambios = new List<Producto_C>();

            foreach (DataRow row in tabla.Rows) 
            {
                BEProducto prod = new BEProducto(Convert.ToInt64(row[1]), row[4].ToString(), row[5].ToString(),
                    row[6].ToString(), row[7].ToString(), Convert.ToDouble(row[8]), Convert.ToInt32(row[9]), Convert.ToInt32(row[10]), Convert.ToBoolean(row[11]));



                Producto_C prodC = new Producto_C(prod, Convert.ToInt32(row[0]), row[2].ToString(), row[3].ToString(), Convert.ToBoolean(row[12]));


                listaCambios.Add(prodC);
            }

            return listaCambios;
        }
    }
}
