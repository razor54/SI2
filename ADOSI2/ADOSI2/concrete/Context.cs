using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ADOSI2.dal;


namespace ADOSI2.concrete
{
    public class Context : IContext
    {
        private string connectionString;
        private SqlConnection con = null;
        private ParqueRepository _parqueRepository;
        private EstadaRepository _estadaRepository;
        private AlojamentoRepository _alojamentoRepository;

        public Context(string cs)
        {
            connectionString = cs;
            _parqueRepository=new ParqueRepository(this);
            _estadaRepository=new EstadaRepository(this);
            _alojamentoRepository=new AlojamentoRepository(this);
           
        }

        public void Open()
        {
            if (con == null)
            {
                con = new SqlConnection(connectionString);

            }
            if (con.State != ConnectionState.Open)
                con.Open();
        }

        public SqlCommand CreateCommand()
        {
            Open();
            SqlCommand cmd = con.CreateCommand();
            return cmd;
        }
        public void Dispose()
        {
            if (con != null)
            {
                con.Dispose();
                con = null;
            }

        }

        public void EnlistTransaction()
        {
            if (con != null)
            {
                con.EnlistTransaction(Transaction.Current);
            }
        }

        public ParqueRepository Parques => _parqueRepository;
        public EstadaRepository Estadas => _estadaRepository;
        public AlojamentoRepository Alojamentos => _alojamentoRepository;
    }
}
