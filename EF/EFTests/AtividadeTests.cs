using System;
using EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EFTests
{
    [TestClass]
    public class AtividadeTests
    {
        [TestMethod]
        public void TestInsert()
        {
            using (var ctx = new Entities())
            {
                /*
                 * CREATE PARQUE
                 */
                Console.WriteLine("sass");
                Parque p = new Parque
                {
                    nome = "brasil",
                    email = "brasil@brasil.com",
                    morada = "Rio de Janeiro, Rua Junqueiro 367",
                    estrelas = 5
                };
                ctx.SaveChanges();

                var atividade = new Atividade
                {
                    Parque = p,
                    data_atividade = new DateTime(2009, 10, 1),
                    descrição = "FORA",
                    preço = 90,
                    lotação = 12,
                    nome_atividade = "HAMBURGER"
                };
                ctx.SaveChanges();

                Console.WriteLine(atividade.número);
                Console.WriteLine(atividade.data_atividade);
                Console.WriteLine(atividade.preço);
                Assert.AreNotEqual(atividade.número, 0);

                var atividade2 = ctx.Atividades.Find(atividade.número);

                Assert.IsNotNull(atividade2);

                Assert.AreEqual(atividade2.Parque.nome, atividade.Parque.nome);
                Assert.AreEqual(atividade2.número, atividade.número);
                Assert.AreEqual(atividade2.data_atividade, atividade.data_atividade);
                Assert.AreEqual(atividade2.descrição, atividade.descrição);


                Assert.AreEqual(atividade2.lotação, atividade.lotação);
                Assert.AreEqual(atividade2.preço, atividade.preço);


                /*
                 * REMOVE THE PARQUE
                 */
                foreach (var s in ctx.Atividades)
                {
                    ctx.Atividades.Remove(s);
                }
                foreach (var parque in ctx.Parques)
                {
                    ctx.Parques.Remove(parque);
                }

                Console.WriteLine("REMOVED");
            }
        }
    }
}
