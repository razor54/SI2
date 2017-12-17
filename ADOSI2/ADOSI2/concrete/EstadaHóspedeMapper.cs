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
    public class EstadaHóspedeMapper : AbstracMapper<EstadaHóspede, KeyValuePair<int, int>, List<EstadaHóspede>>, IEstadaHóspedeMapper
    {


        public EstadaHóspedeMapper(IContext ctx) : base(ctx)
        {
        }

        protected override void UpdateParameters(IDbCommand command, EstadaHóspede e)
        {
            throw new NotSupportedException();
        }

        /*
         * NO UPDATE NECESSARY
         */
        protected override string UpdateCommandText => null;

        protected override string DeleteCommandText => "delete from EstadaHóspede where nif_hóspede=@nif_hóspede and id_estada=@id_estada";


        protected override string InsertCommandText =>
            "insert into EstadaHóspede(nif_hóspede, id_estada)" +
            " values(@nif_hóspede,@id_estada);";

        protected override string SelectAllCommandText => "select nif_hóspede, id_estada from EstadaHóspede";


        protected override string SelectCommandText => $"{SelectAllCommandText} where nif_hóspede=@nif_hóspede and id_estada=@id_estada";

       

        protected override void DeleteParameters(IDbCommand cmd, EstadaHóspede entity)
        {
            SqlParameter p1 = new SqlParameter("@id_estada", entity.Estada?.Id);
            SqlParameter p2 = new SqlParameter("@nif_hóspede", entity.Hóspede?.Nif);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
        }

        protected override void InsertParameters(IDbCommand cmd, EstadaHóspede entity)
        {
           DeleteParameters(cmd,entity);
        }

        protected override EstadaHóspede Map(IDataRecord record)
        {
            EstadaHóspede a = new EstadaHóspede();
            int nif_hóspede = Convert.ToInt32(record.GetDecimal(0));
            int id_estada = Convert.ToInt32(record.GetDecimal(1));


            HóspedeMapper extraMapper = new HóspedeMapper(context);
            a.Hóspede = extraMapper.Read(nif_hóspede);

            EstadaMapper estadaMapper = new EstadaMapper(context);
            a.Estada = estadaMapper.Read(id_estada);


            return a;
        }

        protected override void SelectParameters(IDbCommand cmd, KeyValuePair<int, int> pair)
        {
            SqlParameter p = new SqlParameter("@nif_hóspede", pair.Key);
            SqlParameter p2 = new SqlParameter("@id_estada", pair.Value);
            cmd.Parameters.Add(p);
            cmd.Parameters.Add(p2);
        }

       

        protected override EstadaHóspede UpdateEntityID(IDbCommand cmd, EstadaHóspede e)
        {
            //No Need to to anything here
            return e;
        }

    }
}
    