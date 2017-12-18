using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ADOSI2.concrete
{
    class CriarEstadaParaPeriodoTemporal
    {
        private readonly Context _context;

        #region Helper
        protected void EnsureContext()
        {
            if (_context == null)
                throw new InvalidOperationException("Data Context not set.");
        }


        #endregion

        public CriarEstadaParaPeriodoTemporal(Context ctx)
        {
            _context = ctx;
        }

        /*
         * return success
         */

        public bool Execute(int id, DateTime datainicio, DateTime dataFim,int nifHospede,int bi,string nomeHospede
            ,string morada,string email,decimal preçoBase,string descriçãoAlojamento,string localizaçao,string nomeAlojamento,
            int maxPessoas,string nomeParque,string tipologia,int idExtraAlojamento,string descriçaoExtraAlojamento,decimal preçoExtraAlojamento,
            string tipoExtra,int idFatura,int idExtraPessoal,string descriçãoExtraPessoal,decimal preçoExtraPessoal)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                EnsureContext();
                _context.EnlistTransaction();
                using (IDbCommand cmd = _context.CreateCommand())
                {
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "criarEstadaParaUmPeríodoDeTempo";

                    var idSql = new SqlParameter("@id",id);
                    var dataInicioSql = new SqlParameter("@data_início",datainicio);
                    var dataFimSql = new SqlParameter("@data_fim", dataFim);
                    var nifHospedeSql = new SqlParameter("@nif_hóspede", nifHospede);
                    var biSql = new SqlParameter("@bi", bi);
                    var nomeHospedeSql = new SqlParameter("@nome_hóspede", nomeHospede);
                    var moradaSql = new SqlParameter("@morada", morada);
                    var emailSql = new SqlParameter("@email", email);
                    var preçoBaseSql = new SqlParameter("@preço_base", preçoBase);
                    var descriçaoAlojamentoSql = new SqlParameter("@descrição_alojamento", descriçãoAlojamento);
                    var localizaçaoSql = new SqlParameter("@localização", localizaçao);
                    var nomeAlojamentoSql = new SqlParameter("@nome_alojamento", nomeAlojamento);
                    var maxPessoasSql = new SqlParameter("@max_pessoas", maxPessoas);
                    var nomeParqueSql = new SqlParameter("@nome_parque", maxPessoas);
                    var tipologiaSql = new SqlParameter("@tipologia", tipologia);
                    var idExtraAlojamentoSql = new SqlParameter("@id_extra_alojamento", idExtraAlojamento);
                    var descriçaoExtraAlojamentoSql = new SqlParameter("@descrição_extra_alojamento", descriçaoExtraAlojamento);
                    var preçoExtraAlojamentoSql = new SqlParameter("@preço_extra_alojamento", preçoExtraAlojamento);
                    var tipoExtraSql = new SqlParameter("@tipo_extra", tipoExtra);
                    var idFaturaSql = new SqlParameter("@id_fatura", idFatura);
                    var idExtraPessoalSql = new SqlParameter("@id_extra_pessoal", idExtraPessoal);
                    var descriçãoExtraPessoalSql = new SqlParameter("@descrição_extra_pessoal", descriçãoExtraPessoal);
                    var preçoExtraPessoalSql = new SqlParameter("@preço_extra_pessoal", preçoExtraPessoal);



                    cmd.Parameters.Add(idSql);
                    cmd.Parameters.Add(dataInicioSql);
                    cmd.Parameters.Add(dataFimSql);
                    cmd.Parameters.Add(nifHospedeSql);
                    cmd.Parameters.Add(biSql);
                    cmd.Parameters.Add(nomeHospedeSql);
                    cmd.Parameters.Add(moradaSql);
                    cmd.Parameters.Add(emailSql);
                    cmd.Parameters.Add(preçoBaseSql);
                    cmd.Parameters.Add(descriçaoAlojamentoSql);
                    cmd.Parameters.Add(localizaçaoSql);
                    cmd.Parameters.Add(nomeAlojamentoSql);
                    cmd.Parameters.Add(maxPessoasSql);
                    cmd.Parameters.Add(nomeParqueSql);
                    cmd.Parameters.Add(tipologiaSql);
                    cmd.Parameters.Add(idExtraAlojamentoSql);
                    cmd.Parameters.Add(descriçaoExtraAlojamentoSql);
                    cmd.Parameters.Add(preçoExtraAlojamentoSql);
                    cmd.Parameters.Add(tipoExtraSql);
                    cmd.Parameters.Add(idFaturaSql);
                    cmd.Parameters.Add(idExtraPessoalSql);
                    cmd.Parameters.Add(descriçãoExtraPessoalSql);
                    cmd.Parameters.Add(preçoExtraPessoalSql);



                    var affected = cmd.ExecuteScalar().ToString();

                    Console.WriteLine("{0} rows affected",affected);
                }
                

                ts.Complete();
            }
            return true;
        }
    }
}
