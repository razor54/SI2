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

namespace ADOSI2Tests
{
    [TestClass]
    public class ListarAtividadesDisponíveisTests
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["si2cs"].ConnectionString;

        [TestMethod]
        public void ListAvailableActivitiesTest()
        {
            using (Context ctx = new Context(_connectionString))
            {
                var estada = new Estada
                {
                    DataInicio = new DateTime(2017, 1, 1),
                    DataFim = new DateTime(2017, 5, 2),
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

                Parque p = new Parque();
                p.Nome = "brasil";
                p.Email = "brasil@brasil.com";
                p.Morada = "Rio de Janeiro, Rua Junqueiro 367";
                p.Estrelas = 5;

                ParqueMapper parqueMap = new ParqueMapper(ctx);
                p = parqueMap.Create(p);

                Atividade atividade = new Atividade
                {
                    Parque = p,
                    DataAtividade = new DateTime(2017, 1, 1),
                    Descrição = "Canoagem",
                    Preço = 90,
                    Lotaçao = 12,
                    NomeAtividade = "Canoagem"
                };

                var atividadeMapper = new AtividadeMapper(ctx);
                atividade = atividadeMapper.Create(atividade);

                Atividade atividade2 = new Atividade
                {
                    Parque = p,
                    DataAtividade = new DateTime(2016, 1, 2),
                    Descrição = "Pesca",
                    Preço = 45,
                    Lotaçao = 20,
                    NomeAtividade = "Pesca"
                };

                atividade2 = atividadeMapper.Create(atividade2);

                HóspedeAtividade hóspedeAtividade = new HóspedeAtividade()
                {
                    Hóspede = hóspede,
                    Nome_Atividade = atividade.NomeAtividade,
                    Nome_Parque = p.Nome
                };

                var hóspedeAtividadeMapper = new HóspedeAtividadeMapper(ctx);
                hóspedeAtividade = hóspedeAtividadeMapper.Create(hóspedeAtividade);

                HóspedeAtividade hóspedeAtividade2 = new HóspedeAtividade()
                {
                    Hóspede = hóspede,
                    Nome_Atividade = atividade2.NomeAtividade,
                    Nome_Parque = p.Nome
                };

                hóspedeAtividade2 = hóspedeAtividadeMapper.Create(hóspedeAtividade2);

                var listagem = new ListarAtividadeComLugares(ctx);

                listagem.Execute(new DateTime(2016, 1, 1), new DateTime(2018, 1, 1));

                foreach (var h in hóspedeAtividadeMapper.ReadAll())
                {
                    hóspedeAtividadeMapper.Delete(h);
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

                foreach (var parque in parqueMap.ReadAll())
                {
                    parqueMap.Delete(parque);
                }

                foreach (var e in estadaMapper.ReadAll())
                {
                    estadaMapper.Delete(estada);
                }
            }
        }
    }
}
