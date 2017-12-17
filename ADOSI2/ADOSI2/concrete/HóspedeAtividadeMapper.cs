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
    public class HóspedeAtividadeMapper : AbstracMapper<HóspedeAtividade, string, List<HóspedeAtividade>>, IHóspedeAtividadeMapper
    {
        #region HELPER METHODS  

        internal Hóspede LoadHóspede(HóspedeAtividade s)
        {
            HóspedeMapper cm = new HóspedeMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter> {new SqlParameter("@nome", s.Nome_Atividade)};

            using (var rd = ExecuteReader("select nif_hóspede from HóspedeAtividade where nome_atividade=@nome", parameters))
            {
                int key=0;
                bool read = rd.Read();
                if (read)
                    key = Convert.ToInt32(rd.GetDecimal(0));
                return cm.Read(key);
            }
        }

        #endregion

        public HóspedeAtividadeMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText => "delete from HóspedeAtividade where nome_atividade=@nome_atividade";

        protected override string InsertCommandText =>
            "insert into HóspedeAtividade(nif_hóspede, nome_atividade,nome_parque) values(@nif_hóspede,@nome_atividade,@nome_parque);";

        protected override string SelectAllCommandText => "select nif_hóspede, nome_atividade,nome_parque from HóspedeAtividade";

        protected override string SelectCommandText => $"{SelectAllCommandText} where nome_atividade=@nome_atividade";

        protected override string UpdateCommandText => throw new NotSupportedException("No Update possible Here");

        protected override void DeleteParameters(IDbCommand cmd, HóspedeAtividade entity)
        {
            SqlParameter p1 = new SqlParameter("@nome_atividade", entity.Nome_Atividade);
            cmd.Parameters.Add(p1);
        }

        protected override void InsertParameters(IDbCommand cmd, HóspedeAtividade entity)
        {
            UpdateParameters(cmd, entity);
        }

        protected override HóspedeAtividade Map(IDataRecord record)
        {
            HóspedeAtividade a = new HóspedeAtividade();
            a.Nome_Atividade = record.GetString(1);
            a.Nome_Parque = record.GetString(2);
           
            return new HóspedeAtividadeProxy(a, context);
        }

        protected override void SelectParameters(IDbCommand cmd, string nome)
        {
            SqlParameter p = new SqlParameter("@nome_atividade", nome);
            cmd.Parameters.Add(p);
        }

        protected override HóspedeAtividade UpdateEntityID(IDbCommand cmd, HóspedeAtividade e)
        {
           //Nothing to do HERE
            return e;
        }

        protected override void UpdateParameters(IDbCommand cmd, HóspedeAtividade entity)
        {
            SqlParameter p1 = new SqlParameter("@nome_atividade", entity.Nome_Atividade);
            SqlParameter p2 = new SqlParameter("@nome_parque", entity.Nome_Parque);
            SqlParameter p3 = new SqlParameter("@nif_hóspede", entity.Hóspede.Nif);

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
           
        }
    }
}