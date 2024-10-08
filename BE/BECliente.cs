﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class BECliente
    {
        public int DniCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail { get; set; }
        public string Direccion { get; set; }
        public bool BorradoLogico { get; set; }

        public BECliente() { }  //Constructor vacio porque XMLSerializer lo requiere

        public BECliente(int dnicliente, string nombre, string apellido, string mail, string direccion)
        {
            this.DniCliente = dnicliente;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Mail = mail;
            this.Direccion = direccion;
        }
    }
}
