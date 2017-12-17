using System.Configuration;
using ADOSI2.concrete;
using ADOSI2.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ADOSI2Tests
{
    [TestClass]
    public class ExtraCrudTests
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["si2cs"].ConnectionString;

        [TestMethod]
        public void InsertExtraTest()
        {
            using (Context ctx = new Context(connectionString))
            {
                Extra extra = new Extra();
                extra.Tipo = "alojamento";
                extra.Id = 25;
                extra.Descriçao = "muito bom";
                extra.PreçoDia = 25;

                var extraMapper = new ExtraMapper(ctx);
                extra = extraMapper.Create(extra);

                Extra extra1 = extraMapper.Read(extra.Id);

                Assert.AreEqual(extra.Id, extra1.Id);
                Assert.AreEqual(extra.Tipo, extra1.Tipo);
                Assert.AreEqual(extra.Descriçao, extra1.Descriçao);
                Assert.AreEqual(extra.PreçoDia, extra1.PreçoDia);

                foreach (var e in extraMapper.ReadAll())
                {
                    extraMapper.Delete(e);
                }

            }
        }

        [TestMethod]
        public void UpdateExtraTest()
        {
            using (Context ctx = new Context(connectionString))
            {
                Extra extra = new Extra();
                extra.Tipo = "Alojamento";
                extra.Id = 25;
                extra.Descriçao = "muito bom";
                extra.PreçoDia = 25;


                var extraMapper = new ExtraMapper(ctx);
                extra = extraMapper.Create(extra);

                var manel = "Manel";
                extra.Descriçao = manel;
                extra.PreçoDia = 21;
                extra.Tipo = "Hóspede";
                extraMapper.Update(extra);

                var extra1 = extraMapper.Read(extra.Id);
                Assert.AreEqual(extra.Id, extra1.Id);
                Assert.AreEqual(extra.Tipo, extra1.Tipo);
                Assert.AreEqual(extra.Descriçao, extra1.Descriçao);
                Assert.AreEqual(extra.Descriçao,manel );
                Assert.AreEqual(extra.PreçoDia, extra1.PreçoDia);


                foreach (var e in extraMapper.ReadAll())
                {
                    extraMapper.Delete(e);
                }
            }
        }

        [TestMethod]
        public void DeleteExtraTest()
        {
            using (Context ctx = new Context(connectionString))
            {
                Extra extra = new Extra();
                extra.Tipo = "Alojamento";
                extra.Id = 25;
                extra.Descriçao = "muito bom";
                extra.PreçoDia = 25;


                var extraMapper = new ExtraMapper(ctx);
                extra = extraMapper.Create(extra);


                foreach (var h in extraMapper.ReadAll())
                {
                    extraMapper.Delete(h);
                }
            }
        }
    }

}

