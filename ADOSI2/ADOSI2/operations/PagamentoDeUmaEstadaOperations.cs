using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.concrete;

namespace ADOSI2.operations
{
    public static class PagamentoDeUmaEstadaOperations
    {
        public static void Pagamento(Context ctx)
        {

            var pagamento = new PagamentoEstadaComFatura(ctx);
            Console.WriteLine("Insira o Id da estada");
            pagamento.Execute(Convert.ToInt32( Console.ReadLine()), out var total);
            Console.WriteLine("O valor total da fatura é {0}",total);
            Console.WriteLine("Pressione [Enter] para continuar");
            Console.ReadLine();

        }
    }
}
