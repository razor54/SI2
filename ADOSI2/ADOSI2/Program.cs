using System;
using System.Configuration;
using System.Collections.Generic;
using ADOSI2.concrete;
using ADOSI2.model;
using ADOSI2.operations;

namespace ADOSI2
{
    class Program
    {
        delegate void Command(Context context);
        static string connectionString = ConfigurationManager.ConnectionStrings["si2cs"].ConnectionString;
        static Dictionary<int, KeyValuePair<string, Command>> commands = new Dictionary<int, KeyValuePair<string, Command>>();

        static void Main()
        {
            Dictionary<int, KeyValuePair<string, Command>> commands = new Dictionary<int, KeyValuePair<string, Command>>();
            commands.Add(0, new KeyValuePair<string, Command>("Inserir hóspede", HóspedeOperations.InserirHóspede));
            commands.Add(1, new KeyValuePair<string, Command>("Atualizar hóspede", HóspedeOperations.AtualizarHóspede));
            commands.Add(2, new KeyValuePair<string, Command>("Remover hóspede", HóspedeOperations.RemoverHóspede));
            // not implemented
            commands.Add(3, new KeyValuePair<string, Command>("Inserir hóspede", AlojamentoNumParqueOperations.InserirHóspede));
            commands.Add(4, new KeyValuePair<string, Command>("Atualizar hóspede", AlojamentoNumParqueOperations.AtualizarHóspede));
            commands.Add(5, new KeyValuePair<string, Command>("Remover hóspede", AlojamentoNumParqueOperations.RemoverHóspede));

            commands.Add(6, new KeyValuePair<string, Command>("Inserir extra", ExtraOperations.InserirExtra));
            commands.Add(7, new KeyValuePair<string, Command>("Atualizar extra", ExtraOperations.AtualizarExtra));
            commands.Add(8, new KeyValuePair<string, Command>("Remover extra", ExtraOperations.RemoverExtra));

            commands.Add(9, new KeyValuePair<string, Command>("Inserir atividade", AtividadeOperations.InserirAtividade));
            commands.Add(10, new KeyValuePair<string, Command>("Atualizar atividade", AtividadeOperations.AtualizarAtividade));
            commands.Add(11, new KeyValuePair<string, Command>("Remover atividade", AtividadeOperations.RemoverAtividade));

            commands.Add(12, new KeyValuePair<string, Command>("Inscrever hóspede numa atividade", InscreverHóspedeNumaAtividadeOperations.Inscrever));

            using (Context context = new Context(connectionString))
            {
                while(true)
                {
                    Print(commands);
                    Console.WriteLine("\nO que deseja fazer?");
                    int read = Convert.ToInt32(Console.ReadLine());
                    Command cmd = commands[read].Value;
                    cmd.Invoke(context);
                }
            }
        }

        private static void Print(Dictionary<int, KeyValuePair<string, Command>> cmds)
        {
            for (int i = 0; i < cmds.Count; i++)
            {
                Console.WriteLine("{0} - {1}", i, cmds[i].Key);
            }
        }
    }
}

