using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLCliente
    {
        DALCliente dalCliente = new DALCliente();

        public void EliminarCliente(int dniCliente)
        {
            dalCliente.EliminarCliente(dniCliente);
        }

        public void ModificarCliente(BECliente cliente)
        {
            dalCliente.ModificarCliente(cliente);
        }

        public void RegistrarCliente(BECliente cliente)
        {
            dalCliente.RegistrarCliente(cliente);
        }

        public List<BECliente> TraerListaCliente()
        {
            List<BECliente> lista = new List<BECliente>();
            DataTable tabla = dalCliente.TraerListaCliente();

            foreach (DataRow row in tabla.Rows)
            {
                BECliente cliente = new BECliente(Convert.ToInt32(row[0]), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString());
                lista.Add(cliente);
            }
            return lista;
        }


        /*VERIFICAR CLIENTE*/
    }
}
