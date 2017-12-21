using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EFTests
{
    [TestClass]
    public class PagamentoEstadaComFaturaTests
    {
        [TestMethod]
        void PagamentoEstadaComFaturaTest()
        {
            
            using (var ctx = new Entities())
            {
                try
                {
                    var estada = new Estada
                    {
                        data_início = new DateTime(2000, 1, 1),
                        data_fim = new DateTime(2000, 5, 2),
                        id = 123456,
                        nif_hóspede = 111
                    };

                    ctx.Estadas.Add(estada);

                    Hóspede hóspede = new Hóspede();
                    hóspede.bi = 456;
                    hóspede.nif = 111;
                    hóspede.nome = "Jaquim";
                    hóspede.email = "jaquim@gmail.com";
                    hóspede.morada = "Rua da Calçada";

                    ctx.Hóspede.Add(hóspede);



                    Fatura fatura = new Fatura()
                    {
                        id = 9999,
                        id_estada = estada.id,
                        nif_hóspede = hóspede.nif,
                        nome_hóspede = hóspede.nome,

                    };

                    ctx.Faturas.Add(fatura);

                    Parque p = new Parque();
                    p.nome = "brasil";
                    p.email = "brasil@brasil.com";
                    p.morada = "Rio de Janeiro, Rua Junqueiro 367";
                    p.estrelas = 5;
                    ctx.Parques.Add(p);

                    Alojamento alojamento = new Alojamento
                    {
                        nome = "Primeiro Alojamento",
                        localização = "Quinta da Marinha",
                        descrição = "T0 com duche",
                        max_pessoas = 5,
                        preço_base = 85,
                        nome_parque = p.nome
                    };

                    ctx.Alojamentoes.Add(alojamento);

                    Bungalow bungalow = new Bungalow()
                    {
                        tipologia = "T2",
                        nome_alojamento = alojamento.nome
                    };

                    ctx.Bungalows.Add(bungalow);

                    ComponenteFatura componenteFatura = new ComponenteFatura
                    {
                        preço = 85,
                        id_fatura = fatura.id,

                        tipo = "Alojamento",
                        descrição = "muito bom"
                    };

                    ctx.ComponenteFaturas.Add(componenteFatura);

                    Atividade atividade = new Atividade
                    {

                        nome_parque = p.nome,
                        data_atividade = new DateTime(2017, 1, 1),
                        descrição = "Canoagem",
                        preço = 90,
                        lotação = 12,
                        nome_atividade = "Canoagem"
                    };

                    ctx.Atividades.Add(atividade);

                    var inscResult = ctx.inscreverHóspedeNumaAtividade(hóspede.nif, atividade.nome_atividade,
                        atividade.nome_parque);
                    ObjectParameter output = new ObjectParameter("total", typeof(Int32));

                    var pagamentoResult = ctx.pagamentoEstadaComFatura(estada.id, output);


                    // atualizar valor final da fatura
                    fatura.valor_final = (int) output.Value;

                    ctx.SaveChanges();

                    Assert.AreEqual(175, fatura.valor_final);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                finally
                {


                    foreach (var ha in ctx.HóspedeAtividade)
                    {
                        ctx.HóspedeAtividade.Remove(ha);
                    }

                    foreach (var b in ctx.Bungalows)
                    {
                        ctx.Bungalows.Remove(b);
                    }

                    foreach (var c in ctx.ComponenteFaturas)
                    {
                        ctx.ComponenteFaturas.Remove(c);
                    }

                    foreach (var f in ctx.Faturas)
                    {
                        ctx.Faturas.Remove(f);
                    }

                    foreach (var s in ctx.Atividades)
                    {
                        ctx.Atividades.Remove(s);
                    }


                    foreach (var hospede in ctx.Hóspede)
                    {
                        ctx.Hóspede.Remove(hospede);
                    }

                    foreach (var e in ctx.Estadas)
                    {
                        ctx.Estadas.Remove(e);
                    }

                    foreach (var a in ctx.Alojamentoes)
                    {
                        ctx.Alojamentoes.Remove(a);
                    }

                    foreach (var parque in ctx.Parques)
                    {
                        ctx.Parques.Remove(parque);
                    }

                    ctx.SaveChanges();
                }
            }
        }
    }
}
