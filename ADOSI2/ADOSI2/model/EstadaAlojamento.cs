using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.model
{
    public class EstadaAlojamento
    {
        public Alojamento Alojamento { get; set; }
        public Estada Estada { get; set; }
        public decimal PreçoBase { get; set; }
        public string Descrição { get; set; }
    }
}
