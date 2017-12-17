using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.model
{
    public class ComponenteFatura
    {
        public int Id { get; set; }
        public virtual Fatura Fatura { get; set; }
        public string Descrição { get; set; }
        public decimal Preço { get; set; }

        public string Tipo { get; set; }

    }
}
