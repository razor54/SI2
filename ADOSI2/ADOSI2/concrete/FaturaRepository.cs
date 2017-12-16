using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.dal;
using ADOSI2.model;

namespace ADOSI2.concrete
{
    class FaturaRepository : IFaturaRepository
    {
        private IContext context;
        public FaturaRepository(IContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Fatura> Find(Func<Fatura, bool> criteria)
        {
            return FindAll().Where(criteria);
        }

        public IEnumerable<Fatura> FindAll()
        {
            return new FaturaMapper(context).ReadAll();
        }
    }
}
