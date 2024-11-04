using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEProveedor
    {
        public string CUIT { get; set; }
        public string Nombre { get; set; }
        public string RazonSocial { get; set; }
        public string Email { get; set; }
        public string NumTelefono { get; set; }
        public string CBU { get; set; }
        public string Direccion { get; set; }
        public string Banco { get; set; }

        public bool BorradoLogico { get; set; }


        public BEProveedor(string cuit,string nombre, string razonSocial, string email, string numTelefono,  string direccion, string banco, string cbu)
        {
            CUIT = cuit;
            Nombre = nombre;
            RazonSocial = razonSocial;
            Email = email;
            NumTelefono = numTelefono;
            CBU = cbu;
            Direccion = direccion;
            Banco = banco;
        }
    }
}
