using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ADOSI2.concrete
{
    public struct InscreverHóspedeEmAtividade
    {
        private readonly Context _context;

        #region Helper

        private void EnsureContext()
        {
            if (_context == null)
                throw new InvalidOperationException("Data Context not set.");
        }

        #endregion

        public InscreverHóspedeEmAtividade(Context ctx)
        {
            _context = ctx;
        }

        /*
         * return success
         */

        public bool Execute(int nifHospede, string nomeAtividade, string nomeParque)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                EnsureContext();
                _context.EnlistTransaction();
                using (IDbCommand cmd = _context.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "inscreverHóspedeNumaAtividade";


                    var nifHospedeSql = new SqlParameter("@nif_hóspede", nifHospede);
                    var nomeAtividadeSql = new SqlParameter("@nome_atividade", nomeAtividade);
                    var nomeParqueSql = new SqlParameter("@nome_parque", nomeParque);


                    cmd.Parameters.Add(nifHospedeSql);
                    cmd.Parameters.Add(nomeAtividadeSql);
                    cmd.Parameters.Add(nomeParqueSql);


                    var affected = cmd.ExecuteNonQuery().ToString();
                    cmd.Parameters.Clear();

                    Console.WriteLine("{0} rows affected", affected);
                }


                ts.Complete();
            }

            return true;
        }
    }
}