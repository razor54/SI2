
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;

namespace ADOSI2.operations
{
    public static class InscreverHóspedeNumaAtividadeOperations
    {
        public static void Inscrever(Entities ctx)
        {
            Console.Write("Insira o nif: ");
            var nif = Convert.ToInt32(Console.ReadLine());
            Console.Write("Insira o nome da atividade: ");
            var nomeAtividade = Console.ReadLine();
            Console.Write("Insira o nome do parque: ");
            var nomeParque = Console.ReadLine();

            var inscr = ctx.inscreverHóspedeNumaAtividade(nif, nomeAtividade, nomeParque);
            ctx.SaveChanges();

        }
    }
}
