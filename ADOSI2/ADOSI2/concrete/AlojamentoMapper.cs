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
using System.Transactions;

namespace ADOSI2.concrete
{
    //class AlojamentoMapper : AbstracMapper<Alojamento,string,List<Alojamento>>,IAlojamentoMapper
    //{
    //    #region HELPER METHODS  
    //    internal List<Course> LoadCourses(Student s)
    //    {
    //        List<Course> lst = new List<Course>();

    //        CourseMapper cm = new CourseMapper(context);
    //        List<IDataParameter> parameters = new List<IDataParameter>();
    //        parameters.Add(new SqlParameter("@id", s.Number));
    //        using (IDataReader rd = ExecuteReader("select courseid from studentcourse where studentId=@id", parameters))
    //        {
    //            while (rd.Read())
    //            {
    //                int key = rd.GetInt32(0);
    //                lst.Add(cm.Read(key));
    //            }
    //        }
    //        return lst;
    //    }

    //    internal Country LoadCountry(Student s)
    //    {
    //        CountryMapper cm = new CountryMapper(context);
    //        List<IDataParameter> parameters = new List<IDataParameter>();
    //        parameters.Add(new SqlParameter("@id", s.Number));
    //        using (IDataReader rd = ExecuteReader("select country from student where studentNumber=@id", parameters))
    //        {
    //            if (rd.Read())
    //            {
    //                int key = rd.GetInt32(0);
    //                return cm.Read(key);
    //            }
    //        }
    //        return null;

    //    }

    //    #endregion
    //    public AlojamentoMapper(IContext ctx) : base(ctx) { }

    //    public override Alojamento Create(Alojamento entity)
    //    {
    //        using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
    //        {
    //            EnsureContext();
    //            context.EnlistTransaction();

    //            using (IDbCommand cmd = context.createCommand())
    //            {
    //                cmd.CommandText = InsertCommandText;
    //                cmd.CommandType = InsertCommandType;
    //                InsertParameters(cmd, entity);
    //                cmd.ExecuteNonQuery();
    //                entity = UpdateEntityID(cmd, entity);
    //            }
    //            if (entity != null && entity.EnrolledCourses != null && entity.EnrolledCourses.Count > 0)
    //            {
    //                SqlParameter p = new SqlParameter("@courseId", SqlDbType.Int);
    //                SqlParameter p1 = new SqlParameter("@studentId", entity.Number);

    //                List<IDataParameter> parameters = new List<IDataParameter>();
    //                parameters.Add(p);
    //                parameters.Add(p1);
    //                /*
    //                foreach (var course in entity.EnrolledCourses)
    //                {
    //                    p.Value = course.Id;
    //                    ExecuteNonQuery("INSERT INTO StudentCourse (studentId,courseId) values(@studentId,@courseId)", parameters);
    //                }
    //                */
    //            }
    //            ts.Complete();
    //            return entity;
    //        }

    //    }



    //    public override Alojamento Delete(Alojamento entity)
    //    {
    //        if (entity == null)
    //            throw new ArgumentException("The " + typeof(Alojamento) + " to delete cannot be null");

    //        using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
    //        {
    //            EnsureContext();
    //            context.EnlistTransaction();
    //            /*
    //            if (entity.EnrolledCourses != null && entity.EnrolledCourses.Count > 0)
    //            {
    //                SqlParameter p = new SqlParameter("@nome", entity.Nome);

    //                List<IDataParameter> parameters = new List<IDataParameter>();
    //                parameters.Add(p);
    //                //DELETE DAS ASSOCIAÇOES
    //               // ExecuteNonQuery("delete from StudentCourse where studentId=@studentId", parameters);
    //            }
    //            */
    //            Alojamento del = base.Delete(entity);
    //            ts.Complete();
    //            return del;
    //        }
    //    }
    //    protected override string DeleteCommandText
    //    {
    //        get
    //        {
    //            return "delete from Alojamento where nome=@nome";
    //        }
    //    }

    //    protected override string InsertCommandText
    //    {
    //        get
    //        {
    //            return "insert into ALOJAMENTO(preço_base, descrição,localização,nome," +
    //                " nome_parque,max_pessoas) values(@preço_base,@descrição,@localização,@nome,@nome_parque,@max_pessoas); select @nome=nome from ALOJAMENTO;";
    //        }
    //    }

    //    protected override string SelectAllCommandText
    //    {
    //        get
    //        {
    //            return "select preço_base, descrição,localização," +
    //                "nome, nome_parque, max_pessoas, from Alojamento";
    //        }
    //    }

    //    protected override string SelectCommandText
    //    {
    //        get
    //        {
    //            return String.Format("{0} where nome=@nome", SelectAllCommandText);
    //        }
    //    }

    //    protected override string UpdateCommandText
    //    {
    //        get
    //        {
    //            return "update Alojamento set preço_base=@preço_base, descrição=@descrição,localização=@localizaçao," +
    //                " nome_parque=@nome_parque,max_pessoas=@max_pessoas where nome=@nome";
    //        }
    //    }

    //    protected override void DeleteParameters(IDbCommand cmd, Alojamento entity)
    //    {
    //        SqlParameter p1 = new SqlParameter("@nome", entity.Nome);
    //        cmd.Parameters.Add(p1);
    //    }

    //    protected override void InsertParameters(IDbCommand cmd, Alojamento entity)
    //    {
    //        UpdateParameters(cmd, entity);
    //    }

    //    protected override Alojamento Map(IDataRecord record)
    //    {
    //        Alojamento a = new Alojamento();
    //        a.PreçoBase = record.GetInt32(0);
    //        a.Nome = record.GetString(1);
            
    //        a.Descrição = record.GetString(2);
    //        //TODO

    //        //a.Parque = ???
    //        a.PreçoBase = record.GetInt32(4);//verify
    //        return new AlojamentoProxy(a, context);
    //    }

    //    protected override void SelectParameters(IDbCommand cmd, string nome)
    //    {
    //        SqlParameter p = new SqlParameter("@nome", nome);
    //        cmd.Parameters.Add(p);
    //    }

    //    protected override Alojamento UpdateEntityID(IDbCommand cmd, Alojamento e)
    //    {
    //        var param = cmd.Parameters["@nome"] as SqlParameter;
    //        e.Nome = param.Value.ToString();
    //        return e;
    //    }

    //    protected override void UpdateParameters(IDbCommand cmd, Alojamento entity)
    //    {
    //        SqlParameter p1 = new SqlParameter("@preço_base", entity.PreçoBase);
    //        SqlParameter p2 = new SqlParameter("@descrição", entity.Descrição);
    //        SqlParameter p3 = new SqlParameter("@localização", entity.Localizaçao);

    //        SqlParameter p4 = new SqlParameter("@nome", entity.Nome);
            
    //        SqlParameter p5 = new SqlParameter("@nome_parque", entity.Parque == null ? null : entity.Parque.Nome);

    //        SqlParameter p6 = new SqlParameter("@max_pessoas", entity.MaxPessoas);

    //        p1.Direction = ParameterDirection.InputOutput;

    //        cmd.Parameters.Add(p1);
    //        cmd.Parameters.Add(p2);
    //        cmd.Parameters.Add(p3);
    //        cmd.Parameters.Add(p4);
    //        cmd.Parameters.Add(p5);
    //        cmd.Parameters.Add(p6);
    //    }

    //}
}
*/