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
    public class AtividadeTests
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["si2cs"].ConnectionString;
        [TestMethod]
        public void TestInsert()
        {
          
            Console.WriteLine(" TESTAR ALOJAMENTO");

            using (Context ctx = new Context(_connectionString))
            {

                /*
                 * CREATE PARQUE
                 */
                Parque p = new Parque();
                p.Nome = "brasil";
                p.Email = "brasil@brasil.com";
                p.Morada = "Rio de Janeiro, Rua Junqueiro 367";
                p.Estrelas = 5;

                ParqueMapper parqueMap = new ParqueMapper(ctx);
                p = parqueMap.Create(p);


                var atividade = new Atividade
                {
                    Parque = p,
                    DataAtividade = new DateTime(2009, 10, 1),
                    Descrição = "FORA",
                    Preço = 90,
                    Lotaçao = 12,
                    NomeAtividade = "HAMBURGER"

                };
                var atividadeMapper = new AtividadeMapper(ctx);
                atividade = atividadeMapper.Create(atividade);

                Assert.IsNotNull(atividade);
                Assert.AreNotEqual(atividade.Número,0);

                var atividade2 = atividadeMapper.Read(atividade.Número);

                Assert.AreEqual(atividade2.Parque.Nome, atividade.Parque.Nome);
                Assert.AreEqual(atividade2.Número, atividade.Número);
                Assert.AreEqual(atividade2.DataAtividade, atividade.DataAtividade);
                Assert.AreEqual(atividade2.Descrição, atividade.Descrição);

               
                Assert.AreEqual(atividade2.Lotaçao, atividade.Lotaçao);
                Assert.AreEqual(atividade2.Preço, atividade.Preço);





                /*
                 * REMOVE THE PARQUE
                 */
                foreach (var s in atividadeMapper.ReadAll())
                {
                    atividadeMapper.Delete(s);
                }
                foreach (var parque in parqueMap.ReadAll())
                {
                    parqueMap.Delete(parque);
                }
                
                Console.WriteLine("REMOVED");



            }
        }

        [TestMethod]
        public void TestUpdate()
        {
           
            Console.WriteLine(" TESTAR UPDATE");

            using (Context ctx = new Context(_connectionString))
            {

                /*
                 * CREATE PARQUE
                 */
                Parque p = new Parque();
                p.Nome = "brasil";
                p.Email = "brasil@brasil.com";
                p.Morada = "Rio de Janeiro, Rua Junqueiro 367";
                p.Estrelas = 5;

                ParqueMapper parqueMap = new ParqueMapper(ctx);
                p = parqueMap.Create(p);


                var atividade = new Atividade
                {
                    Parque = p,
                    DataAtividade = new DateTime(2009, 10, 1),
                    Descrição = "FORA",
                    Preço = 90,
                    Lotaçao = 12,
                    NomeAtividade = "HAMBURGER"

                };
                var atividadeMapper = new AtividadeMapper(ctx);
                atividade = atividadeMapper.Create(atividade);

                Assert.IsNotNull(atividade);
                Assert.AreNotEqual(atividade.Número, 0);

                var atividade2 = atividadeMapper.Read(atividade.Número);

                Assert.AreEqual(atividade2.Parque.Nome, atividade.Parque.Nome);
                Assert.AreEqual(atividade2.Número, atividade.Número);
                Assert.AreEqual(atividade2.DataAtividade, atividade.DataAtividade);
                Assert.AreEqual(atividade2.Descrição, atividade.Descrição);


                Assert.AreEqual(atividade2.Lotaçao, atividade.Lotaçao);
                Assert.AreEqual(atividade2.Preço, atividade.Preço);

                var dataAtividade = new DateTime(2010, 10, 1);
                var descrição = "DENTRO";
                var preço = 1000;
                var lotaçao = 112;

                atividade.DataAtividade = dataAtividade;
                atividade.Descrição = descrição;
                atividade.Preço = preço;
                atividade.Lotaçao = lotaçao;

                atividade2=atividadeMapper.Update(atividade);

                Assert.AreEqual(atividade2.Parque.Nome, atividade.Parque.Nome);
                Assert.AreEqual(atividade2.Número, atividade.Número);

                Assert.AreEqual(atividade2.DataAtividade, atividade.DataAtividade);
                Assert.AreEqual(atividade2.Descrição, atividade.Descrição);
                Assert.AreEqual(atividade2.Lotaçao, atividade.Lotaçao);
                Assert.AreEqual(atividade2.Preço, atividade.Preço);

                Assert.AreEqual(atividade2.DataAtividade, dataAtividade);
                Assert.AreEqual(atividade2.Descrição, descrição);
                Assert.AreEqual(atividade2.Lotaçao, lotaçao);
                Assert.AreEqual(atividade2.Preço, preço);




                /*
                 * REMOVE THE PARQUE
                 */
                foreach (var s in atividadeMapper.ReadAll())
                {
                    atividadeMapper.Delete(s);
                }
                foreach (var parque in parqueMap.ReadAll())
                {
                    parqueMap.Delete(parque);
                }

                Console.WriteLine("REMOVED");



            }
        }
    }
}
