using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.concrete;
using ADOSI2.concrete.logic;

namespace ADOSI2.operations
{
    public static class EliminarParqueEAssociaçoesOperation
    {
        public static void EliminarParqueEAssociaçoes(Context ctx)
        {

            Console.WriteLine("Insira o nome do Parque");
            var nomeParque = Console.ReadLine();

            var pMapper = new ParqueMapper(ctx);
            

            var p = pMapper.Read(nomeParque);
            var apagrParque = new ApagarParqueEAssociaçoes(ctx);
            apagrParque.Execute(p);
        }
    }
}
