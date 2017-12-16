using ADOSI2.dal;
using ADOSI2.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.mapper
{
    class TendaProxy:Tenda
    {
        private IContext context;
        public TendaProxy(Tenda c, IContext ctx) : base()
        {
            context = ctx;

            base.Tipo = c.Tipo;
            base.Area = c.Area;
            base.Alojamento = null;
        }

        public override Alojamento Alojamento
        {

            get
            {
                if (base.Alojamento == null)
                {
                    //TODO
                    //AlojamentoMapper pm = new AlojamentoMapper(context);
                    //base.NomeAlojamento = pm.LoadAlojamento(this);
                }

                return base.Alojamento;
            }

            set => base.Alojamento = value;

        }
    }
}
