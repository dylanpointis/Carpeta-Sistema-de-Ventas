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
    public class BLLDigitoVerificador
    {
        DALDigitoVerificador dalDV = new DALDigitoVerificador();

        public string[] CalcularDVHActual()
        {
            DataTable tablaDVGlobal = TraerTablaDV();

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

        public string[] CalcularDVVActual()
        {
            DataTable tablaDVGlobal = TraerTablaDV();

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

        //recibe un vector dvh y un vector dvv con los valores calculados dvh y dvv de todas las tablas
        public bool CompararDV(string[] DVHCalculado, string[] DVVCalculado)
        {
            DataTable tablaDVGlobal = TraerTablaDV();
            int i = 0;
            foreach (DataRow row in tablaDVGlobal.Rows)
            {
                //compara el dvh y dvv global con el calculado
                if (row[1].ToString() != DVHCalculado[i] && row[2].ToString() != DVVCalculado[i])
                {
                    return false;
                }
                i++;
            }
            return true;
        }

        public void PersistirDV(DataTable dataTable)
        {
            int CantColumnas = dataTable.Columns.Count;
            int CantFilas = dataTable.Rows.Count;

            string DVH = "";
            string DVV = "";


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

        public DataTable TraerTablaDV()
        {
            return dalDV.TraerTablaDV();
        }


        public DataTable TraerTablaAConsultarDV(string tablaAConsultarDV)
        {
            return dalDV.TraerTablaAConsultarDV(tablaAConsultarDV);
        }

    }
}
