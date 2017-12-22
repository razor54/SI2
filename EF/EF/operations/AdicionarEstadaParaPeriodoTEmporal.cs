using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.operations
{
    public static class AdicionarEstadaParaPeriodoTEmporal
    {
        public static void AdicionarEstadaParaPeriodoTemporal(Entities ctx)
        {
            int id;
            DateTime datainicio ;
            DateTime dataFim;
            int nifHospede;
            int bi;
            string nomeHospede;
            string morada;
            string email;

            decimal preçoBase;
            string descriçãoAlojamento;
            string localizaçao;
            string nomeAlojamento;
            int maxPessoas;

            string nomeParque;
            string tipologia;
            int idExtraAlojamento;
            string descriçaoExtraAlojamento;
            decimal preçoExtraAlojamento;
            string tipoExtra;
            int idFatura;

            int idExtraPessoal;
            string descriçãoExtraPessoal;
            decimal preçoExtraPessoal;

            Console.WriteLine("ID da Estada:");
            id = Convert.ToInt32(Console.ReadLine());
            Console.Write("data de inicio :");
            datainicio = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Data de fim :");
            dataFim = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Nif Hospede");
            nifHospede = Convert.ToInt32(Console.ReadLine());
            Console.Write("Insira o bi: ");
            bi = Convert.ToInt32(Console.ReadLine());
           
            Console.Write("Insira o nome: ");
            nomeHospede = Console.ReadLine();
            Console.Write("Insira o email: ");
            email = Console.ReadLine();
            Console.Write("Insira a morada: ");
            morada = Console.ReadLine();

            Console.WriteLine("Insire o preço base do alojamento ");
            preçoBase = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("insere a descriçao do alojamento");
            descriçãoAlojamento = Console.ReadLine();

            Console.WriteLine("insere a localizaçao do alojamento");
            localizaçao = Console.ReadLine();

            Console.WriteLine("insere o nome do alojamento");
            nomeAlojamento = Console.ReadLine();

            Console.WriteLine("Insire o nr maximo de pessoas do alojamento ");
            maxPessoas = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("insere o nome do parque");
            nomeParque = Console.ReadLine();


            Console.WriteLine("insere a tipologia ");
            tipologia = Console.ReadLine();

            Console.WriteLine("insere a id Extra alojamento ");
            idExtraAlojamento = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("insere descriçao do extra do alojamento ");
            descriçaoExtraAlojamento = Console.ReadLine();

            Console.WriteLine("Insire o preço do extra do alojamento ");
            preçoExtraAlojamento = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("insere tipo do extra do alojamento ");
            tipoExtra = Console.ReadLine();

            Console.WriteLine("Insire o id fatura ");
            idFatura = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Insire o id extra pessoal ");
            idExtraPessoal = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("insere descriçao extra pessoal ");
            descriçãoExtraPessoal = Console.ReadLine();

            Console.WriteLine("Insire o preço extra pessoal ");
            preçoExtraPessoal = Convert.ToDecimal(Console.ReadLine());

            ctx.criarEstadaParaUmPeríodoDeTempo(id, datainicio, dataFim, nifHospede, bi, nomeHospede
                , morada, email, preçoBase, descriçãoAlojamento, localizaçao,
                nomeAlojamento,
                maxPessoas, nomeParque, tipologia, idExtraAlojamento, descriçaoExtraAlojamento,
                preçoExtraAlojamento,
                tipoExtra, idFatura, idExtraPessoal, descriçãoExtraPessoal, preçoExtraPessoal);
            ctx.SaveChanges();
        }
    }
}
