using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.concrete;
using ADOSI2.concrete.logic;

namespace ADOSI2.operations
{
    public static class EnviarEmailsOperations
    {
        public static void EnviarEmails(Context ctx)
        {

            Console.WriteLine("Insira o número de dias para o qual considera enviar emails");

            var enviar = new EnviarEmailsNumPeriodo(ctx);
            enviar.Execute(Convert.ToInt32(Console.ReadLine()), out var contador);

            Console.WriteLine("O numero total de emails enviados é {0}", contador);
            Console.WriteLine("Pressione [Enter] para continuar");
            Console.ReadLine();
        }
    }
}
