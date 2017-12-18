using ADOSI2.dal;
using ADOSI2.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.concrete
{
    class BungalowRepository : IBungalowRepository
    {
        private IContext context;
        public BungalowRepository(IContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Bungalow> Find(Func<Bungalow, bool> criteria)
        {
            return FindAll().Where(criteria);
        }

        public IEnumerable<Bungalow> FindAll()
        {
            return new BungalowMapper(context).ReadAll();
        }

    }
}
