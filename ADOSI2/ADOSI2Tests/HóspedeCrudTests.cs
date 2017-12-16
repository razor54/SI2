using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADOSI2.concrete;
using ADOSI2.model;

namespace ADOSI2Tests
{
    [TestClass]
    public class HóspedeCrudTests
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["si2cs"].ConnectionString;

        [TestMethod]
        public void InsertHóspedeTest()
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

                Hóspede hóspede1 = hóspedeMapper.Read(hóspede.Nif);

                Assert.AreEqual(hóspede.Nif, hóspede1.Nif);
                Assert.AreEqual(hóspede.Nome, hóspede1.Nome);

            }
        }

        [TestMethod]
        public void UpdateHóspedeTest()
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

                hóspede.Nome = "Manel";
                hóspedeMapper.Update(hóspede);

                Hóspede hóspede1 = hóspedeMapper.Read(hóspede.Nif);

                Assert.AreEqual(hóspede.Nif, hóspede1.Nif);
                Assert.AreEqual(hóspede.Nome, hóspede1.Nome);
            }
        }

        // FAILING
        [TestMethod]
        public void DeleteHóspedeTest()
        {
            using (Context ctx = new Context(connectionString))
            {
                Hóspede hóspede = new Hóspede();
                hóspede.Bi = 123456789;
                hóspede.Nif = 987654321;
                hóspede.Nome = "Jaquim";
                hóspede.Email = "jaquim@gmail.com";
                hóspede.Morada = "Rua da Calçada";

                HóspedeMapper hóspedeMapper = new HóspedeMapper(ctx);
                hóspede = hóspedeMapper.Create(hóspede);

                foreach (var h in hóspedeMapper.ReadAll())
                {
                    hóspedeMapper.Delete(h);
                }
            }
        }
    }
}
