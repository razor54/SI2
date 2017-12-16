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
            base.Lotaçao = c.Lotaçao;
            base.Numero = c.Numero;
            base.Preço = c.Preço;
            base.Decriçao = c.Decriçao;

            base.Parque = null;
        }

        public override Parque Parque
        {

            get
            {
                if (base.Parque == null)
                {
                    //TODO
                    //ParqueMapper pm = new ParqueMapper(context);
                    //base.Parque = pm.LoadParque(this);
                }

                return base.Parque;
            }

            set => base.Parque = value;

        }

    }
}
