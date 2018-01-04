using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EFTests
{ 

    [TestClass]
    public class EnviarEmailsTests
    {
        [TestMethod]
        public void EnviarEmailsNumPeriodoTest()
        {
            using (var ctx = new Entities())
            {
                var estada = new Estada
                {
                    data_início = new DateTime(2000, 1, 2),
                    data_fim = new DateTime(2000, 1, 5),
                    id = 123456,
                    nif_hóspede = 111
                };

                ctx.Estadas.Add(estada);

                Hóspede hóspede = new Hóspede
                {
                    bi = 456,
                    nif = 111,
                    nome = "Jaquim",
                    email = "jaquim@gmail.com",
                    morada = "Rua da Calçada"
                };

                ctx.Hóspede.Add(hóspede);

                
                ////////////////////
                var estada2 = new Estada
                {
                    data_início = new DateTime(2000, 1, 10),
                    data_fim = new DateTime(2000, 1, 12),
                    id = 576586,
                    nif_hóspede = 222
                };

                ctx.Estadas.Add(estada2);

                Hóspede hóspede2 = new Hóspede
                {
                    bi = 789,
                    nif=222,
                    nome = "Pedro",
                    email = "pedro@gmail.com",
                    morada = "Rua de Juz"
                };

                ctx.Hóspede.Add(hóspede2);

                ctx.SaveChanges();

                ObjectParameter output = new ObjectParameter("contador", typeof(Int32));
                ctx.enviarEmailsNumIntervaloTemporal(5, output);
                ctx.SaveChanges();

                Assert.AreEqual(0, Convert.ToInt32(output.Value));

               

                foreach (var hospede in ctx.Hóspede)
                {
                    ctx.Hóspede.Remove(hospede);
                }

                foreach (var e in ctx.Estadas)
                {
                    ctx.Estadas.Remove(e);
                }
            }
        }
    }
}
