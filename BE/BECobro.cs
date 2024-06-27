using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BECobro
    {
        public int NumTransaccionBancaria { get; set; }
        public string MarcaTarjeta { get; set; }
        public string NumTarjeta { get; set; } //encriptada reversiblemente
        public int CantCuotas { get; set; }
        public string ComentarioAdicional { get; set; } //puede ser Null
        public EnumMetodoPago MetodoPago { get; set; }
        public string AliasMP { get; set; } //puede ser Null
    }
}
