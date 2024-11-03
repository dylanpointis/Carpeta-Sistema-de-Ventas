using BE;
using BE.Composite;
using DAL;
using Services;
using Services.Observer;
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
        BLLEvento bllEv = new BLLEvento();
        BLLFamilia bllFamilia = new BLLFamilia();

        public void Login(string username, string clave)
        {
            if (SessionManager.GetInstance.ObtenerUsuario() != null)
                throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("yaInicio"));


            BEUsuario user = ValidarUsuario(username, 0, ""); //verifica si existe el usuario por el username

            if (user == null)
                throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("noSeEncontro"));

            if (user.Activo == false || user.Bloqueado == true)
                throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("yaExiste"));



            if (user.Clave != Encriptador.EncriptarSHA256(clave))
            {
                ModificarContFallido(user.NombreUsuario, ++user.ContFallidos);
                if (user.ContFallidos >= 3)
                {
                    ModificarBloqueo(user.DNI, true);
                    bllEv.RegistrarEvento(new Evento(username, "Sesiones", "Usuario bloqueado", 1));
                    throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("seBloqueo"));
                }
                throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("incorrecta"));
            }

            //si pasa todo
            ModificarContFallido(username, 0);
            user.listaPermisosRol = bllFamilia.TraerListaPermisosRol(user.codRol);
            SessionManager.GetInstance.LogIn(user);
            bllEv.RegistrarEvento(new Evento(username, "Sesiones", "Inicio sesión", 1));
        }




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

        public void CambiarClave(int DNICliente, string clave, string claveActual)
        {
            BEUsuario usuarioActual = SessionManager.GetInstance.ObtenerUsuario();
            if (usuarioActual == null)
                throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("debeIniciar"));


            if(claveActual != "")
            {
                if (Encriptador.EncriptarSHA256(claveActual) != SessionManager.GetInstance.ObtenerUsuario().Clave)
                { 
                    throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("incorrecta"));
                }
            }


            if (clave.Length < 8)
                throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("masde8"));
            
            dalUsuario.CambiarClave(DNICliente, Encriptador.EncriptarSHA256(clave));
            bllEv.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Sesiones", "Cambio de clave", 1));
        }

        public void ActivarUsuario(int DNICliente)
        {
            dalUsuario.ActivarUsuario(DNICliente);
        }

        public void ModificarContFallido(string nombreUsuario, int contClaveIncorrecta)
        {
            dalUsuario.ModificarContFallido(nombreUsuario, contClaveIncorrecta);
        }


        public void ResetearClavePorDefecto(int DNI)
        {
            BEUsuario user = ValidarUsuario("", DNI, "");
            string clave = DNI.ToString() + user.Apellido;

            CambiarClave(DNI, clave,"");
            //resetea el contador de fallidos a 0
            ModificarContFallido(user.NombreUsuario, 0);
            bllEv.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Gestión usuarios", "Usuario desbloqueado", 1));
        }
    }
}
