using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EF.logic
{
    public struct RemoverParqueEAssociaçoes
    {
        private readonly Entities _context;

        #region Helper

        private void EnsureContext()
        {
            if (_context == null)
                throw new InvalidOperationException("Data Context not set.");
        }

        #endregion

        public RemoverParqueEAssociaçoes(Entities ctx)
        {
            _context = ctx;
        }

        /*
         * return success
         */

        public void Execute(Parque p)
        {
            EnsureContext();


            var alojamentos = _context.Alojamentoes.Where(a => a.Parque.nome.Equals(p.nome));

            //find relations of alojamento-estada
            var estadaAlojamentos = _context.EstadaAlojamentoes
                .Where(ea => alojamentos.Any(a => a.nome.Equals(ea.Alojamento.nome)));

            var estadaHóspedes = _context.Estadas
                .Where(eh => estadaAlojamentos.Any(al => al.Estada.id == eh.id));

            var hospedes = estadaHóspedes.SelectMany(eh => eh.Hóspede);


            foreach (var estadaHóspede in estadaHóspedes)
            {
                _context.Estadas.Remove(estadaHóspede);
            }


            foreach (var ea in estadaAlojamentos)
            {
                _context.EstadaAlojamentoes.Remove(ea);
            }

            foreach (var alojamento in alojamentos)
            {
                _context.Alojamentoes.Remove(alojamento);
            }
            //alojamentos deleted

            var hospedesSemEstada =
                hospedes.Where(h => h.Estadas.Count == 0);

            foreach (var e in hospedesSemEstada)
            {
                _context.Hóspede.Remove(e);
            }

            _context.Parques.Remove(p);

            _context.SaveChanges();
        }
    }
}