using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.concrete;
using ADOSI2.dal;
using ADOSI2.model;

namespace ADOSI2.mapper
{
    class HóspedeAtividadeProxy : HóspedeAtividade
    {
        private IContext context;

        public HóspedeAtividadeProxy(HóspedeAtividade c, IContext ctx) : base()
        {
            context = ctx;

            base.Nome_Atividade = c.Nome_Atividade;
            base.Nome_Parque = c.Nome_Parque;
            base.Hóspede = null;
        }

        public override Hóspede Hóspede
        {
            get
            {
                if (base.Hóspede == null)
                {
                    //TODO
                    HóspedeAtividadeMapper pm = new HóspedeAtividadeMapper(context);
                    base.Hóspede = pm.LoadHóspede(this);
                }

                return base.Hóspede;
            }

            set => base.Hóspede = value;
        }
    }
}
