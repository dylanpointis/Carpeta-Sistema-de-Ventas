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
    public class BLLDigitoVerificador
    {
        DALDigitoVerificador dalDV = new DALDigitoVerificador();
        BLLUsuario bLLUsuario = new BLLUsuario();

        public string[] CalcularDVHActual(DataTable tablaDVGlobal)
        {
            List<string> DVH = new List<string>();

            foreach (DataRow row in tablaDVGlobal.Rows) //recorre cada fila de la tabla DigitoVerificador global
            {
                string NombreTablaActual = row[0].ToString(); //Consigue el nombre de la tabla PK
                DataTable tablaActual = TraerTablaAConsultarDV(NombreTablaActual); //trae la tabla actual PK
                int CantColumnas = tablaActual.Columns.Count;

                string dvhTablaActual = "";
               
                foreach (DataRow rowTablaActual in tablaActual.Rows) //calcula el DVH de la tabla actual pk
                {
                    for (int i = 0; i < CantColumnas; i++)
                    {
                        dvhTablaActual += rowTablaActual[i].ToString();
                    }
                }
                DVH.Add(Encriptador.EncriptarSHA256(dvhTablaActual)); //lo agrega al vector DVH global
               
            }
            return DVH.ToArray();
        }



        public string[] CalcularDVVActual(DataTable tablaDVGlobal)
        {

            List<string> DVV = new List<string>();


            foreach (DataRow row in tablaDVGlobal.Rows) //recorre cada fila de la tabla DigitoVerificador global
            {
                string NombreTablaActual = row[0].ToString(); //Consigue el nombre de la tabla PK
                DataTable tablaActual = TraerTablaAConsultarDV(NombreTablaActual); //trae la tabla actual PK

                int CantFilas = tablaActual.Rows.Count;

                string dvvTablaActual = "";

                foreach (DataColumn column in tablaActual.Columns)
                {
                    for (int i = 0; i < CantFilas; i++)
                    {
                        dvvTablaActual += tablaActual.Rows[i][column].ToString();
                    }
                }
                DVV.Add(Encriptador.EncriptarSHA256(dvvTablaActual)); //lo agrega al vector DVH global
            }
            return DVV.ToArray();
        }



        public void CompararDV(string nombreusuario)
        {
            DataTable tablaDVGlobal = TraerTablaDV();
            //consigue un vector dvh y un vector dvv con los valores calculados dvh y dvv de todas las tablas
            string[] DVHCalculado = CalcularDVHActual(tablaDVGlobal); //vector con los dvh de todas las tablas
            string[] DVVCalculado = CalcularDVVActual(tablaDVGlobal); //vector con los dvv de todas las tablas

            if (SessionManager.GetInstance.ObtenerUsuario() != null)
                throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("yaInicio"));

            BEUsuario user = bLLUsuario.ValidarUsuario(nombreusuario,0,"");
            if (user == null)
                throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("noSeEncontro"));
            if (user.Bloqueado == true || user.Activo == false)
                throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("estaBloqueado"));

            int i = 0;
            foreach (DataRow row in tablaDVGlobal.Rows)
            {
                string tablaaaaa = row[0].ToString();
                string dvhhhhhhhhhhh = row[1].ToString();
                string dvvvvvvv = row[2].ToString();

                if (row[1].ToString() != "" && row[2].ToString() != "") //si ya se les persistio algun DV continua. Si no, lo saltea
                { 
                    //compara el dvh y dvv global con el calculado
                    if (row[1].ToString() != DVHCalculado[i] && row[2].ToString() != DVVCalculado[i])
                    {
                        if (user.Rol.Nombre == "Admin")
                        {
                            string tablaError = row[0].ToString(); //tabla que da error
                            throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("inconsistenciaDVAdmin") + tablaError);
                            //en la ui muestra el formulario reparar DV para el admin
                        }
                        else
                        {
                            throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("inconsistenciaDV"));//para un usuario normal solo muestra error.
                        }
                    }
                }
                i++;
            }
        }

        public void PersistirDV(DataTable dataTable)
        {
            int CantColumnas = dataTable.Columns.Count;
            int CantFilas = dataTable.Rows.Count;

            string DVH = "";
            string DVV = "";

            if(CantFilas > 0) // si la tabla no esta vacia
            {  
                //CALCULAR DVH
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < CantColumnas; i++)
                    {
                        DVH += row[i].ToString();
                    }
                }

                DVH = Encriptador.EncriptarSHA256(DVH);

                //CALCULAR DVV
                foreach (DataColumn column in dataTable.Columns)
                {
                    for (int i = 0; i < CantFilas; i++)
                    {
                        DVV += dataTable.Rows[i][column].ToString();
                    }
                }

                DVV = Encriptador.EncriptarSHA256(DVV);

                DV_Object dvObj = new DV_Object(DVH, DVV);

                dalDV.PersistirDV(dvObj, dataTable.TableName);
            }
        }

        public DataTable TraerTablaDV()
        {
            return dalDV.TraerTablaDV();
        }


        public DataTable TraerTablaAConsultarDV(string tablaAConsultarDV)
        {
            return dalDV.TraerTablaAConsultarDV(tablaAConsultarDV);
        }

        public void RecalcularDV()
        {
            DataTable tablaDVGlobal = TraerTablaDV(); //trae la tabla DV global
            foreach (DataRow row in tablaDVGlobal.Rows)
            {
                PersistirDV(TraerTablaAConsultarDV(row[0].ToString())); 
                //persiste de vuelta el dv de cada registro (cada tabla) asi se puede seguir utilizando el sistema
            }

        }
    }
}
