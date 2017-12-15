using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.dal;
using ADOSI2.model;

namespace ADOSI2.concrete
{
    class ParqueRepository : IParqueRepository
    {
        private IContext context;
        public ParqueRepository(IContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Parque> Find(Func<Parque, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<Parque> FindAll()
        {
            return new ParqueMapper(context).ReadAll();
        }
    }
}
