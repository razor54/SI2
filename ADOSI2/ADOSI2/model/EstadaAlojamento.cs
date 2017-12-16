using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.model
{
    class EstadaAlojamento
    {
        public Alojamento Alojamento { get; set; }
        public Estada Estada { get; set; }
        public int Preço_Base { get; set; }
        public string Descrição { get; set; }
    }
}
