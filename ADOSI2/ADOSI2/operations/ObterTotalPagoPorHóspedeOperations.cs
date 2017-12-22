using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.concrete;

namespace ADOSI2.operations
{
    public static class ObterTotalPagoPorHóspedeOperations
    {
        public static void ObterTotalPagoPorHóspede(Context ctx)
        {
            Console.WriteLine("Insira o nome do Parque");
            var nomeParque = Console.ReadLine();
            Console.WriteLine("Insira a Data Inicial (ANO-MÊS-DIA)");
            var dataInicial = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Insira a Data Final (ANO-MÊS-DIA)");
            var dataFinal = Convert.ToDateTime(Console.ReadLine());

            var estadasMapper = new EstadaMapper(ctx);
            var estadaAlojamentoMapper = new EstadaAlojamentoMapper(ctx);
            var alojamentosMapper = new AlojamentoMapper(ctx);
            var extraEstadasMapper = new ExtraEstadaMapper(ctx);
            var estadaHospedeMapper = new EstadaHóspedeMapper(ctx);

            var idEstada = from estada in estadasMapper.ReadAll()
                           where estada.DataFim <= dataFinal && estada.DataInicio >= dataInicial
                           select estada.Id;//Estadas para a data inserida;

            var estadaAlojamentos = from alojamento in estadaAlojamentoMapper.ReadAll()
                                    where idEstada.Contains(alojamento.Estada.Id)
                                    select alojamento;

            var countALojamentos = (from alojameno in alojamentosMapper.ReadAll()
                                    where alojameno.Parque.Nome == nomeParque
                                    select alojameno).Count();


            var extraEstadas = from extraEstada in extraEstadasMapper.ReadAll()
                               where estadaAlojamentos.Select(e => e.Estada.Id).Contains(extraEstada.Estada.Id)
                               select extraEstada;

            var estrasAlojamento = from extrasAlojamento in extraEstadas
                                   where extrasAlojamento.Extra.Tipo.Equals("Alojamento")
                                   select extrasAlojamento;

            var extrasPreços = (from estras in estrasAlojamento select estras.PreçoDia);
            var somaDosPreçosDosExtrasAlojamento = extrasPreços.Any() ? extrasPreços.Sum() : 0;

            var numeroHospedes = (from estadaHospede in estadaHospedeMapper.ReadAll()
                                  where estadaAlojamentos.Select(e => e.Estada.Id).Contains(estadaHospede.Estada.Id)
                                  select estadaHospede.Hóspede).Count();
            var extraHospedes = from extras in extraEstadas
                                where extras.Extra.Tipo.Equals("Hóspede")
                                select extras.Extra.PreçoDia;

            var preçoTotal = (extraHospedes.Any() ? extraHospedes.Sum() : 0) + somaDosPreçosDosExtrasAlojamento / (numeroHospedes != 0 ? numeroHospedes : 1);

            Console.WriteLine("O preço total pago por hóspede é {0}", preçoTotal);


        }
    }
}
