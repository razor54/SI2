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
    class AlojamentoProxy : Alojamento
    {
        private IContext context;
        public AlojamentoProxy(Alojamento c, IContext ctx) : base()
        {
            context = ctx;
            base.Nome = c.Nome;
            base.Descrição = c.Descrição;
            base.Localizaçao = c.Localizaçao;
            base.MaxPessoas = c.MaxPessoas;
            base.PreçoBase = c.PreçoBase;
            base.Parque = null;
        }

        public override Parque Parque {

            get
            {
                if (base.Parque == null)
                {
                    //TODO
                    AlojamentoMapper pm = new AlojamentoMapper(context);
                    base.Parque = pm.LoadParque(this);
                }

                return base.Parque;
            }

            set => base.Parque = value;

        }
       

    }
}
