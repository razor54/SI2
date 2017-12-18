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
    class EnviarEmailsNumPeriodo
    {
        private readonly Context _context;

        #region Helper
        protected void EnsureContext()
        {
            if (_context == null)
                throw new InvalidOperationException("Data Context not set.");
        }


        #endregion

        public EnviarEmailsNumPeriodo(Context ctx)
        {
            _context = ctx;
        }

        /*
         * return success
         */

        public bool Execute(int dias)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                EnsureContext();
                _context.EnlistTransaction();
                using (IDbCommand cmd = _context.CreateCommand())
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "enviarEmailsNumIntervaloTemporal";


                    var diasSql = new SqlParameter("@dias", dias);
                   
                    cmd.Parameters.Add(diasSql);
                    

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
