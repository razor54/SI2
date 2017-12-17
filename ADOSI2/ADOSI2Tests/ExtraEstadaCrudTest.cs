using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.concrete;
using ADOSI2.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ADOSI2Tests
{
    [TestClass]
    public class ExtraEstadaCrudTest
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["si2cs"].ConnectionString;

        [TestMethod]
        public void InsertExtraTest()
        {
            using (Context ctx = new Context(connectionString))
            {
                var extra = new Extra
                {
                    Tipo = "alojamento",
                    Id = 25,
                    Descriçao = "muito bom",
                    PreçoDia = 25
                };

                var extraMapper = new ExtraMapper(ctx);
                extra = extraMapper.Create(extra);

                var estada = new Estada
                {
                    DataInicio = new DateTime(2007, 3, 1),
                    DataFim = new DateTime(2017, 3, 1),
                    Id = 25,
                    NifHospede = 0
                };

                //TODO

                EstadaMapper estadaMapper = new EstadaMapper(ctx);
                estada = estadaMapper.Create(estada);

                ExtraEstada extraEstada = new ExtraEstada
                {
                    PreçoDia = 12,
                    Descrição = "Pessimo",
                    Estada = estada,
                    Extra = extra
                };

                var extraEstadaMapper = new ExtraEstadaMapper(ctx);

                extraEstada = extraEstadaMapper.Create(extraEstada);

                var ee = extraEstadaMapper.Read(new KeyValuePair<int, int>(extraEstada.Extra.Id, extraEstada.Estada.Id));

                Assert.IsNotNull(ee);

                Assert.AreEqual(ee.Extra.Id,extraEstada.Extra.Id);
                Assert.AreEqual(ee.Estada.Id,extraEstada.Estada.Id);

                Assert.AreEqual(ee.PreçoDia,extraEstada.PreçoDia);
                Assert.AreEqual(ee.Descrição,extraEstada.Descrição);




                foreach (var e in extraEstadaMapper.ReadAll())
                {
                    extraEstadaMapper.Delete(e);
                }

                foreach (var e in estadaMapper.ReadAll())
                { 
                    estadaMapper.Delete(estada);
                }
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
                var extra = new Extra
                {
                    Tipo = "alojamento",
                    Id = 25,
                    Descriçao = "muito bom",
                    PreçoDia = 25
                };

                var extraMapper = new ExtraMapper(ctx);
                extra = extraMapper.Create(extra);

                var estada = new Estada
                {
                    DataInicio = new DateTime(2007, 3, 1),
                    DataFim = new DateTime(2017, 3, 1),
                    Id = 25,
                    NifHospede = 0
                };

                //TODO

                EstadaMapper estadaMapper = new EstadaMapper(ctx);
                estada = estadaMapper.Create(estada);

                ExtraEstada extraEstada = new ExtraEstada
                {
                    PreçoDia = 12,
                    Descrição = "Pessimo",
                    Estada = estada,
                    Extra = extra
                };

                var extraEstadaMapper = new ExtraEstadaMapper(ctx);

                extraEstada = extraEstadaMapper.Create(extraEstada);

                var ee = extraEstadaMapper.Read(new KeyValuePair<int, int>(extraEstada.Extra.Id, extraEstada.Estada.Id));

                Assert.IsNotNull(ee);

                Assert.AreEqual(ee.Extra.Id, extraEstada.Extra.Id);
                Assert.AreEqual(ee.Estada.Id, extraEstada.Estada.Id);

                Assert.AreEqual(ee.PreçoDia, extraEstada.PreçoDia);
                Assert.AreEqual(ee.Descrição, extraEstada.Descrição);

                var extraEstadaPreçoDia = 32;

                extraEstada.PreçoDia = extraEstadaPreçoDia;
                var s = extraEstada.Descrição = "Nada de mais";

                ee = extraEstadaMapper.Update(extraEstada);

                Assert.AreEqual(ee.Extra.Id, extraEstada.Extra.Id);
                Assert.AreEqual(ee.Estada.Id, extraEstada.Estada.Id);

                Assert.AreEqual(ee.PreçoDia, extraEstada.PreçoDia);
                Assert.AreEqual(ee.Descrição, extraEstada.Descrição);

                Assert.AreEqual(ee.PreçoDia, extraEstadaPreçoDia);
                Assert.AreEqual(ee.Descrição, s);





                foreach (var e in extraEstadaMapper.ReadAll())
                {
                    extraEstadaMapper.Delete(e);
                }

                foreach (var e in estadaMapper.ReadAll())
                {
                    estadaMapper.Delete(estada);
                }
                foreach (var e in extraMapper.ReadAll())
                {
                    extraMapper.Delete(e);
                }



            }
        }
    }
}
