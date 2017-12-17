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
    public class ExtraEstadaMapper : AbstracMapper<ExtraEstada, KeyValuePair<int,int>, List<ExtraEstada>>, IExtraEstadaMapper
    {
       

        public ExtraEstadaMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText => "delete from ExtraEstada where id_extra=@id_extra and id_estada=@id_estada";

        protected override string InsertCommandText =>
            "insert into ExtraEstada(id_extra, id_estada,preço_dia,descrição)" +
            " values(@id_extra,@id_estada,@preço_dia,@descrição);";

        protected override string SelectAllCommandText => "select id_extra, id_estada,preço_dia,descrição from ExtraEstada";
                                                          

        protected override string SelectCommandText => $"{SelectAllCommandText} where id_extra=@id_extra and id_estada=@id_estada";

        protected override string UpdateCommandText =>
            "update ExtraEstada set preço_dia=@preço_dia, descrição=@descrição where id_extra=@id_extra and id_estada=@id_estada";

        protected override void DeleteParameters(IDbCommand cmd, ExtraEstada entity)
        {
            SqlParameter p1 = new SqlParameter("@id_estada", entity.Estada?.Id);
            SqlParameter p2 = new SqlParameter("@id_extra", entity.Extra?.Id);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
        }

        protected override void InsertParameters(IDbCommand cmd, ExtraEstada entity)
        {
            UpdateParameters(cmd, entity);
        }

        protected override ExtraEstada Map(IDataRecord record)
        {
            ExtraEstada a = new ExtraEstada();
            int id_extra = Convert.ToInt32(record.GetDecimal(0));
            int id_estada = Convert.ToInt32(record.GetDecimal(1));
            a.PreçoDia = record.GetDecimal(2);
            a.Descrição = record.GetString(3);

            ExtraMapper extraMapper = new ExtraMapper(context);
            a.Extra = extraMapper.Read(id_extra);

            EstadaMapper estadaMapper = new EstadaMapper(context);
            a.Estada = estadaMapper.Read(id_estada);


            return a;
        }

        protected override void SelectParameters(IDbCommand cmd, KeyValuePair<int,int> pair)
        {
            SqlParameter p = new SqlParameter("@id_extra", pair.Key);
            SqlParameter p2 = new SqlParameter("@id_estada", pair.Value);
            cmd.Parameters.Add(p);
            cmd.Parameters.Add(p2);
        }

        protected override ExtraEstada UpdateEntityID(IDbCommand cmd, ExtraEstada e)
        {
            //No Need to to anything here
            return e;
        }

        protected override void UpdateParameters(IDbCommand cmd, ExtraEstada entity)
        {
            SqlParameter p1 = new SqlParameter("@preço_dia", entity.PreçoDia);
            SqlParameter p2 = new SqlParameter("@descrição", entity.Descrição);

            SqlParameter p5 = new SqlParameter("@id_extra", entity.Extra?.Id);
            SqlParameter p6 = new SqlParameter("@id_estada", entity.Estada?.Id);


            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);

            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
        }
    }
}