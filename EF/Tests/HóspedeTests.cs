using System;
using EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class HóspedeTests
    {
        [TestMethod]
        public void InsertHóspedeTest()
        {
            using (var ctx = new Entities())
            {
                Hóspede hóspede = new Hóspede()
                {
                    bi = 1234567890,
                    nif = 0987654321,
                    nome = "Jaquim",
                    email = "jaquim@gmail.com",
                    morada = "Rua da Calçada"
                };

                ctx.SaveChanges();

                /*
                HóspedeMapper hóspedeMapper = new HóspedeMapper(ctx);
                hóspede = hóspedeMapper.Create(hóspede);

                Hóspede hóspede1 = hóspedeMapper.Read(hóspede.Nif);

                Assert.AreEqual(hóspede.Nif, hóspede1.Nif);
                Assert.AreEqual(hóspede.Nome, hóspede1.Nome);

                foreach (var hospede in hóspedeMapper.ReadAll())
                {
                    hóspedeMapper.Delete(hospede);
                }
                */
            }
        }
    }
}