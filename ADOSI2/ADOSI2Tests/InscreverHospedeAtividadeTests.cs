using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.concrete;
using ADOSI2.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ADOSI2Tests
{
    [TestClass]
    public class InscreverHospedeAtividadeTests
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["si2cs"].ConnectionString;

        [TestMethod]
        public void InsertHóspedeTest()
        {
            using (Context ctx = new Context(_connectionString))
            {
                var estada = new Estada
                {
                    DataInicio = new DateTime(2007, 3, 1),
                    DataFim = new DateTime(2017, 3, 1),
                    Id = 25,
                    NifHospede = 0
                };

                EstadaMapper estadaMapper = new EstadaMapper(ctx);
                estada = estadaMapper.Create(estada);


                Hóspede hóspede = new Hóspede();
                hóspede.Bi = 1234567890;
                hóspede.Nif = 0987654321;
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

                /*
                 * CREATE PARQUE
                 */
                Parque p = new Parque();
                p.Nome = "brasil";
                p.Email = "brasil@brasil.com";
                p.Morada = "Rio de Janeiro, Rua Junqueiro 367";
                p.Estrelas = 5;

                ParqueMapper parqueMap = new ParqueMapper(ctx);
                p = parqueMap.Create(p);


                var atividade = new Atividade
                {
                    Parque = p,
                    DataAtividade = new DateTime(2009, 10, 1),
                    Descrição = "FORA",
                    Preço = 90,
                    Lotaçao = 12,
                    NomeAtividade = "HAMBURGER"
                };
                var atividadeMapper = new AtividadeMapper(ctx);
                atividade = atividadeMapper.Create(atividade);


                Fatura fatura = new Fatura();
                fatura.Hóspede = hóspede;
                fatura.Estada = estada;
                fatura.Id = 1;
                fatura.ValorFinal = 0;


                var faturaMapper = new FaturaMapper(ctx);
                fatura = faturaMapper.Create(fatura);


                var inscr = new InscreverHóspedeEmAtividade(ctx);

                inscr.Execute(hóspede.Nif, atividade.NomeAtividade, p.Nome);


                 var hospedeAtividadeMapper = new HóspedeAtividadeMapper(ctx);
                 var componenteFaturaMapper = new ComponenteFaturaMapper(ctx);

                var hospedeAtividade = hospedeAtividadeMapper.Read(atividade.NomeAtividade);
                Assert.IsNotNull(hospedeAtividade);
                var count = componenteFaturaMapper.ReadAll().Count;
                Assert.AreNotEqual(count,0);

                /*
                 * REMOVE THE PARQUE
                 */
               

                foreach (var cp in hospedeAtividadeMapper.ReadAll())
                {
                    hospedeAtividadeMapper.Delete(cp);
                }
                foreach (var s in atividadeMapper.ReadAll())
                {
                    atividadeMapper.Delete(s);
                }
                foreach (var e in estadaHospedeMapper.ReadAll())
                {
                    estadaHospedeMapper.Delete(e);
                }

                foreach (var cp in componenteFaturaMapper.ReadAll())
                {
                    componenteFaturaMapper.Delete(cp);
                }

                foreach (var e in faturaMapper.ReadAll())
                {
                    faturaMapper.Delete(e);
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
