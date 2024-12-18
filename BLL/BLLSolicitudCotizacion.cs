﻿using BE;
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
    public class BLLSolicitudCotizacion
    {
        private DALSolicitudCotizacion dalSolC = new DALSolicitudCotizacion();
        private BLLDigitoVerificador bllDV = new BLLDigitoVerificador();
        private BLLEvento bllEv = new BLLEvento();

        public int RegistrarSolicitudCotizacion(BESolicitudCotizacion solicitudCoti)
        {
            if (solicitudCoti.obtenerProveedoresSolicitud().Count == 0)
            {
                throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("seleccioneAlMenosUnProv"));
            }

            if (solicitudCoti.obtenerItems().TrueForAll(i => i.Cantidad > 0))
            {
                solicitudCoti.NumSolicitud = dalSolC.RegistrarSolicitudCotizacion(solicitudCoti);
                bllDV.PersistirDV(dalSolC.TraerListaSolicitudes()); 
                
                //registrar items de la solicitud
                foreach (BEItemSolicitud item in solicitudCoti.obtenerItems())
                {
                    RegistrarItemSolicitud(item, solicitudCoti.NumSolicitud);
                }

                //registrar proveedores de la solicitud
                foreach (BEProveedor prov in solicitudCoti.obtenerProveedoresSolicitud())
                {
                    RegistrarProveedorSolicitud(prov, solicitudCoti.NumSolicitud);
                }
                //registra evento
                bllEv.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Compras", "Solicitud de cotización generada", 5));
                return solicitudCoti.NumSolicitud;
            }
            else {  throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("ingreseCantidades"));}
        }


        public void RegistrarItemSolicitud(BEItemSolicitud item, int idSolicitud)
        {
            dalSolC.RegistrarItemSolicitud(item, idSolicitud);
            bllDV.PersistirDV(traerTablaItemSolicitud());
        }

        public void RegistrarProveedorSolicitud(BEProveedor prov, int idSolicitud)
        {
            dalSolC.RegistrarProveedorSolicitud(prov, idSolicitud);
            bllDV.PersistirDV(traerTablaProveedorSolicitud());
        }


        public List<BESolicitudCotizacion> TraerListaSolicitudes()
        {
            List<BESolicitudCotizacion> lista = new List<BESolicitudCotizacion>();
            DataTable tabla = dalSolC.TraerListaSolicitudes();


            foreach (DataRow row in tabla.Rows)
            {
                BESolicitudCotizacion sol = new BESolicitudCotizacion(row[2].ToString(), Convert.ToDateTime(row[1]));
                sol.NumSolicitud = Convert.ToInt32(row[0].ToString());
                lista.Add(sol);
            }
            return lista;
        }

        public List<BEItemSolicitud> TraerItemsSolicitud(int numSol)
        {
            DataTable tabla = dalSolC.TraerItemsSolicitud(numSol);
            List<BEItemSolicitud> lista = new List<BEItemSolicitud>();


            foreach (DataRow row in tabla.Rows)
            {
                BEProducto prod = new BEProducto(
                    Convert.ToInt64(row[4]),
                    row[5].ToString(),
                    row[6].ToString(),
                    row[7].ToString(),
                    row[8].ToString(),
                    Convert.ToDouble(row[9]),
                    Convert.ToInt32(row[10]),
                    Convert.ToInt32(row[11]),
                    Convert.ToInt32(row[12]),
                    Convert.ToInt32(row[13]),
                    Convert.ToBoolean(row[14])
                    );
                lista.Add(new BEItemSolicitud(prod,Convert.ToInt32(row[3])));
            }

            return lista;
        }

        public List<BEProveedor> TraerProveedoresSolicitud(int numSol)
        {
            List<BEProveedor> lista = new List<BEProveedor>();
            DataTable tabla = dalSolC.TraerProveedoresSolicitud(numSol);

            foreach (DataRow row in tabla.Rows)
            {
                BEProveedor proveedor = new BEProveedor(
                    row[3].ToString(), //cuit
                    row[4].ToString(),  //nombre
                    row[5].ToString(),  //razonSocial
                    row[6].ToString(),  //email
                    row[7].ToString(),  //numTelefono
                    row[8].ToString(),  //direccion
                    row[9].ToString(),  //banco
                    row[10].ToString()   //cbu
                );
                proveedor.BorradoLogico = Convert.ToBoolean(row[11]);

                lista.Add(proveedor);
            }

            return lista;
        }

        public void ModificarEstadoSolicitud(int numeroSolicitudCompra, string estado)
        {
            dalSolC.ModificarEstadoSolicitud(numeroSolicitudCompra, estado);
            bllDV.PersistirDV(dalSolC.TraerListaSolicitudes());
        }


        //estos metodos son para hacer persistir el digito verificador en la tabla Item_Solicitud e Solicitud_Proveedor
        private DataTable traerTablaItemSolicitud()
        {
            return dalSolC.traerTablaItemSolicitud();
        }
        private DataTable traerTablaProveedorSolicitud()
        {
            return dalSolC.traerTablaProveedorSolicitud();
        }
    }
}
