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
    class FaturaProxy:Fatura
    {
        private IContext context;
        public FaturaProxy(Fatura c, IContext ctx) : base()
        {
            context = ctx;

            base.Id = c.Id;
            base.Estada = null;
            base.Hóspede = null;
            base.ValorFinal = c.ValorFinal;
        }

        public override Estada Estada 
        {

            get
            {
                if (base.Estada == null)
                {
                    //TODO
                    FaturaMapper pm = new FaturaMapper(context);
                    base.Estada = pm.LoadEstada(this);
                }

                return base.Estada;
            }

            set => base.Estada = value;

        }

        public override Hóspede Hóspede
        {

            get
            {
                if (base.Estada == null)
                {
                    //TODO
                    FaturaMapper pm = new FaturaMapper(context);
                    base.Hóspede = pm.LoadHóspede(this);
                }

                return base.Hóspede;
            }

            set => base.Hóspede= value;

        }
    }
}
