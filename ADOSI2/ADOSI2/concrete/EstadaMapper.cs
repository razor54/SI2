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
    class EstadaMapper : AbstracMapper<Estada, int?, List<Estada>>, IEstadaMapper
    {
       
       
        public EstadaMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText
        {
            get
            {
                return "delete from Estada where id=@id";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Estada(id, data_início, data_fim, nif_hóspede) values (@id,@data_início, @data_fim, @nif_hóspede)";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select id, data_início, data_fim, nif_hóspede from Estada";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0}  where id=@id", SelectAllCommandText);
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update Estada set id=@id, data_início=@data_início, data_fim=@data_fim, nif_hóspede=@nif_hóspede where courseId=@id";
            }
        }

        protected override void DeleteParameters(IDbCommand cmd, Estada e)
        {
            SelectParameters(cmd, e.Id);
        }

        protected override void InsertParameters(IDbCommand cmd, Estada e)
        {
            SqlParameter p1 = new SqlParameter("@id",e.Id);
            SqlParameter p2 = new SqlParameter("@data_início", e.DataInicio);
            SqlParameter p3 = new SqlParameter("@data_fim", e.DataFim);
            SqlParameter p4 = new SqlParameter("@nif_hóspede", e.NifHospede);


            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
        }

        protected override Estada Map(IDataRecord record)
        {
            Estada c = new Estada();
            c.Id = record.GetInt32(0);
            //DATE TIME???
            c.DataInicio = record.GetDateTime(1);
            c.DataFim = record.GetDateTime(2);
            return c;
        }

        public override Estada Create(Estada entity)
        {
            return base.Create(entity);
        }

        public override Estada Update(Estada entity)
        {
            return base.Update(entity);
        }

        protected override void SelectParameters(IDbCommand cmd, int? k)
        {
            SqlParameter p1 = new SqlParameter("@id", k);
            cmd.Parameters.Add(p1);
        }

        protected override Estada UpdateEntityID(IDbCommand cmd, Estada e)
        {
            var param = cmd.Parameters["@id"] as SqlParameter;
            e.Id = int.Parse(param.Value.ToString());
            return e;

        }

        protected override void UpdateParameters(IDbCommand command, Estada e)
        {
            InsertParameters(command, e);
        }
    }
}