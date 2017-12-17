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
    public class AtividadeMapper : AbstracMapper<Atividade, int, List<Atividade>>, IAtividadeMapper
    {
        #region HELPER METHODS  

        internal Parque LoadParque(Atividade s)
        {
            ParqueMapper cm = new ParqueMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter> {new SqlParameter("@número", s.Número)};

            using (var rd = ExecuteReader("select nome_parque from Atividade where número=@número", parameters))
            {
                string key = "";
                bool read = rd.Read();
                if (read)
                    key = rd.GetString(0);
                return cm.Read(key);
            }
        }

        #endregion

        public AtividadeMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText => "delete from Atividade where número=@número";

        protected override string InsertCommandText =>
            "insert into ATIVIDADE( data_atividade,preço,lotação," +
            " nome_atividade,nome_parque,descrição) values( @data_atividade,@preço,@lotação,@nome_atividade,@nome_parque,@descrição ); select @número=número from Atividade where nome_atividade=@nome_atividade;";

        protected override string SelectAllCommandText => "select número, data_atividade,preço,lotação," +
                                                          " nome_atividade,nome_parque,descrição from ATIVIDADE";

        protected override string SelectCommandText => $"{SelectAllCommandText} where número=@número";

        protected override string UpdateCommandText =>
            "update ATIVIDADE set preço=@preço, descrição=@descrição,data_atividade=@data_atividade," +
            " lotação=@lotação where número=@número";

        protected override void DeleteParameters(IDbCommand cmd, Atividade entity)
        {
            SqlParameter p1 = new SqlParameter("@número", entity.Número);
            cmd.Parameters.Add(p1);
        }

        protected override void InsertParameters(IDbCommand cmd, Atividade entity)
        {
            UpdateParameters(cmd, entity);
        }

        protected override Atividade Map(IDataRecord record)
        {
            Atividade a = new Atividade();
            a.Número = Convert.ToInt32(record.GetDecimal(0));
            a.DataAtividade = record.GetDateTime(1);
            a.Preço = record.GetDecimal(2);
            a.Lotaçao = Convert.ToInt32( record.GetDecimal(3));
            a.NomeAtividade = record.GetString(4);
            a.Descrição = record.GetString(6);

            return new AtividadeProxy(a, context);
        }

        protected override void SelectParameters(IDbCommand cmd, int número)
        {
            SqlParameter p = new SqlParameter("@número", número);
            cmd.Parameters.Add(p);
        }

        protected override Atividade UpdateEntityID(IDbCommand cmd, Atividade e)
        {
            var param = cmd.Parameters["@número"] as SqlParameter;
            e.Número = Convert.ToInt32(param.Value.ToString());
            return e;
        }

        protected override void UpdateParameters(IDbCommand cmd, Atividade entity)
        {
            SqlParameter p1 = new SqlParameter("@preço", entity.Preço);
            SqlParameter p2 = new SqlParameter("@descrição", entity.Descrição);
            SqlParameter p3 = new SqlParameter("@data_atividade", entity.DataAtividade);
            SqlParameter p4 = new SqlParameter("@nome_atividade", entity.NomeAtividade);

            SqlParameter p5 = new SqlParameter("@nome_parque", entity.Parque?.Nome);
            SqlParameter p6 = new SqlParameter("@número", entity.Número);
            SqlParameter p7 = new SqlParameter("@lotação", entity.Lotaçao);
            p6.Direction = ParameterDirection.InputOutput;

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            cmd.Parameters.Add(p7);
        }
    }
}