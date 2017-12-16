using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.dal;
using ADOSI2.model;

namespace ADOSI2.concrete
{
    class ExtraRepository : IExtraRepository
    {
        private IContext context;
        public ExtraRepository(IContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Extra> Find(Func<Extra, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<Extra> FindAll()
        {
            return new ExtraMapper(context).ReadAll();
        }
    }

}
