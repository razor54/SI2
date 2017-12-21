using ADOSI2.concrete;
using ADOSI2.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.operations
{
    static class ExtraOperations
    {
        public static void RemoverExtra(Context context)
        {
            ExtraMapper extraMapper = new ExtraMapper(context);
            Console.Write("Insira o id: ");
            Extra extra = extraMapper.Read(Convert.ToInt32(Console.ReadLine()));
            extraMapper.Delete(extra);
        }

        public static void AtualizarExtra(Context context)
        {
            ExtraMapper extraMapper = new ExtraMapper(context);
            Console.Write("Insira o id: ");
            Extra extra = extraMapper.Read(Convert.ToInt32(Console.ReadLine()));

            Console.Write("Insira o tipo: ");
            extra.Tipo = Console.ReadLine();
            Console.Write("Insira o id: ");
            extra.Id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Insira a descrição: ");
            extra.Descriçao = Console.ReadLine();
            Console.Write("Insira o preço por dia: ");
            extra.PreçoDia = Convert.ToDecimal(Console.ReadLine());

            extraMapper.Update(extra);
        }

        public static void InserirExtra(Context context)
        {
            Extra extra = new Extra();

            Console.Write("Insira o tipo: ");
            extra.Tipo = Console.ReadLine();
            Console.Write("Insira o id: ");
            extra.Id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Insira a descrição: ");
            extra.Descriçao = Console.ReadLine();
            Console.Write("Insira o preço por dia: ");
            extra.PreçoDia = Convert.ToDecimal(Console.ReadLine());

            ExtraMapper extraMapper = new ExtraMapper(context);
            extra = extraMapper.Create(extra);
        }
    }
}
