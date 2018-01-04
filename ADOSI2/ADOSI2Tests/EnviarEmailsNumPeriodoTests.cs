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
    public class EnviarEmailsNumPeriodoTests
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["si2cs"].ConnectionString;

        [TestMethod]
        public void EnviarEmailsNumPeriodoTest()
        {
            using (Context ctx = new Context(_connectionString))
            {
                var estada = new Estada
                {
                    DataInicio = new DateTime(2000, 1, 2),
                    DataFim = new DateTime(2000, 1, 5),
                    Id = 123456,
                    NifHospede = 111
                };

                EstadaMapper estadaMapper = new EstadaMapper(ctx);
                estada = estadaMapper.Create(estada);

                Hóspede hóspede = new Hóspede
                {
                    Bi = 456,
                    Nif = 111,
                    Nome = "Jaquim",
                    Email = "jaquim@gmail.com",
                    Morada = "Rua da Calçada"
                };

                HóspedeMapper hóspedeMapper = new HóspedeMapper(ctx);
                hóspede = hóspedeMapper.Create(hóspede);

                EstadaHóspede estadaHóspede = new EstadaHóspede()
                {
                    Estada = estada,
                    Hóspede = hóspede
                };

                var estadaHospedeMapper = new EstadaHóspedeMapper(ctx);
                estadaHóspede = estadaHospedeMapper.Create(estadaHóspede);
                ////////////////////
                var estada2 = new Estada
                {
                    DataInicio = new DateTime(2000, 1, 10),
                    DataFim = new DateTime(2000, 1, 12),
                    Id = 576586,
                    NifHospede = 222
                };

                estada2 = estadaMapper.Create(estada2);

                Hóspede hóspede2 = new Hóspede
                {
                    Bi = 789,
                    Nif = 222,
                    Nome = "Pedro",
                    Email = "pedro@gmail.com",
                    Morada = "Rua de Juz"
                };

                hóspede2 = hóspedeMapper.Create(hóspede2);

                EstadaHóspede estadaHóspede2 = new EstadaHóspede()
                {
                    Estada = estada2,
                    Hóspede = hóspede2
                };

                estadaHóspede2 = estadaHospedeMapper.Create(estadaHóspede2);

                var enviar = new EnviarEmailsNumPeriodo(ctx);
                enviar.Execute(5, out int contador);

                Assert.AreEqual(1, contador);

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
            }
        }
    }
}