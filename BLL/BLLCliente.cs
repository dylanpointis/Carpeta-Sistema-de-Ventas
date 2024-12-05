using BE;
using DAL;
using Services;
using Services.Observer;
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
        BLLDigitoVerificador bllDV = new BLLDigitoVerificador();
        BLLEvento bllEvento = new BLLEvento();

        public void EliminarCliente(int dniCliente)
        {
            dalCliente.EliminarCliente(dniCliente);
            bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Clientes", "Cliente eliminado", 3));
            bllDV.PersistirDV(dalCliente.TraerListaCliente());
        }

        public void HabilitarCliente(int dni)
        {
            dalCliente.HabilitarCliente(dni);
            bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Clientes", "Cliente habilitado", 2));
            bllDV.PersistirDV(dalCliente.TraerListaCliente());
        }

        public void ModificarCliente(BECliente cliente)
        {
            dalCliente.ModificarCliente(cliente); 
            bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Clientes", "Cliente modificado", 4));
            bllDV.PersistirDV(dalCliente.TraerListaCliente());
        }

        public void RegistrarCliente(BECliente cliente)
        {
            BECliente clienteEncontrado = VerificarCliente(cliente.DniCliente);
            if (clienteEncontrado == null)
            {
                try
                {
                    string direccionEncriptada = Encriptador.EncriptarAES(cliente.Direccion);
                    cliente.Direccion = direccionEncriptada;
                    dalCliente.RegistrarCliente(cliente);
                    bllDV.PersistirDV(dalCliente.TraerListaCliente());
                    bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Clientes", "Cliente creado", 4));
                } catch (Exception ex) { throw ex; }
            }
            else{ throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("yaExiste")); }
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


        //public bool VerificarSiClienteTieneFacturas(int dniCliente)
        //{
        //    return dalCliente.VerificarSiClienteTieneFacturas(dniCliente);
        //}
    }
}
