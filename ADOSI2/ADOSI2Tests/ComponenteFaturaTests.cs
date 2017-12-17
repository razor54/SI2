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
    public class ComponenteFaturaTests
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["si2cs"].ConnectionString;

        [TestMethod]
        public void InsertTest()
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


                ComponenteFatura componente = new ComponenteFatura();
                componente.Preço = 25;
                componente.Fatura = fatura;
                componente.Tipo = "Alojamento";
                componente.Descrição = "muito bom";
                var componenteMapper = new ComponenteFaturaMapper(ctx);
                componente = componenteMapper.Create(componente);

                ComponenteFatura componente2 = componenteMapper.Read(componente.Id);
                Assert.IsNotNull(componente2);


                foreach (var e in componenteMapper.ReadAll())
                {
                    componenteMapper.Delete(e);
                }

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
        public void UpdateTest()
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


                ComponenteFatura componente = new ComponenteFatura();
                componente.Preço = 25;
                componente.Fatura = fatura;
                componente.Tipo = "Alojamento";
                componente.Descrição = "muito bom";
                var componenteMapper = new ComponenteFaturaMapper(ctx);
                componente = componenteMapper.Create(componente);

                ComponenteFatura componente2 = componenteMapper.Read(componente.Id);
                Assert.IsNotNull(componente2);


                var componenteDescrição = "nada";
                var extraAlojamento = "Extra Alojamento";
                var componentePreço = 52;

                componente.Descrição = componenteDescrição;
                componente.Tipo = extraAlojamento;
                componente.Preço = componentePreço;

                componente=componenteMapper.Update(componente);

                Assert.AreEqual(componente.Tipo,extraAlojamento);
                Assert.AreEqual(componente.Descrição,componenteDescrição);
                Assert.AreEqual(componente.Preço,componentePreço);


                foreach (var e in componenteMapper.ReadAll())
                {
                    componenteMapper.Delete(e);
                }

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
