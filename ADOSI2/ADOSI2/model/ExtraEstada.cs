using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.model
{
    public class ExtraEstada
    {
        public virtual Extra Extra { get; set; }
        public virtual Estada Estada { get; set; }
        public decimal PreçoDia { get; set; }
        public string Descrição { get; set; }
    }
}
