using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.model
{
    class Atividade
    {
        public int Numero { get; set; }
        public int Preço { get; set; }
        public int Lotaçao { get; set; }
        public int NomeAtividade { get; set; }
        public virtual Parque NomeParque{ get; set; }
        public virtual string Decriçao{ get; set; }


    }
}
