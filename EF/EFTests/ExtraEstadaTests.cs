using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EFTests
{
    [TestClass]
    public class ExtraEstadaTests
    {
        [TestMethod]
        public void InsertExtraTest()
        {
            using (var ctx = new Entities())
            {
                try
                {
                    var extra = new Extra
                    {
                        tipo = "alojamento",
                        id = 25,
                        descrição = "muito bom",
                        preço_dia = 25
                    };

                    ctx.Extras.Add(extra);

                    var estada = new Estada
                    {
                        data_início = new DateTime(2007, 3, 1),
                        data_fim = new DateTime(2017, 3, 1),
                        id = 25,
                        nif_hóspede = 0
                    };

                    //TODO

                    ctx.Estadas.Add(estada);

                    ExtraEstada extraEstada = new ExtraEstada
                    {
                        preço_dia = 12,
                        descrição = "Pessimo",
                        Estada = estada,
                        Extra = extra
                    };

                    ctx.ExtraEstadas.Add(extraEstada);

                    ctx.SaveChanges();

                    var ee = ctx.ExtraEstadas.Find(extraEstada.Extra.id, extraEstada.Estada.id);

                    Assert.IsNotNull(ee);

                    Assert.AreEqual(ee.Extra.id, extraEstada.Extra.id);
                    Assert.AreEqual(ee.Estada.id, extraEstada.Estada.id);

                    Assert.AreEqual(ee.preço_dia, extraEstada.preço_dia);
                    Assert.AreEqual(ee.descrição, extraEstada.descrição);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                finally
                {
                    foreach (var e in ctx.ExtraEstadas)
                    {
                        ctx.ExtraEstadas.Remove(e);
                    }

                    foreach (var e in ctx.Estadas)
                    {
                        ctx.Estadas.Remove(e);
                    }

                    foreach (var e in ctx.Extras)
                    {
                        ctx.Extras.Remove(e);
                    }

                    ctx.SaveChanges();
                }
            }
        }


        [TestMethod]
        public void UpdateExtraTest()
        {
            using (var ctx = new Entities())
            {
                try
                {
                    var extra = new Extra
                    {
                        tipo = "alojamento",
                        id = 25,
                        descrição = "muito bom",
                        preço_dia = 25
                    };

                    ctx.Extras.Add(extra);

                    var estada = new Estada
                    {
                        data_início = new DateTime(2007, 3, 1),
                        data_fim = new DateTime(2017, 3, 1),
                        id = 25,
                        nif_hóspede = 0
                    };

                    //TODO

                    ctx.Estadas.Add(estada);

                    ExtraEstada extraEstada = new ExtraEstada
                    {
                        preço_dia = 12,
                        descrição = "Pessimo",
                        Estada = estada,
                        Extra = extra
                    };

                    ctx.ExtraEstadas.Add(extraEstada);

                    ctx.SaveChanges();

                    var ee = ctx.ExtraEstadas.Find(extraEstada.Extra.id, extraEstada.Estada.id);

                    Assert.IsNotNull(ee);

                    Assert.AreEqual(ee.Extra.id, extraEstada.Extra.id);
                    Assert.AreEqual(ee.Estada.id, extraEstada.Estada.id);

                    Assert.AreEqual(ee.preço_dia, extraEstada.preço_dia);
                    Assert.AreEqual(ee.descrição, extraEstada.descrição);

                    var extraEstadaDescrição = "MUITO MAU";
                    var extraEstadaPreçoDia = 11111;

                    extraEstada.descrição = extraEstadaDescrição;
                    extraEstada.preço_dia = extraEstadaPreçoDia;
                    ctx.SaveChanges();

                    ee = ctx.ExtraEstadas.Find(extraEstada.Extra.id, extraEstada.Estada.id);

                    Assert.IsNotNull(ee);

                    Assert.AreEqual(ee.Extra.id, extraEstada.Extra.id);
                    Assert.AreEqual(ee.Estada.id, extraEstada.Estada.id);

                    Assert.AreEqual(ee.preço_dia, extraEstadaPreçoDia);
                    Assert.AreEqual(ee.descrição,extraEstadaDescrição);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                finally
                {
                    foreach (var e in ctx.ExtraEstadas)
                    {
                        ctx.ExtraEstadas.Remove(e);
                    }

                    foreach (var e in ctx.Estadas)
                    {
                        ctx.Estadas.Remove(e);
                    }

                    foreach (var e in ctx.Extras)
                    {
                        ctx.Extras.Remove(e);
                    }

                    ctx.SaveChanges();
                }
            }
        }
    }
}