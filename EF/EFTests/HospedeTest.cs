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
                Assert.IsNotNull(hóspede1);
                Assert.AreEqual(hóspede.nif, hóspede1.nif);
                Assert.AreEqual(hóspede.nome, hóspede1.nome);

                foreach (var hospede in ctx.Hóspede)
                {
                    ctx.Hóspede.Remove(hospede);
                }

                ctx.SaveChanges();
            }
        }

        [TestMethod]
        public void UpdateHóspedeTest()
        {
            using (var ctx = new Entities())
            {
                try
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

                    hóspede.nome = "Manel";
                    ctx.SaveChanges();

                    Hóspede hóspede1 = ctx.Hóspede.Find(hóspede.nif);
                    Assert.IsNotNull(hóspede1);
                    Assert.AreEqual(hóspede.nif, hóspede1.nif);
                    Assert.AreEqual(hóspede.nome, hóspede1.nome);
                    Assert.AreEqual(hóspede1.nome, "Manel");


                    foreach (var hospede in ctx.Hóspede)
                    {
                        ctx.Hóspede.Remove(hospede);
                    }

                    ctx.SaveChanges();

                }
                catch (Exception e)
                {
                    foreach (var hospede in ctx.Hóspede)
                    {
                        ctx.Hóspede.Remove(hospede);
                    }

                    ctx.SaveChanges();
                    throw;
                }
            }
        }
    }
}