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
    public class EstadaHóspedeCrudTest
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["si2cs"].ConnectionString;

        [TestMethod]
        public void InsertExtraTest()
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

                var ee = estadaHospedeMapper.Read(new KeyValuePair<int, int>(estadaHóspede.Hóspede.Nif, estadaHóspede.Estada.Id));

                Assert.IsNotNull(ee);

                Assert.AreEqual(ee.Hóspede.Nif, estadaHóspede.Hóspede.Nif);
                Assert.AreEqual(ee.Estada.Id, estadaHóspede.Estada.Id);




                foreach (var e in estadaHospedeMapper.ReadAll())
                {
                    estadaHospedeMapper.Delete(e);
                }

                foreach (var e in estadaMapper.ReadAll())
                {
                    estadaMapper.Delete(estada);
                }

                foreach (var hospede in hóspedeMapper.ReadAll())
                {
                    hóspedeMapper.Delete(hospede);
                }

            }
        }
    }
}
