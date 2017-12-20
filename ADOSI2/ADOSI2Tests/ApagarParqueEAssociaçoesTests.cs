using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.concrete;
using ADOSI2.concrete.logic;
using ADOSI2.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ADOSI2Tests
{
    [TestClass]
    public class ApagarParqueEAssociaçoesTests
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["si2cs"].ConnectionString;

        [TestMethod]
        public void ApagarParqueEAssociaçoesTest()
        {
            using (var ctx = new Context(_connectionString))
            {
                EstadaMapper estadaMapper = new EstadaMapper(ctx);
                ParqueMapper parqueMap = new ParqueMapper(ctx);
                AlojamentoMapper alojamentoMapper = new AlojamentoMapper(ctx);
                var estadaAlojamentoMapper = new EstadaAlojamentoMapper(ctx);
                try
                {
                    var estada = new Estada
                    {
                        DataInicio = new DateTime(2007, 3, 1),
                        DataFim = new DateTime(2017, 3, 1),
                        Id = 25,
                        NifHospede = 0
                    };

                    
                    estada = estadaMapper.Create(estada);


                    /*
                    * CREATE PARQUE
                    */
                    Parque p = new Parque();
                    p.Nome = "brasil";
                    p.Email = "brasil@brasil.com";
                    p.Morada = "Rio de Janeiro, Rua Junqueiro 367";
                    p.Estrelas = 5;

                    
                    p = parqueMap.Create(p);

                    /*
                     * Alojamento
                     */


                    Alojamento c = new Alojamento
                    {
                        PreçoBase = 50,
                        Nome = "OI",
                        Descrição = "sem descricao",
                        Localizaçao = "Brasil",
                        MaxPessoas = 20,
                        Parque = p
                    };

                    
                    c = alojamentoMapper.Create(c);


                    EstadaAlojamento estadaAlojamento = new EstadaAlojamento()
                    {
                        Estada = estada,
                        Alojamento = c,
                        Descrição = "OLA",
                        PreçoBase = 1234
                    };

                   

                    estadaAlojamento = estadaAlojamentoMapper.Create(estadaAlojamento);


                    var apagrParque = new ApagarParqueEAssociaçoes(ctx);
                    apagrParque.Execute(p);

                    var parqueExists = parqueMap.ReadAll().Any(parque => parque.Nome.Equals(p.Nome));
                    Assert.AreEqual(false, parqueExists);

                    var alojamentoExists = alojamentoMapper.ReadAll().Any(a => a.Nome.Equals(c.Nome));
                    Assert.AreEqual(false, alojamentoExists);

                    var estadaExists = estadaMapper.ReadAll().Any(e => e.Id == estada.Id);
                    Assert.AreEqual(false, estadaExists);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);


                    foreach (var e in estadaAlojamentoMapper.ReadAll())
                    {
                        estadaAlojamentoMapper.Delete(e);
                    }

                    foreach (var e in estadaMapper.ReadAll())
                    {
                        estadaMapper.Delete(e);
                    }


                    foreach (var alojamento in alojamentoMapper.ReadAll())
                    {
                        alojamentoMapper.Delete(alojamento);
                    }


                    foreach (var parque in parqueMap.ReadAll())
                    {
                        parqueMap.Delete(parque);
                    }

                    throw;
                }
            }
        }
    }
}