using ADOSI2.dal;
using ADOSI2.mapper;
using ADOSI2.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ADOSI2.concrete
{

    class ParqueMapper : AbstracMapper<Parque, string, List<Parque>>, IParqueMapper
    {


        public ParqueMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText => "delete from Parque where nome=@nome";

        protected override string InsertCommandText => "insert into Parque(email, nome, morada, estrelas)" +

                                                       "values(@email, @nome, @morada, @estrelas)";

        protected override string SelectAllCommandText => "select email, nome, morada, estrelas from Parque";

        protected override string SelectCommandText => $"{SelectAllCommandText}  where nome=@nome";

        protected override string UpdateCommandText => "update Parque " +
                                                       "set nome=@nome,morada = @morada," +
                                                       "email = @email, estrelas = @estrelas where nome=@nome";

        protected override void DeleteParameters(IDbCommand cmd, Parque e)
        {
            SelectParameters(cmd, e.Nome);
        }

        protected override void InsertParameters(IDbCommand cmd, Parque e)
        {

            
            SqlParameter p1 = new SqlParameter("@email", e.Email);
            SqlParameter p2 = new SqlParameter("@nome", e.Nome);
            SqlParameter p3 = new SqlParameter("@morada", e.Morada);
            SqlParameter p4 = new SqlParameter("@estrelas", e.Estrelas);

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
        }

        protected override Parque Map(IDataRecord record)
        {
            Parque c = new Parque();
            c.Email = record.GetString(0);
            c.Nome = record.GetString(1);
            c.Morada = record.GetString(2);
            //NUMERIC NOT INT ???
            var estrelas =record.GetDecimal(3);

            c.Estrelas = Convert.ToInt32(estrelas);


            return c;
        }


        protected override void SelectParameters(IDbCommand cmd, string k)
        {
            SqlParameter p1 = new SqlParameter("@nome", k);
            cmd.Parameters.Add(p1);
        }

        protected override Parque UpdateEntityID(IDbCommand cmd, Parque e)
        {
            var param = cmd.Parameters["@nome"] as SqlParameter;
            e.Nome = param.Value.ToString();
            return e;

        }

        protected override void UpdateParameters(IDbCommand command, Parque e)
        {
            InsertParameters(command, e);
        }

    }
}