using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using ADOSI2.concrete;
using ADOSI2.model;
using ADOSI2.operations;

namespace ADOSI2
{
    class Program
    {
        delegate void Command(Context context);

        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["si2cs"].ConnectionString;


        static void Main()
        {
            Dictionary<int, KeyValuePair<string, Command>> commands =
                new Dictionary<int, KeyValuePair<string, Command>>
                {
                    {0, new KeyValuePair<string, Command>("Inserir hóspede", HóspedeOperations.InserirHóspede)},
                    {1, new KeyValuePair<string, Command>("Atualizar hóspede", HóspedeOperations.AtualizarHóspede)},
                    {2, new KeyValuePair<string, Command>("Remover hóspede", HóspedeOperations.RemoverHóspede)},
                    {3, new KeyValuePair<string, Command>("Listar Hóspedes", HóspedeOperations.PrintHóspede)},
                    {
                        4,
                        new KeyValuePair<string, Command>("Inserir alojamento num Parque existente",
                            AlojamentoNumParqueOperations.InserirAlojamentoEmParque)
                    },
                    {
                        5,
                        new KeyValuePair<string, Command>("Atualizar alojamento num Parque existente",
                            AlojamentoNumParqueOperations.AtualizarAlojamento)
                    },
                    {
                        6,
                        new KeyValuePair<string, Command>("Remover alojamento num Parque existente",
                            AlojamentoNumParqueOperations.RemoverAlojamento)
                    },
                    {
                        7,
                        new KeyValuePair<string, Command>("Listar Alojamentos",
                            AlojamentoNumParqueOperations.ListarAlojamentos)
                    },
                    {8, new KeyValuePair<string, Command>("Inserir extra", ExtraOperations.InserirExtra)},
                    {9, new KeyValuePair<string, Command>("Atualizar extra", ExtraOperations.AtualizarExtra)},
                    {10, new KeyValuePair<string, Command>("Remover extra", ExtraOperations.RemoverExtra)},
                    {11, new KeyValuePair<string, Command>("Inserir atividade", AtividadeOperations.InserirAtividade)},
                    {
                        12,
                        new KeyValuePair<string, Command>("Atualizar atividade", AtividadeOperations.AtualizarAtividade)
                    },
                    {13, new KeyValuePair<string, Command>("Remover atividade", AtividadeOperations.RemoverAtividade)},
                    {
                        14,
                        new KeyValuePair<string, Command>("Inscrever hóspede numa atividade",
                            InscreverHóspedeNumaAtividadeOperations.Inscrever)
                    },
                    {
                        15,
                        new KeyValuePair<string, Command>("Pagamento de uma estada",
                            PagamentoDeUmaEstadaOperations.Pagamento)
                    },
                    {
                        16, new KeyValuePair<string, Command>("Enviar emails a todos os hóspedes responsáveis " +
                                                              "por estadas que se irão iniciar dentro de \r\num dado período temporal",
                            EnviarEmailsOperations.EnviarEmails)
                    },
                    {
                        17, new KeyValuePair<string, Command>(
                            "Listar  todas  as  atividades  com  lugares  disponíveis  para  um  intervalo  de  datas \r\nespecificado",
                            ListarAtividadesDisponiveisOperations.ListarAtividadesDisponiveis)
                    },
                    {
                        18, new KeyValuePair<string, Command>("Apagar Parque e associações",
                            EliminarParqueEAssociaçoesOperation.EliminarParqueEAssociaçoes)
                    },
                    {19,new KeyValuePair<string, Command>("Criar uma estada para um dado período de tempo",AdicionarEstadaParaPeriodoTEmporal.AdicionarEstadaParaPeriodoTemporal) },
                    {20,new KeyValuePair<string, Command>("  obter o total pago por hóspede \r\nrelativo a estadas num dado parque num intervalo de datas especificado ",
                        ObterTotalPagoPorHóspedeOperations.ObterTotalPagoPorHóspede)}
                };
            // not implemented


            using (Context context = new Context(ConnectionString))
            {
                while (true)
                {
                    try
                    {
                        Print(commands);
                        Console.WriteLine("\nO que deseja fazer?");
                        var readLine = Console.ReadLine();
                        if (!readLine.Any()) break;
                        int read = Convert.ToInt32(readLine);
                        Command cmd = commands[read].Value;
                        Console.Clear();
                        cmd.Invoke(context);
                    }
                    catch (Exception)
                    {
                        Console.Clear();
                        Console.WriteLine("Houve um erro.Tente novamente.");
                    }

                    Console.WriteLine("\nPressione [Enter] para continuar");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        private static void Print(Dictionary<int, KeyValuePair<string, Command>> cmds)
        {
            for (int i = 0; i < cmds.Count; i++)
            {
                Console.WriteLine("{0} - {1}", i, cmds[i].Key);
            }

            Console.WriteLine("\nPressione apenas [Enter] para sair");
        }
    }
}