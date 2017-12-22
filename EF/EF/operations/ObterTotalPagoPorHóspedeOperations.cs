using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.operations
{
    public static class ObterTotalPagoPorHóspedeOperations
    {
        public static void ObterTotalPagoPorHóspede(Entities ctx)
        {
            Console.WriteLine("Insira o nome do Parque");
            var nomeParque = Console.ReadLine();
            Console.WriteLine("Insira a Data Inicial (ANO-MÊS-DIA)");
            var dataInicial = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Insira a Data Final (ANO-MÊS-DIA)");
            var dataFinal = Convert.ToDateTime(Console.ReadLine());

            var idEstada = from estada in ctx.Estadas
                where estada.data_fim <= dataFinal && estada.data_início >= dataInicial select estada.id;//Estadas para a data inserida;

            var estadaAlojamentos = from alojamento in ctx.EstadaAlojamentoes
                where idEstada.Contains(alojamento.id_estada) select alojamento;

            var countALojamentos = (from alojameno in ctx.Alojamentoes
                where alojameno.nome_parque == nomeParque
                select alojameno).Count();

            
            var extraEstadas = from extraEstada in ctx.ExtraEstadas
                where estadaAlojamentos.Select(e => e.id_estada).Contains(extraEstada.id_estada)
                select extraEstada;

            var estrasAlojamento = from extrasAlojamento in extraEstadas
                where extrasAlojamento.Extra.tipo.Equals("Alojamento") select extrasAlojamento;

            var extrasPreços = (from estras in estrasAlojamento select estras.preço_dia);
            var somaDosPreçosDosExtrasAlojamento = extrasPreços.Any()?extrasPreços.Sum():0;

            var numeroHospedes = (from estadaHospede in ctx.Estadas
                where estadaAlojamentos.Select(e => e.id_estada).Contains(estadaHospede.id)
                select estadaHospede.Hóspede).Count();
            var extraHospedes = from extras in extraEstadas
                where extras.Extra.tipo.Equals("Hóspede")
                select extras.Extra.preço_dia;

            var preçoTotal = (extraHospedes.Any()?extraHospedes.Sum():0) + somaDosPreçosDosExtrasAlojamento/(numeroHospedes!=0? numeroHospedes:1);

            Console.WriteLine("O preço total pago por hóspede é {0}",preçoTotal);


        }
    }
}
