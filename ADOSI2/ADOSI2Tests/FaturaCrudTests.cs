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
    public class FaturaCrudTests
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["si2cs"].ConnectionString;

        [TestMethod]
        public void InsertFaturaTest()
        {
            using (Context ctx = new Context(connectionString))
            {
                Hóspede hóspede = new Hóspede();
                hóspede.Bi = 1234567890;
                hóspede.Nif = 0987654321;
                hóspede.Nome = "Jaquim";
                hóspede.Email = "jaquim@gmail.com";
                hóspede.Morada = "Rua da Calçada";

                HóspedeMapper hóspedeMapper = new HóspedeMapper(ctx);
                hóspede = hóspedeMapper.Create(hóspede);

                Estada estada = new Estada();
                estada.DataInicio = new DateTime(2007, 3, 1);
                estada.DataFim = new DateTime(2017, 3, 1);
                estada.Id = 25;
                //TODO
                estada.NifHospede = 0;

                EstadaMapper estadaMapper = new EstadaMapper(ctx);
                estada = estadaMapper.Create(estada);


                Fatura fatura = new Fatura();
                fatura.Hóspede = hóspede;
                fatura.Estada = estada;
                fatura.Id = 1;
                fatura.ValorFinal = 0;


                var faturaMapper = new FaturaMapper(ctx);
                fatura = faturaMapper.Create(fatura);

                Fatura fatura1 = faturaMapper.Read(fatura.Id);

                Assert.AreEqual(fatura.Id, fatura1.Id);
                Assert.AreEqual(fatura.Hóspede.Nome, fatura1.Hóspede.Nome);
                Assert.AreEqual(fatura.Hóspede.Nif, fatura1.Hóspede.Nif);
                Assert.AreEqual(fatura.Hóspede.Bi, fatura1.Hóspede.Bi);
                Assert.AreEqual(fatura.Estada.Id, fatura1.Estada.Id);
                Assert.AreEqual(fatura.Estada.DataFim, fatura1.Estada.DataFim);
                Assert.AreEqual(fatura.Estada.DataInicio, fatura1.Estada.DataInicio);
               

                foreach (var e in faturaMapper.ReadAll())
                {
                    faturaMapper.Delete(e);
                }

                foreach (var e in hóspedeMapper.ReadAll())
                {
                    hóspedeMapper.Delete(e);
                }

                foreach (var e in estadaMapper.ReadAll())
                {
                    estadaMapper.Delete(e);
                }

            }
        }

        [TestMethod]
        public void UpdateFaturaTest()
        {
            using (Context ctx = new Context(connectionString))
            {
                Hóspede hóspede = new Hóspede();
                hóspede.Bi = 1234567890;
                hóspede.Nif = 0987654321;
                hóspede.Nome = "Jaquim";
                hóspede.Email = "jaquim@gmail.com";
                hóspede.Morada = "Rua da Calçada";

                HóspedeMapper hóspedeMapper = new HóspedeMapper(ctx);
                hóspede = hóspedeMapper.Create(hóspede);

                Estada estada = new Estada();
                estada.DataInicio = new DateTime(2007, 3, 1);
                estada.DataFim = new DateTime(2017, 3, 1);
                estada.Id = 25;
                //TODO
                estada.NifHospede = 0;

                EstadaMapper estadaMapper = new EstadaMapper(ctx);
                estada = estadaMapper.Create(estada);


                Fatura fatura = new Fatura();
                fatura.Hóspede = hóspede;
                fatura.Estada = estada;
                fatura.Id = 1;
                fatura.ValorFinal = 0;


                var faturaMapper = new FaturaMapper(ctx);
                fatura = faturaMapper.Create(fatura);

                var valorFinal = 500;
                fatura.ValorFinal = valorFinal;
                faturaMapper.Update(fatura);

                Fatura fatura1 = faturaMapper.Read(fatura.Id);

                Assert.AreEqual(fatura.Id, fatura1.Id);
                Assert.AreEqual(fatura.Hóspede.Nome, fatura1.Hóspede.Nome);
                Assert.AreEqual(fatura.Hóspede.Nif, fatura1.Hóspede.Nif);
                Assert.AreEqual(fatura.Hóspede.Bi, fatura1.Hóspede.Bi);
                Assert.AreEqual(fatura.Estada.Id, fatura1.Estada.Id);
                Assert.AreEqual(fatura.Estada.DataFim, fatura1.Estada.DataFim);
                Assert.AreEqual(fatura.Estada.DataInicio, fatura1.Estada.DataInicio);

                Assert.AreEqual(fatura.ValorFinal, fatura1.ValorFinal);
                Assert.AreEqual(fatura.ValorFinal, valorFinal);


                foreach (var e in faturaMapper.ReadAll())
                {
                    faturaMapper.Delete(e);
                }

                foreach (var e in hóspedeMapper.ReadAll())
                {
                    hóspedeMapper.Delete(e);
                }

                foreach (var e in estadaMapper.ReadAll())
                {
                    estadaMapper.Delete(e);
                }
            }
        }

      
    }
}
