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
        public virtual Estada Estada { get; set; }
        public virtual Hóspede Hóspede { get; set; }
        public  decimal? ValorFinal { get; set; }

    }
}
