using ADOSI2.dal;
using ADOSI2.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.concrete
{
    class TendaRepository : ITendaRepository
    {
        private IContext context;
        public TendaRepository(IContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Tenda> Find(Func<Tenda, bool> criteria)
        {
            return FindAll().Where(criteria);
        }

        public IEnumerable<Tenda> FindAll()
        {
            return new TendaMapper(context).ReadAll();
        }

    }
}
