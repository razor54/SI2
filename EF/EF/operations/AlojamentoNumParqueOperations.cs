
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using EF;

namespace ADOSI2.operations
{
    public static class AlojamentoNumParqueOperations
    {
        public static void RemoverAlojamento(Entities context)
        {
            
            

            Console.WriteLine("Insira o nome do alojamento que pretende remover :");

            var alojamento = context.Alojamentoes.Find(Console.ReadLine());

            while (alojamento == null)
            {
                Console.WriteLine("Por favor tente novamente, o alojamento indicado não Existe. Ou pressione [Enter] para sair");
                var input = Console.ReadLine();
                if (!input.Any()) return;
                alojamento = context.Alojamentoes.Find(input);
            }

            context.Alojamentoes.Remove(alojamento);
            context.SaveChanges();
        }

        public static void AtualizarAlojamento(Entities context)
        {
       
            Console.WriteLine("Insira o nome do alojamento que pretende alterar :");

           var alojamento = context.Alojamentoes.Find( Console.ReadLine());

            while (alojamento == null)
            {
                Console.WriteLine("Por favor tente novamente, o alojamento indicado não Existe. Ou pressione [Enter] para sair");
                var input = Console.ReadLine();
                if(!input.Any())return;
                 alojamento = context.Alojamentoes.Find(input);
            }

            Console.WriteLine("Insira o nome do Parque :");
            var parqueNome = Console.ReadLine();
            alojamento.Parque = context.Parques.Find(parqueNome);
            if (alojamento.Parque == null)
                throw new KeyNotFoundException("O parque não existe");

            Console.WriteLine("Insira a descrição do alojamento :");
            alojamento.descrição = Console.ReadLine();

            Console.WriteLine("Insira a localização do alojamento :");
            alojamento.localização = Console.ReadLine();

            Console.WriteLine("insira o número máximo de pessoas");
            alojamento.max_pessoas = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("insira o preço base");
            alojamento.preço_base = Convert.ToInt32(Console.ReadLine());


            context.SaveChanges();

        }

        public static void InserirAlojamentoEmParque(Entities context)
        {
 
            Alojamento alojamento = new Alojamento();

            Console.WriteLine("Insira o nome :");

            alojamento.nome = Console.ReadLine();

            Console.WriteLine("Insira o nome do Parque :");
            var parqueNome = Console.ReadLine();
            alojamento.nome_parque = parqueNome;
            

            Console.WriteLine("Insira a descrição do alojamento :");
            alojamento.descrição = Console.ReadLine();

            Console.WriteLine("Insira a localização do alojamento :");
            alojamento.localização = Console.ReadLine();

            Console.WriteLine("insira o número máximo de pessoas");
            alojamento.max_pessoas= Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("insira o preço base");
            alojamento.preço_base = Convert.ToInt32(Console.ReadLine());


            context.Alojamentoes.Add(alojamento);

            context.SaveChanges();

        }

        public static void ListarAlojamentos(Entities ctx)
        {
           

            Console.WriteLine("Lista de Alojamentos\n");
            Console.WriteLine("Nome - Descrição - Localização - Máximo número de pessoas - Nome do Parque - Preço base");

            foreach (var alojamento in ctx.Alojamentoes)
            {
                Console.WriteLine("{0} - {1} - {2} - {3} - {4} - {5}", alojamento.nome,alojamento.descrição,alojamento.localização,alojamento.max_pessoas,alojamento.Parque.nome,alojamento.preço_base);
            }
        }
    }
}
