using ADOSI2.concrete;
using ADOSI2.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.operations
{
    static class InscreverHóspedeNumaAtividadeOperations
    {
        public static void Inscrever(Context context)
        {
            Console.Write("Insira o nif: ");
            var nif = Convert.ToInt32(Console.ReadLine());
            Console.Write("Insira o nome da atividade: ");
            var nomeAtividade = Console.ReadLine();
            Console.Write("Insira o nome do parque: ");
            var nomeParque = Console.ReadLine();

            var inscr = new InscreverHóspedeEmAtividade(context);
            inscr.Execute(nif, nomeAtividade, nomeParque);
        }
    }
}
