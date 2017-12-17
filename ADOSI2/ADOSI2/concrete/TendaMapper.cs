using ADOSI2.dal;
using ADOSI2.mapper;
using ADOSI2.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ADOSI2.concrete;

namespace ADOSI2.concrete
{
    public class TendaMapper : AbstracMapper<Tenda, string, List<Tenda>>, ITendaMapper
    {

        public TendaMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText => "delete from Tenda where nome_alojamento=@nome_alojamento";

        protected override string InsertCommandText =>
            "insert into Tenda(área, nome_alojamento, tipo)" +
            "values(@área,@nome_alojamento,@tipo); select @nome_alojamento=nome_alojamento from Tenda;";

        protected override string SelectAllCommandText => "select área, nome_alojamento, tipo from Tenda";

        protected override string SelectCommandText => $"{SelectAllCommandText} where nome_alojamento=@nome_alojamento";

        protected override string UpdateCommandText =>
            "update Tenda set área=@área, nome_alojamento=@nome_alojamento, tipo=@tipo where nome_alojamento=@nome_alojamento";

        protected override void DeleteParameters(IDbCommand cmd, Tenda entity)
        {
            SqlParameter p1 = new SqlParameter("@nome_alojamento", entity.Alojamento.Nome);
            cmd.Parameters.Add(p1);
        }

        protected override void InsertParameters(IDbCommand cmd, Tenda entity)
        {
            UpdateParameters(cmd, entity);
        }

        protected override Tenda Map(IDataRecord record)
        {
            Tenda t = new Tenda();
            t.Area = record.GetDecimal(0);
            var nome_alojamento = record.GetString(1);
            t.Tipo = record.GetString(2);

            AlojamentoMapper am = new AlojamentoMapper(context);
            t.Alojamento = am.Read(nome_alojamento);

            return t;
        }

        protected override void SelectParameters(IDbCommand cmd, string nome)
        {
            SqlParameter p = new SqlParameter("@nome_alojamento", nome);
            cmd.Parameters.Add(p);
        }

        protected override Tenda UpdateEntityID(IDbCommand cmd, Tenda e)
        {
            var param = cmd.Parameters["@nome_alojamento"] as SqlParameter;
            e.Alojamento.Nome = param.Value.ToString();
            return e;
        }

        protected override void UpdateParameters(IDbCommand cmd, Tenda entity)
        {
            SqlParameter p1 = new SqlParameter("@área", entity.Area);
            SqlParameter p2 = new SqlParameter("@nome_alojamento", entity.Alojamento.Nome);
            SqlParameter p3 = new SqlParameter("@tipo", entity.Tipo);

            p1.Direction = ParameterDirection.InputOutput;

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
        }
    }
}
