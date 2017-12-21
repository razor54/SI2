
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;

namespace ADOSI2.operations
{
    public static class AtividadeOperations
    {
        public static void RemoverAtividade(Entities context)
        {
            
            Console.Write("Insira o id: ");
            Atividade atividade = context.Atividades.Find(Convert.ToInt32(Console.ReadLine()));
            context.Atividades.Remove(atividade);
            context.SaveChanges();
        }

        public static void AtualizarAtividade(Entities context)
        {
            // id não controlamos logo pode haver repetições na atualização
            // read só deixa receber 1 parâmetro
           
            Console.Write("Insira o id: ");
            Atividade atividade = context.Atividades.Find(Convert.ToInt32(Console.ReadLine()));

            Console.Write("Insira o nome da atividade: ");
            atividade.nome_atividade = Console.ReadLine();
            Console.Write("Insira o nome do parque: ");
            atividade.nome_parque= Console.ReadLine();
            Console.Write("Insira a data de início: ");
            atividade.data_atividade = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Insira a descrição: ");
            atividade.descrição = Console.ReadLine();
            Console.Write("Insira o preço: ");
            atividade.preço = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Insira a lotação: ");
            atividade.lotação = Convert.ToInt32(Console.ReadLine());

            context.SaveChanges();
        }

        public static void InserirAtividade(Entities context)
        {
            // ainda sem parque!
            Atividade atividade = new Atividade();

            Console.Write("Insira o nome da atividade: ");
            atividade.nome_atividade = Console.ReadLine();
            Console.Write("Insira o nome do parque: ");
            atividade.nome_parque= Console.ReadLine();
            Console.Write("Insira a data de início: ");
            atividade.data_atividade = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Insira a descrição: ");
            atividade.descrição = Console.ReadLine();
            Console.Write("Insira o preço: ");
            atividade.preço = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Insira a lotação: ");
            atividade.lotação = Convert.ToInt32(Console.ReadLine());

            context.Atividades.Add(atividade);
            context.SaveChanges();
        }

        public static void ReadAtividades(Entities ctx)
        {
            Console.WriteLine("Nome atividade - Descrição - Nome Parque - Lotaçao - Preço - número");
            foreach (var atividade in ctx.Atividades)
            {
                Console.WriteLine("{0} - {1} - {2} - {3} - {4} - {5}",atividade.nome_atividade,atividade.descrição,atividade.nome_parque,atividade.lotação,atividade.preço,atividade.número);
            }
        }
    }
}
