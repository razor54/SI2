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
    public class BungalowMapper : AbstracMapper<Bungalow, string, List<Bungalow>>, IBungalowMapper
    {

        public BungalowMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText => "delete from Bungalow where nome_alojamento=@nome_alojamento";

        protected override string InsertCommandText =>
            "insert into Bungalow(tipologia, nome_alojamento)" +
            "values(@tipologia,@nome_alojamento); select @nome_alojamento=nome_alojamento from Bungalow;";

        protected override string SelectAllCommandText => "select tipologia, nome_alojamento from Bungalow";

        protected override string SelectCommandText => $"{SelectAllCommandText} where nome_alojamento=@nome_alojamento";

        protected override string UpdateCommandText =>
            "update Bungalow set tipologia=@tipologia, nome_alojamento=@nome_alojamento where nome_alojamento=@nome_alojamento";

        protected override void DeleteParameters(IDbCommand cmd, Bungalow entity)
        {
            SqlParameter p1 = new SqlParameter("@nome_alojamento", entity.Alojamento.Nome);
            cmd.Parameters.Add(p1);
        }

        protected override void InsertParameters(IDbCommand cmd, Bungalow entity)
        {
            UpdateParameters(cmd, entity);
        }

        protected override Bungalow Map(IDataRecord record)
        {
            Bungalow b = new Bungalow();
            b.Tipologia = record.GetString(0);
            var nome_alojamento = record.GetString(1);

            AlojamentoMapper am = new AlojamentoMapper(context);
            b.Alojamento = am.Read(nome_alojamento);

            return b;
        }

        protected override void SelectParameters(IDbCommand cmd, string nome)
        {
            SqlParameter p = new SqlParameter("@nome_alojamento", nome);
            cmd.Parameters.Add(p);
        }

        protected override Bungalow UpdateEntityID(IDbCommand cmd, Bungalow e)
        {
            var param = cmd.Parameters["@nome_alojamento"] as SqlParameter;
            e.Alojamento.Nome = param.Value.ToString();
            return e;
        }

        protected override void UpdateParameters(IDbCommand cmd, Bungalow entity)
        {
            SqlParameter p1 = new SqlParameter("@tipologia", entity.Tipologia);
            SqlParameter p2 = new SqlParameter("@nome_alojamento", entity.Alojamento.Nome);

            p1.Direction = ParameterDirection.InputOutput;

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
        }
    }
}
