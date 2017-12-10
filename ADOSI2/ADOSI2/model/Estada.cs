using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.model
{
    class Estada
    {
        public int Id { get; set; }
        //??
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int NifHospede { get; set; }
        public string Pagamento { get; set; }

    }
}