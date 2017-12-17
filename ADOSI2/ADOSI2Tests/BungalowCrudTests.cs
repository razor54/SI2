using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADOSI2.concrete;
using ADOSI2.model;

namespace ADOSI2Tests
{
    [TestClass]
    public class BungalowCrudTests
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["si2cs"].ConnectionString;

        [TestMethod]
        public void InsertBungalowTest()
        {
            using (Context ctx = new Context(connectionString))
            {
                Parque parque = new Parque();
                parque.Nome = "Marechal Carmona";
                parque.Morada = "Rua de Cascais";
                parque.Estrelas = 4;
                parque.Email = "marechal@cascais.com";

                ParqueMapper pm = new ParqueMapper(ctx);
                parque = pm.Create(parque);

                Alojamento alojamento = new Alojamento();
                alojamento.Nome = "Primeiro Alojamento";
                alojamento.Localizaçao = "Quinta da Marinha";
                alojamento.Descrição = "T0 com duche";
                alojamento.MaxPessoas = 5;
                alojamento.PreçoBase = 85;
                alojamento.Parque = parque;

                AlojamentoMapper am = new AlojamentoMapper(ctx);
                alojamento = am.Create(alojamento);

                Bungalow bungalow = new Bungalow();
                bungalow.Tipologia = "T0";
                bungalow.Alojamento = alojamento;

                BungalowMapper bungalowMapper = new BungalowMapper(ctx);
                bungalow = bungalowMapper.Create(bungalow);

                Bungalow bungalow1 = bungalowMapper.Read(bungalow.Alojamento.Nome);

                Assert.AreEqual(bungalow.Alojamento.Nome, bungalow1.Alojamento.Nome);
                Assert.AreEqual(bungalow.Tipologia, bungalow1.Tipologia);

                foreach (var b in bungalowMapper.ReadAll())
                    bungalowMapper.Delete(b);
                foreach (var a in am.ReadAll())
                    am.Delete(a);
                foreach (var p in pm.ReadAll())
                    pm.Delete(p);
            }
        }

        [TestMethod]
        public void UpdateTendaTest()
        {
            using (Context ctx = new Context(connectionString))
            {
                Parque parque = new Parque();
                parque.Nome = "Marechal Carmona";
                parque.Morada = "Rua de Cascais";
                parque.Estrelas = 4;
                parque.Email = "marechal@cascais.com";

                ParqueMapper pm = new ParqueMapper(ctx);
                parque = pm.Create(parque);

                Alojamento alojamento = new Alojamento();
                alojamento.Nome = "Primeiro Alojamento";
                alojamento.Localizaçao = "Quinta da Marinha";
                alojamento.Descrição = "T0 com duche";
                alojamento.MaxPessoas = 5;
                alojamento.PreçoBase = 85;
                alojamento.Parque = parque;

                AlojamentoMapper am = new AlojamentoMapper(ctx);
                alojamento = am.Create(alojamento);

                Bungalow bungalow = new Bungalow();
                bungalow.Tipologia = "T0";
                bungalow.Alojamento = alojamento;

                BungalowMapper bungalowMapper = new BungalowMapper(ctx);
                bungalow = bungalowMapper.Create(bungalow);

                bungalow.Tipologia = "T1";
                bungalowMapper.Update(bungalow);

                Bungalow bungalow1 = bungalowMapper.Read(bungalow.Alojamento.Nome);

                Assert.AreEqual(bungalow.Alojamento.Nome, bungalow1.Alojamento.Nome);
                Assert.AreEqual(bungalow.Tipologia, bungalow1.Tipologia);

                foreach (var b in bungalowMapper.ReadAll())
                    bungalowMapper.Delete(b);
                foreach (var a in am.ReadAll())
                    am.Delete(a);
                foreach (var p in pm.ReadAll())
                    pm.Delete(p);
            }
        }
    }
}
