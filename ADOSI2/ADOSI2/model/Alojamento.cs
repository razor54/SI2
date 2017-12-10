using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.model
{
    class Alojamento
    {
        public virtual int PreçoBase { get; set; } 
        public virtual string Descrição { get; set; } 
        public virtual string Localizaçao { get; set; } 
        public virtual Parque NomeParque { get; set; } 
        public virtual int MaxPessoas { get; set; } 
    }
}
