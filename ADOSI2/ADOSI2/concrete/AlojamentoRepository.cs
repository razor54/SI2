using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.dal;
using ADOSI2.model;

namespace ADOSI2.concrete
{
    class AlojamentoRepository:IAlojamentoRepository
    {
        private IContext context;
        public AlojamentoRepository(IContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Alojamento> Find(Func<Alojamento, bool> criteria)
        {
            return FindAll().Where(criteria);
        }


        public IEnumerable<Alojamento> FindAll()
        {
            return new AlojamentoMapper(context).ReadAll();
        }
    }

}
