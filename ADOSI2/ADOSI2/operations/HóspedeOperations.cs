using ADOSI2.concrete;
using ADOSI2.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.operations
{
    static class HóspedeOperations
    {
        public static void RemoverHóspede(Context context)
        {
            HóspedeMapper hóspedeMapper = new HóspedeMapper(context);
            Console.Write("Insira o nif: ");
            Hóspede hóspede = hóspedeMapper.Read(Convert.ToInt32(Console.ReadLine()));
            hóspedeMapper.Delete(hóspede);
        }

        public static void AtualizarHóspede(Context context)
        {
            HóspedeMapper hóspedeMapper = new HóspedeMapper(context);
            Console.Write("Insira o nif: ");
            Hóspede hóspede = hóspedeMapper.Read(Convert.ToInt32(Console.ReadLine()));

            Console.Write("Insira o bi: ");
            hóspede.Bi = Convert.ToInt32(Console.ReadLine());
            Console.Write("Insira o nif: ");
            hóspede.Nif = Convert.ToInt32(Console.ReadLine());
            Console.Write("Insira o nome: ");
            hóspede.Nome = Console.ReadLine();
            Console.Write("Insira o email: ");
            hóspede.Email = Console.ReadLine();
            Console.Write("Insira a morada: ");
            hóspede.Morada = Console.ReadLine();

            hóspedeMapper.Update(hóspede);
        }

        public static void InserirHóspede(Context context)
        {
            Hóspede hóspede = new Hóspede();

            Console.Write("Insira o bi: ");
            hóspede.Bi = Convert.ToInt32(Console.ReadLine());
            Console.Write("Insira o nif: ");
            hóspede.Nif = Convert.ToInt32(Console.ReadLine());
            Console.Write("Insira o nome: ");
            hóspede.Nome = Console.ReadLine();
            Console.Write("Insira o email: ");
            hóspede.Email = Console.ReadLine();
            Console.Write("Insira a morada: ");
            hóspede.Morada = Console.ReadLine();

            HóspedeMapper hóspedeMapper = new HóspedeMapper(context);
            hóspede = hóspedeMapper.Create(hóspede);
        }
    }
}
