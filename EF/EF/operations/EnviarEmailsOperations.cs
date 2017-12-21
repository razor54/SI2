using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;

namespace ADOSI2.operations
{
    public static class EnviarEmailsOperations
    {
        public static void EnviarEmails(Entities ctx)
        {

            Console.WriteLine("Insira o número de dias para o qual considera enviar emails");

            ObjectParameter output = new ObjectParameter("total", typeof(Int32));
            var enviar = ctx.enviarEmailsNumIntervaloTemporal(Convert.ToInt32(Console.ReadLine()),output);
            

            Console.WriteLine("O numero total de emails enviados é {0}", output.Value);
            Console.WriteLine("Pressione [Enter] para continuar");
            Console.ReadLine();
            ctx.SaveChanges();
        }
    }
}


