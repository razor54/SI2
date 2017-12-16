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
    public class ExtraMapper : AbstracMapper<Extra, int, List<Extra>>, IExtraMapper
    {


        public ExtraMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText => "delete from Extra where id=@id";

        protected override string InsertCommandText => "INSERT INTO Extra(id, descrição,preço_dia, tipo) values (@id,@descrição, @preço_dia, @tipo)";

        protected override string SelectAllCommandText => "select id, descrição, preço_dia, tipo from Extra ";

        protected override string SelectCommandText => $"{SelectAllCommandText}  where id=@id";

        protected override string UpdateCommandText => "update Extra set descrição=@descrição, preço_dia=@preço_dia, tipo=@tipo where id=@id";

        protected override void DeleteParameters(IDbCommand cmd, Extra e)
        {
            SelectParameters(cmd, e.Id);
        }

        protected override void InsertParameters(IDbCommand cmd, Extra e)
        {
            SqlParameter p1 = new SqlParameter("@id", e.Id);
            SqlParameter p2 = new SqlParameter("@descrição", e.Descriçao);
            SqlParameter p3 = new SqlParameter("@preço_dia", e.PreçoDia);
            SqlParameter p4 = new SqlParameter("@tipo", e.Tipo);


            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
        }

        protected override Extra Map(IDataRecord record)
        {
            Extra c = new Extra();
            c.Id = Convert.ToInt32(record.GetDecimal(0));
            c.Descriçao = record.GetString(1);
            c.PreçoDia = record.GetDecimal(2);
            c.Tipo = record.GetString(3);
           
            return c;
        }

        protected override void SelectParameters(IDbCommand cmd, int k)
        {
            SqlParameter p1 = new SqlParameter("@id", k);
            cmd.Parameters.Add(p1);
        }

        protected override Extra UpdateEntityID(IDbCommand cmd, Extra e)
        {
            var param = cmd.Parameters["@id"] as SqlParameter;
            e.Id = int.Parse(param.Value.ToString());
            return e;

        }

        protected override void UpdateParameters(IDbCommand command, Extra e)
        {
            InsertParameters(command, e);
        }
    }
}