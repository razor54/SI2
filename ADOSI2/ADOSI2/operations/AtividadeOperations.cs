using ADOSI2.concrete;
using ADOSI2.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.operations
{
    static class AtividadeOperations
    {
        public static void RemoverAtividade(Context context)
        {
            AtividadeMapper atividadeMapper = new AtividadeMapper(context);
            Console.Write("Insira o id: ");
            Atividade atividade = atividadeMapper.Read(Convert.ToInt32(Console.ReadLine()));
            atividadeMapper.Delete(atividade);
        }

        public static void AtualizarAtividade(Context context)
        {
            // id não controlamos logo pode haver repetições na atualização
            // read só deixa receber 1 parâmetro
            AtividadeMapper atividadeMapper = new AtividadeMapper(context);
            Console.Write("Insira o id: ");
            Atividade atividade = atividadeMapper.Read(Convert.ToInt32(Console.ReadLine()));

            Console.Write("Insira o nome da atividade: ");
            atividade.NomeAtividade = Console.ReadLine();
            Console.Write("Insira o nome do parque: ");
            atividade.Parque = null; // Console.ReadLine();
            Console.Write("Insira a data de início: ");
            atividade.DataAtividade = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Insira a descrição: ");
            atividade.Descrição = Console.ReadLine();
            Console.Write("Insira o preço: ");
            atividade.Preço = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Insira a lotação: ");
            atividade.Lotaçao = Convert.ToInt32(Console.ReadLine());

            atividadeMapper.Update(atividade);
        }

        public static void InserirAtividade(Context context)
        {
            // ainda sem parque!
            Atividade atividade = new Atividade();

            Console.Write("Insira o nome da atividade: ");
            atividade.NomeAtividade = Console.ReadLine();
            Console.Write("Insira o nome do parque: ");
            atividade.Parque = null; // Console.ReadLine();
            Console.Write("Insira a data de início: ");
            atividade.DataAtividade = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Insira a descrição: ");
            atividade.Descrição = Console.ReadLine();
            Console.Write("Insira o preço: ");
            atividade.Preço = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Insira a lotação: ");
            atividade.Lotaçao = Convert.ToInt32(Console.ReadLine());

            AtividadeMapper atividadeMapper = new AtividadeMapper(context);
            atividade = atividadeMapper.Create(atividade);
        }
    }
}
