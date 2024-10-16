using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLSolicitudCotizacion
    {
        private DALSolicitudCotizacion dalSolC = new DALSolicitudCotizacion();

        public int RegistrarSolicitudCotizacion(BESolicitudCotizacion solicitudCoti)
        {
            int id =dalSolC.RegistrarSolicitudCotizacion(solicitudCoti);
            return id;
        }


        public void RegistrarItemSolicitud(BEItemSolicitud item, int idSolicitud)
        {
            dalSolC.RegistrarItemSolicitud(item, idSolicitud);
        }

        public void RegistrarProveedorSolicitud(BEProveedor prov, int idSolicitud)
        {
            dalSolC.RegistrarProveedorSolicitud(prov, idSolicitud);
        }

    }
}
