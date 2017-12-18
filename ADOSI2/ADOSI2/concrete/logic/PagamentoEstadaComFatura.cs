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
    class PagamentoEstadaComFatura
    {
        private readonly Context _context;

        #region Helper
        protected void EnsureContext()
        {
            if (_context == null)
                throw new InvalidOperationException("Data Context not set.");
        }


        #endregion

        public PagamentoEstadaComFatura(Context ctx)
        {
            _context = ctx;
        }

        /*
         * return success
         */

        public bool Execute(int idEstada,out int total)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                EnsureContext();
                _context.EnlistTransaction();
                using (IDbCommand cmd = _context.CreateCommand())
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "pagamentoEstadaComFatura";


                    var idEstadaSql = new SqlParameter("@id_estada", idEstada);
                    total = 0;
                    var totalSql = new SqlParameter("@total", total);

                    totalSql.Direction=ParameterDirection.Output;

                    cmd.Parameters.Add(idEstadaSql);
                    cmd.Parameters.Add(totalSql);

                    var affected = cmd.ExecuteNonQuery().ToString();
                    total=Convert.ToInt32(cmd.Parameters[@total].ToString());
                    cmd.Parameters.Clear();


                    Console.WriteLine("{0} rows affected", affected);
                }


                ts.Complete();

            }
            return true;
        }
    }
}
