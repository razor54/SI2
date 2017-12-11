using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.model
{
    class Alojamento
    {
        public int PreçoBase { get; set; } 
        public string Descrição { get; set; } 
        public string Localizaçao { get; set; } 
        public virtual Parque Parque { get; set; } 
        public int MaxPessoas { get; set; } 
    }
}
