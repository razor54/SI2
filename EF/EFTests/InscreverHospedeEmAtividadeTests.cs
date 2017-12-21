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
    public class InscreverHospedeEmAtividadeTests
    {
        [TestMethod]
        public void InsertHóspedeTest()
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
                        nif_hóspede = 0987654321
                    };

                    ctx.Estadas.Add(estada);


                    /*
                     * CREATE PARQUE
                     */
                    Parque p = new Parque();
                    p.nome = "brasil";
                    p.email = "brasil@brasil.com";
                    p.morada = "Rio de Janeiro, Rua Junqueiro 367";
                    p.estrelas = 5;

                    ctx.Parques.Add(p);


                    var atividade = new Atividade
                    {
                        Parque = p,
                        data_atividade = new DateTime(2009, 10, 1),
                        descrição = "FORA",
                        preço = 90,
                        lotação = 12,
                        nome_atividade = "HAMBURGER"
                    };
                    ctx.Atividades.Add(atividade);

                    Fatura fatura = new Fatura
                    {
                        id_estada = estada.id,
                        nif_hóspede = hóspede.nif,
                        nome_hóspede = hóspede.nome,
                        id = 1
                    };

                    ctx.Faturas.Add(fatura);
                    ctx.SaveChanges();

                    ctx.inscreverHóspedeNumaAtividade(hóspede.nif, atividade.nome_atividade, p.nome);
                    ctx.SaveChanges();

                    var hospedeAtividade = ctx.HóspedeAtividade.Find(hóspede.nif, atividade.nome_atividade, p.nome);
                    Assert.IsNotNull(hospedeAtividade);
                    var count = ctx.ComponenteFaturas.Count();
                    Assert.AreNotEqual(count, 0);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    /*
                     * REMOVE THE PARQUE
                     */


                    foreach (var cp in ctx.HóspedeAtividade)
                    {
                        ctx.HóspedeAtividade.Remove(cp);
                    }

                    foreach (var s in ctx.Atividades)
                    {
                        ctx.Atividades.Remove(s);
                    }


                    foreach (var cp in ctx.ComponenteFaturas)
                    {
                        ctx.ComponenteFaturas.Remove(cp);
                    }

                    foreach (var e in ctx.Faturas)
                    {
                        ctx.Faturas.Remove(e);
                    }

                    foreach (var hospede in ctx.Hóspede)
                    {
                        ctx.Hóspede.Remove(hospede);
                    }

                    foreach (var parque in ctx.Parques)
                    {
                        ctx.Parques.Remove(parque);
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