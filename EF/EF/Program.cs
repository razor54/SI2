using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new Entities())
            {
                ctx.Hóspede.Add(new Hóspede()
                {
                    bi = 12,
                    email = "sasa@sasa.sa",
                    morada = "Rua sem nome",
                    nome = "Jaquim",
                    nif = 1234567

                });
                ctx.SaveChanges();

                foreach (var h in ctx.Hóspede)
                {
                    Console.WriteLine("Hospede -- {0},{1},{2},{3},{4}",h.nome,h.nif,h.bi,h.morada,h.email);
                    ctx.Hóspede.Remove(h);
                }
                Console.WriteLine("To delete press [Enter]");
                Console.ReadKey();
                ctx.SaveChanges();

                

            }
            

        }
    }
}
