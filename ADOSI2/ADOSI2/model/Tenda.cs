using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.model
{
    public class Tenda
    {
        public Decimal Area { get; set; }
        public virtual Alojamento Alojamento { get; set; }
        public string Tipo { get; set; }
    }
}
