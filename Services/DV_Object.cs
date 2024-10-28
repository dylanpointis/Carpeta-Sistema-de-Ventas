using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DV_Object
    {
        public string DVH { get; }
        public string DVV { get; }
        public DV_Object(string dvh, string dvv)
        {
            DVH = dvh;
            DVV = dvv;
        }
    }
}
