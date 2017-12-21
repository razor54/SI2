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
    public class ComponenteFaturaTests
    {
        [TestMethod]
        public void InsertTest()
        {
            using (var ctx = new Entities())
            {
                try
                {
                    Hóspede hóspede = new Hóspede
                    {
                        bi = 1234567890,
                        nif = 0987654321,
                        nome = "Jaquim",
                        email = "jaquim@gmail.com",
                        morada = "Rua da Calçada"
                    };

                    ctx.Hóspede.Add(hóspede);


                    var estada = new Estada
                    {
                        data_início = new DateTime(2007, 3, 1),
                        data_fim = new DateTime(2017, 3, 1),
                        id = 25,
                        nif_hóspede = 0
                    };


                    ctx.Estadas.Add(estada);


                    Fatura fatura = new Fatura
                    {
                        id_estada = estada.id,
                        nif_hóspede = hóspede.nif,

                        nome_hóspede = hóspede.nome,
                        id = 1,
                        valor_final = 0
                    };


                    ctx.Faturas.Add(fatura);
                    ctx.SaveChanges();

                    ComponenteFatura componente = new ComponenteFatura
                    {
                        preço = 25,
                        Fatura = fatura,
                        tipo = "Alojamento",
                        descrição = "muito bom"
                    };

                    ctx.ComponenteFaturas.Add(componente);
                    ctx.SaveChanges();
                    var id = componente.id;

                    ComponenteFatura componente2 = ctx.ComponenteFaturas.Find(id, fatura.id);
                    Assert.IsNotNull(componente2);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    foreach (var e in ctx.ComponenteFaturas)
                    {
                        ctx.ComponenteFaturas.Remove(e);
                    }

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


        [TestMethod]
        public void UpdateTest()
        {
            using (var ctx = new Entities())
            {
                try
                {
                    Hóspede hóspede = new Hóspede
                    {
                        bi = 1234567890,
                        nif = 0987654321,
                        nome = "Jaquim",
                        email = "jaquim@gmail.com",
                        morada = "Rua da Calçada"
                    };

                    ctx.Hóspede.Add(hóspede);


                    var estada = new Estada
                    {
                        data_início = new DateTime(2007, 3, 1),
                        data_fim = new DateTime(2017, 3, 1),
                        id = 25,
                        nif_hóspede = 0
                    };


                    ctx.Estadas.Add(estada);


                    Fatura fatura = new Fatura
                    {
                        id_estada = estada.id,
                        nif_hóspede = hóspede.nif,

                        nome_hóspede = hóspede.nome,
                        id = 1,
                        valor_final = 0
                    };


                    ctx.Faturas.Add(fatura);
                    ctx.SaveChanges();

                    ComponenteFatura componente = new ComponenteFatura
                    {
                        preço = 25,
                        Fatura = fatura,
                        tipo = "Alojamento",
                        descrição = "muito bom"
                    };

                    ctx.ComponenteFaturas.Add(componente);
                    ctx.SaveChanges();
                    var id = componente.id;

                    ComponenteFatura componente2 = ctx.ComponenteFaturas.Find(id, fatura.id);
                    Assert.IsNotNull(componente2);

                    var componenteDescrição = "NOt recommenended";
                    componente.descrição = componenteDescrição;
                    ctx.SaveChanges();

                    Assert.AreEqual(componente.descrição, componenteDescrição);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    foreach (var e in ctx.ComponenteFaturas)
                    {
                        ctx.ComponenteFaturas.Remove(e);
                    }

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
    }
}

