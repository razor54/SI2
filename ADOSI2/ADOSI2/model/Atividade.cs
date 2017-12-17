using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.model
{
    public class Atividade
    {
        public int Número { get; set; }
        public DateTime DataAtividade { get; set; }
        public decimal Preço { get; set; }
        public int Lotaçao { get; set; }
        public string NomeAtividade { get; set; }
        public virtual Parque Parque{ get; set; }
        public string Descrição{ get; set; }


    }
}
