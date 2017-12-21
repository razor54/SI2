using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ADOSI2.mapper;
using ADOSI2.model;

namespace ADOSI2.concrete.logic
{
    public struct ApagarParqueEAssociaçoes
    {
        private readonly Context _context;

        #region Helper
        private void EnsureContext()
        {
            if (_context == null)
                throw new InvalidOperationException("Data Context not set.");
        }


        #endregion

        public ApagarParqueEAssociaçoes(Context ctx)
        {
            _context = ctx;
        }

        /*
         * return success
         */

        public void Execute(Parque p)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                EnsureContext();
                _context.EnlistTransaction();
                using (IDbCommand cmd = _context.CreateCommand())
                {

                    var parqueMapper = new ParqueMapper(_context);
                    var alojamentoMapper = new AlojamentoMapper(_context);
                    var estadaMapper = new EstadaMapper(_context);
                    var estadaAlojamentoMapper = new EstadaAlojamentoMapper(_context);
                    var estadaHóspedeMapper = new EstadaHóspedeMapper(_context);
                    var hospedeMapper = new HóspedeMapper(_context);

                    var alojamentos = alojamentoMapper.ReadAll().Where(a => a.Parque.Nome.Equals(p.Nome));

                    //find relations of alojamento-estada
                    var estadaAlojamentos = estadaAlojamentoMapper.ReadAll()
                        .Where(ea => alojamentos.Any(a=>a.Nome.Equals(ea.Alojamento.Nome)));

                    var estadaHóspedes = estadaHóspedeMapper.ReadAll()
                        .Where(eh => estadaAlojamentos.Any(al => al.Estada.Id == eh.Estada.Id));

                    var hospedes = estadaHóspedes.Select(eh => eh.Hóspede);

                    
                    foreach (var estadaHóspede in estadaHóspedes)
                    {
                        estadaHóspedeMapper.Delete(estadaHóspede);
                    }
                   

                   foreach (var ea in estadaAlojamentos)
                   {
                       estadaAlojamentoMapper.Delete(ea);
                       estadaMapper.Delete(ea.Estada);
                   }
                    foreach (var alojamento in alojamentos)
                    {
                        alojamentoMapper.Delete(alojamento);
                    }
                    //alojamentos deleted

                    var hospedesSemEstada =
                        hospedes.Where(h => estadaHóspedeMapper.ReadAll().All(eh => eh.Hóspede.Nif != h.Nif));

                    foreach (var e in hospedesSemEstada)
                    {
                        hospedeMapper.Delete(e);
                    }

                    parqueMapper.Delete(p);


                }


                ts.Complete();
            }
           
        }
    }
}
