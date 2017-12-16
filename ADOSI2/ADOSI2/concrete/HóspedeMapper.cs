using ADOSI2.dal;
using ADOSI2.mapper;
using ADOSI2.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.concrete
{
    public class HóspedeMapper : AbstracMapper<Hóspede, int?, List<Hóspede>>, IHospedeMapper
    {
        public HóspedeMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText => "delete from Hóspede where nif = @nif";

        protected override string InsertCommandText => "insert into Hóspede(nif, bi, nome, morada, email)"+

                                                       "values(@nif, @bi, @nome, @morada, @email)";

        protected override string SelectAllCommandText => "select nif, bi, nome, morada,email from Hóspede";

        protected override string SelectCommandText => $"{SelectAllCommandText}  where nif=@nif";

        protected override string UpdateCommandText => "update Hóspede " +
                                                       "set nome = @nome, morada = @morada, " +
                                                       "email = @email where nif=@nif";

        protected override void DeleteParameters(IDbCommand cmd, Hóspede e)
        {
            SelectParameters(cmd, e.Nif);
        }

        protected override void InsertParameters(IDbCommand cmd, Hóspede e)
        {

            SqlParameter p1 = new SqlParameter("@nif", e.Nif);
            SqlParameter p2 = new SqlParameter("@bi", e.Bi);
            SqlParameter p3 = new SqlParameter("@nome", e.Nome);
            SqlParameter p4 = new SqlParameter("@morada", e.Morada);
            SqlParameter p5 = new SqlParameter("@email", e.Email);

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
        }

        protected override Hóspede Map(IDataRecord record)
        {
            Hóspede c = new Hóspede();
            c.Nif = Convert.ToInt32(record.GetDecimal(0));
            c.Bi = Convert.ToInt32(record.GetDecimal(1));
            c.Nome = record.GetString(2);
            c.Morada = record.GetString(3);
            c.Email = record.GetString(4);

            return c;
        }

        protected override void SelectParameters(IDbCommand cmd, int? k)
        {
            SqlParameter p1 = new SqlParameter("@nif", k);
            cmd.Parameters.Add(p1);
        }

        protected override Hóspede UpdateEntityID(IDbCommand cmd, Hóspede e)
        {
            var param = cmd.Parameters["@nif"] as SqlParameter;
            e.Nif = int.Parse(param.Value.ToString());
            return e;

        }

        protected override void UpdateParameters(IDbCommand command, Hóspede e)
        {
            InsertParameters(command, e);
        }
    }
}