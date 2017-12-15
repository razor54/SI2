﻿using System;
using System.Configuration;
using System.Collections.Generic;
using ADOSI2.concrete;
using ADOSI2.model;

namespace ADOSI2
{
    class Program
    {
        static void Main()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["si2cs"].ConnectionString;

            /** TESTAR PARQUE*/
            Console.WriteLine(" TESTAR PARQUE");

            using (Context ctx = new Context(connectionString))
            {
                Parque c = new Parque();
                c.Nome = "brasil";
                c.Email = "brasil@brasil.com";
                c.Morada = "Rio de Janeiro, Rua Junqueiro 367";
                c.Estrelas = 5;

                ParqueMapper countryMap = new ParqueMapper(ctx);
                c = countryMap.Create(c);
                Parque c1 = countryMap.Read(c.Nome);
                Console.WriteLine("Parque: {0}-{1}-{2},{3}", c1.Nome, c1.Morada, c1.Estrelas, c1.Email);


                c1.Nome = "Brasil";
                c1.Estrelas = 3;
                c1.Email = "sem email";
                c1.Morada = "mora";
                countryMap.Update(c1);
                c1 = countryMap.Read(c1.Nome);
                Console.WriteLine("Parque: {0}-{1}-{2},{3}", c1.Nome, c1.Morada, c1.Estrelas, c1.Email);

                Parque c2 = new Parque();
                c2.Nome = "Portugal";
                c2.Email = "portugal@portugal.com";
                c2.Morada = "Lisboa, Av Emidio Navarro, Marvila";
                c2.Estrelas = 4;
                countryMap.Create(c2);


                Console.WriteLine("FindAll");
                foreach (var country in ctx.Parques.FindAll())
                {
                    Console.WriteLine("Parque: {0}-{1}-{2},{3}", country.Nome, country.Morada, country.Estrelas,
                        country.Email);
                }
                Console.WriteLine("Find");
                foreach (var country in ctx.Parques.Find(ct => ct.Nome.Equals("Portugal")))
                {
                    Console.WriteLine("Parque: {0}-{1}-{2},{3}", country.Nome, country.Morada, country.Estrelas,
                        country.Email);
                }

                Console.WriteLine("ReadAll and Delete");
                foreach (var parque in countryMap.ReadAll())
                {
                    Console.WriteLine("Parque: {0}-{1}-{2},{3}", parque.Nome, parque.Morada, parque.Estrelas,
                        parque.Email);
                    countryMap.Delete(parque);
                }

                Console.WriteLine("READ");
                foreach (var country in countryMap.ReadAll())
                {
                    Console.WriteLine("Parque: {0}-{1}-{2},{3}", country.Nome, country.Morada, country.Estrelas,
                        country.Email);
                }
                Console.WriteLine("REMOVED");
               
            }



            /** TESTAR EESTADA*/
            Console.WriteLine(" TESTAR Estada");

            using (Context ctx = new Context(connectionString))
            {
                Estada c = new Estada();
                c.DataInicio=new DateTime(2007,3,1);
                c.DataFim=new DateTime(2017,3,1);
                c.Id = 25;
                //TODO
                c.NifHospede = 0;

                EstadaMapper estadaMapper = new EstadaMapper(ctx);
                c = estadaMapper.Create(c);
                Estada c1 = estadaMapper.Read(c.Id);

                Console.WriteLine("Estada: {0}-{1}-{2},{3}", c1.DataInicio, c1.DataFim, c1.NifHospede, c1.Id);


                c1.DataInicio = new DateTime(2217, 3, 1);
                c1.DataFim = new DateTime(2317, 3, 1);
                c1.NifHospede = 0;

                estadaMapper.Update(c1);
                c1 = estadaMapper.Read(c1.Id);
                Console.WriteLine("Estada: {0}-{1}-{2},{3}", c1.DataInicio, c1.DataFim, c1.NifHospede, c1.Id);

                Estada c2 = new Estada();
                c2.DataInicio = new DateTime(2017, 3, 1);
                c2.DataFim = new DateTime(2217, 3, 1);
                c2.Id = 121;
                c2.NifHospede = 0;
                estadaMapper.Create(c2);


                Console.WriteLine("FindAll");
                foreach (var estada in ctx.Estadas.FindAll())
                {
                    Console.WriteLine("Estada: {0}-{1}-{2},{3}", estada.DataInicio, estada.DataFim, estada.NifHospede, estada.Id);
                }
                Console.WriteLine("Find");
                foreach (var estada in ctx.Estadas.Find(ct => ct.Id.Equals(121)))
                {
                    Console.WriteLine("Estada: {0}-{1}-{2},{3}", estada.DataInicio, estada.DataFim, estada.NifHospede, estada.Id);
                }

                Console.WriteLine("ReadAll and Delete");
                foreach (var estada in estadaMapper.ReadAll())
                {
                    Console.WriteLine("Estada: {0}-{1}-{2},{3}", estada.DataInicio, estada.DataFim, estada.NifHospede, estada.Id);
                    estadaMapper.Delete(estada);
                }

                Console.WriteLine("READ");
                foreach (var estada in estadaMapper.ReadAll())
                {
                    Console.WriteLine("Estada: {0}-{1}-{2},{3}", estada.DataInicio, estada.DataFim, estada.NifHospede, estada.Id);
                }
                Console.WriteLine("REMOVED");

            }



            Console.ReadKey();
        }
    }
}

