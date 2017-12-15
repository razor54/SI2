using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.dal;
using ADOSI2.model;

namespace ADOSI2.concrete
{
    class EstadaRepository : IEstadaRepository
    {
        private IContext context;
        public EstadaRepository(IContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Estada> Find(Func<Estada, bool> criteria)
        {
            return FindAll().Where(criteria);
        }

        public IEnumerable<Estada> FindAll()
        {
            return new EstadaMapper(context).ReadAll();
        }
    }
}
