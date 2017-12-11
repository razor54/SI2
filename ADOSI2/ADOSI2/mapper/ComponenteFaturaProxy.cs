using ADOSI2.dal;
using ADOSI2.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.mapper
{
    class ComponenteFaturaProxy:ComponenteFatura
    {
        private IContext context;
        public ComponenteFaturaProxy(ComponenteFatura c, IContext ctx) : base()
        {
            context = ctx;

            base.Id = c.Id;
            base.IdFatura = null;
            base.Tipo = c.Tipo;
            base.Preço = c.Preço;
            base.Descriçao = c.Descriçao;
        }

        public override Fatura IdFatura 
        {

            get
            {
                if (base.IdFatura == null)
                {
                    //TODO
                    FaturaMapper pm = new FaturaMapper(context);
                    base.IdFatura = pm.LoadFatura(this);
                }

                return base.IdFatura;
            }

            set => base.IdFatura = value;

        }
    }
}
