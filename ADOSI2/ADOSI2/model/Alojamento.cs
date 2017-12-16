using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.model
{
    public class 
        Alojamento
    {
        public Decimal PreçoBase { get; set; } 
        public string Descrição { get; set; } 
        public string Localizaçao { get; set; } 
        public string Nome { get; set; }
        public virtual Parque Parque { get; set; } 
        public int MaxPessoas { get; set; } 
    }
}
