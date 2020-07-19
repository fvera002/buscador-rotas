using Desafio.Bexs.Domain.Interfaces;
using Desafio.Bexs.Domain.Models;
using Desafio.Bexs.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Desafio.Bexs.Infra.IoC;

namespace Desafio.Bexs.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .ConfiguraDataFile(args)
                .ConfigureDomainServices();
            var serviceProvider = services.BuildServiceProvider();
            var rotaService = serviceProvider.GetService<IRotaService>();

            rotaService.CarregarTodasRotas();
            BuscarInput(rotaService);
        }

        private static void BuscarInput(IRotaService djstra)
        {
            System.Console.Write("please enter the route: ");
            var input = System.Console.ReadLine();

            while(input != "exit")
            {
                var ids = input.Split('-');
                var melhorCaminho = djstra.BuscarMelhorNota(ids[0], ids[1]);

                if (melhorCaminho != null)
                {
                    System.Console.WriteLine(melhorCaminho.Descricao);
                }

                System.Console.Write("please enter the route: ");
                input = System.Console.ReadLine();
            }
        }
    }

}
