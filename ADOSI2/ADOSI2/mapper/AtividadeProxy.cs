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
    class AtividadeProxy:Atividade
    {
        private IContext context;
        public AtividadeProxy(Atividade c, IContext ctx) : base()
        {
            context = ctx;

            base.NomeAtividade = c.NomeAtividade;
            base.DataAtividade = c.DataAtividade;
            base.Lotaçao = c.Lotaçao;
            base.Número = c.Número;
            base.Preço = c.Preço;
            base.Descrição = c.Descrição;

            base.Parque = null;
        }

        public override Parque Parque
        {

            get
            {
                if (base.Parque == null)
                {
                    //TODO
                    AtividadeMapper pm = new AtividadeMapper(context);
                    base.Parque = pm.LoadParque(this);
                }

                return base.Parque;
            }

            set => base.Parque = value;

        }

    }
}
