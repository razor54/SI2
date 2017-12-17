using ADOSI2.dal;
using ADOSI2.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.concrete;

namespace ADOSI2.mapper
{
    class ComponenteFaturaProxy:ComponenteFatura
    {
        private IContext context;
        public ComponenteFaturaProxy(ComponenteFatura c, IContext ctx) : base()
        {
            context = ctx;

            base.Id = c.Id;
            base.Fatura = null;
            base.Tipo = c.Tipo;
            base.Preço = c.Preço;
            base.Descrição = c.Descrição;
        }

        public override Fatura Fatura 
        {

            get
            {
                if (base.Fatura == null)
                {
                    //TODO
                    ComponenteFaturaMapper pm = new ComponenteFaturaMapper(context);
                    base.Fatura = pm.LoadFatura(this);
                }

                return base.Fatura;
            }

            set => base.Fatura = value;

        }
    }
}
