using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EFTests
{
    [TestClass]
    public class BungalowCrudTests
    {
        //NOT WORKING because bungalows don't have primary key
        [TestMethod]
        public void InsertBungalowTest()
        {
            using (var ctx = new Entities())
            {
                try
                {
                    Parque parque = new Parque()
                    {
                        nome = "Marechal Carmona",
                        morada = "Rua de Cascais",
                        estrelas = 4,
                        email = "marechal@cascais.com"
                    };


                    ctx.Parques.Add(parque);
                    ctx.SaveChanges();

                    Alojamento alojamento = new Alojamento
                    {
                        nome = "Primeiro Alojamento",
                        localização = "Quinta da Marinha",
                        descrição = "T0 com duche",
                        max_pessoas = 5,
                        preço_base = 85,
                        Parque = parque
                    };

                    ctx.Alojamentoes.Add(alojamento);

                    ctx.SaveChanges();

                    Bungalow bungalow = new Bungalow
                    {
                        tipologia = "T0",
                        nome_alojamento = alojamento.nome
                    };

                    ctx.Bungalows.Add(bungalow);

                    ctx.SaveChanges();

                    Bungalow bungalow1 = ctx.Bungalows.Find(bungalow.Alojamento.nome);
                    Assert.IsNotNull(bungalow1);

                    Assert.AreEqual(bungalow.Alojamento.nome, bungalow1.Alojamento.nome);
                    Assert.AreEqual(bungalow.tipologia, bungalow1.tipologia);

                    foreach (var b in ctx.Bungalows)
                        ctx.Bungalows.Remove(b);
                    foreach (var a in ctx.Alojamentoes)
                        ctx.Alojamentoes.Remove(a);
                    foreach (var p in ctx.Parques)
                        ctx.Parques.Remove(p);

                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    foreach (var b in ctx.Bungalows)
                        ctx.Bungalows.Remove(b);
                    foreach (var a in ctx.Alojamentoes)
                        ctx.Alojamentoes.Remove(a);
                    foreach (var p in ctx.Parques)
                        ctx.Parques.Remove(p);

                    ctx.SaveChanges();
                    throw;
                }
            }
        }

/*
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
        }*/
    }
}