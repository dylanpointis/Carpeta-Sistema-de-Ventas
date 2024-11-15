using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BECobro
    {
        public int? NumTransaccionBancaria { get; set; }
        public string MarcaTarjeta { get; set; }
        public int CantCuotas { get; set; }
        public string ComentarioAdicional { get; set; } //puede ser Null
        public EnumMetodoPago MetodoPago { get; set; }
        public string AliasMP { get; set; } //puede ser Null
    
        public string stringMetodoPago { get; set; }
    }
}
