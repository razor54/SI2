using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.model
{
    class ComponenteFatura
    {
        public int Id { get; set; }
        public virtual Fatura IdFatura { get; set; }
        public string Descriçao { get; set; }
        public int Preço { get; set; }

        public string Tipo { get; set; }

    }
}
