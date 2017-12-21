
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using EF;

namespace ADOSI2.operations
{
    public static class HóspedeOperations
    {
        public static void RemoverHóspede(Entities ctx)
        {
            
            Console.Write("Insira o nif: ");
            Hóspede hóspede = ctx.Hóspede.Find(Convert.ToInt32(Console.ReadLine()));
            ctx.Hóspede.Remove(hóspede);
            ctx.SaveChanges();
        }

        public static void AtualizarHóspede(Entities ctx)
        {
            
            Console.Write("Insira o nif: ");
            Hóspede hóspede = ctx.Hóspede.Find(Convert.ToInt32(Console.ReadLine()));

            Console.Write("Insira o bi: ");
            hóspede.bi = Convert.ToInt32(Console.ReadLine());
            Console.Write("Insira o nif: ");
            hóspede.nif = Convert.ToInt32(Console.ReadLine());
            Console.Write("Insira o nome: ");
            hóspede.nome = Console.ReadLine();
            Console.Write("Insira o email: ");
            hóspede.email = Console.ReadLine();
            Console.Write("Insira a morada: ");
            hóspede.morada = Console.ReadLine();

            ctx.SaveChanges();
        }

        public static void InserirHóspede(Entities ctx)
        {
            Hóspede hóspede = new Hóspede();

            Console.Write("Insira o bi: ");
            hóspede.bi = Convert.ToInt32(Console.ReadLine());
            Console.Write("Insira o nif: ");
            hóspede.nif = Convert.ToInt32(Console.ReadLine());
            Console.Write("Insira o nome: ");
            hóspede.nome = Console.ReadLine();
            Console.Write("Insira o email: ");
            hóspede.email = Console.ReadLine();
            Console.Write("Insira a morada: ");
            hóspede.morada = Console.ReadLine();

            ctx.Hóspede.Add(hóspede);
            ctx.SaveChanges();
        }

        public static void PrintHóspede(Entities ctx)
        {
           


            Console.WriteLine("Hóspedes\n");
            Console.WriteLine("BI - Email - NIF - NOME - NIF");
            foreach (var h in ctx.Hóspede)
            {
                Console.WriteLine("{0} - {1} - {2} - {3} - {4}", h.bi, h.email, h.morada, h.nome, h.nif);
            }
        }
    }
}