using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;

namespace ADOSI2.operations
{
    public static class PagamentoDeUmaEstadaOperations
    {
        public static void Pagamento(Entities ctx)
        {

            Console.WriteLine("Insira o Id da estada");
             ObjectParameter output = new ObjectParameter("total", typeof(Int32));
            var pagamento = ctx.pagamentoEstadaComFatura(Convert.ToInt32(Console.ReadLine()),output);
            
            
            Console.WriteLine("O valor total da fatura é {0}", output.Value);
            

        }
    }
}
