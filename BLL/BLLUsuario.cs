using BE;
using BE.Composite;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLUsuario
    {
        DALUsuario dalUsuario = new DALUsuario();

        public BEUsuario ValidarUsuario(string nombreusuario, int dni, string email)
        {
            BEUsuario user = dalUsuario.ValidarUsuario(nombreusuario, dni, email);
            return user;
        }

        public List<BEUsuario> TraerListaUsuarios()
        {
            List<BEUsuario> listaUsuario = new List<BEUsuario>();
           
            BLLFamilia bLLFamilia = new BLLFamilia();
            List<Familia> listaRoles = bLLFamilia.TraerListaRoles();


            DataTable tabla = dalUsuario.TraerListaUsuario();
            
            foreach (DataRow dr in tabla.Rows)
            {
                Familia rolEncontrado = listaRoles.FirstOrDefault(r => r.Id == Convert.ToInt32(dr[6]));

                BEUsuario user = new BEUsuario(Convert.ToInt32(dr[0]), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), rolEncontrado.Id, Convert.ToBoolean(dr[7]), Convert.ToBoolean(dr[8]));
                user.ContFallidos = Convert.ToInt16(dr[9]);
                user.Rol = rolEncontrado;
                
                
                listaUsuario.Add(user);
            }
            return listaUsuario;
        }


        public void ModificarBloqueo(int DNI, bool bloqueo)
        {
            dalUsuario.ModificarBloqueo(DNI,  bloqueo);
        }

        public void RegistrarUsuario(BEUsuario user)
        {
            dalUsuario.RegistrarUsuario(user);
        }

        public void ModificarUsuario(BEUsuario user)
        {
            dalUsuario.ModificarUsuario(user);
        }

        public void EliminarUsuario(int DNICliente)
        {
            dalUsuario.EliminarUsuario(DNICliente);
        }

        public void CambiarClave(int DNICliente, string clave)
        {
            dalUsuario.CambiarClave(DNICliente, clave);
        }

        public void ActivarUsuario(int DNICliente)
        {
            dalUsuario.ActivarUsuario(DNICliente);
        }

        public void ModificarContFallido(string nombreUsuario, int contClaveIncorrecta)
        {
            dalUsuario.ModificarContFallido(nombreUsuario, contClaveIncorrecta);
        }
    }
}
