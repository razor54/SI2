using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;
using EF.logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EFTests
{
    [TestClass]
    public class RemoverParqueEAssociaçoesTests
    {
        [TestMethod]
        public void ApagarParqueEAssociaçoesTest()
        {
            using (var ctx = new Entities())
            {
                try
                {
                    var estada = new Estada
                    {
                        data_início = new DateTime(2007, 3, 1),
                        data_fim = new DateTime(2017, 3, 1),
                        id = 25,
                        nif_hóspede = 0
                    };


                    ctx.Estadas.Add(estada);


                    /*
                    * CREATE PARQUE
                    */
                    Parque p = new Parque
                    {
                        nome = "brasil",
                        email = "brasil@brasil.com",
                        morada = "Rio de Janeiro, Rua Junqueiro 367",
                        estrelas = 5
                    };


                    ctx.Parques.Add(p);

                    /*
                     * Alojamento
                     */


                    Alojamento c = new Alojamento
                    {
                        preço_base = 50,
                        nome = "OI",
                        descrição = "sem descricao",
                        localização = "Brasil",
                        max_pessoas = 20,
                        nome_parque = p.nome
                    };


                    ctx.Alojamentoes.Add(c);


                    EstadaAlojamento estadaAlojamento = new EstadaAlojamento()
                    {
                        id_estada = estada.id,
                        nome_alojamento = c.nome,

                        descrição = "OLA",
                        preço_base = 1234
                    };
                    ctx.EstadaAlojamentoes.Add(estadaAlojamento);

                    ctx.SaveChanges();

                    var apagrParque = new RemoverParqueEAssociaçoes(ctx);
                    apagrParque.Execute(p);

                    var parqueExists = ctx.Parques.Any(parque => parque.nome.Equals(p.nome));
                    Assert.AreEqual(false, parqueExists);

                    var alojamentoExists = ctx.Alojamentoes.Any(a => a.nome.Equals(c.nome));
                    Assert.AreEqual(false, alojamentoExists);

                    var estadaExists = ctx.Estadas.Any(e => e.id == estada.id);
                    Assert.AreEqual(false, estadaExists);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);


                    foreach (var e in ctx.EstadaAlojamentoes)
                    {
                        ctx.EstadaAlojamentoes.Remove(e);
                    }

                    foreach (var e in ctx.Estadas)
                    {
                        ctx.Estadas.Remove(e);
                    }


                    foreach (var alojamento in ctx.Alojamentoes)
                    {
                        ctx.Alojamentoes.Remove(alojamento);
                    }


                    foreach (var parque in ctx.Parques)
                    {
                        ctx.Parques.Remove(parque);
                    }

                    throw;
                }
            }
        }
    }
}