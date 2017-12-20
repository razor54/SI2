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
    public class FaturaCrudTests
    {
        [TestMethod]
        public void InsertFaturaTest()
        {
            using (var ctx = new Entities())
            {
                try
                {
                    Hóspede hóspede = new Hóspede();
                    hóspede.bi = 1234567890;
                    hóspede.nif = 0987654321;
                    hóspede.nome = "Jaquim";
                    hóspede.email = "jaquim@gmail.com";
                    hóspede.morada = "Rua da Calçada";

                    ctx.Hóspede.Add(hóspede);
                    ctx.SaveChanges();

                    Estada estada = new Estada
                    {
                        data_início = new DateTime(2007, 3, 1),
                        data_fim = new DateTime(2017, 3, 1),
                        id = 25,
                        nif_hóspede = 0987654321
                    };


                    ctx.Estadas.Add(estada);
                    ctx.SaveChanges();

                    Fatura fatura = new Fatura
                    {
                        Hóspede = hóspede,
                        Estada = estada,
                        id = 1,
                        valor_final = 0,
                        id_estada = estada.id,
                        nif_hóspede = hóspede.nif,
                        nome_hóspede = hóspede.nome
                    };

                    ctx.Faturas.Add(fatura);
                    ctx.SaveChanges();

                    Fatura fatura1 = ctx.Faturas.Find(fatura.id);
                    Assert.IsNotNull(fatura1);
                    Assert.AreEqual(fatura.id, fatura1.id);
                    Assert.AreEqual(fatura.Hóspede.nome, fatura1.Hóspede.nome);
                    Assert.AreEqual(fatura.Hóspede.nif, fatura1.Hóspede.nif);
                    Assert.AreEqual(fatura.Hóspede.bi, fatura1.Hóspede.bi);
                    Assert.AreEqual(fatura.Estada.id, fatura1.Estada.id);
                    Assert.AreEqual(fatura.Estada.data_fim, fatura1.Estada.data_fim);
                    Assert.AreEqual(fatura.Estada.data_início, fatura1.Estada.data_início);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {

                    foreach (var e in ctx.Faturas)
                    {
                        ctx.Faturas.Remove(e);
                    }

                    foreach (var e in ctx.Hóspede)
                    {
                        ctx.Hóspede.Remove(e);
                    }

                    foreach (var e in ctx.Estadas)
                    {
                        ctx.Estadas.Remove(e);
                    }

                    ctx.SaveChanges();
                }
           


            }
        }
        /*
        [TestMethod]
        public void UpdateFaturaTest()
        {
            using (Context ctx = new Context(connectionString))
            {
                Hóspede hóspede = new Hóspede();
                hóspede.Bi = 1234567890;
                hóspede.Nif = 0987654321;
                hóspede.Nome = "Jaquim";
                hóspede.Email = "jaquim@gmail.com";
                hóspede.Morada = "Rua da Calçada";

                HóspedeMapper hóspedeMapper = new HóspedeMapper(ctx);
                hóspede = hóspedeMapper.Create(hóspede);

                Estada estada = new Estada();
                estada.DataInicio = new DateTime(2007, 3, 1);
                estada.DataFim = new DateTime(2017, 3, 1);
                estada.Id = 25;
                //TODO
                estada.NifHospede = 0;

                EstadaMapper estadaMapper = new EstadaMapper(ctx);
                estada = estadaMapper.Create(estada);


                Fatura fatura = new Fatura();
                fatura.Hóspede = hóspede;
                fatura.Estada = estada;
                fatura.Id = 1;
                fatura.ValorFinal = 0;


                var faturaMapper = new FaturaMapper(ctx);
                fatura = faturaMapper.Create(fatura);

                var valorFinal = 500;
                fatura.ValorFinal = valorFinal;
                faturaMapper.Update(fatura);

                Fatura fatura1 = faturaMapper.Read(fatura.Id);

                Assert.AreEqual(fatura.Id, fatura1.Id);
                Assert.AreEqual(fatura.Hóspede.Nome, fatura1.Hóspede.Nome);
                Assert.AreEqual(fatura.Hóspede.Nif, fatura1.Hóspede.Nif);
                Assert.AreEqual(fatura.Hóspede.Bi, fatura1.Hóspede.Bi);
                Assert.AreEqual(fatura.Estada.Id, fatura1.Estada.Id);
                Assert.AreEqual(fatura.Estada.DataFim, fatura1.Estada.DataFim);
                Assert.AreEqual(fatura.Estada.DataInicio, fatura1.Estada.DataInicio);

                Assert.AreEqual(fatura.ValorFinal, fatura1.ValorFinal);
                Assert.AreEqual(fatura.ValorFinal, valorFinal);


                foreach (var e in faturaMapper.ReadAll())
                {
                    faturaMapper.Delete(e);
                }

                foreach (var e in hóspedeMapper.ReadAll())
                {
                    hóspedeMapper.Delete(e);
                }

                foreach (var e in estadaMapper.ReadAll())
                {
                    estadaMapper.Delete(e);
                }
            }
        }*/
    }
}
