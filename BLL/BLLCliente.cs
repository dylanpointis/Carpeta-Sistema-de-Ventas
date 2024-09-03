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
    public class BLLCliente
    {
        DALCliente dalCliente = new DALCliente();

        public void EliminarCliente(int dniCliente)
        {
            dalCliente.EliminarCliente(dniCliente);
        }

        public void HabilitarCliente(int dni)
        {
            dalCliente.HabilitarCliente(dni);
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
                BECliente cliente = new BECliente(Convert.ToInt32(row[0]), row[1].ToString(), row[2].ToString(), row[3].ToString(), Encriptador.DesencriptarAES(row[4].ToString()));
                cliente.BorradoLogico = Convert.ToBoolean(row[5]);
                lista.Add(cliente);
            }
            return lista;
        }

        public BECliente VerificarCliente(int dniCliente)
        {
            return dalCliente.VerificarCliente(dniCliente);
        }


        public bool VerificarSiClienteTieneFacturas(int dniCliente)
        {
            return dalCliente.VerificarSiClienteTieneFacturas(dniCliente);
        }
    }
}
