using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.dal;
using ADOSI2.mapper;
using ADOSI2.model;

namespace ADOSI2.concrete
{
    public class EstadaAlojamentoMapper : AbstracMapper<EstadaAlojamento, KeyValuePair<string, int>, List<EstadaAlojamento>>, IEstadaAlojamentoMapper
    {


        public EstadaAlojamentoMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText => "delete from EstadaAlojamento where nome_alojamento=@nome_alojamento and id_estada=@id_estada";

        protected override string InsertCommandText =>
            "insert into EstadaAlojamento(nome_alojamento, id_estada,preço_base,descrição)" +
            " values(@nome_alojamento,@id_estada,@preço_base,@descrição);";

        protected override string SelectAllCommandText => "select nome_alojamento, id_estada,preço_base,descrição from EstadaAlojamento";


        protected override string SelectCommandText => $"{SelectAllCommandText} where nome_alojamento=@nome_alojamento and id_estada=@id_estada";

        protected override string UpdateCommandText =>
            "update EstadaAlojamento set preço_base=@preço_base, descrição=@descrição where nome_alojamento=@nome_alojamento and id_estada=@id_estada";

        protected override void DeleteParameters(IDbCommand cmd, EstadaAlojamento entity)
        {
            SqlParameter p1 = new SqlParameter("@id_estada", entity.Estada?.Id);
            SqlParameter p2 = new SqlParameter("@nome_alojamento", entity.Alojamento?.Nome);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
        }

        protected override void InsertParameters(IDbCommand cmd, EstadaAlojamento entity)
        {
            UpdateParameters(cmd, entity);
        }

        protected override EstadaAlojamento Map(IDataRecord record)
        {
            EstadaAlojamento a = new EstadaAlojamento();
            var nomeAlojamento = record.GetString(0);
            var idEstada = Convert.ToInt32(record.GetDecimal(1));
            a.PreçoBase = record.GetDecimal(2);
            a.Descrição = record.GetString(3);

            AlojamentoMapper extraMapper = new AlojamentoMapper(context);
            a.Alojamento = extraMapper.Read(nomeAlojamento);

            EstadaMapper estadaMapper = new EstadaMapper(context);
            a.Estada = estadaMapper.Read(idEstada);


            return a;
        }

        protected override void SelectParameters(IDbCommand cmd, KeyValuePair<string, int> pair)
        {
            SqlParameter p = new SqlParameter("@id_estada", pair.Value);
            SqlParameter p2 = new SqlParameter("@nome_alojamento", pair.Key);
            cmd.Parameters.Add(p);
            cmd.Parameters.Add(p2);
        }

        protected override EstadaAlojamento UpdateEntityID(IDbCommand cmd, EstadaAlojamento e)
        {
            //No Need to to anything here
            return e;
        }

        protected override void UpdateParameters(IDbCommand cmd, EstadaAlojamento entity)
        {
            SqlParameter p1 = new SqlParameter("@preço_base", entity.PreçoBase);
            SqlParameter p2 = new SqlParameter("@descrição", entity.Descrição);

            SqlParameter p5 = new SqlParameter("@nome_alojamento", entity.Alojamento?.Nome);
            SqlParameter p6 = new SqlParameter("@id_estada", entity.Estada?.Id);


            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);

            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
        }

    }
}
