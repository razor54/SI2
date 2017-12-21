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
    public class ListarAtividadesDisponiveisTests
    {
        [TestMethod]
        public void TestListarAtividadesDisponiveis()
        {
            using (var ctx = new Entities())
            {
                var estada = new Estada
                {
                    data_início = new DateTime(2017, 1, 1),
                    data_fim = new DateTime(2017, 5, 2),
                    id = 123456,
                    nif_hóspede = 111
                };

                ctx.Estadas.Add(estada);
                

                Hóspede hóspede = new Hóspede
                {
                    bi = 456,
                    nif = 111,
                    nome = "Jaquim",
                    email = "jaquim@gmail.com",
                    morada = "Rua da Calçada"
                };

                ctx.Hóspede.Add(hóspede);
                


                Parque p = new Parque
                {
                    nome = "brasil",
                    email = "brasil@brasil.com",
                    morada = "Rio de Janeiro, Rua Junqueiro 367",
                    estrelas = 5
                };

                ctx.Parques.Add(p);
                

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
                ctx.SaveChanges();

                Atividade atividade2 = new Atividade
                {
                    nome_parque = p.nome,
                    data_atividade = new DateTime(2016, 1, 2),
                    descrição = "Pesca",
                    preço = 45,
                    lotação = 20,
                    nome_atividade = "Pesca"
                };

                ctx.Atividades.Add(atividade2);
                ctx.SaveChanges();

                HóspedeAtividade hóspedeAtividade = new HóspedeAtividade()
                {
                    nif_hóspede = hóspede.nif,
                    nome_atividade = atividade.nome_atividade,
                    nome_parque = p.nome
                };

                ctx.HóspedeAtividade.Add(hóspedeAtividade);
                

                HóspedeAtividade hóspedeAtividade2 = new HóspedeAtividade()
                {
                    nif_hóspede = hóspede.nif,
                    nome_atividade = atividade2.nome_atividade,
                    nome_parque = p.nome
                };

                ctx.HóspedeAtividade.Add(hóspedeAtividade2);

                ctx.SaveChanges();

                var res = ctx.listarAtividadesComlugares(new DateTime(2016, 1, 1), new DateTime(2018, 1, 1));

                foreach (var listarAtividadesComlugaresResult in res)
                {
                    Console.WriteLine(listarAtividadesComlugaresResult.nome_atividade);
                }

                foreach (var hóspedeAtividade1 in ctx.HóspedeAtividade)
                {
                    ctx.HóspedeAtividade.Remove(hóspedeAtividade1);
                }

               
                foreach (var s in ctx.Atividades)
                {
                    ctx.Atividades.Remove(s);
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
                    ctx.Estadas.Remove(estada);
                }


                ctx.SaveChanges();
            }
        }
    }
}