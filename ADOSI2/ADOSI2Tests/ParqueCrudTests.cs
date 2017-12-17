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
    public class ParqueCrudTests
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["si2cs"].ConnectionString;


        [TestMethod]
        public void InsertParqueTest()
        {
            using (Context ctx = new Context(connectionString))
            {
                Parque c = new Parque();
                c.Nome = "brasil";
                c.Email = "brasil@brasil.com";
                c.Morada = "Rio de Janeiro, Rua Junqueiro 367";
                c.Estrelas = 5;

                ParqueMapper map = new ParqueMapper(ctx);
                c = map.Create(c);
                Parque c1 = map.Read(c.Nome);


                Assert.AreEqual(c1.Nome,c.Nome);
                Assert.AreEqual(c1.Estrelas,c.Estrelas);
                Assert.AreEqual(c1.Email,c.Email);
                Assert.AreEqual(c1.Morada,c.Morada);

                foreach (var hospede in map.ReadAll())
                {
                    map.Delete(hospede);
                }
            }
        }
    }
}
