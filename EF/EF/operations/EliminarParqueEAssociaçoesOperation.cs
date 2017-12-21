using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;
using EF.logic;

namespace ADOSI2.operations
{
    public static class EliminarParqueEAssociaçoesOperation
    {
        public static void EliminarParqueEAssociaçoes(Entities ctx)
        {

            Console.WriteLine("Insira o nome do Parque");
            var nomeParque = Console.ReadLine();

            var p = ctx.Parques.Find(nomeParque);
            var apagrParque = new RemoverParqueEAssociaçoes(ctx);
            apagrParque.Execute(p);
            ctx.SaveChanges();
        }
    }
}
