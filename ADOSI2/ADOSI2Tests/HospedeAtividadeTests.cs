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
    public class HospedeAtividadeTests
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

                var hospedeAtividade = new HóspedeAtividade
                {
                    Hóspede = hóspede,
                    Nome_Atividade = "LOLITOS",
                    Nome_Parque = "Brasil"
                };

                var hospedeAtividadeMapper = new HóspedeAtividadeMapper(ctx);

                hospedeAtividade = hospedeAtividadeMapper.Create(hospedeAtividade);

                var hospedeatividade2 = hospedeAtividadeMapper.Read(hospedeAtividade.Nome_Atividade);

                Assert.AreEqual(hospedeAtividade.Hóspede.Nif, hospedeatividade2.Hóspede.Nif);
                Assert.AreEqual(hospedeAtividade.Hóspede.Bi, hospedeatividade2.Hóspede.Bi);

                Assert.AreEqual(hospedeAtividade.Nome_Atividade, hospedeatividade2.Nome_Atividade);
                Assert.AreEqual(hospedeAtividade.Nome_Parque, hospedeatividade2.Nome_Parque);


                foreach (var v in hospedeAtividadeMapper.ReadAll())
                {
                    hospedeAtividadeMapper.Delete(v);
                }
                foreach (var hospede in hóspedeMapper.ReadAll())
                {
                    hóspedeMapper.Delete(hospede);
                }
            }
        }
    }
}
