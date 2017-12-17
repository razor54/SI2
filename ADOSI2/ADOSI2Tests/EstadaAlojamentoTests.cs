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
    public class EstadaAlojamentoTests
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["si2cs"].ConnectionString;

        [TestMethod]
        public void InsertTest()
        {
            using (Context ctx = new Context(_connectionString))
            {

                var estada = new Estada
                {
                    DataInicio = new DateTime(2007, 3, 1),
                    DataFim = new DateTime(2017, 3, 1),
                    Id = 25,
                    NifHospede = 0
                };

                EstadaMapper estadaMapper = new EstadaMapper(ctx);
                estada = estadaMapper.Create(estada);


                /*
                * CREATE PARQUE
                */
                Parque p = new Parque();
                p.Nome = "brasil";
                p.Email = "brasil@brasil.com";
                p.Morada = "Rio de Janeiro, Rua Junqueiro 367";
                p.Estrelas = 5;

                ParqueMapper parqueMap = new ParqueMapper(ctx);
                p = parqueMap.Create(p);

                /*
                 * Alojamento
                 */


                Alojamento c = new Alojamento();
                c.PreçoBase = 50;
                c.Nome = "OI";
                c.Descrição = "sem descricao";
                c.Localizaçao = "Brasil";
                c.MaxPessoas = 20;
                c.Parque = p;

                AlojamentoMapper alojamentoMapper = new AlojamentoMapper(ctx);
                c = alojamentoMapper.Create(c);





                EstadaAlojamento estadaAlojamento = new EstadaAlojamento()
                {

                    Estada = estada,
                    Alojamento = c,
                    Descrição = "OLA",
                    PreçoBase = 1234

                };

                var estadaAlojamentoMapper = new EstadaAlojamentoMapper(ctx);

                estadaAlojamento = estadaAlojamentoMapper.Create(estadaAlojamento);

                var ee = estadaAlojamentoMapper.Read(new KeyValuePair<string, int>(estadaAlojamento.Alojamento.Nome, estadaAlojamento.Estada.Id));

                Assert.IsNotNull(ee);

                Assert.AreEqual(ee.Alojamento.Nome, estadaAlojamento.Alojamento.Nome);
                Assert.AreEqual(ee.Estada.Id, estadaAlojamento.Estada.Id);




                foreach (var e in estadaAlojamentoMapper.ReadAll())
                {
                    estadaAlojamentoMapper.Delete(e);
                }

                foreach (var e in estadaMapper.ReadAll())
                {
                    estadaMapper.Delete(estada);
                }

  
                foreach (var alojamento in alojamentoMapper.ReadAll())
                {  
                    alojamentoMapper.Delete(alojamento);
                }

              
                foreach (var parque in parqueMap.ReadAll())
                {
                    parqueMap.Delete(parque);
                }

            }
        }


        [TestMethod]
        public void UpdateTest()
        {
            using (Context ctx = new Context(_connectionString))
            {

                var estada = new Estada
                {
                    DataInicio = new DateTime(2007, 3, 1),
                    DataFim = new DateTime(2017, 3, 1),
                    Id = 25,
                    NifHospede = 0
                };

                EstadaMapper estadaMapper = new EstadaMapper(ctx);
                estada = estadaMapper.Create(estada);


                /*
                * CREATE PARQUE
                */
                Parque p = new Parque();
                p.Nome = "brasil";
                p.Email = "brasil@brasil.com";
                p.Morada = "Rio de Janeiro, Rua Junqueiro 367";
                p.Estrelas = 5;

                ParqueMapper parqueMap = new ParqueMapper(ctx);
                p = parqueMap.Create(p);

                /*
                 * Alojamento
                 */


                Alojamento c = new Alojamento();
                c.PreçoBase = 50;
                c.Nome = "OI";
                c.Descrição = "sem descricao";
                c.Localizaçao = "Brasil";
                c.MaxPessoas = 20;
                c.Parque = p;

                AlojamentoMapper alojamentoMapper = new AlojamentoMapper(ctx);
                c = alojamentoMapper.Create(c);





                EstadaAlojamento estadaAlojamento = new EstadaAlojamento()
                {

                    Estada = estada,
                    Alojamento = c,
                    Descrição = "OLA",
                    PreçoBase = 1234

                };

                var estadaAlojamentoMapper = new EstadaAlojamentoMapper(ctx);

                estadaAlojamento = estadaAlojamentoMapper.Create(estadaAlojamento);

                var ee = estadaAlojamentoMapper.Read(new KeyValuePair<string, int>(estadaAlojamento.Alojamento.Nome, estadaAlojamento.Estada.Id));

                Assert.IsNotNull(ee);

                Assert.AreEqual(ee.Alojamento.Nome, estadaAlojamento.Alojamento.Nome);
                Assert.AreEqual(ee.Estada.Id, estadaAlojamento.Estada.Id);

                Assert.AreEqual(ee.Descrição, estadaAlojamento.Descrição);
                Assert.AreEqual(ee.PreçoBase, estadaAlojamento.PreçoBase);


                var estadaAlojamentoPreçoBase = 9010;
                estadaAlojamento.PreçoBase = estadaAlojamentoPreçoBase;
                var s = estadaAlojamento.Descrição = "NADA";

                ee=estadaAlojamentoMapper.Update(estadaAlojamento);

                Assert.IsNotNull(ee);

                Assert.AreEqual(ee.Alojamento.Nome, estadaAlojamento.Alojamento.Nome);
                Assert.AreEqual(ee.Estada.Id, estadaAlojamento.Estada.Id);

                Assert.AreEqual(ee.Descrição, estadaAlojamento.Descrição);
                Assert.AreEqual(ee.PreçoBase, estadaAlojamento.PreçoBase);

                Assert.AreEqual(ee.Descrição, s);
                Assert.AreEqual(ee.PreçoBase, estadaAlojamentoPreçoBase);


                

                foreach (var e in estadaAlojamentoMapper.ReadAll())
                {
                    estadaAlojamentoMapper.Delete(e);
                }

                foreach (var e in estadaMapper.ReadAll())
                {
                    estadaMapper.Delete(estada);
                }


                foreach (var alojamento in alojamentoMapper.ReadAll())
                {
                    alojamentoMapper.Delete(alojamento);
                }


                foreach (var parque in parqueMap.ReadAll())
                {
                    parqueMap.Delete(parque);
                }

            }
        }
    }
}
