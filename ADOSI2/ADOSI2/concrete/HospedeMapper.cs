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
    class HospedeMapper : AbstracMapper<Hospede, int?, List<Hospede>>, IHospedeMapper
    {


        public HospedeMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText
        {
            get
            {
                return "delete from Hospede where nif=@nif";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "insert into Hóspede(nif, bi, nome, morada, email)"+

                        "values(@nif, @bi, @nome, @morada, @email)";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select nif, bi, nome, morada,email from Estada";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0}  where nif=@nif", SelectAllCommandText);
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                //DAR UPDATE AO BI OU NIF??? SUPOSTAMENTE NAO DEVE MUDAR ISSO NÉ???
                return "update Hóspede"+
                    "set bi = @bi, nome = @nome, morada = @morada,"+
                    "email = @email where nif=@nif";
            }
        }

        protected override void DeleteParameters(IDbCommand cmd, Hospede e)
        {
            SelectParameters(cmd, e.Bi);
        }

        protected override void InsertParameters(IDbCommand cmd, Hospede e)
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

        protected override Hospede Map(IDataRecord record)
        {
            Hospede c = new Hospede();
            c.Nif = record.GetInt32(0);
            c.Bi = record.GetInt32(1);
            c.Nome = record.GetString(2);
            c.Morada = record.GetString(3);
            c.Email = record.GetString(4);
           
            
            return c;
        }

        public override Hospede Create(Hospede entity)
        {
            return base.Create(entity);
        }

        public override Hospede Update(Hospede entity)
        {
            return base.Update(entity);
        }

        protected override void SelectParameters(IDbCommand cmd, int? k)
        {
            SqlParameter p1 = new SqlParameter("@nif", k);
            cmd.Parameters.Add(p1);
        }

        protected override Hospede UpdateEntityID(IDbCommand cmd, Hospede e)
        {
            var param = cmd.Parameters["@nif"] as SqlParameter;
            e.Nif = int.Parse(param.Value.ToString());
            return e;

        }

        protected override void UpdateParameters(IDbCommand command, Hospede e)
        {
            InsertParameters(command, e);
        }

        public Hospede Read(string id)
        {
            throw new NotImplementedException();
        }
    }
}