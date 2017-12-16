using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.dal;
using ADOSI2.model;

namespace ADOSI2.concrete
{
    class HóspedeRepository : IHóspedeRepository
    {
        private IContext context;
        public HóspedeRepository(IContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Hóspede> Find(Func<Hóspede, bool> criteria)
        {
            return FindAll().Where(criteria);
        }

        public IEnumerable<Hóspede> FindAll()
        {
            return new HóspedeMapper(context).ReadAll();
        }

    }
}
