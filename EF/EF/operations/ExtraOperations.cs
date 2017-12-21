
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using EF;

namespace ADOSI2.operations
{
    public static class ExtraOperations
    {
        public static void RemoverExtra(Entities context)
        {
            
            Console.Write("Insira o id: ");
            Extra extra = context.Extras.Find(Convert.ToInt32(Console.ReadLine()));
            context.Extras.Remove(extra);
            context.SaveChanges();
        }

        public static void AtualizarExtra(Entities ctx)
        {
           
            Console.Write("Insira o id: ");
            Extra extra = ctx.Extras.Find(Convert.ToInt32(Console.ReadLine()));

            Console.Write("Insira o tipo: ");
            extra.tipo = Console.ReadLine();
            Console.Write("Insira o id: ");
            extra.id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Insira a descrição: ");
            extra.descrição = Console.ReadLine();
            Console.Write("Insira o preço por dia: ");
            extra.preço_dia = Convert.ToDecimal(Console.ReadLine());

            ctx.Extras.Add(extra);
            ctx.SaveChanges();
        }

        public static void InserirExtra(Entities ctx)
        {
            Extra extra = new Extra();

            Console.Write("Insira o tipo: ");
            extra.tipo = Console.ReadLine();
            Console.Write("Insira o id: ");
            extra.id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Insira a descrição: ");
            extra.descrição = Console.ReadLine();
            Console.Write("Insira o preço por dia: ");
            extra.preço_dia = Convert.ToDecimal(Console.ReadLine());

            ctx.Extras.Add(extra);
            ctx.SaveChanges();
        }

        public static void ReadExtras(Entities ctx)
        {
            Console.WriteLine("ID - Descriçao - Preço Dia - Tipo");
            foreach (var e in ctx.Extras)
            {
                Console.WriteLine("{0} - {1} - {2} - {3}",e.id,e.descrição,e.preço_dia,e.tipo);
            }
        }
    }
}
