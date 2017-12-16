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
    public class FaturaMapper : AbstracMapper<Fatura, int?, List<Fatura>>, IFaturaMapper
    {
        #region HELPER METHODS  

        internal Hóspede LoadHóspede(Fatura s)
        {
            HóspedeMapper cm = new HóspedeMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter> { new SqlParameter("@id", s.Id) };

            using (var rd = ExecuteReader("select nif_hóspede from Fatura where id=@id", parameters))
            {
                int key = 0;
                bool read = rd.Read();
                if (read)
                    key = Convert.ToInt32(rd.GetDecimal(0));
                return cm.Read(key);
            }
        }

        internal Estada LoadEstada(Fatura s)
        {
            EstadaMapper cm = new EstadaMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter> { new SqlParameter("@id", s.Id) };

            using (var rd = ExecuteReader("select id_estada from Fatura where id=@id", parameters))
            {
                int key = 0;
                bool read = rd.Read();
                if (read)
                    key = Convert.ToInt32(rd.GetDecimal(0));
                return cm.Read(key);
            }
        }

        #endregion

        public FaturaMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText => "delete from Fatura where id=@id";

        protected override string InsertCommandText => "INSERT INTO Fatura(id, id_estada, nome_hóspede, nif_hóspede,valor_final) values (@id,@id_estada, @nome_hóspede, @nif_hóspede,@valor_final)";

        protected override string SelectAllCommandText => "select id, id_estada, nome_hóspede, nif_hóspede,valor_final from Fatura ";

        protected override string SelectCommandText => $"{SelectAllCommandText}  where id=@id";

        protected override string UpdateCommandText => "update Fatura set valor_final=@valor_final where id=@id";

        protected override void DeleteParameters(IDbCommand cmd, Fatura e)
        {
            SelectParameters(cmd, e.Id);
        }

        protected override void InsertParameters(IDbCommand cmd, Fatura e)
        {
            SqlParameter p1 = new SqlParameter("@id", e.Id);
            SqlParameter p2 = new SqlParameter("@id_estada", e.Estada?.Id);
            SqlParameter p3 = new SqlParameter("@nome_hóspede", e.Hóspede?.Nome);
            SqlParameter p4 = new SqlParameter("@nif_hóspede", e.Hóspede?.Nif);
            SqlParameter p5 = new SqlParameter("@valor_final", e.ValorFinal);


            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
        }

        protected override Fatura Map(IDataRecord record)
        {
            FaturaProxy c = new FaturaProxy(new Fatura(), context);
            c.Id = Convert.ToInt32(record.GetDecimal(0));
            c.ValorFinal = record.GetDecimal(4);

            return c;
        }

        protected override void SelectParameters(IDbCommand cmd, int? k)
        {
            SqlParameter p1 = new SqlParameter("@id", k);
            cmd.Parameters.Add(p1);
        }

        protected override Fatura UpdateEntityID(IDbCommand cmd, Fatura e)
        {
            var param = cmd.Parameters["@id"] as SqlParameter;
            e.Id = int.Parse(param.Value.ToString());
            return e;

        }

        protected override void UpdateParameters(IDbCommand command, Fatura e)
        {
            SqlParameter p1 = new SqlParameter("@valor_final", e.ValorFinal);
            SqlParameter p2 = new SqlParameter("@id", e.Id);
            command.Parameters.Add(p1);
            command.Parameters.Add(p2);
        }
    }
}