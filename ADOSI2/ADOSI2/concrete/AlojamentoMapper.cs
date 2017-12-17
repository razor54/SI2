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
    public class AlojamentoMapper : AbstracMapper<Alojamento, string, List<Alojamento>>, IAlojamentoMapper
    {
        #region HELPER METHODS  

        internal Parque LoadParque(Alojamento s)
        {
            ParqueMapper cm = new ParqueMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter> {new SqlParameter("@nome", s.Nome)};

            using (var rd = ExecuteReader("select nome_parque from Alojamento where nome=@nome", parameters))
            {
                string key = "";
                bool read = rd.Read();
                if (read)
                    key = rd.GetString(0);
                return cm.Read(key);
            }
        }

        #endregion

        public AlojamentoMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText => "delete from Alojamento where nome=@nome";

        protected override string InsertCommandText =>
            "insert into ALOJAMENTO(preço_base, descrição,localização,nome," +
            " nome_parque,max_pessoas) values(@preço_base,@descrição,@localização,@nome,@nome_parque,@max_pessoas); select @nome=nome from ALOJAMENTO;";

        protected override string SelectAllCommandText => "select preço_base, descrição,localização," +
                                                          "nome, nome_parque, max_pessoas from Alojamento";

        protected override string SelectCommandText => $"{SelectAllCommandText} where nome=@nome";

        protected override string UpdateCommandText =>
            "update Alojamento set preço_base=@preço_base, descrição=@descrição,localização=@localização," +
            " nome_parque=@nome_parque,max_pessoas=@max_pessoas where nome=@nome";

        protected override void DeleteParameters(IDbCommand cmd, Alojamento entity)
        {
            SqlParameter p1 = new SqlParameter("@nome", entity.Nome);
            cmd.Parameters.Add(p1);
        }

        protected override void InsertParameters(IDbCommand cmd, Alojamento entity)
        {
            UpdateParameters(cmd, entity);
        }

        protected override Alojamento Map(IDataRecord record)
        {
            Alojamento a = new Alojamento();
            a.PreçoBase = record.GetDecimal(0);
            a.Descrição = record.GetString(1);
            a.Localizaçao = record.GetString(2);
            a.Nome = record.GetString(3);
            //a.nomeparque --- 3
            a.MaxPessoas = Convert.ToInt32(record.GetDecimal(5));

            //TODO

            //a.Parque = ???

            return new AlojamentoProxy(a, context);
        }

        protected override void SelectParameters(IDbCommand cmd, string nome)
        {
            SqlParameter p = new SqlParameter("@nome", nome);
            cmd.Parameters.Add(p);
        }

        protected override Alojamento UpdateEntityID(IDbCommand cmd, Alojamento e)
        {
            var param = cmd.Parameters["@nome"] as SqlParameter;
            e.Nome = param.Value.ToString();
            return e;
        }

        protected override void UpdateParameters(IDbCommand cmd, Alojamento entity)
        {
            SqlParameter p1 = new SqlParameter("@preço_base", entity.PreçoBase);
            SqlParameter p2 = new SqlParameter("@descrição", entity.Descrição);
            SqlParameter p3 = new SqlParameter("@localização", entity.Localizaçao);

            SqlParameter p4 = new SqlParameter("@nome", entity.Nome);

            SqlParameter p5 = new SqlParameter("@nome_parque", entity.Parque?.Nome);

            SqlParameter p6 = new SqlParameter("@max_pessoas", entity.MaxPessoas);

            p1.Direction = ParameterDirection.InputOutput;

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
        }
    }
}
