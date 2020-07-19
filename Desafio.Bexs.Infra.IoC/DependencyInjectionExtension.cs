using Desafio.Bexs.Data.Repositories;
using Desafio.Bexs.Domain.Dtos;
using Desafio.Bexs.Domain.Interfaces;
using Desafio.Bexs.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;

namespace Desafio.Bexs.Infra.IoC
{
    public static class DependencyInjectionExtension
    {
        private static string _dataFile;

        public static IServiceCollection ConfiguraDataFile(this IServiceCollection services, string[] args)
        {
            _dataFile = GetDataFile(args);

            return services;
        }

        public static IServiceCollection ConfigureDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<IAeroportoRepository, AeroportoRepository>();
            services.AddSingleton<IRotaRepository>(x => new RotaRepository(_dataFile));
            services.AddSingleton<IBuscadorMelhorRota, BuscadorMelhorRotaDijkstra>();
            services.AddSingleton<IRotaService, RotaService>();

            return services;
        }

        private static string GetDataFile(string[] args)
        {
            if (args == null || args.Count() < 1 || string.IsNullOrEmpty(args[0]) || !File.Exists(args[0]))
            {
                throw new ArgumentException("Routes data file not found");
            }

            return args[0];
        }
    }
}
