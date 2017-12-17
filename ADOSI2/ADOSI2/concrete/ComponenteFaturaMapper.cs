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
    public class ComponenteFaturaMapper : AbstracMapper<ComponenteFatura, int, List<ComponenteFatura>>,
        IComponenteFaturaMapper
    {
        #region HELPER METHODS  

        internal Fatura LoadFatura(ComponenteFatura s)
        {
            FaturaMapper faturaMapper = new FaturaMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter> {new SqlParameter("@id", s.Id)};

            using (var rd = ExecuteReader("select id_fatura from ComponenteFatura where id=@id", parameters))
            {
                int key = 0;
                bool read = rd.Read();
                if (read)
                    key = Convert.ToInt32(rd.GetDecimal(0));
                return faturaMapper.Read(key);
            }
        }

        #endregion

        public ComponenteFaturaMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText => "delete from ComponenteFatura where id=@id";

        protected override string InsertCommandText =>
            "insert into ComponenteFatura(id_fatura, descrição,preço,tipo) " +
            " values(@id_fatura,@descrição,@preço,@tipo); select @id=id from ComponenteFatura where id_fatura=@id_fatura";


        protected override ComponenteFatura UpdateEntityID(IDbCommand cmd, ComponenteFatura e)
        {
            //NOTING NEEDED HERE
            return e;
        }

        protected override string SelectAllCommandText =>
            "select id ,id_fatura, descrição,preço,tipo from ComponenteFatura";

        protected override string SelectCommandText => $"{SelectAllCommandText} where id=@id";

        protected override string UpdateCommandText =>
            "update ComponenteFatura set  descrição = @descrição ,preço = @preço,tipo=@tipo" +
            "  where id=@id";

        protected override void DeleteParameters(IDbCommand cmd, ComponenteFatura entity)
        {
            SqlParameter p1 = new SqlParameter("@id", entity.Id);
            cmd.Parameters.Add(p1);
        }

        protected override void InsertParameters(IDbCommand cmd, ComponenteFatura entity)
        {
            UpdateParameters(cmd, entity);
        }

        public override ComponenteFatura Create(ComponenteFatura entity)
        {
            EnsureContext();
            using (IDbCommand cmd = context.CreateCommand())
            {
                cmd.CommandText = InsertCommandText;
                cmd.CommandType = InsertCommandType;
                InsertParameters(cmd, entity);


                cmd.ExecuteNonQuery();

                using (var rd = ExecuteReader("select id from ComponenteFatura where id_fatura=@id_fatura",
                    new List<IDataParameter>()
                    {
                        new SqlParameter("id_fatura", entity.Fatura?.Id)
                    }))
                {
                    int key = 0;
                    bool read = rd.Read();
                    if (read)
                        key = Convert.ToInt32(rd.GetDecimal(0));
                    entity.Id = key;
                }

                cmd.Parameters.Clear();

                return entity;
            }
        }


        protected override ComponenteFatura Map(IDataRecord record)
        {
            ComponenteFatura a = new ComponenteFatura();
            a.Id = Convert.ToInt32(record.GetDecimal(0));
            //id_estada ---1
            a.Descrição = record.GetString(2);
            a.Preço = record.GetDecimal(3);
            a.Tipo = record.GetString(4);

            return new ComponenteFaturaProxy(a, context);
        }

        protected override void SelectParameters(IDbCommand cmd, int id)
        {
            SqlParameter p = new SqlParameter("@id", id);
            cmd.Parameters.Add(p);
        }

       


        protected override void UpdateParameters(IDbCommand cmd, ComponenteFatura entity)
        {
            SqlParameter p1 = new SqlParameter("@id_fatura", entity.Fatura?.Id);
            SqlParameter p2 = new SqlParameter("@descrição", entity.Descrição);

            SqlParameter p3 = new SqlParameter("@preço", entity.Preço);
            SqlParameter p4 = new SqlParameter("@tipo", entity.Tipo);
            SqlParameter p5 = new SqlParameter("@id", entity.Id);

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
        }
    }
}