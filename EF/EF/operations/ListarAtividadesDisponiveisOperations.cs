using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;

namespace ADOSI2.operations
{
    public static class ListarAtividadesDisponiveisOperations
    {
        public static void ListarAtividadesDisponiveis(Entities ctx)
        {
            Console.WriteLine("Insira a data de Inicio (ano-mes-dia)");

            var line = Console.ReadLine();

            var strings = line.Split('-').Select(e => Convert.ToInt32(e)).ToArray();

            var dataInicio = new DateTime(strings[0], strings[1], strings[2]);


            Console.WriteLine("Insira a data final (ano-mes-dia)");

            var line2 = Console.ReadLine();

            var strings2 = line2.Split('-').Select(e => Convert.ToInt32(e)).ToArray();

            var dataFim = new DateTime(strings2[0], strings2[1], strings2[2]);

            var listagem = ctx.listarAtividadesComlugares(dataInicio, dataFim);
            ctx.SaveChanges();

            Console.WriteLine("Descriçao - Data - Nome Atividade - Lotaçao - Numero -Preço");
            foreach (var l in listagem)
            {
                Console.WriteLine("{0} - {1} - {2} - {3} - {4} - {5}",l.descrição,l.data_atividade,l.nome_atividade,l.lotação,l.número,l.preço);
            }

            
        }
    }
}
