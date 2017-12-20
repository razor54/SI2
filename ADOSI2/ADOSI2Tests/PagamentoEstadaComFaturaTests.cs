using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.concrete;
using ADOSI2.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADOSI2.concrete.logic;
using System.Data.SqlClient;
using System.Data;

namespace ADOSI2Tests
{
    [TestClass]
    public class PagamentoEstadaComFaturaTests
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["si2cs"].ConnectionString;

        [TestMethod]
        public void PagamentoEstadaComFaturaTest()
        {
            using (Context ctx = new Context(_connectionString))
            {
                var estada = new Estada
                {
                    DataInicio = new DateTime(2000, 1, 1),
                    DataFim = new DateTime(2000, 5, 2),
                    Id = 123456,
                    NifHospede = 111
                };

                EstadaMapper estadaMapper = new EstadaMapper(ctx);
                estada = estadaMapper.Create(estada);

                Hóspede hóspede = new Hóspede();
                hóspede.Bi = 456;
                hóspede.Nif = 111;
                hóspede.Nome = "Jaquim";
                hóspede.Email = "jaquim@gmail.com";
                hóspede.Morada = "Rua da Calçada";

                HóspedeMapper hóspedeMapper = new HóspedeMapper(ctx);
                hóspede = hóspedeMapper.Create(hóspede);

                EstadaHóspede estadaHóspede = new EstadaHóspede()
                {
                    Estada = estada,
                    Hóspede = hóspede
                };

                var estadaHospedeMapper = new EstadaHóspedeMapper(ctx);
                estadaHóspede = estadaHospedeMapper.Create(estadaHóspede);

                Fatura fatura = new Fatura()
                {
                    Id = 9999,
                    Estada = estada,
                    Hóspede = hóspede,
                    ValorFinal = 0
                };

                FaturaMapper faturaMapper = new FaturaMapper(ctx);
                fatura = faturaMapper.Create(fatura);

                Parque p = new Parque();
                p.Nome = "brasil";
                p.Email = "brasil@brasil.com";
                p.Morada = "Rio de Janeiro, Rua Junqueiro 367";
                p.Estrelas = 5;

                ParqueMapper parqueMap = new ParqueMapper(ctx);
                p = parqueMap.Create(p);

                Alojamento alojamento = new Alojamento
                {
                    Nome = "Primeiro Alojamento",
                    Localizaçao = "Quinta da Marinha",
                    Descrição = "T0 com duche",
                    MaxPessoas = 5,
                    PreçoBase = 85,
                    Parque = p
                };

                AlojamentoMapper alojamentoMapper = new AlojamentoMapper(ctx);
                alojamento = alojamentoMapper.Create(alojamento);

                Bungalow bungalow = new Bungalow()
                {
                    Tipologia = "T2",
                    Alojamento = alojamento
                };

                BungalowMapper bungalowMapper = new BungalowMapper(ctx);
                bungalow = bungalowMapper.Create(bungalow);

                ComponenteFatura componenteFatura = new ComponenteFatura
                {
                    Preço = 85,
                    Fatura = fatura,
                    Tipo = "Alojamento",
                    Descrição = "muito bom"
                };

                var componenteMapper = new ComponenteFaturaMapper(ctx);
                componenteFatura = componenteMapper.Create(componenteFatura);

                Atividade atividade = new Atividade
                {
                    Parque = p,
                    DataAtividade = new DateTime(2017, 1, 1),
                    Descrição = "Canoagem",
                    Preço = 90,
                    Lotaçao = 12,
                    NomeAtividade = "Canoagem"
                };

                AtividadeMapper atividadeMapper = new AtividadeMapper(ctx);
                atividade = atividadeMapper.Create(atividade);

                var inscr = new InscreverHóspedeEmAtividade(ctx);
                inscr.Execute(hóspede.Nif, atividade.NomeAtividade, p.Nome);

                var pagamento = new PagamentoEstadaComFatura(ctx);
                pagamento.Execute(estada.Id, out int total);

                // atualizar valor final da fatura
                fatura.ValorFinal = total;
                fatura = faturaMapper.Update(fatura);

                Assert.AreEqual(175, fatura.ValorFinal);

                var hospedeAtividadeMapper = new HóspedeAtividadeMapper(ctx);

                foreach(var ha in hospedeAtividadeMapper.ReadAll())
                {
                    hospedeAtividadeMapper.Delete(ha);
                }

                foreach (var b in bungalowMapper.ReadAll())
                {
                    bungalowMapper.Delete(b);
                }

                foreach(var c in componenteMapper.ReadAll())
                {
                    componenteMapper.Delete(c);
                }

                foreach(var f in faturaMapper.ReadAll())
                {
                    faturaMapper.Delete(f);
                }

                foreach (var s in atividadeMapper.ReadAll())
                {
                    atividadeMapper.Delete(s);
                }

                foreach (var e in estadaHospedeMapper.ReadAll())
                {
                    estadaHospedeMapper.Delete(e);
                }

                foreach (var hospede in hóspedeMapper.ReadAll())
                {
                    hóspedeMapper.Delete(hospede);
                }

                foreach (var e in estadaMapper.ReadAll())
                {
                    estadaMapper.Delete(estada);
                }
                
                foreach(var a in alojamentoMapper.ReadAll())
                {
                    alojamentoMapper.Delete(a);
                }

                foreach (var parque in parqueMap.ReadAll())
                {
                    parqueMap.Delete(parque);
                }
            }
        }
    }
}