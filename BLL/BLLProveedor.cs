using BE;
using DAL;
using Services;
using Services.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLProveedor
    {
        private DALProveedor dalProv = new DALProveedor();
        private BLLDigitoVerificador bllDV = new BLLDigitoVerificador();
        private BLLEvento bllEvento = new BLLEvento();

        public void HabilitarProveedor(string cuit)
        {
            dalProv.HabilitarProveedor(cuit);
            bllDV.PersistirDV(dalProv.TraerListaProveedores());
            bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Proveedores", "Proveedor habilitado", 4));
        }
        public void EliminarProveedor(string cuit)
        {
            dalProv.EliminarProveedor(cuit);
            bllDV.PersistirDV(dalProv.TraerListaProveedores());
            bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Proveedores", "Proveedor eliminado", 2));
        }

        public void ModificarProveedor(BEProveedor prov)
        {
            BEProveedor proveedorEncontrado = VerificarProveedor("", prov.CBU,"");
            if (proveedorEncontrado != null && proveedorEncontrado.CUIT != prov.CUIT) //busca un cbu igual pero distinto CUIT
                throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("yaExisteCBU"));


            proveedorEncontrado = VerificarProveedor("", "", prov.Email);
            if (proveedorEncontrado != null && proveedorEncontrado.CUIT != prov.CUIT) //busca un email igual pero distinto CUIT
                throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("yaExisteEmail"));


            prov.BorradoLogico = true;
            dalProv.ModificarProveedor(prov);
            bllDV.PersistirDV(dalProv.TraerListaProveedores());
            bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Proveedores", "Proveedor modificado", 4));
        }

        public void RegistrarProveedor(BEProveedor prov)
        {
            BEProveedor proveedorEncontrado = VerificarProveedor(prov.CUIT, "","");
            if (proveedorEncontrado != null)
                throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("yaExisteCUIT"));
            proveedorEncontrado = VerificarProveedor("", prov.CBU, "");
            if (proveedorEncontrado != null)
                throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("yaExisteCBU"));
            proveedorEncontrado = VerificarProveedor("", "", prov.Email);
            if (proveedorEncontrado != null)
                throw new Exception(IdiomaManager.GetInstance().ConseguirTexto("yaExisteEmail"));



            dalProv.RegistrarProveedor(prov);
            bllDV.PersistirDV(dalProv.TraerListaProveedores());
            bllEvento.RegistrarEvento(new Evento(SessionManager.GetInstance.ObtenerUsuario().NombreUsuario, "Proveedores", "Proveedor creado", 5));
        }

        public List<BEProveedor> TraerListaProveedores()
        {
            List<BEProveedor> lista = new List<BEProveedor>();
            DataTable tabla = dalProv.TraerListaProveedores(); 

            foreach (DataRow row in tabla.Rows)
            {
                BEProveedor proveedor = new BEProveedor(
                    row[0].ToString(), //cuit
                    row[1].ToString(),  //nombre
                    row[2].ToString(),  //razonSocial
                    row[3].ToString(),  //email
                    row[4].ToString(),  //numTelefono
                    row[5].ToString(),  //direccion
                    row[6].ToString(),   //banco
                    row[7].ToString()  //cBU
                );
                proveedor.BorradoLogico = Convert.ToBoolean(row[8]);
                lista.Add(proveedor);
            }
            return lista;
        }


        public BEProveedor VerificarProveedor(string CUITProv, string CBU, string Email)
        {
            if (CBU == "")
                CBU = null;

            return dalProv.VerificarProveedor(CUITProv, CBU, Email);
        }

    }
}
