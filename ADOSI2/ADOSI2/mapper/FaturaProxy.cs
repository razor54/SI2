using ADOSI2.dal;
using ADOSI2.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.mapper
{
    class FaturaProxy:Fatura
    {
        private IContext context;
        public FaturaProxy(Fatura c, IContext ctx) : base()
        {
            context = ctx;

            base.Id = c.Id;
            base.IdEstada = null;
            base.NomeHospede = c.NomeHospede;
            base.ValorFinal = c.ValorFinal;
        }

        public override Estada IdEstada 
        {

            get
            {
                if (base.IdEstada == null)
                {
                    //TODO
                    //EstadaMapper pm = new EstadaMapper(context);
                    //base.IdEstada = pm.LoadEstada(this);
                }

                return base.IdEstada;
            }

            set => base.IdEstada = value;

        }
    }
}
