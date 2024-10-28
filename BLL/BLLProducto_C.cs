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
    public class BLLProducto_C
    {
        DALProducto_C dalCambio = new DALProducto_C();

        public List<BEProducto_C> FiltrarCambios(string codProd, string modeloProd, string fechaInicial, string fechaFinal)
        {

            List<BEProducto_C> lista = new List<BEProducto_C>();
            DataTable tabla = dalCambio.FiltrarCambios(codProd, modeloProd, fechaInicial, fechaFinal);

            foreach(DataRow row in tabla.Rows)
            {
                BEProducto prod = new BEProducto(Convert.ToInt64(row[1]), row[4].ToString(), row[5].ToString(),
                    row[6].ToString(), row[7].ToString(), Convert.ToDouble(row[8]), Convert.ToInt32(row[9]), Convert.ToInt32(row[10]), Convert.ToInt32(row[11]), Convert.ToInt32(row[12]), Convert.ToBoolean(row[13]));



                BEProducto_C prodC = new BEProducto_C(prod, Convert.ToInt32(row[0]), row[2].ToString(), row[3].ToString(), Convert.ToBoolean(row[14]));


                lista.Add(prodC);

            }

            return lista;

        }

        public List<BEProducto_C> TraerListaCambios()
        {
            DataTable tabla = dalCambio.TraerListaCambios();
            List<BEProducto_C> listaCambios = new List<BEProducto_C>();

            foreach (DataRow row in tabla.Rows) 
            {
                BEProducto prod = new BEProducto(Convert.ToInt64(row[1]), row[4].ToString(), row[5].ToString(),
                    row[6].ToString(), row[7].ToString(), Convert.ToDouble(row[8]), Convert.ToInt32(row[9]), Convert.ToInt32(row[10]), Convert.ToInt32(row[11]), Convert.ToInt32(row[12]), Convert.ToBoolean(row[13]));



                BEProducto_C prodC = new BEProducto_C(prod, Convert.ToInt32(row[0]), row[2].ToString(), row[3].ToString(), Convert.ToBoolean(row[14]));


                listaCambios.Add(prodC);
            }

            return listaCambios;
        }
    }
}
