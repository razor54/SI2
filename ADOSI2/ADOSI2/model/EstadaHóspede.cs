using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.model
{
    public class EstadaHóspede
    {
        public virtual Hóspede Hóspede { get; set; }
        public virtual Estada Estada { get; set; }
    }
}
