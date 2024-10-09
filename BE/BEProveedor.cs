using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEProveedor
    {
        public string Nombre { get; set; }
        public string RazonSocial { get; set; }
        public string Email { get; set; }
        public string NumTelefono { get; set; }
        public string CBU { get; set; }
        public string Direccion { get; set; }
        public string Banco { get; set; }


        public BEProveedor(string nombre, string razonSocial, string email, string numTelefono, string cbu, string direccion, string banco)
        {
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
