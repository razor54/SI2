using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.concrete;
using ADOSI2.concrete.logic;

namespace ADOSI2.operations
{
    public static class ListarAtividadesDisponiveisOperations
    {
        public static void ListarAtividadesDisponiveis(Context ctx)
        {
            Console.WriteLine("Insira a data de Inicio (ano-mes-dia)");

            var line = Console.ReadLine();

            var strings = line.Split('-').Select(e=> Convert.ToInt32(e)).ToArray();

            var dataInicio = new DateTime(strings[0],strings[1],strings[2]);


            Console.WriteLine("Insira a data final (ano-mes-dia)");

            var line2 = Console.ReadLine();

            var strings2 = line2.Split('-').Select(e => Convert.ToInt32(e)).ToArray();

            var dataFim = new DateTime(strings2[0], strings2[1], strings2[2]);

            var listagem = new ListarAtividadeComLugares(ctx);

            listagem.Execute(dataInicio,dataFim);
        }
    }
}
