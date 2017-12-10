using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.model
{
    class Tenda
    {
        public int Area { get; set; }
        public virtual Alojamento NomeAlojamento { get; set; }
        public string Tipo { get; set; }
    }
}
