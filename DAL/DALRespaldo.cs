using Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALRespaldo
    {
        private DALConexion dalCon = new DALConexion();
        public void RealizarBackUp(string ruta)
        {
            dalCon.EjecutarComando($"BACKUP DATABASE CarpetaIngSoftware TO DISK= '{ruta}'");
        }


        public void RealizarRestore(string ruta)
        {
            try
            {
                dalCon.EstablecerConexionConMaster(); //Metodo para conectarse con MASTER. No me funcionaba el "USE master"


                //Cambiar la base de datos a modo SINGLE_USER y cerrar todas las conexiones activas
                dalCon.EjecutarComando("ALTER DATABASE CarpetaIngSoftware SET SINGLE_USER WITH ROLLBACK IMMEDIATE;");
                //Restaurar
                dalCon.EjecutarComando($"RESTORE DATABASE CarpetaIngSoftware FROM DISK = '{ruta}' WITH REPLACE;");
                //Volver a poner en MULTI_USER
                dalCon.EjecutarComando("ALTER DATABASE CarpetaIngSoftware SET MULTI_USER;");
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            finally // Asegura que la base de datos esté en modo multiusuario al final del proceso, incluso si hay un fallo
            { 
                dalCon.EjecutarComando("ALTER DATABASE CarpetaIngSoftware SET MULTI_USER;"); 
            }
        }
    }
}
