using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADOSI2.concrete;
using ADOSI2.model;

namespace ADOSI2Tests
{
    [TestClass]
    public class TendaCrudTests
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["si2cs"].ConnectionString;

        [TestMethod]
        public void InsertTendaTest()
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

                Tenda tenda = new Tenda();
                tenda.Area = 1234;
                tenda.Alojamento = alojamento;
                tenda.Tipo = "yurt";

                TendaMapper tendaMapper = new TendaMapper(ctx);
                tenda = tendaMapper.Create(tenda);

                Tenda tenda1 = tendaMapper.Read(tenda.Alojamento.Nome);

                Assert.AreEqual(tenda.Alojamento.Nome, tenda1.Alojamento.Nome);
                Assert.AreEqual(tenda.Tipo, tenda1.Tipo);

                foreach (var t in tendaMapper.ReadAll())
                    tendaMapper.Delete(t);
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

                Tenda tenda = new Tenda();
                tenda.Area = 1234;
                tenda.Alojamento = alojamento;
                tenda.Tipo = "yurt";

                TendaMapper tendaMapper = new TendaMapper(ctx);
                tenda = tendaMapper.Create(tenda);

                tenda.Area = 5678;
                tenda.Tipo = "safari";
                tendaMapper.Update(tenda);

                Tenda tenda1 = tendaMapper.Read(tenda.Alojamento.Nome);

                Assert.AreEqual(tenda.Alojamento.Nome, tenda1.Alojamento.Nome);
                Assert.AreEqual(tenda.Tipo, tenda1.Tipo);

                foreach (var t in tendaMapper.ReadAll())
                    tendaMapper.Delete(t);
                foreach (var a in am.ReadAll())
                    am.Delete(a);
                foreach (var p in pm.ReadAll())
                    pm.Delete(p);
            }
        }
    }
}
