using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ADOSI2.concrete.logic
{
    public class ListarAtividadeComLugares
    {
        private readonly Context _context;

        #region Helper
        protected void EnsureContext()
        {
            if (_context == null)
                throw new InvalidOperationException("Data Context not set.");
        }


        #endregion

        public ListarAtividadeComLugares(Context ctx)
        {
            _context = ctx;
        }

        /*
         * return success
         */

        public bool Execute(DateTime dataInicio, DateTime dataFim)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                EnsureContext();
                _context.EnlistTransaction();
                using (IDbCommand cmd = _context.CreateCommand())
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "listarAtividadesComlugares";


                    var dataInitSql = new SqlParameter("@dataInit", dataInicio);
                    var dataFimSql = new SqlParameter("@dataFim", dataFim);

                    cmd.Parameters.Add(dataInitSql);
                    cmd.Parameters.Add(dataFimSql);

                    var affected = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    Console.WriteLine("{0} rows affected", affected);
                }


                ts.Complete();

            }
            return true;
        }
    }
}
