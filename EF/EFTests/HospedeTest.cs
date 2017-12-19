using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EFTests
{
    [TestClass]
    public class HospedeTest
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

                ctx.Hóspede.Add(hóspede);

                ctx.SaveChanges();



                Hóspede hóspede1 = ctx.Hóspede.Find(hóspede.nif);

                Assert.AreEqual(hóspede.nif, hóspede1.nif);
                Assert.AreEqual(hóspede.nome, hóspede1.nome);

                foreach (var hospede in ctx.Hóspede)
                {
                    ctx.Hóspede.Remove(hospede);
                }
                ctx.SaveChanges();

            }
        }
        /*
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


                foreach (var hospede in hóspedeMapper.ReadAll())
                {
                    hóspedeMapper.Delete(hospede);
                }
            }
        }*/
    }
}
