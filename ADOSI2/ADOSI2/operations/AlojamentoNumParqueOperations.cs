using ADOSI2.concrete;
using ADOSI2.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.operations
{
    static class AlojamentoNumParqueOperations
    {
        public static void RemoverAlojamento(Context context)
        {
            var alojamentoMapper = new AlojamentoMapper(context);
            

            Console.WriteLine("Insira o nome do alojamento que pretende remover :");

            var alojamento = alojamentoMapper.Read(Console.ReadLine());

            while (alojamento == null)
            {
                Console.WriteLine("Por favor tente novamente, o alojamento indicado não Existe. Ou pressione [Enter] para sair");
                var input = Console.ReadLine();
                if (!input.Any()) return;
                alojamento = alojamentoMapper.Read(input);
            }

            alojamentoMapper.Delete(alojamento);
        }

        public static void AtualizarAlojamento(Context context)
        {
            var alojamentoMapper = new AlojamentoMapper(context);
            var parqueMapper = new ParqueMapper(context);

            Console.WriteLine("Insira o nome do alojamento que pretende alterar :");

           var alojamento = alojamentoMapper.Read( Console.ReadLine());

            while (alojamento == null)
            {
                Console.WriteLine("Por favor tente novamente, o alojamento indicado não Existe. Ou pressione [Enter] para sair");
                var input = Console.ReadLine();
                if(!input.Any())return;
                 alojamento = alojamentoMapper.Read(input);
            }

            Console.WriteLine("Insira o nome do Parque :");
            var parqueNome = Console.ReadLine();
            alojamento.Parque = parqueMapper.Read(parqueNome);
            if (alojamento.Parque == null)
                throw new KeyNotFoundException("O parque não existe");

            Console.WriteLine("Insira a descrição do alojamento :");
            alojamento.Descrição = Console.ReadLine();

            Console.WriteLine("Insira a localização do alojamento :");
            alojamento.Localizaçao = Console.ReadLine();

            Console.WriteLine("insira o número máximo de pessoas");
            alojamento.MaxPessoas = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("insira o preço base");
            alojamento.PreçoBase = Convert.ToInt32(Console.ReadLine());


            alojamentoMapper.Update(alojamento);
           
        }

        public static void InserirAlojamentoEmParque(Context context)
        {
            var alojamentoMapper = new AlojamentoMapper(context);
            var parqueMapper = new ParqueMapper(context);

            Alojamento alojamento = new Alojamento();

            Console.WriteLine("Insira o nome :");

            alojamento.Nome = Console.ReadLine();

            Console.WriteLine("Insira o nome do Parque :");
            var parqueNome = Console.ReadLine();
            alojamento.Parque = parqueMapper.Read(parqueNome);
            if(alojamento.Parque==null)
                throw new KeyNotFoundException("O parque não existe");

            Console.WriteLine("Insira a descrição do alojamento :");
            alojamento.Descrição = Console.ReadLine();

            Console.WriteLine("Insira a localização do alojamento :");
            alojamento.Localizaçao = Console.ReadLine();

            Console.WriteLine("insira o número máximo de pessoas");
            alojamento.MaxPessoas= Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("insira o preço base");
            alojamento.PreçoBase = Convert.ToInt32(Console.ReadLine());


            alojamentoMapper.Create(alojamento);

        }

        public static void ListarAlojamentos(Context ctx)
        {
            var alojamentoMapper = new AlojamentoMapper(ctx);

            Console.WriteLine("Lista de Alojamentos\n");
            Console.WriteLine("Nome - Descrição - Localização - Máximo número de pessoas - Nome do Parque - Preço base");

            foreach (var alojamento in alojamentoMapper.ReadAll())
            {
                Console.WriteLine("{0} - {1} - {2} - {3} - {4} - {5}", alojamento.Nome,alojamento.Descrição,alojamento.Localizaçao,alojamento.MaxPessoas,alojamento.Parque.Nome,alojamento.PreçoBase);
            }
        }
    }
}
