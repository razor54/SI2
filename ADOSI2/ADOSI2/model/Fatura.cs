using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.model
{
    public class Fatura
    {
        public int Id { get; set; }
        public virtual Estada IdEstada { get; set; }
        public  string NomeHospede { get; set; }
        public  int? ValorFinal { get; set; }

    }
}
